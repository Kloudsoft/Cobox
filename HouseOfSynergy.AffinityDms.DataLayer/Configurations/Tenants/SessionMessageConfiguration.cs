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
	public partial class SessionMessageConfiguration:
		EntityTypeConfiguration<SessionMessage>
	{
		public SessionMessageConfiguration ()
		{
			// Table name in database.
			this.ToTable("SessionMessage");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired(p => p.Session).WithMany(p => p.SessionMessages).HasForeignKey(p => p.SessionId);
		}
	}
}
