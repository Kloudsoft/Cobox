using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Master;
using HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			//filters.Add(new MvcMasterTokenAuthorizeAttribute());
			//filters.Add(new MvcTenantsTokenAuthorizeAttribute());
		}
	}
}
