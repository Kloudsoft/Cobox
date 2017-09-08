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
	public partial class DiscourseUserConfiguration:
		EntityTypeConfiguration<DiscourseUser>
	{
		public DiscourseUserConfiguration ()
		{
			// Table name in database.
			this.ToTable("DiscourseUser");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Discourse).WithMany(p => p.Users).HasForeignKey(p => p.DiscourseId);
			//this.HasRequired(p => p.User).WithMany(p => p.DiscussionUsers).HasForeignKey(p => p.UserId);


			//Need to Check
			// Class property validation.
		}
	}
}