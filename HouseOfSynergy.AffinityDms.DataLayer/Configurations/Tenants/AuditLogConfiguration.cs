using HouseOfSynergy.AffinityDms.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public partial class AuditLogConfiguration:
		EntityTypeConfiguration<AuditLog>
	{
		public AuditLogConfiguration ()
		{
			// Table name in database.
			this.ToTable("AuditlogNews");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Screen);
		}
	}
}
