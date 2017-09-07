using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.WebRole;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Utility;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Startup))]

namespace HouseOfSynergy.AffinityDms.WebRole
{
    public partial class Startup
    {
		private static object SyncRoot = new object();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

			//app.Use
			//(
			//	async (ctx, next) =>
			//	{
			//		Tenant tenant = this.GetTenantFromUrl(ctx.Request.Uri.Host);

			//		if (tenant == null)
			//		{
			//			throw (new Exception("Tenant not found by Owin."));
			//		}

			//		ctx.Environment.Add("Tenant", tenant);

			//		await next();
			//	}
			//);
		}

		private Tenant GetTenantFromUrl (string urlHost)
		{
			List<Tenant> tenants = null;

			if (string.IsNullOrWhiteSpace(urlHost))
			{
				throw (new Exception("Tenant URL not valid."));
			}
			else
			{
				int cacheTimeoutInSeconds = 30;
				string cacheName = "TenantCache";

				tenants = new TCacheInternal<List<Tenant>>().Get
				(
					cacheName,
					cacheTimeoutInSeconds,
					() =>
					{
						Exception exception = null;
						List<Tenant> entities = null;
						if (MasterTenantManagement.GetTenants(out entities, out exception)) { return (entities); } else { throw (exception); }
					}
				);

				//var tenant = tenants.SingleOrDefault(t => (t.Domain.ToLower() == urlHost.Trim().ToLower()));
				var tenant = tenants.FirstOrDefault();

				return (tenant);
			}
		}
	}
}
