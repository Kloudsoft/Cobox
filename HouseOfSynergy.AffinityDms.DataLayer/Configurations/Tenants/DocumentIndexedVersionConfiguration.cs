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
//	public partial class DocumentIndexedVersionConfiguration:
//		EntityTypeConfiguration<DocumentIndexedVersion>
//	{
//		public DocumentIndexedVersionConfiguration ()
//		{
//			// Table name in database.
//			this.ToTable("DocumentIndexedVersion");

//			// Primary Key.
//			this.HasKey<long>(p => p.Id);
//			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

//			// Foriegn Keys.
//			this.HasRequired(p => p.Document).WithMany(p => p.DocumentIndexedVersions).HasForeignKey(p => p.DocumentId);
//			this.HasMany(p => p.ElementValues).WithRequired(p => p.DocumentIndexedVersion).HasForeignKey(p => p.DocumentIndexedVersionId);
//			// Class property validation.
//			this.Property(p => p.Version).IsRequired().HasMaxLength(10);
//			this.Property(p => p.DateTime).IsRequired();
//		}
//	}
//}