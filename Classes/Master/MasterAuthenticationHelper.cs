using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Web;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Master;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Master
{
	public static class MasterAuthenticationHelper
	{
		public static MasterUserSession ThrowOnInValidToken (HttpRequestBase request, SessionType sessionType)
		{
			Exception exception = null;
			MasterUserSession masterUserSession = null;

			var cookie = request.Cookies [MasterUserSessionResolver.MasterUserSessionCookieName];
			if (cookie == null) { throw (new TokenInvalidException()); }

			if (!MasterUserSessionResolver.GetMasterUserSessionFromHttpRequest(request, out masterUserSession, out exception)) { throw (exception); }

			MasterAuthenticationManagement.ThrowOnInvalidToken
			(
				masterUserSession.Session.Token,
				sessionType,
				masterUserSession.User.UserName,
				HttpRequestUtilities.GetClientIpAddressFromHttpRequestBase(request),
				request.UserAgent,
				0,
				masterUserSession.Session.SessionId,
				out masterUserSession
			);

			return (masterUserSession);
		}

		public static bool ValidateToken (HttpRequestBase request, SessionType sessionType, out MasterUserSession masterUserSession, out Exception exception)
		{
			var result = false;

			exception = null;
			masterUserSession = null;

			try
			{
				masterUserSession = MasterAuthenticationHelper.ThrowOnInValidToken(request, sessionType);

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