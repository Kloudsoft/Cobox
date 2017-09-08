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
	public partial class ButtonConfiguration:
		EntityTypeConfiguration<Button>
	{
		public ButtonConfiguration ()
		{
			// Table name in database.
			this.ToTable("Button");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Screen).WithMany(p => p.Buttons).HasForeignKey(p => p.ScreenId);
			this.HasMany(p => p.RoleRights).WithOptional(p => p.Button).HasForeignKey(p => p.ButtonId);

			// Class property validation.
			this.Property(p => p.Name).IsRequired().HasMaxLength(50);
		}
	}
}