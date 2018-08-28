using BankingLedger.Core.DataModels;
using NUnit.Framework;
using System;

namespace BankingLedger.Core.Test
{
    [TestFixture]
    public class BankingAccountSpec
    {
        private string defaultAccountName = "Testing New Account";
        private decimal defaultTrxPrice = 6.33m;
        private BankingAccount acc;

        [SetUp]
        public void SetUp()
        {
            acc = new BankingAccount(defaultAccountName);
        }

        [TestCase]
        public void Bank_Accounts_Start_At_Zero_Balance()
        {
            var initialBalance = acc.GetBalance();
            Assert.AreEqual(0.00m, initialBalance);
        }

        [TestCase]
        public void Adding_Credit_Transaction_Updates_Account_Balance()
        {
            var initialBalance = acc.GetBalance();
            var trxToPost = new FinancialTransaction(defaultTrxPrice, "Testing Credit Trx", DateTime.UtcNow);
            acc.PostTransaction(trxToPost);
            var updatedBalance = acc.GetBalance();
            Assert.AreEqual(initialBalance + defaultTrxPrice, updatedBalance);        
        }


    }
}
