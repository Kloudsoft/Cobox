using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.TenantControllers.Api
{
	public class TenantApiUsersController:
		ApiController
	{
		// GET api/<controller>
		public IHttpActionResult Get (string token)
		{
			User user = null;
			Exception exception = null;
			var userEntities = new List<User>();

			TenantUserSession tenantUserSession = null;

			//if (!AuthenticationManagement.ValidateToken(token, out user, out exception)) { return (this.BadRequest()); }
			if (user == null) { return (this.BadRequest()); }

			if (!TenantUserManagement.GetUsers(tenantUserSession, out userEntities, out exception)) { return (this.BadRequest()); }

			return (this.Ok(userEntities));
		}

		// GET api/<controller>/5
		public string Get (int id)
		{
			return "value";
		}

		// POST api/<controller>
		public void Post ([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put (int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete (int id)
		{
		}
	}
}