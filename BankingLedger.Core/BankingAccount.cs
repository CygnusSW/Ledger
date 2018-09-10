using BankingLedger.Core.DataModels;
using BankingLedger.Core.Enums;
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
        public List<FinancialTransaction> AccountRecords;
        private List<BalanceSnapshot> _snapshots;
        private string _accountName;
        private int _snapshotFrequency;

        public long AccountNumber { get; }

        public string AccountName
        {
            get
            {
                return _accountName;
            }
        }

        public decimal CurrentBalance
        {
            get
            {
                return GetBalance();
            }
        }


        public BankingAccount(string accountName, int snapshotFrequency)
        {
            AccountNumber = MockIDGenerator.Generate();
            _accountName= accountName;
            _snapshots = new List<BalanceSnapshot>();
            _snapshotFrequency = snapshotFrequency;
            AccountRecords = new List<FinancialTransaction>();
        }

        public decimal GetBalance()
        {
            decimal currentBalance = 0.00m;

            //This is a 'hacky' implementation. 
            //In a production system, this would be based on date, not ID
            //There would also be cleaner separation between read and write models (think cqrs)
            BalanceSnapshot lastSnapshot = GetMostRecentSnapshot();
            if (lastSnapshot != null)
            {
                var trxsSinceSnapshot = AccountRecords.Where(t => t.TransactionID > lastSnapshot.TransactionID);
                currentBalance = SumTransactions(trxsSinceSnapshot) + lastSnapshot.Balance;
            }
            else
            {
                currentBalance = SumTransactions(AccountRecords);
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
            AccountRecords.Add(trx);
            var currentBalance = GetBalance();

            //In a production system, we would run a separate process that would snapshot
            //the previous days event, and we would make the snapshot time-based instead of ID-based.
            var shouldSnapshot = AccountRecords.Count % _snapshotFrequency == 0;
            if (shouldSnapshot)
            {                
                _snapshots.Add(new BalanceSnapshot(currentBalance, DateTime.UtcNow, trx.TransactionID));
            }

            return currentBalance;
        }

        public decimal SumTransactions(IEnumerable<FinancialTransaction> trxs)
        {
            return trxs.Sum(t => t.RecordType == FinancialTransactionType.CREDIT ? t.Amount : -t.Amount);
        }

        /// <summary>
        /// Returns the most-recent snapshot of the account balance.
        /// </summary>
        /// <returns>A snapshot with the account's balance as of that snapshot.</returns>
        public BalanceSnapshot GetMostRecentSnapshot()
        {
            return _snapshots.LastOrDefault();
        }

    }
}
