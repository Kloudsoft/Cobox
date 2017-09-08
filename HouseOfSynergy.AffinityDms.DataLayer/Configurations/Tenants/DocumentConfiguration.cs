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
	public partial class DocumentConfiguration:
		EntityTypeConfiguration<Document>
	{
		public DocumentConfiguration ()
		{
			// Table name in database.
			this.ToTable("Document");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasOptional(p => p.Template).WithMany(p => p.Documents).HasForeignKey(p => p.TemplateId);
			this.HasRequired(p => p.User).WithMany(p => p.Documents).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
			this.HasRequired(p => p.Folder).WithMany(p => p.Documents).HasForeignKey(p => p.FolderId);

			this.HasRequired (p => p.CheckedOutByUser).WithMany (p => p.CheckedOutDocuments).HasForeignKey (p => p.CheckedOutByUserId).WillCascadeOnDelete (false);
            this.HasOptional(p => p.AssignedToUser).WithMany(p => p.DocumentsAssignedTo).HasForeignKey(p => p.AssignedToUserId);
			this.HasOptional(p => p.AssignedByUser).WithMany(p => p.DocumentsAssignedBy).HasForeignKey(p => p.AssignedByUserId);
			this.HasOptional(p => p.ScanSession).WithMany(p => p.Documents).HasForeignKey(p => p.ScanSessionId);

            this.HasMany(p => p.DocumentCorrectiveIndexValues).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId);
            this.HasMany(p => p.DocumentUsers).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId);
            this.HasMany(p => p.DocumentFragments).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId);
			this.HasMany(p => p.DiscussionPostDocuments).WithOptional(p => p.Document).HasForeignKey(p => p.DocumentId);
			this.HasMany(p => p.DocumentElements).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId);
			this.HasMany(p => p.DocumentXmlElements).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId);
			this.HasMany(p => p.DocumentTags).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId);
			this.HasMany(p => p.DocumentTagUsers).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId);
			this.HasMany(p => p.DocumentTemplates).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId);
			this.HasMany(p => p.ElementValues).WithOptional(p => p.Document).HasForeignKey(p => p.DocumentId);
			this.HasMany(p => p.UserDocumentLabels).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId);
			this.HasMany(p => p.WorkFlowInstances).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId).WillCascadeOnDelete(false);
			this.HasMany(p => p.EntityWorkflowMappings).WithRequired(p => p.Document).HasForeignKey(p => p.DocumentId).WillCascadeOnDelete(false);

			this.Property(p => p.Hash).IsRequired().HasMaxLength(999);
			this.Property(p => p.DeviceName).HasMaxLength(EntityConstants.LengthFilename);
			this.Property(p => p.FullTextOCRXML).IsOptional().IsMaxLength();
			this.Property(p => p.Name).IsRequired().HasMaxLength(EntityConstants.LengthFilename);
			this.Property(p => p.FileNameClient).IsRequired().HasMaxLength(EntityConstants.LengthFilename);
			this.Property(p => p.FileNameServer).IsRequired().HasMaxLength(EntityConstants.LengthFilename);
		}
	}
}
