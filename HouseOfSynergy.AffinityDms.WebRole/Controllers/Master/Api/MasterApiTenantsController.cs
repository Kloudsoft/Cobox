using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers.Api
{
	public class MasterApiTenantsController:
		ApiController
	{
		[HttpGet]
		public JsonResult<List<Tenant>> Get ()
		{
			Exception exception = null;
			List<Tenant> tenants = null;

			if (MasterTenantManagement.GetTenants(out tenants, out exception))
			{
				foreach (var tenant in tenants)
				{
					tenant.RsaKeyPrivate = "";
					tenant.UrlStorage = "";
					tenant.UrlStorageBlob = "";
					tenant.UrlStorageTable = "";
					tenant.UrlStorageQueue = "";
					tenant.UrlStorageFile = "";
					tenant.StorageAccessKeyPrimary = "";
					tenant.StorageAccessKeySecondary = "";
					tenant.StorageConnectionStringPrimary = "";
					tenant.StorageConnectionStringSecondary = "";
					tenant.DatabaseConnectionString = "";

					foreach (var tenantSubscription in tenant.TenantSubscriptions)
					{
						tenantSubscription.Tenant = null;
					}
				}

				return (this.Json(tenants));
			}
			else
			{
				return (this.Json(tenants));
				// TODO: Log exception.
				throw (new HttpResponseException(HttpStatusCode.InternalServerError));
			}
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