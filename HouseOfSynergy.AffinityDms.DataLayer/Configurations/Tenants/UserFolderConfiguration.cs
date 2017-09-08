using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public class UserFolderConfiguration:
		EntityTypeConfiguration<UserFolder>
	{
		public UserFolderConfiguration ()
		{
			// Table name in database.
			this.ToTable("UserFolder");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired(p => p.User).WithMany(p => p.UserFolders).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
			this.HasRequired(p => p.Folder).WithMany(p => p.FolderUsers).HasForeignKey(p => p.FolderId).WillCascadeOnDelete(false);
		}
	}
}
