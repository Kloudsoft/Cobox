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
	public partial class DocumentSearchCriteriaConfiguration:
		EntityTypeConfiguration<DocumentSearchCriteria>
	{
		public DocumentSearchCriteriaConfiguration ()
		{
			// Table name in database.
			this.ToTable("DocumentSearchCriteria");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired(p => p.User).WithMany(p => p.DocumentSearchCriterias).HasForeignKey(p => p.UserId);

			this.Property(p => p.Name).IsRequired().HasMaxLength(256);

			this.Property(p => p.DateTimeFrom).IsOptional();
			this.Property(p => p.DateTimeUpTo).IsOptional();
			this.Property(p => p.TagsUser).IsOptional().HasMaxLength(256);
			this.Property(p => p.TagsGlobal).IsOptional().HasMaxLength(256);
			this.Property(p => p.Content).IsOptional().HasMaxLength(256);
			this.Property(p => p.Filename).IsOptional().HasMaxLength(256);
			this.Property(p => p.FolderName).IsOptional().HasMaxLength(256);
			this.Property(p => p.TemplateName).IsOptional().HasMaxLength(256);
		}
	}
}
