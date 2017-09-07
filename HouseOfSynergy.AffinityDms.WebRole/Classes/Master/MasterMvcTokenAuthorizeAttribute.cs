using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Master
{
    public class MasterMvcTokenAuthorizeAttribute:
		AuthorizeAttribute
    {
		public MasterUserSession MasterUserSession { get; private set; }

		public MasterMvcTokenAuthorizeAttribute ()
        {
        }

		protected override bool AuthorizeCore (HttpContextBase httpContext)
		{
			var authorized = false;
			Exception exception = null;
			MasterUserSession masterUserSession = null;

			if (!MasterAuthenticationHelper.ValidateToken(httpContext.Request, SessionType.Mvc, out masterUserSession, out exception)) { httpContext.Response.RedirectToRoute("TenantSignIn"); }

			authorized = ((exception == null) && (masterUserSession != null));

			this.MasterUserSession = masterUserSession;

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