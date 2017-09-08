using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Configurations;
using HouseOfSynergy.AffinityDms.DataLayer.Initializers;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.DataLayer.Contexts
{
	public abstract class ContextBase:
		DbContext
	{
		protected ContextBase (string nameOrConnectionString, bool proxyCreationEnabled = false, bool lazyLoadingEnabled = false)
			: base(nameOrConnectionString)
		{
			this.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
			this.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
		}
	}
}