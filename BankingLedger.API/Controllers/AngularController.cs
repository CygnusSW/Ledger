using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;

namespace BankingLedger.API.Controllers
{
    public class AngularController : ApiController
    {
        [System.Web.Http.HttpGet]
        // GET: Angular
        public  HttpResponseMessage Index()
        {
            var path = HttpContext.Current.Server.MapPath(@"~/index.html");
            var response = new HttpResponseMessage();
            var fileContents = File.ReadAllText(path);
            response.Content = new StringContent(fileContents);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}