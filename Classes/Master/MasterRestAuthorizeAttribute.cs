using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Master
{
	public class MasterRestAuthorizeAttribute:
		AuthorizeAttribute
	{
		private const string ParameterNameSecurityToken = "Token";

		public IEnumerable<MasterActionType> Actions;

        public MasterRestAuthorizeAttribute(params MasterActionType[] actions)
        {
            this.Actions = actions; 
        }

		public MasterRestAuthorizeAttribute (IEnumerable<MasterActionType> actions)
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
				string token = request.Params [MasterRestAuthorizeAttribute.ParameterNameSecurityToken];

				return (false);
				//return (MasterAuthenticationHelper.ThrowOnInValidToken(token, MasterAuthenticationHelper.GetClientIpAddress(request), request.UserAgent));
			}
			catch
			{
				return (false);
			}
		}
	}
}