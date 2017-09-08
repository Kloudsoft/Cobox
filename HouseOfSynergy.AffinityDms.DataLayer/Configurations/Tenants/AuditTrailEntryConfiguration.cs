using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public partial class AuditTrailEntryConfiguration :
		EntityTypeConfiguration<AuditTrailEntry>
	{
		public AuditTrailEntryConfiguration ()
		{
			this.ToTable("AuditTrailEntry");

			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired(p => p.User);

			this.HasOptional(p => p.EntityUser);

			this.Property(p => p.Description).IsRequired().HasMaxLength(999);
		}
	}
}
