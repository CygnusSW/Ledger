using BankingLedger.API.CustomIdentity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BankingLedger.API.CustomIdentity.Stores
{
    public class CustomRoleStore : IRoleStore<CustomRole, string>
    {
        public Task CreateAsync(CustomRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CustomRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<CustomRole> FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomRole> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomRole role)
        {
            throw new NotImplementedException();
        }
    }
}