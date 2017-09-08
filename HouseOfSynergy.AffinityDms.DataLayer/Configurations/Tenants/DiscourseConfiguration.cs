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
	public partial class DiscourseConfiguration:
		EntityTypeConfiguration<Discourse>
	{
		public DiscourseConfiguration ()
		{
			// Table name in database.
			this.ToTable("Discourse");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.

			this.HasMany(p => p.Posts).WithRequired(p => p.Discourse).HasForeignKey(p => p.DiscourseId);
			this.HasMany(p => p.Users).WithRequired(p => p.Discourse).HasForeignKey(p => p.DiscourseId);
            this.HasMany(p => p.PostVersion).WithRequired(p => p.Discourse).HasForeignKey(p => p.DiscourseId);
            this.HasMany(p => p.PostVersionAttachments).WithRequired(p => p.Discourse).HasForeignKey(p => p.DiscourseId);


            // Class property validation.

            this.Property(p => p.Topic).IsRequired().HasMaxLength(50);
			this.Property(p => p.Description).IsRequired().HasMaxLength(500);
		}
	}
}