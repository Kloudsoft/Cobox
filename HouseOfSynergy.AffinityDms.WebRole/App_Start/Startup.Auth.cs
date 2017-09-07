using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using HouseOfSynergy.AffinityDms.WebRole.Models;

namespace HouseOfSynergy.AffinityDms.WebRole
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
        }
        //public void ConfigureServices(ServiceCollection services)
        //{
        //    services.AddMvc();
        //    services.Configure<MvcOptions>(options =>
        //    {
        //        options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
        //    });
        //}
    }
}