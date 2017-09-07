using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Common;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers
{
    public class TenantUserCurrentController:
		Controller
    {
        [TenantMvcTokenAuthorize]
        public ActionResult Index()
        {
			Exception exception = null;
			TenantUserSession tenantUserSession = null;
			var userViewModel = new UserCurrentViewModel();

			if (!TenantAuthenticationHelper.ValidateToken(this.Request, SessionType.Mvc, out tenantUserSession, out exception)) { this.Response.RedirectToRoute("TenantSignIn"); }

			userViewModel.NameGiven = tenantUserSession.User.NameGiven;
			userViewModel.NameFamily = tenantUserSession.User.NameFamily;
			userViewModel.Email = tenantUserSession.User.Email;
			userViewModel.UserName = tenantUserSession.User.UserName;
			userViewModel.UserType = tenantUserSession.User.AuthenticationType;
			userViewModel.Company = tenantUserSession.Tenant.CompanyName;
			userViewModel.Domain = tenantUserSession.Tenant.Domain;

			this.TempData.Remove(typeof(TenantUserSession).Name);
			this.TempData.Add(typeof(TenantUserSession).Name, tenantUserSession);

			return (this.PartialView("~/Views/Tenants/Users/UserCurrent.cshtml", tenantUserSession));
        }
    }
}