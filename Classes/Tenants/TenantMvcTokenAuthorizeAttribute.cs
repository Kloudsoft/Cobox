using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
    public class TenantMvcTokenAuthorizeAttribute:
		AuthorizeAttribute
    {
		public TenantUserSession TenantUserSession { get; private set; }

        public TenantMvcTokenAuthorizeAttribute()
        {
		}

		protected override bool AuthorizeCore (HttpContextBase httpContext)
		{
			var authorized = false;
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!TenantAuthenticationHelper.ValidateToken(httpContext.Request, SessionType.Mvc, out tenantUserSession, out exception)) { httpContext.Response.RedirectToRoute("TenantSignIn"); }

			authorized = ((exception == null) && (tenantUserSession != null));

			this.TenantUserSession = tenantUserSession;

			return (authorized);
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}