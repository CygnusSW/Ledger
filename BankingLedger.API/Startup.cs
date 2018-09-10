using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(BankingLedger.API.Startup))]

namespace BankingLedger.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseDefaultFiles(new DefaultFilesOptions()
            {
                DefaultFileNames = new List<string> { "index.html" }            
            });
            app.UseStaticFiles();
            ConfigureAuth(app);
        }
    }
}
