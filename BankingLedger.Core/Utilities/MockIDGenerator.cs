using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Core.Utilities
{
    public static class MockIDGenerator
    {
        private static long CurrentID = 13050440281;
        /// <summary>
        /// This would be the auto-generated ID from whatever persistence we use.
        /// </summary>
        /// <returns></returns>
        public static long Generate()
        {
            CurrentID++;
            return CurrentID;            
        }
    }
}
