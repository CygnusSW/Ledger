using BankingLedger.Core;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BankingLedger.API.CustomIdentity.Models
{
    public class CustomUser : IUser<long>
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public List<BankingAccount> BankAccounts { get; set; } = new List<BankingAccount>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
                UserManager<CustomUser, long> manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(
                this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here 
            return userIdentity;
        }
    }
}