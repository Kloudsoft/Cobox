using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Web;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
	public static class TenantAuthenticationHelper
	{
		private static TenantUserSession ThrowOnInValidToken (HttpRequestBase request, SessionType sessionType)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			var cookie = request.Cookies [TenantUserSessionResolver.TenantUserSessionCookieName];
			if (cookie == null) { throw (new TokenInvalidException()); }

			if (!TenantUserSessionResolver.GetTenantUserSessionFromHttpRequest(request, out tenantUserSession, out exception)) { throw (exception); }

			AuthenticationManagement.ThrowOnInvalidToken
			(
				tenantUserSession.Session.Token,
				sessionType,
				tenantUserSession.Tenant.Domain,
				tenantUserSession.User.UserName,
				HttpRequestUtilities.GetClientIpAddressFromHttpRequestBase(request),
				request.UserAgent,
				0,
				tenantUserSession.Session.SessionId,
				out tenantUserSession
			);

			return (tenantUserSession);
		}

		public static bool ValidateToken (HttpRequestBase request, SessionType sessionType, out TenantUserSession tenantUserSession, out Exception exception)
		{
			var result = false;

			exception = null;
			tenantUserSession = null;

			try
			{
				tenantUserSession = TenantAuthenticationHelper.ThrowOnInValidToken(request, sessionType);

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}
	}
}