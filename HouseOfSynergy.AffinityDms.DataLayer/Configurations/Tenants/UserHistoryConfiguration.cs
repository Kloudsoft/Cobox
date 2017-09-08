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
	public partial class UserHistoryConfiguration:
		EntityTypeConfiguration<UserHistory>
	{
		public UserHistoryConfiguration ()
		{
			// Table name in database.
			this.ToTable("UserHistory");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.

			// Class property validation.
			this.Property(p => p.FieldName).HasMaxLength(200);
			this.Property(p => p.PreviousValue).HasMaxLength(200);
			this.Property(p => p.NewValue).HasMaxLength(200);
			this.Property(p => p.ModifiedBy).HasMaxLength(200);
		}
	}
}