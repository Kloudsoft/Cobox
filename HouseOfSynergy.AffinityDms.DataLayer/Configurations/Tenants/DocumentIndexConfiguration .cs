using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public partial class DocumentIndexConfiguration:
		EntityTypeConfiguration<DocumentIndex>
	{
		public DocumentIndexConfiguration ()
		{
			// Table name in database.
			this.ToTable("DocumentIndex");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Document).WithMany(p => p.DocumentIndexs).HasForeignKey(p => p.DocumentId).WillCascadeOnDelete(false);
            
			this.Property(p => p.Name).IsRequired().HasMaxLength(200);
			this.Property(p => p.Value).IsRequired().HasMaxLength(999);
		}
	}
}
