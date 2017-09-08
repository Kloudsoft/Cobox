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
	public partial class DocumentElementConfiguration:
		EntityTypeConfiguration<DocumentElement>
	{
		public DocumentElementConfiguration ()
		{
			// Table name in database.
			this.ToTable("DocumentElement");

			// Primary key.
			this.HasKey<long>(p => p.Id);
			// Auto-increment will be handled by the database.
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired(p => p.Document).WithMany(p => p.DocumentElements).HasForeignKey(p => p.DocumentId);
			this.HasRequired(p => p.TemplateElement).WithMany(p => p.DocumentElements).HasForeignKey(p => p.TemplateElementId);
			this.HasOptional(p => p.TemplateElementDetail).WithMany(p => p.DocumentElements).HasForeignKey(p => p.TemplateElementDetailId);

			this.Property(p => p.OcrXml).IsOptional().IsMaxLength();
            this.Property(p => p.OcrText).IsOptional().IsMaxLength();
		}
	}
}
