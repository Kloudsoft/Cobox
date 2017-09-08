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
	public partial class DiscoursePostVersionConfiguration:
		EntityTypeConfiguration<DiscoursePostVersion>
	{
		public DiscoursePostVersionConfiguration ()
		{
			// Table name in database.
			this.ToTable("DiscoursePostVersion");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Post).WithMany(p => p.Versions).HasForeignKey(p => p.PostId);
            this.HasRequired(p => p.Discourse).WithMany(p => p.PostVersion).HasForeignKey(p => p.DiscourseId);
            this.HasMany(p => p.Attachments).WithRequired(p => p.PostVersion).HasForeignKey(p => p.PostVersionId);

			// Class property validation.
			this.Property(p => p.Comments).IsRequired().HasMaxLength(50);
		}
	}
}