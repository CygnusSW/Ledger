using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BankingLedger.API.Filters
{
    public class JWTFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext ctx)
        {
           
        }
    }
}