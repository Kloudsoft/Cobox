using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Desktop;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Desktop
{
	public partial class DocumentEntryConfiguration:
		EntityTypeConfiguration<DocumentEntry>
	{
		public DocumentEntryConfiguration ()
		{
			// Table name in database.
			this.ToTable("DocumentEntry");

			// Primary key.
			this.HasKey<long>(p => p.Id);
			// Auto-increment will be handled by the database.
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasOptional(p => p.ScanSession).WithMany(p => p.DocumentEntries).HasForeignKey(p => p.ScanSessionId);

			this.Property(p => p.Hash).IsRequired().HasMaxLength(EntityConstants.LengthFullTextSearchMaximum);
			this.Property(p => p.Filename).IsRequired().HasMaxLength(EntityConstants.LengthFilename);
			this.Property(p => p.AttemptHistory).IsOptional().IsMaxLength();

			this.Ignore(p => p.Name);
			this.Ignore(p => p.PathLocal);
			this.Ignore(p => p.FileInfo);
			this.Ignore(p => p.FileType);
			this.Ignore(p => p.FileFormatType);
			this.Ignore(p => p.DocumentEntryStateText);
		}
	}
}
