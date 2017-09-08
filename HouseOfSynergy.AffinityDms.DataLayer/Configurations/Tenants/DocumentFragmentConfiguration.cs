using HouseOfSynergy.AffinityDms.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public partial class DocumentFragmentConfiguration:
		EntityTypeConfiguration<DocumentFragment>
	{
		public DocumentFragmentConfiguration ()
		{
			// Table name in database.
			this.ToTable("DocumentFragment");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired(p => p.Document).WithMany(p => p.DocumentFragments).HasForeignKey(p => p.DocumentId);

			this.Property(p => p.FullTextOcr).IsRequired().HasMaxLength(EntityConstants.LengthFullTextSearchMaximum);
		}
	}
}
