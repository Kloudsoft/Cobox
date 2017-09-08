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
	public partial class DocumentTemplateConfiguration:
		EntityTypeConfiguration<DocumentTemplate>
	{
		public DocumentTemplateConfiguration ()
		{
			// Table name in database.
			this.ToTable("DocumentTemplate");

			// Primary key.
			this.HasKey<long>(p => p.Id);
			// Auto-increment will be handled by the database.
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired(p => p.Document).WithMany(p => p.DocumentTemplates).HasForeignKey(p => p.DocumentId);
			this.HasRequired(p => p.Template).WithMany(p => p.DocumentTemplates).HasForeignKey(p => p.TemplateId);
		}
	}
}
