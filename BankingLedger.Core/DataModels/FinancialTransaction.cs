using BankingLedger.Core.Enums;
using BankingLedger.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Core.DataModels
{
    public class FinancialTransaction
    {
        public long TransactionID;
        public DateTime TransactionDate { get; }
        public DateTime? PostedDate { get; }
        public virtual decimal Amount { get; }
        public string Description { get; }
        public FinancialTransactionType RecordType { get; }

        public FinancialTransaction(decimal amount, string description, DateTime trxDate, FinancialTransactionType recordType, bool post = true)
        {
            TransactionID = MockIDGenerator.Generate();
            Amount = amount;
            Description = description;

            var now = DateTime.UtcNow;
            TransactionDate = now;
            PostedDate = post ? now : (DateTime?)null;
            RecordType = recordType;
        }



    }
}
