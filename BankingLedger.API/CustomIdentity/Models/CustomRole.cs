using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingLedger.API.CustomIdentity.Models
{
    public class CustomRole : IRole
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}