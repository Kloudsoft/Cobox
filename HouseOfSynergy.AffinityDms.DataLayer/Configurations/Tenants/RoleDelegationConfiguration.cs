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
	public partial class RoleDelegationConfiguration:
		EntityTypeConfiguration<RoleDelegation>
	{
		public RoleDelegationConfiguration ()
		{
			// Table name in database.
			this.ToTable("RoleDelegation");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.User).WithMany(p => p.RoleDelegations).HasForeignKey(p => p.UserId);
			this.HasRequired(p => p.UserDelegations).WithMany(p => p.RoleDelegations).HasForeignKey(p => p.UserDelegationId);
			// Class property validation.
		}
	}
}