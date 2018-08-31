using BankingLedger.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingLedger.API.Models
{
    public class UserModel
    {
        public long UserID;
        public string Username;
        public string Password;
        public List<BankingAccount> Accounts = new List<BankingAccount>();
    }
}