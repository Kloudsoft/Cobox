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
	public partial class DiscoursePostAttachmentConfiguration:
		EntityTypeConfiguration<DiscoursePostVersionAttachment>
	{
		public DiscoursePostAttachmentConfiguration ()
		{
			// Table name in database.
			this.ToTable("DiscoursePostDocument");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.PostVersion).WithMany(p => p.Attachments).HasForeignKey(p => p.PostVersionId);
            this.HasRequired(p => p.Discourse).WithMany(p => p.PostVersionAttachments).HasForeignKey(p => p.DiscourseId);

        }
    }
}