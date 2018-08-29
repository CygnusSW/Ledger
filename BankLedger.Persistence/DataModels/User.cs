using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLedger.Persistence.DataModels
{
    public class User
    {
        public string Username;
        public string HashedPassword;
        public Customer Customer;
    }
}
