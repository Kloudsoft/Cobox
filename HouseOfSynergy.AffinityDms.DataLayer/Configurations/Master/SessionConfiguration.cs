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
	public partial class MasterSessionConfiguration:
		EntityTypeConfiguration<MasterSession>
	{
		public MasterSessionConfiguration ()
		{
			// Table name in database.
			this.ToTable("Session");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn key association.
			this.HasRequired(p => p.User).WithMany(p => p.Sessions).HasForeignKey(p => p.UserId);

			this.Property(p => p.Guid).IsRequired();
			this.Property(p => p.SessionId).IsRequired().HasMaxLength(EntityConstants.LengthFullTextSearchMaximum);
			this.Property(p => p.CultureName).IsRequired().HasMaxLength(EntityConstants.LengthCultureName);
			this.Property(p => p.DeviceType).IsRequired();
			this.Property(p => p.DateTimeCreated).IsRequired();
			this.Property(p => p.DateTimeExpiration).IsRequired();
			this.Property(p => p.Token).IsRequired().HasMaxLength(EntityConstants.LengthUserTokenMaximum);
			this.Property(p => p.UserAgent).IsRequired().HasMaxLength(EntityConstants.LengthFullTextSearchMaximum);
			this.Property(p => p.IPAddressString).IsRequired().HasMaxLength(EntityConstants.LengthFullTextSearchMaximum);
			this.Property(p => p.RijndaelKey).IsRequired().IsMaxLength();
			this.Property(p => p.RsaKeyPublic).IsRequired().IsMaxLength();
			this.Property(p => p.RsaKeyPrivate).IsRequired().IsMaxLength();
		}
	}
}