using BankLedger.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLedger.Core.Models
{
    public class FinancialTransactionModel
    {
        public decimal Amount;
        public bool IsPosted;
        public string Description;
        public TransactionType Type;
        public DateTime TransactionDate;
    }
}
