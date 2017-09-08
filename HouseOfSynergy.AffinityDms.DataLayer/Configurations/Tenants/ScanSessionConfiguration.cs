using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public partial class ScanSessionConfiguration:
		EntityTypeConfiguration<ScanSession>
	{
		public ScanSessionConfiguration ()
		{
			// Table name in database.
			this.ToTable("ScanSession");

			// Primary key.
			this.HasKey<long>(p => p.Id);
			// Auto-increment will be handled by the database.
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.Property(p => p.Guid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired(p => p.User).WithMany(p => p.ScanSessions).HasForeignKey(p => p.UserId);
			//this.HasRequired(p => p.User).WithMany(p => p.ScannerSessions).HasForeignKey(p => p.UserId);

			this.HasMany(p => p.Documents).WithOptional(p => p.ScanSession).HasForeignKey(p => p.ScanSessionId);

			this.Property(p => p.Name).IsRequired().HasMaxLength(EntityConstants.LengthNameGeneral);
			this.Property(p => p.Description).IsRequired().HasMaxLength(EntityConstants.LengthNameGeneral);
		}
	}
}