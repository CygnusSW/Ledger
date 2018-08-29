using BankingLedger.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevOne.Security.Cryptography.BCrypt;
using BankingLedger.Core.Utilities;

namespace BankingLedger.API.Utilities
{
    public class AuthenticationModule
    {
        private static Dictionary<string, User> _users = new Dictionary<string, User>();

        public User AuthenticateUser(string username, string password)
        {
            var user = GetUserByUsername(username);
            
            if (user != null)
            {
                var pwMatch = BCryptHelper.CheckPassword(password, user.HashedPassword);

                if (!pwMatch)
                    user = null;
            }

            return user;            
        }

        public User GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Key == username).Value;
        }

        public User AddUser(string username, string password)
        {
            var salt = BCryptHelper.GenerateSalt();//Would provide random seed in production
            var userToAdd = new User()
            {
                UserID = MockIDGenerator.Generate(),
                HashedPassword = BCryptHelper.HashPassword(password, salt),
                Username = username
            };

            _users.Add(username, userToAdd);
            return userToAdd;
        }
    }
}