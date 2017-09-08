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
	public partial class RoleConfiguration:
		EntityTypeConfiguration<Role>
	{
		public RoleConfiguration ()
		{
			// Table name in database.
			this.ToTable("Role");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn key association.
			this.HasMany(p => p.Users).WithMany(p => p.Roles);
			this.HasMany(p => p.UserRoles).WithRequired(p => p.Role).HasForeignKey(p => p.RoleId);
			this.HasMany(p => p.RoleRights).WithRequired(p => p.Roles).HasForeignKey(p => p.RoleId);

			this.Property(p => p.Name).IsRequired().HasMaxLength(500);
			this.Property(p => p.Description).IsRequired().IsMaxLength();
		}
	}
}
