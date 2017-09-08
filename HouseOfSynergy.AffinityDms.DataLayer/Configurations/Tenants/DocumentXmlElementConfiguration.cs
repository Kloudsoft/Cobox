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
	public partial class DocumentXmlElementConfiguration:
		EntityTypeConfiguration<DocumentXmlElement>
	{
		public DocumentXmlElementConfiguration ()
		{
			// Table name in database.
			this.ToTable("DocumentXmlElement");

			// Primary key.
			this.HasKey<long>(p => p.Id);
			// Auto-increment will be handled by the database.
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired(p => p.Document).WithMany(p => p.DocumentXmlElements).HasForeignKey(p => p.DocumentId);

			this.Property(p => p.OcrXml).IsRequired().IsMaxLength();
			this.Property(p => p.OcrText).IsRequired().IsMaxLength();
		}
	}
}