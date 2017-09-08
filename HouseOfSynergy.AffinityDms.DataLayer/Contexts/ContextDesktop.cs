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
using HouseOfSynergy.AffinityDms.DataLayer.Configurations.Common;
using HouseOfSynergy.AffinityDms.DataLayer.Configurations.Desktop;
using HouseOfSynergy.AffinityDms.DataLayer.Initializers;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Desktop;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.DataLayer.Contexts
{
	public class ContextDesktop:
		ContextBase
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public DbSet<Culture> Cultures { get; set; }
		public DbSet<ScanSession> ScanSessions { get; set; }
		public DbSet<DocumentEntry> DocumentEntries { get; set; }

		#endregion Members.

		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		public ContextDesktop (string nameOrConnectionString, bool proxyCreationEnabled = false, bool lazyLoadingEnabled = false)
			: base(nameOrConnectionString, proxyCreationEnabled, lazyLoadingEnabled)
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
			modelBuilder.Configurations.Add<Culture>(new CultureConfiguration());
			modelBuilder.Configurations.Add<ScanSession>(new ScanSessionConfiguration());
			modelBuilder.Configurations.Add<DocumentEntry>(new DocumentEntryConfiguration());

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
					//Debug.Print(exception.ToString());
				}
				else
				{
					throw;
				}
			}
			catch (Exception exception)
			{
				//Debug.Print(exception.ToString());

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

		static ContextDesktop ()
		{
		}

		public static void Initialize (string databaseConnectionString)
		{
			IDatabaseInitializer<ContextDesktop> initializer = null;

			if (ContextDesktop.HasInitializeBeenCalled)
			{
				// No need to throw an exception here.
			}
			else
			{
				ContextDesktop.HasInitializeBeenCalled = true;

				try
				{
					initializer = new CreateDatabaseIfNotExistsInitializer();
					//initializer = new DropCreateDatabaseAlwaysInitializer();
					//initializer = new DropCreateDatabaseIfModelChangesInitializer();

					Database.SetInitializer<ContextDesktop>(initializer);

					using (var context = new ContextDesktop(databaseConnectionString))
					{
						context.Database.Initialize(force : true);
					}
				}
				catch (Exception exception)
				{
					if (AffinityConfiguration.IsConfigurationDebug)
					{
						Debugger.Break();
						exception.ToString();
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