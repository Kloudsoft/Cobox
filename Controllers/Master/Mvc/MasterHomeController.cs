using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers
{
    public class MasterHomeController:
		Controller
    {
        // GET: Home
         [AllowAnonymous]
        public ActionResult Index()
        {
			return (this.View("~/Views/Master/Home.cshtml"));
        }
    }
}