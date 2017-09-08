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
	public partial class DiscoursePostConfiguration:
		EntityTypeConfiguration<DiscoursePost>
	{
		public DiscoursePostConfiguration ()
		{
			// Table name in database.
			this.ToTable("DiscoursePost");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Discourse).WithMany(p => p.Posts).HasForeignKey(p => p.DiscourseId);
			this.HasMany(p => p.Versions).WithRequired(p => p.Post).HasForeignKey(p => p.PostId);
			this.HasRequired(p => p.User).WithMany(p => p.DiscoursePosts).HasForeignKey(p => p.UserId);
		}
	}
}