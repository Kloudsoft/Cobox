using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Common
{
	public partial class SubscriptionConfiguration:
	EntityTypeConfiguration<Subscription>
	{
		public SubscriptionConfiguration ()
		{
			// Table name in database.
			this.ToTable("Subscription");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasMany(p => p.TenantSubscriptions).WithRequired(p => p.Subscription).HasForeignKey(p => p.SubscriptionId);

			this.Property(p => p.Description).IsRequired().IsMaxLength();
		}
	}
}