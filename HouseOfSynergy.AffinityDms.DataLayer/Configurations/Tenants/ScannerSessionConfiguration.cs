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
//	public partial class ScannerSessionConfiguration:
//		EntityTypeConfiguration<ScannerSession>
//	{
//		public ScannerSessionConfiguration ()
//		{
//			// Table name in database.
//			this.ToTable("ScannerSession");

//			// Primary Key.
//			this.HasKey<long>(p => p.Id);
//			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

//			// Foriegn Keys.
//			this.HasMany(p => p.ScannerSessionDetails).WithRequired(p => p.ScannerSessions).HasForeignKey(p => p.ScannerSessionId);
//			this.HasRequired(p => p.ScanningJob).WithMany(p => p.ScannerSessions).HasForeignKey(p => p.ScanningJobId);
//			this.HasOptional(p => p.User).WithMany(p => p.ScannerSessions).HasForeignKey(p => p.UserId);

//			// Class property validation.
//			this.Property(p => p.HardwareInfo).HasMaxLength(10);
//			this.Property(p => p.FeederType).HasMaxLength(10);
//			this.Property(p => p.FilesPath).HasMaxLength(10);

//		}
//	}
//}