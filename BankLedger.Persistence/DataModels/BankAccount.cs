using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLedger.Persistence.DataModels
{
    public class BankAccount
    {
        public long AccountID;
        public long AccountNumber;
        public string AccountName;
        public List<FinancialTransaction> Transactions;
    }
}
