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
	public partial class UserLabelConfiguration:
		EntityTypeConfiguration<UserLabel>
	{
		public UserLabelConfiguration ()
		{
			// Table name in database.
			this.ToTable("UserLabel");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.User).WithMany(p => p.UserLabels).HasForeignKey(p => p.UserId);
			this.Property(p => p.LabelName).HasMaxLength(50);
			// Class property validation.
		}
	}
}