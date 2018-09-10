using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using BankingLedger.API.Models;
using BankingLedger.API.Providers;
using BankingLedger.API.Results;
using BankingLedger.API.CustomIdentity.Models;
using System.Web.Http.Cors;
using BankingLedger.Core;
using System.Configuration;
using BankingLedger.Core.Enums;
using System.Linq;
using BankingLedger.Core.DataModels;

namespace BankingLedger.API.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }


        [HttpPost]
        [Route("api/v1/logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        
        // POST api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/register")]
        public async Task<IHttpActionResult> Register(AuthRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var initialBankAccount = new BankingAccount(model.AccountName, int.Parse(ConfigurationManager.AppSettings["snapshotFrequency"]));
            var user = new CustomUser() {
                UserName = model.Username,
                BankAccounts = new List<BankingAccount>() {
                    initialBankAccount
                }
            };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok(result.Succeeded);
        }


        [HttpGet]
        [Authorize]
        [Route("api/v1/accounts")]
        public IHttpActionResult GetAccounts()
        {
            var userName = User.Identity.GetUserName();            
            CustomUser us = UserManager.FindByName(userName);

            return Ok(us.BankAccounts);
        }



        [HttpPost]
        [Authorize]
        [Route("api/v1/transaction")]
        public IHttpActionResult CreateTransaction(CreateTransactionRequest trxInfo)
        {
            var transactionType = (FinancialTransactionType)trxInfo.TransactionType;
            var userName = User.Identity.GetUserName();
            CustomUser us = UserManager.FindByName(userName);
            var accountReceivingTrx = us.BankAccounts.FirstOrDefault(acc => acc.AccountNumber == trxInfo.AccountNumber);
            if (accountReceivingTrx == null)
                return BadRequest("Invalid Account Number");
            var transactionBeingAdded = new FinancialTransaction(trxInfo.Amount, trxInfo.Description, DateTime.UtcNow, transactionType);
            
            accountReceivingTrx.AccountRecords.Add(transactionBeingAdded);
            UserManager.Update(us);
            return Ok(transactionBeingAdded);
        }

        

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && _userManager != null)
        //    {
        //        _userManager.Dispose();
        //        _userManager = null;
        //    }

        //    base.Dispose(disposing);
        //}

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }        

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
