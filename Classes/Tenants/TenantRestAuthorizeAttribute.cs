using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
	public class TenantRestAuthorizeAttribute:
		AuthorizeAttribute
	{
		private const string ParameterNameSecurityToken = "Token";

        public IEnumerable<TenantActionType> Actions;

        public TenantRestAuthorizeAttribute(params TenantActionType[] actions)
        {
            this.Actions = actions; 
        }

        public TenantRestAuthorizeAttribute(IEnumerable<TenantActionType> actions)
        {
            this.Actions = actions;
        }

		public override void OnAuthorization (AuthorizationContext filterContext)
		{
			if (!this.Authorize(filterContext))
			{
				this.HandleUnauthorizedRequest(filterContext);
			}
		}

		protected override void HandleUnauthorizedRequest (AuthorizationContext filterContext)
		{
			base.HandleUnauthorizedRequest(filterContext);
		}

		private bool Authorize (AuthorizationContext actionContext)
		{
			try
			{
				var request = actionContext.RequestContext.HttpContext.Request;
				string token = request.Params [TenantRestAuthorizeAttribute.ParameterNameSecurityToken];

				return (false);
				//return (AuthenticationHelper.IsTokenValid(token, AuthenticationHelper.GetClientIpAddress(request), request.UserAgent));
			}
			catch
			{
				return (false);
			}
		}
	}
}