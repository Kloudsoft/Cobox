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
	public partial class TenantSubscriptionConfiguration:
		EntityTypeConfiguration<TenantSubscription>
	{
		public TenantSubscriptionConfiguration ()
		{
			// Table name in database.
			this.ToTable("TenantSubscription");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Tenant).WithMany(p => p.TenantSubscriptions).HasForeignKey(p => p.TenantId);
			this.HasRequired(p => p.Subscription).WithMany(p => p.TenantSubscriptions).HasForeignKey(p => p.SubscriptionId);

			// Class property validation.
			this.Property(p => p.DateTimeExpires).IsRequired();
			this.Property(p => p.DateTimeStart).IsRequired();
			this.Property(p => p.NumberOfFormsAllowed).IsRequired();
			this.Property(p => p.NumberOfFormsUsed).IsRequired();
			this.Property(p => p.NumberOfPagesAllowed).IsRequired();
			this.Property(p => p.NumberOfPagesUsed).IsRequired();
			this.Property(p => p.NumberOfTemplatesAllowed).IsRequired();
			this.Property(p => p.NumberOfTemplatesUsed).IsRequired();
			this.Property(p => p.NumberOfUsersAllowed).IsRequired();
			this.Property(p => p.NumberOfUsersUsed).IsRequired();

			this.Ignore(p => p.NumberOfPagesRemaining);
			this.Ignore(p => p.NumberOfFormsRemaining);
			this.Ignore(p => p.NumberOfTemplatesRemaining);
		}
	}
}