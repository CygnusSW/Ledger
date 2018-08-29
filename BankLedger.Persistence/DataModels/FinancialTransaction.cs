using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLedger.Persistence.DataModels
{
    public class FinancialTransaction
    {
        public DateTime TransactionDateTime;
        public DateTime? PostedDateTime;
        public string Description;
        public decimal Amount;
        public int TransactionTypeID;
    }
}
