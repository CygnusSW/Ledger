using BankLedger.Core.Enums;
using BankLedger.Core.Models;
using BankLedger.Persistence.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLedger.Core.Adapters
{
    public static class FinancialTransactionAdapter
    {
        public static FinancialTransactionModel ToModel(FinancialTransaction trx)
        {
            return new FinancialTransactionModel()
            {
                Amount = trx.Amount,
                IsPosted = trx.PostedDateTime != null,
                TransactionDate = DateTime.UtcNow,
                Type = (TransactionType)trx.TransactionTypeID,
                Description = trx.Description
            };
        }

        public static FinancialTransaction ToPersistenceEntity(FinancialTransactionModel model)
        {
            return new FinancialTransaction()
            {
                Amount = model.Amount,
                Description = model.Description,
                PostedDateTime = null,
                TransactionDateTime = model.TransactionDate,
                TransactionTypeID = (int)model.Type
            };
        }
    }
}
