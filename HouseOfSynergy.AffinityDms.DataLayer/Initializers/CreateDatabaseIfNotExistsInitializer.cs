using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.DataLayer.Seeders;

namespace HouseOfSynergy.AffinityDms.DataLayer.Initializers
{
	public sealed class CreateDatabaseIfNotExistsInitializer:
		CreateDatabaseIfNotExists<ContextBase>
	{
		public override void InitializeDatabase (ContextBase context)
		{
			base.InitializeDatabase(context);
		}

		protected override void Seed (ContextBase context)
		{
			if (context is ContextMaster)
			{
				SeederMaster.Seed(context as ContextMaster);
			}
			else if (context is ContextTenant)
			{
				SeederTenant.Seed(context as ContextTenant);
			}
			else if (context is ContextDesktop)
			{
				SeederDesktop.Seed(context as ContextDesktop);
			}
			else
			{
				throw (new NotImplementedException());
			}

			base.Seed(context);
		}
	}
}