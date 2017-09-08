using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public partial class ScreenConfiguration:
		EntityTypeConfiguration<Screen>
	{

		public ScreenConfiguration ()
		{
			// Table name in database.
			this.ToTable("Screen");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasOptional(p => p.Parent).WithMany(p => p.Screens).HasForeignKey(p => p.ParentId);
			this.HasMany(p => p.Screens).WithOptional(p => p.Parent).HasForeignKey(p => p.ParentId);

			// Foriegn Keys.
			this.HasMany(p => p.Buttons).WithRequired(p => p.Screen).HasForeignKey(p => p.ScreenId);
			this.HasMany(p => p.RoleRights).WithRequired(p => p.Screen).HasForeignKey(p => p.ScreenId);

			// Class property validation.
		}
	}
}