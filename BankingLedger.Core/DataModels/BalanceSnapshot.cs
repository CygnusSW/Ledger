using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Core.DataModels
{
    public class BalanceSnapshot
    {
        public long TransactionID { get; }
        public decimal Balance { get; }
        public DateTime SnapshotDate { get; }

        //Accept snapshot date to capture in-flight transactions in the balance.
        public BalanceSnapshot(decimal balance, DateTime snapshotDate, long transactionID)
        {
            Balance = balance;
            SnapshotDate = snapshotDate;
            TransactionID = transactionID;
        }
    }
}
