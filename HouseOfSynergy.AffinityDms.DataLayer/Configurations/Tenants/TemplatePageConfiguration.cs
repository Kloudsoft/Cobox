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
	public partial class TemplatePageConfiguration:
		EntityTypeConfiguration<TemplatePage>
	{
		public TemplatePageConfiguration ()
		{
			// Table name in database.
			this.ToTable("TemplatePage");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			//this.HasRequired(p => p.TemplateVersion).WithMany(p => p.TemplatePages).HasForeignKey(p => p.TemplateVersionId);
			// Class property validation.
			this.Property(p => p.Name).HasMaxLength(500);
		}
	}
}