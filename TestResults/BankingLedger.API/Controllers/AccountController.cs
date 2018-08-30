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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using BankingLedger.API.Models;
using BankingLedger.API.Providers;
using BankingLedger.API.Results;
using BankingLedger.API.Utilities;
using BankingLedger.API.Adapters;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;

namespace BankingLedger.API.Controllers
{
    public class AccountController : ApiController
    {
        private AuthenticationModule _authModule;

        public AccountController()
        {
            _authModule = new AuthenticationModule();

        }

        [HttpPost]
        [Route("api/v1/register")]
        public IHttpActionResult Register(string username, string password, string confirmPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (password != confirmPassword)
                return BadRequest("Passwords do not match.");

            var user = _authModule.AddUser(username, password);

            if (user == null)
                return BadRequest("That username is already taken.");

            return Ok();
        }


        [HttpPost]
        [Route("api/v1/login")]
        public HttpResponseMessage Login(string userName, string password)
        {

            FakeUserDBEntity user = _authModule.GetUserByUsername(userName);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid User", Configuration.Formatters.JsonFormatter);
            }
            else
            {
                AuthenticationModule authentication = new AuthenticationModule();
                var u = _authModule.AuthenticateUser(userName, password);

                var claims = new[] 
                {
                    new Claim(ClaimTypes.Name, u.Username),
                    new Claim(ClaimTypes.NameIdentifier, u.UserID.ToString())
                };

                var signingKey = ConfigurationManager.AppSettings["AuthSecretKey"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
                var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var token = new JwtSecurityToken(
                    issuer: "localhost.com",
                    audience: "localhost.com",
                    expires: DateTime.Now.AddMinutes(20),
                    claims: claims,
                    signingCredentials: signInCredentials
                    );
                var encryptedToken = new JwtSecurityTokenHandler().WriteToken(token);
                //string token = authentication.GenerateTokenForUser(user.Username, user.UserID);
                return Request.CreateResponse(HttpStatusCode.OK, encryptedToken, Configuration.Formatters.JsonFormatter);
            }

        }

        [Authorize]
        [HttpPost]
        [Route("api/v1/test")]
        public IHttpActionResult TestAuthentication()
        {
            return Ok("Testing");
        }







    }
}
