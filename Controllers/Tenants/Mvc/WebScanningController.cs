using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
    public class WebScanningController : Controller
    {
        // GET: WebScanning
        public ActionResult Index()
        {
            return View("~/Views/Tenants/WebScanning/WebScanning.cshtml");
        }
    }
}