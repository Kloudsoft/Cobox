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
	public partial class TemplateElementValueConfiguration:
		EntityTypeConfiguration<TemplateElementValue>
	{
		public TemplateElementValueConfiguration ()
		{
			// Table name in database.
			this.ToTable("ElementValue");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Element).WithMany(p => p.ElementValues).HasForeignKey(p => p.ElementId);
			this.HasOptional(p => p.Document).WithMany(p => p.ElementValues).HasForeignKey(p => p.DocumentId);
			// Class property validation.
			this.Property(p => p.Value).HasMaxLength(10);
		}
	}
}