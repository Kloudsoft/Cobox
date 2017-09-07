using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Master
{
    public class MasterTenantsController:
		Controller
    {
        public ActionResult Index()
        {
			Exception exception = null;
			List<Tenant> tenants = null;
            MasterUserSession MasterUserSession = null;
            try
            {
				if (!MasterAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out MasterUserSession, out exception)) { this.Response.RedirectToRoute("MasterSignIn"); }

                if (!MasterTenantManagement.GetTenants(out tenants, out exception)) { throw (exception); }

                if (exception != null)
                    throw exception;
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                    this.ViewBag.Exception = ex.InnerException.Message;
                else
                    this.ViewBag.Exception = ex.Message;
            }

            return View("~/Views/Master/Tenants.cshtml", tenants);
        }
    }
}