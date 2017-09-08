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
	public partial class TemplateConfiguration:
		EntityTypeConfiguration<Template>
	{
		public TemplateConfiguration ()
		{
			// Table name in database.
			this.ToTable("Template");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasMany(p => p.Documents).WithOptional(p => p.Template).HasForeignKey(p => p.TemplateId);
			this.HasMany(p => p.DiscussionPostDocuments).WithOptional(p => p.Template).HasForeignKey(p => p.TemplateId);
			this.HasMany(p => p.TemplateVersions).WithRequired(p => p.TemplateOriginal).HasForeignKey(p => p.TemplateOriginalId).WillCascadeOnDelete(false);
            this.HasMany(p => p.RuleDetails).WithRequired(p => p.Template).HasForeignKey(p => p.TemplateId).WillCascadeOnDelete(false);
			this.HasMany(p => p.RuleDetailInstances).WithRequired(p => p.Template).HasForeignKey(p => p.TemplateId).WillCascadeOnDelete(false);
			this.HasMany(p => p.Elements).WithRequired(p => p.Template).HasForeignKey(p => p.TemplateId);
			this.HasMany(p => p.TemplateTags).WithRequired(p => p.Template).HasForeignKey(p => p.TemplateId).WillCascadeOnDelete(false);
			this.HasMany(p => p.TemplateTagUsers).WithRequired(p => p.Template).HasForeignKey(p => p.TemplateId).WillCascadeOnDelete(false);
			this.HasMany(p => p.DocumentTemplates).WithRequired(p => p.Template).HasForeignKey(p => p.TemplateId);
            this.HasMany(p => p.TemplateUsers).WithRequired(p => p.Template).HasForeignKey(p => p.TemplateId);

            this.HasRequired(p => p.CheckedOutByUser).WithMany(p => p.CheckedOutTemplates).HasForeignKey(p => p.CheckedOutByUserId).WillCascadeOnDelete(false);
            this.HasRequired(p => p.User).WithMany(p => p.Templates).HasForeignKey(p => p.UserId);
            
            this.Property(p => p.Title).IsRequired().HasMaxLength(200);
			this.Property(p => p.Description).IsRequired().HasMaxLength(500);
			this.Property(p => p.VersionCount).IsOptional();
		}
	}
}