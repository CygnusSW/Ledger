using BankingLedger.Core.DataModels;
using BankingLedger.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Core
{
    /// <summary>
    /// Event-Sourced Ledger, with the current balance being
    /// </summary>
    public class BankingAccount
    {
        private List<FinancialTransaction> _accountRecords;
        private List<BalanceSnapshot> _snapshots;
        private string _accountName;

        private long AccountNumber { get; }

        public string AccountName
        {
            get
            {
                return _accountName;
            }
        }
        

        public BankingAccount(string accountName)
        {

            AccountNumber = MockIDGenerator.Generate();
            _accountName= accountName;
            _accountRecords = new List<FinancialTransaction>();
            _snapshots = new List<BalanceSnapshot>();
        }

        public decimal GetBalance()
        {
            decimal currentBalance = 0.00m;

            BalanceSnapshot lastSnapshot = _snapshots.LastOrDefault();
            if (lastSnapshot != null)
            {
                var trxsSinceSnapshot = _accountRecords.Where(t => t.TransactionID > lastSnapshot.TransactionID);
            }
            else
            {
                currentBalance = SumTransactions(_accountRecords);
            }               
            
            return currentBalance;
        }

        /// <summary>
        /// Logs a transaction against an account.
        /// </summary>
        /// <param name="trx">The transaction to record for the bank account.</param>
        /// <returns>The account balance after the transaction has been posted.</returns>
        public decimal PostTransaction(FinancialTransaction trx)
        {
            _accountRecords.Add(trx);
            return GetBalance();            
        }

        public decimal SumTransactions(IEnumerable<FinancialTransaction> trxs)
        {
            return trxs.Sum(t => t.Amount);
        }

        


        



    }
}
