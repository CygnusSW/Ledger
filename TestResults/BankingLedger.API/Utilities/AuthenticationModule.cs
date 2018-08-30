using BankingLedger.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevOne.Security.Cryptography.BCrypt;
using BankingLedger.Core.Utilities;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;

namespace BankingLedger.API.Utilities
{
    public class AuthenticationModule
    {
        private static Dictionary<string, FakeUserDBEntity> _users = new Dictionary<string, FakeUserDBEntity>();
        private int _timeoutInMinutes = int.Parse(ConfigurationManager.AppSettings["TimeoutInMinutes"]);

        SecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["AuthSecretKey"]));

        public FakeUserDBEntity GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Key == username).Value;
        }

        public FakeUserDBEntity AddUser(string username, string password)
        {
            var salt = BCryptHelper.GenerateSalt();//Would provide random seed in production
            var userToAdd = new FakeUserDBEntity()
            {
                UserID = MockIDGenerator.Generate(),
                HashedPassword = BCryptHelper.HashPassword(password, salt),
                Username = username
            };

            _users.Add(username, userToAdd);
            return userToAdd;
        }

        public FakeUserDBEntity AuthenticateUser(string username, string password)
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

        // The Method is used to generate token for user
        public string GenerateTokenForUser(string userName, long userId)
        {

            var now = DateTime.UtcNow;
            var signingCredentials = new SigningCredentials(signingKey,
               SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            }, "Custom");

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = "http://www.localhost:64975",
                Expires = DateTime.UtcNow.AddMinutes(_timeoutInMinutes),
                Issuer = "self",
                IssuedAt = DateTime.UtcNow,
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials                
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);

            return signedAndEncodedToken;

        }

        /// Using the same key used for signing token, user payload is generated back
        public JwtSecurityToken GenerateUserClaimFromJWT(string authToken)
        {

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[]
                      {
                        "http://www.localhost:64975",
                      },

                ValidIssuers = new string[]
                  {
                      "self",
                  },
                IssuerSigningKey = signingKey
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken validatedToken;

            try
            {

                tokenHandler.ValidateToken(authToken, tokenValidationParameters, out validatedToken);
            }
            catch (Exception)
            {
                return null;

            }

            return validatedToken as JwtSecurityToken;

        }

        public TokenAuthenticationIdentity PopulateUserIdentity(JwtSecurityToken userPayloadToken)
        {
            string name = ((userPayloadToken)).Claims.FirstOrDefault(m => m.Type == "unique_name").Value;
            string userId = ((userPayloadToken)).Claims.FirstOrDefault(m => m.Type == "nameid").Value;
            return new TokenAuthenticationIdentity(name) { UserId = Convert.ToInt32(userId), UserName = name };

        }


    }
}