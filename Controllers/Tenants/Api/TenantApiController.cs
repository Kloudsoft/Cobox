using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers.Api
{
	public class TenantApiController:
		ApiController
	{
		[HttpGet]
		public IHttpActionResult Get ()
		{
			return (this.Ok());
		}
	}
}