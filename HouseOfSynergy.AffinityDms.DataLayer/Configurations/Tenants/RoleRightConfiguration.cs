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
	public partial class RoleRightConfiguration:
		EntityTypeConfiguration<RoleRight>
	{
		public RoleRightConfiguration ()
		{
			// Table name in database.
			this.ToTable("RoleRight");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Roles).WithMany(p => p.RoleRights).HasForeignKey(p => p.RoleId);
			this.HasRequired(p => p.Screen).WithMany(p => p.RoleRights).HasForeignKey(p => p.ScreenId);
			this.HasOptional(p => p.Button).WithMany(p => p.RoleRights).HasForeignKey(p => p.ButtonId);
			// Class property validation.
		}
	}
}