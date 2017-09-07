using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.WebRole.Controllers.Tenants.Mvc
{
	public class TenantBaseMvcController:
		Controller
	{
		public Tenant Tenant
		{
			get
			{
				object tenant = null;

				if (!Request.GetOwinContext().Environment.TryGetValue("Tenant", out tenant))
				{
					throw (new Exception("Tenant not found by Owin."));
				}

				return ((Tenant) tenant);
			}
		}
	}
}
