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
	public partial class TemplateInstanceConfiguration:
		EntityTypeConfiguration<TemplateInstance>
	{
		public TemplateInstanceConfiguration ()
		{
			// Table name in database.
			this.ToTable("TemplateInstance");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			//this.HasRequired(p => p.TemplateVersion).WithMany(p => p.TemplateInstances).HasForeignKey(p => p.TemplateVersionId);
			// Class property validation.
			this.Property(p => p.Number).HasMaxLength(10);
		}
	}
}