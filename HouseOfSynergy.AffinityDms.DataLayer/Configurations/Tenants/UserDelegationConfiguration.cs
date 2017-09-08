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
	public partial class UserDelegationConfiguration:
		EntityTypeConfiguration<UserDelegation>
	{
		public UserDelegationConfiguration ()
		{
			// Table name in database.
			this.ToTable("UserDelegation");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasOptional(p => p.FromUser).WithMany(p => p.FormUserDelegations).HasForeignKey(p => p.FormUserId);
			this.HasOptional(p => p.ToUser).WithMany(p => p.ToUserDelegations).HasForeignKey(p => p.ToUserId);
			this.HasMany(p => p.RoleDelegations).WithRequired(p => p.UserDelegations).HasForeignKey(p => p.UserDelegationId);
			// Class property validation.
			this.Property(p => p.StartDate).IsRequired();
			this.Property(p => p.EndDate).IsRequired();
			this.Property(p => p.ActiveTag).IsRequired();
		}
	}
}