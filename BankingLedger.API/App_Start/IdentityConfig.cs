using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using BankingLedger.API.Models;
using BankingLedger.API.CustomIdentity.Models;
using BankingLedger.API.CustomIdentity.Stores;
using System.Collections.Generic;

namespace BankingLedger.API
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<CustomUser, long>
    {

        public ApplicationUserManager(IUserStore<CustomUser, long> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var userStore = new CustomUserStore();
            var manager = new ApplicationUserManager(userStore);
            // Configure validation logic for usernames

     
            return manager;
        }
    }
}
