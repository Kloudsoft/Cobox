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
	public partial class FolderConfiguration:
		EntityTypeConfiguration<Folder>
	{
		public FolderConfiguration ()
		{
			// Table name in database.
			this.ToTable("Folder");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasOptional(p => p.Parent).WithMany(p => p.Folders).HasForeignKey(p => p.ParentId);
			this.HasOptional(p => p.Department).WithMany(p => p.Folders).HasForeignKey(p => p.DepartmentId);
            this.HasRequired(p => p.UserCreatedBy).WithMany(p => p.Folders).HasForeignKey(p => p.UserCreatedById).WillCascadeOnDelete(false);

			this.HasMany(p => p.Folders).WithOptional(p => p.Parent).HasForeignKey(p => p.ParentId);
			this.HasMany(p => p.Documents).WithRequired(p => p.Folder).HasForeignKey(p => p.FolderId);
			this.HasMany(p => p.FolderUsers).WithRequired(p => p.Folder).HasForeignKey(p => p.FolderId);

            this.Property(p => p.Name).IsRequired().HasMaxLength(EntityConstants.LengthFolderName);
			this.Property(p => p.DateTimeCreated).IsRequired();
            this.Property(p => p.DateTimeModified).IsOptional();
			this.Ignore (p => p.HasChildren);
		}
	}
}