using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Configurations;
using HouseOfSynergy.AffinityDms.DataLayer.Configurations.Common;
using HouseOfSynergy.AffinityDms.DataLayer.Configurations.Master;
using HouseOfSynergy.AffinityDms.DataLayer.Initializers;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.DataLayer.Contexts
{
	public class ContextMaster:
		ContextBase
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public DbSet<Culture> Cultures { get; set; }

		public DbSet<MasterUser> Users { get; set; }
		public DbSet<MasterRole> Roles { get; set; }
		public DbSet<MasterSession> Sessions { get; set; }
		public DbSet<MasterUserRole> UserRoles { get; set; }

		public DbSet<Tenant> Tenants { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }
		public DbSet<TenantSubscription> TenantSubscriptions { get; set; }

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public ContextMaster (bool proxyCreationEnabled = false, bool lazyLoadingEnabled = false, DeploymentLocation? deploymentLocation = null)
			: base(AffinityConfigurationMaster.GetDatabaseConnectionStringBuilder(deploymentLocation ?? AffinityConfiguration.DeploymentLocation).ConnectionString, proxyCreationEnabled, lazyLoadingEnabled)
			//: base(AffinityConfigurationMaster.DatabaseConnectionString, proxyCreationEnabled, lazyLoadingEnabled)
		{
			// Not allowed for databases created outside of EF.
			//this.ObjectContext.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
		}

		#endregion Constructors.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public ObjectContext ObjectContext { get { return (((IObjectContextAdapter) this).ObjectContext); } }

		#endregion Properties.

		#region Base Overrides.

		//====================================================================================================
		// Base Overrides.
		//====================================================================================================

		protected override void OnModelCreating (DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			modelBuilder.Configurations.Add<MasterUser>(new MasterUserConfiguration());
			modelBuilder.Configurations.Add<MasterRole>(new MasterRoleConfiguration());
			modelBuilder.Configurations.Add<MasterSession>(new MasterSessionConfiguration());
			modelBuilder.Configurations.Add<MasterUserRole>(new MasterUserRoleConfiguration());

			modelBuilder.Configurations.Add<Tenant>(new TenantConfiguration());
			modelBuilder.Configurations.Add<Culture>(new CultureConfiguration());
			modelBuilder.Configurations.Add<Subscription>(new SubscriptionConfiguration());
			modelBuilder.Configurations.Add<TenantSubscription>(new TenantSubscriptionConfiguration());

			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges ()
		{
			var result = 0;

			try
			{
				result = base.SaveChanges();
			}
			catch (DbEntityValidationException exception)
			{
				if (AffinityConfiguration.IsConfigurationDebug)
				{
					Debugger.Break();
					////Debug.Print(exception.ToString());
				}
				else
				{
					throw;
				}
			}
			catch (Exception exception)
			{
				////Debug.Print(exception.ToString());

				throw;
			}

			return (result);
		}

		protected override bool ShouldValidateEntity (DbEntityEntry entityEntry)
		{
			return (base.ShouldValidateEntity(entityEntry));
		}

		protected override DbEntityValidationResult ValidateEntity (DbEntityEntry entityEntry, IDictionary<object, object> items)
		{
			return (base.ValidateEntity(entityEntry, items));
		}

		#endregion Base Overrides.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static bool HasInitializeBeenCalled { get; private set; }

		static ContextMaster ()
		{
			ContextMaster.HasInitializeBeenCalled = false;

			ContextMaster.Initialize();
		}

		public static void Initialize (DeploymentLocation? deploymentLocation = null)
		{
			IDatabaseInitializer<ContextMaster> initializer = null;

			if (ContextMaster.HasInitializeBeenCalled)
			{
				// No need to throw an exception here.
			}
			else
			{
				ContextMaster.HasInitializeBeenCalled = true;

				try
				{
					initializer = new CreateDatabaseIfNotExistsInitializer();
					//initializer = new DropCreateDatabaseAlwaysInitializer();
					//initializer = new DropCreateDatabaseIfModelChangesInitializer();

					Database.SetInitializer<ContextMaster>(initializer);

					using (var context = new ContextMaster(deploymentLocation: deploymentLocation ?? AffinityConfiguration.DeploymentLocation))
					{
						context.Database.Initialize(force : true);
					}
				}
				catch (Exception exception)
				{
					if (AffinityConfiguration.IsConfigurationDebug)
					{
						Debugger.Break();

						if (AffinityConfiguration.DeploymentLocation != DeploymentLocation.Live)
						{
							Debug.Print(exception.ToString());
						}
					}
					else
					{
						throw;
					}
				}
			}
		}

		#endregion Static.
	}
}