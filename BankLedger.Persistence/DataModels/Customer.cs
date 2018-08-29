using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLedger.Persistence.DataModels
{
    public class Customer
    {
        public long CustomerID;
        public List<BankAccount> Accounts = new List<BankAccount>();
    }
}
