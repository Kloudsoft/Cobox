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
	public partial class UserInviteConfiguration:
		EntityTypeConfiguration<UserInvite>
	{
		public UserInviteConfiguration ()
		{
			// Table name in database.
			this.ToTable("UserInvite");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn key association.
			this.HasRequired(p => p.User).WithMany(p => p.UserInvites).HasForeignKey(p => p.InviterUserId);


		}
	}
}