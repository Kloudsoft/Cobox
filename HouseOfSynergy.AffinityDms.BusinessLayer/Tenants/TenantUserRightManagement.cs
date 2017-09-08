using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
	public class TenantUserRightManagement
	{
		public static IList<Screen> GetScreens ()
		{
			IList<Screen> screen = null;
			//using (var context = new ContextTenant())
			//{
			//	screen = context.Screens.ToList();
			//}
			return screen;
		}


      

    }
}