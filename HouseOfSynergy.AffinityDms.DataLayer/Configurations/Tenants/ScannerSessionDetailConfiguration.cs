//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HouseOfSynergy.AffinityDms.Entities;
//using HouseOfSynergy.AffinityDms.Entities.Tenants;

//namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
//{
//	public partial class ScannerSessionDetailConfiguration:
//		EntityTypeConfiguration<ScannerSessionDetail>
//	{
//		public ScannerSessionDetailConfiguration ()
//		{
//			// Table name in database.
//			this.ToTable("ScannerSessionDetail");

//			// Primary Key.
//			this.HasKey<long>(p => p.Id);
//			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

//			// Foriegn Keys.
//			this.HasRequired(p => p.ScannerSessions).WithMany(p => p.ScannerSessionDetails).HasForeignKey(p => p.ScannerSessionId);
//			// Class property validation.
//			this.Property(p => p.FileName).HasMaxLength(10);
//			this.Property(p => p.FileGuidName).HasMaxLength(10);
//			this.Property(p => p.Ordinal).HasMaxLength(10);

//		}
//	}
//}