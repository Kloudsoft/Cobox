using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Master;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Master
{
	public partial class MasterUserConfiguration:
		EntityTypeConfiguration<MasterUser>
	{
		public MasterUserConfiguration ()
		{
			// Table name in database.
			this.ToTable("User");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn key association.
			this.HasMany(p => p.Roles).WithMany(p => p.Users);
			this.HasMany(p => p.Sessions).WithRequired(p => p.User).HasForeignKey(p => p.UserId);
			this.HasMany(p => p.UserRoles).WithRequired(p => p.User).HasForeignKey(p => p.UserId);

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

			this.Property(p => p.ActiveDirectoryId).IsOptional().HasMaxLength(EntityConstants.LengthFullTextSearchMaximum);
		}
	}
}