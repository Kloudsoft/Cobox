using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
	public class MasterMvcActionAuthorizeAttribute:
		AuthorizeAttribute
	{
		public IEnumerable<MasterActionType> Actions { get; private set; }

		public MasterMvcActionAuthorizeAttribute (IEnumerable<MasterActionType> actions)
		{
			this.Actions = actions;
		}

		public MasterMvcActionAuthorizeAttribute (params MasterActionType [] actions)
		{
			this.Actions = actions;
		}

		protected override bool AuthorizeCore (HttpContextBase httpContext)
		{
			var authorized = false;
			Exception exception = null;
			MasterUserSession masterUserSession = null;

			if (!MasterUserManagement.IsUserActionAllowed(masterUserSession.User, this.Actions))
			{
				// TODO: Notify user.
				throw (new UserNotAuthorizedException());
				//httpContext.Response.RedirectToRoute(RoutingUtilities.RouteMapsMvc[RouteMapTypeMvc.TenantSignIn].Name);
			}

			authorized = ((exception == null) && (masterUserSession != null));

			return authorized;
		}

		public override void OnAuthorization (AuthorizationContext filterContext)
		{
			base.OnAuthorization(filterContext);
		}

		protected override void HandleUnauthorizedRequest (AuthorizationContext filterContext)
		{
			base.HandleUnauthorizedRequest(filterContext);
		}
	}
}