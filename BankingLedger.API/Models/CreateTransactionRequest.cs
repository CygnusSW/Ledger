using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingLedger.API.Models
{
    public class CreateTransactionRequest
    {
        public long AccountNumber;
        public decimal Amount;
        public string Description;
        public int TransactionType;
    }
}