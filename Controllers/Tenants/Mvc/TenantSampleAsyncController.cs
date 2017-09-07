using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Utility;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
	public class TenantSampleAsyncController:
		TenantBaseMvcController
	{
		[HttpGet]
		public async Task<ActionResult> Index()
		{
			var users = new TCache<Task<List<User>>>().Get
			(
				"CacheUsers",
				30,
				async () =>
				{
					using (var context = new ContextTenant(base.Tenant.DatabaseConnectionString))
					{
						return (await context.Users.ToListAsync());
					}
				}
			);

			return (this.View("", await users));
		}
	}
}