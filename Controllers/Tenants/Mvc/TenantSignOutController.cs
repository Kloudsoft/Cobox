using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantSignOutController:
		Controller
    {
        public ActionResult Index()
        {
			HttpCookie cookie = null;
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());

			if (!TenantUserSessionResolver.GetTenantUserSessionHttpCookieFromTenantUserSession(tenantUserSession, out cookie, out exception)) { throw (exception); }

			this.Response.Cookies.Remove(TenantUserSessionResolver.TenantUserSessionCookieName);
			this.Response.SetCookie(cookie);

			// TODO: See which method allows the cookie to be deleted before redirection.
			//return (this.View("~/Views/Tenants/Home.cshtml"));
			return RedirectToAction("Index", "TenantSignIn", new { ErrorMessage = string.Empty, SuccessMessage = "You have been logged out successfully." });
		}
	}
}