using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
	public class TenantDashboardController:
		Controller
	{
		[HttpGet]
        [TenantMvcActionAuthorize]
		public ActionResult Index ()
		{
            var tenantUserSessionKey = HttpContext.Items["KEY"];
			Exception exception = null;
			var tenantUserSession = this.TempData [typeof(TenantUserSession).Name] as TenantUserSession;

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { return (this.RedirectToRoute("TenantSignIn")); }
            //if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, TenantActionType.DashboardView)) { throw (new UserNotAuthorizedException()); }
            if (tenantUserSession.User.AuthenticationType == AuthenticationType.External)
            {
                return RedirectToAction("Index", "TenantDocumentsFolderWise");
            }
            return (this.View("~/Views/Tenants/Dashboard.cshtml"));
		}
	}
}