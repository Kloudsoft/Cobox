using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers.Api
{
    public class TenantApiAuthenticationController:
		ApiController
    {
		[HttpGet]
		public JsonResult<TenantUserSession> SignIn (string domain, string username, string passwordHash)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			var domainDecoded = HttpUtility.UrlDecode(domain);
			var usernameDecoded = HttpUtility.UrlDecode(username);
			var passwordHashDecoded = HttpUtility.UrlDecode(passwordHash);

			if (!AuthenticationManagement.SignIn(SessionType.Api, domainDecoded, usernameDecoded, passwordHashDecoded, HttpRequestUtilities.GetClientIpAddressFromHttpRequestMessage(this.Request), this.Request.Headers.UserAgent.ToString(), 0, "", out tenantUserSession, out exception)) { throw (exception); }

			return (this.Json(tenantUserSession, JsonNetFormatter.JsonSerializerSettings));
		}

		[HttpGet]
		public JsonResult<TenantUserSession> ValidateToken (string domain, string username, string token, bool? invalidate)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			if (!AuthenticationManagement.ValidateToken(token, SessionType.Api, domain, username, HttpRequestUtilities.GetClientIpAddressFromHttpRequestMessage(this.Request), this.Request.Headers.UserAgent.ToString(), 0, "", out tenantUserSession, out exception)) { throw (exception); }

			return (this.Json(tenantUserSession, JsonNetFormatter.JsonSerializerSettings));
		}

		[HttpGet]
		public JsonResult<TenantUserSession> SignOut (string domain, string username, string token)
		{
			Exception exception = null;
			TenantUserSession tenantUserSession = null;

			AuthenticationManagement.SignOut(domain, username, token, out exception);
			tenantUserSession = new TenantUserSession(new Tenant(), new User(), new Session());

			return (this.Json(tenantUserSession, JsonNetFormatter.JsonSerializerSettings));
		}
	}
}