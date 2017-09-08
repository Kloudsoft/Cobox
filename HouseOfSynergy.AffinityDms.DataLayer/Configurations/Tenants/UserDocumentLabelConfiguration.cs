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
	public partial class UserDocumentLabelConfiguration:
		EntityTypeConfiguration<UserDocumentLabel>
	{
		public UserDocumentLabelConfiguration ()
		{
			// Table name in database.
			this.ToTable("UserDocumentLabel");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Document).WithMany(p => p.UserDocumentLabels).HasForeignKey(p => p.DocumentId);
			// Class property validation.
		}
	}
}