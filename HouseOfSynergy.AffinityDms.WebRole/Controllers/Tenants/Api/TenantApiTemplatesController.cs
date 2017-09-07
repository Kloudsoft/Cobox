using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Common;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers.Api
{
	public class TenantApiTemplatesController:
		ApiController
	{
		[HttpGet]
		public JsonResult<List<Template>> GetAllTemplates (string domain, string username, string token)
		{
			Exception exception = null;
			List<Template> templates = null;
			TenantUserSession tenantUserSession = null;

			AuthenticationManagement.ThrowOnInvalidToken
			(
				token,
				SessionType.Api,
				domain,
				username,
				HttpRequestUtilities.GetClientIpAddressFromHttpRequestMessage(this.Request),
				this.Request.Headers.UserAgent.ToString(),
				0,
				"",
				out tenantUserSession
			);

			if (!ElementManagement.GetAllTemplates(tenantUserSession, out templates, out exception)) { throw (exception); }

			return (this.Json(templates, JsonNetFormatter.JsonSerializerSettings));
		}
	}
}