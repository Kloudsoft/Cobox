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
	public partial class UserConfiguration:
		EntityTypeConfiguration<User>
	{
		public UserConfiguration ()
		{
			// Table name in database.
			this.ToTable("User");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //ASK RAHEEL BHAI
			// Foriegn key association.
			this.HasRequired(p => p.Tenant).WithMany(p => p.Users).HasForeignKey(p => p.TenantId);
			this.HasOptional(p => p.Department).WithMany(p => p.Users).HasForeignKey(p => p.DepartmentId);

            // Foriegn key association.
            this.HasMany(p => p.DocumentCorrectiveIndexValues).WithRequired(p => p.Indexer).HasForeignKey(p => p.IndexerId);
            this.HasMany(p => p.CheckedOutDocuments).WithRequired (p => p.CheckedOutByUser).HasForeignKey (p => p.CheckedOutByUserId).WillCascadeOnDelete (false);
            this.HasMany(p => p.CheckedOutTemplates).WithRequired(p => p.CheckedOutByUser).HasForeignKey(p => p.CheckedOutByUserId).WillCascadeOnDelete(false);

            this.HasMany(p => p.Documents).WithOptional(p => p.AssignedToUser).HasForeignKey(p => p.AssignedToUserId);
            this.HasMany(p => p.Documents).WithOptional(p => p.AssignedByUser).HasForeignKey(p => p.AssignedByUserId);

            this.HasMany(p => p.Templates).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
            this.HasMany(p => p.Roles).WithMany(p => p.Users);
			this.HasMany(p => p.Sessions).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.DiscoursePosts).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.UserLabels).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.FormUserDelegations).WithOptional(p => p.FromUser).HasForeignKey(p => p.FormUserId);
			this.HasMany(p => p.ToUserDelegations).WithOptional(p => p.ToUser).HasForeignKey(p => p.ToUserId);
			this.HasMany(p => p.RoleDelegations).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.UserRoles).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.RuleDetails).WithRequired(p => p.User).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
			this.HasMany(p => p.RuleDetailInstances).WithRequired(p => p.User).HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
			this.HasMany(p => p.ScanSessions).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			//this.HasMany(p => p.ScannerSessions).WithOptional(p => p.User).HasForeignKey(p => p.UserId);
			//this.HasMany(p => p.ScanningJobs).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.WorkFlowActorsInstances).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.DocumentSearchCriterias).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.Documents).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.UserFolders).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
            this.HasMany(p => p.Folders).WithRequired(p => p.UserCreatedBy).HasForeignKey(p => p.UserCreatedById).WillCascadeOnDelete(false);
            this.HasMany(p => p.UserDocuments).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
            this.HasMany(p => p.UserTemplates).WithRequired(p => p.User).HasForeignKey(p => p.UserId);

            this.Property(p => p.UserName).IsRequired().HasMaxLength(EntityConstants.LengthUserNameMaximum);
			this.Property(p => p.Email).IsRequired().HasMaxLength(EntityConstants.LengthEmail);
			this.Property(p => p.PasswordHash).IsRequired().HasMaxLength(EntityConstants.LengthPasswordHashMaximum);
			this.Property(p => p.PasswordSalt).IsRequired().HasMaxLength(EntityConstants.LengthPasswordSaltMaximum);
			this.Property(p => p.NameGiven).IsRequired().HasMaxLength(EntityConstants.LengthNameGiven);
			this.Property(p => p.NameFamily).IsRequired().HasMaxLength(EntityConstants.LengthNameFamily);
			this.Property(p => p.Address1).IsRequired().HasMaxLength(EntityConstants.LengthAddress);
			this.Property(p => p.Address2).IsRequired().HasMaxLength(EntityConstants.LengthAddress);
			this.Property(p => p.City).IsRequired().HasMaxLength(EntityConstants.LengthAddress);
			this.Property(p => p.ZipOrPostCode).IsRequired().HasMaxLength(EntityConstants.LengthAddress);
			this.Property(p => p.Country).IsRequired().HasMaxLength(EntityConstants.LengthAddress);
			this.Property(p => p.PhoneWork).IsRequired().HasMaxLength(EntityConstants.LengthPhoneNumber);
			this.Property(p => p.PhoneMobile).IsRequired().HasMaxLength(EntityConstants.LengthPhoneNumber);
		}
	}
}