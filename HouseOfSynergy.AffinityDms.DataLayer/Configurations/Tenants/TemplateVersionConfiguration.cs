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
	public partial class TemplateVersionConfiguration:
		EntityTypeConfiguration<TemplateVersion>
	{
		public TemplateVersionConfiguration ()
		{
			// Table name in database.
			this.ToTable("TemplateVersion");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(p => p.TemplateOriginal).WithMany(p => p.TemplateVersions).HasForeignKey(p => p.TemplateOriginalId).WillCascadeOnDelete(false);
            this.HasRequired(p => p.TemplateCurrent);
            this.HasOptional(p => p.TemplateParent);
		}
	}
}