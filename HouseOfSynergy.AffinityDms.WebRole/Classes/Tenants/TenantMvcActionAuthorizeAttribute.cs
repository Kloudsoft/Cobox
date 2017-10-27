using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
	public class TenantMvcActionAuthorizeAttribute:
		AuthorizeAttribute
	{
		public IEnumerable<TenantActionType> Actions { get; private set; }
        public TenantUserSession TenantUserSession { get; private set; }

		public TenantMvcActionAuthorizeAttribute (IEnumerable<TenantActionType> actions)
		{
			this.Actions = actions;
		}

		public TenantMvcActionAuthorizeAttribute (params TenantActionType [] actions)
		{
			this.Actions = actions;
		}




		protected override bool AuthorizeCore (HttpContextBase httpContext)
		{
            //var authorized = false;
            //Exception exception = null;
            //TenantUserSession tenantUserSession = null;

            //if (!TenantUserManagement.IsUserActionAllowed(tenantUserSession.User, this.Actions))
            //{
            //    // TODO: Notify user.
            //    throw (new UserNotAuthorizedException());
            //    //httpContext.Response.RedirectToRoute(RoutingUtilities.RouteMapsMvc[RouteMapTypeMvc.TenantSignIn].Name);
            //}

            //authorized = ((exception == null) && (tenantUserSession != null));

			return true;
		}

		public override void OnAuthorization (AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Items.Contains("KEY"))
                filterContext.HttpContext.Items.Remove("KEY");

            if (TenantUserSession == null)
            {
                var authorized = false;
                Exception exception = null;
                TenantUserSession tenantUserSession2 = null;

                if (!TenantAuthenticationHelper.ValidateToken(filterContext.HttpContext.Request, SessionType.Mvc, out tenantUserSession2, out exception)) { filterContext.HttpContext.Response.RedirectToRoute("TenantSignIn"); }

                authorized = ((exception == null) && (tenantUserSession2 != null));

                this.TenantUserSession = tenantUserSession2;
            }
            filterContext.HttpContext.Items.Add("KEY", this.TenantUserSession);

			base.OnAuthorization(filterContext);
		}

		protected override void HandleUnauthorizedRequest (AuthorizationContext filterContext)
		{
			base.HandleUnauthorizedRequest(filterContext);
		}
	}
}