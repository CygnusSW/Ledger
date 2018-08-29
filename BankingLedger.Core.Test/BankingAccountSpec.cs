using BankingLedger.Core.DataModels;
using BankingLedger.Core.Enums;
using NUnit.Framework;
using System;

namespace BankingLedger.Core.Test
{
    [TestFixture]
    public class BankingAccountSpec
    {
        private string defaultAccountName = "Testing New Account";
        private decimal defaultCreditAmount = 6.33m;
        private decimal defaultDebitAmount = 3.66m;
        private int defaultSnapshotFrequency = 3;
        private BankingAccount acc;

        [SetUp]
        public void SetUp()
        {
            acc = new BankingAccount(defaultAccountName, defaultSnapshotFrequency);
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
            var trxToPost = new FinancialTransaction(defaultCreditAmount, "Testing Credit Trx", DateTime.UtcNow, FinancialTransactionType.CREDIT);
            acc.PostTransaction(trxToPost);
            var updatedBalance = acc.GetBalance();
            Assert.AreEqual(initialBalance + defaultCreditAmount, updatedBalance);        
        }

        [TestCase]
        public void Adding_Debit_Transaction_Updates_Account_Balance()
        {
            var initialBalance = acc.GetBalance();
            var trxToPost = new FinancialTransaction(defaultDebitAmount, "Testing Debit Transaction", DateTime.UtcNow, FinancialTransactionType.DEBIT);
            acc.PostTransaction(trxToPost);
            var updatedBalance = acc.GetBalance();
            Assert.AreEqual(initialBalance - defaultDebitAmount, updatedBalance);
        }

        [TestCase]
        public void Balance_Snapshots_Are_Taken_Periodically()
        {
            var now = DateTime.UtcNow;
            var initialBalance = acc.GetBalance();
            var trxToPost1 = new FinancialTransaction(defaultCreditAmount, "Trx 1", now, FinancialTransactionType.CREDIT);
            var trxToPost2 = new FinancialTransaction(defaultCreditAmount, "Trx 1", now, FinancialTransactionType.CREDIT);
            var trxToPost3 = new FinancialTransaction(defaultDebitAmount, "Trx 1", now, FinancialTransactionType.DEBIT);
            acc.PostTransaction(trxToPost1);
            acc.PostTransaction(trxToPost2);
            acc.PostTransaction(trxToPost3);
            var snapshot = acc.GetMostRecentSnapshot();
            Assert.That(snapshot != null);
            var expectedBalance = initialBalance + trxToPost1.Amount + trxToPost2.Amount - trxToPost3.Amount;
            Assert.AreEqual(expectedBalance, snapshot.Balance);

        }


    }
}
