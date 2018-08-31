using BankingLedger.API.CustomIdentity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BankingLedger.API.CustomIdentity.Stores
{
    public class CustomUserStore : IUserStore<CustomUser, long>,
                                    IUserPasswordStore<CustomUser, long>
    {
        private static Dictionary<string, CustomUser> _users = new Dictionary<string, CustomUser>();

        public CustomUserStore()
        {
        }

        public CustomUserStore(Dictionary<string, CustomUser> users)
        {
            _users = users;
        }

        public Task CreateAsync(CustomUser user)
        {
            if (!_users.ContainsKey(user.UserName))
                _users.Add(user.UserName, user);

            return Task.FromResult(user);
        }

        public Task DeleteAsync(CustomUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<CustomUser> FindByIdAsync(long userId)
        {
            var user = _users.FirstOrDefault(u => u.Value.Id == userId).Value;
            
            return Task.FromResult(user);
        }

        public Task<CustomUser> FindByNameAsync(string userName)
        {
            var user = _users.FirstOrDefault(u => u.Value.UserName == userName).Value;

            return Task.FromResult(user);
        }

        public Task<string> GetPasswordHashAsync(CustomUser user)
        {
            return Task.FromResult(user.HashedPassword);
        }

        public Task<bool> HasPasswordAsync(CustomUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(CustomUser user, string passwordHash)
        {
            user.HashedPassword = passwordHash;

            return Task.CompletedTask;
        }

        public Task UpdateAsync(CustomUser user)
        {
            var us = _users.FirstOrDefault(u => u.Value.Id == user.Id);
            if (us.Value != null)
            {
                _users.Remove(user.UserName);
                _users.Add(user.UserName, user);
            }
            return Task.CompletedTask;
        }
    }
}