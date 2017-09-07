using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.BusinessLayer;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.MasterControllers.Api
{
	public class MasterApiTenantController:
		ApiController
	{
		[HttpGet]
		public JsonResult<Tenant> Get (string domain)
		{
			Tenant tenant = null;
			Exception exception = null;

			if (MasterTenantManagement.GetTenantByDomain(domain, out tenant, out exception))
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

				return (this.Json(tenant));
			}
			else
			{
				// TODO: Log exception.
				throw (new HttpResponseException(HttpStatusCode.InternalServerError));
			}
		}
	}
}