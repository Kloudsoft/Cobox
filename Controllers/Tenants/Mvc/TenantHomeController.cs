using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantHomeController : Controller
    {
        // GET: Home
         [AllowAnonymous]
        public ActionResult Index()
        {
			return (this.View("~/Views/Tenants/Home.cshtml"));
        }
    }
}