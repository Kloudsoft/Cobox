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
//	public partial class ScanningJobConfiguration:
//		EntityTypeConfiguration<ScanningJob>
//	{
//		public ScanningJobConfiguration ()
//		{
//			// Table name in database.
//			this.ToTable("ScanningJob");

//			// Primary Key.
//			this.HasKey<long>(p => p.Id);
//			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

//			// Foriegn Keys.
//			this.HasMany(p => p.ScannerSessions).WithRequired(p => p.ScanningJob).HasForeignKey(p => p.ScanningJobId);
//			this.HasRequired(p => p.User).WithMany(p => p.ScanningJobs).HasForeignKey(p => p.UserId);

//			// Class property validation.
//			this.Property(p => p.Description).HasMaxLength(10);

//		}
//	}
//}