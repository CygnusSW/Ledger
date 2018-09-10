using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingLedger.API.Models
{
    public class AuthRequestModel
    {
        [Required]
        public string Username;
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage ="Passwords must be at least 6 characters long.")]
        public string Password;
        [Required]
        public string AccountName;
    }
}