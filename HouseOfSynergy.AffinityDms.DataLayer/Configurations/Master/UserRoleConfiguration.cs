using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Master;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Master
{
	public partial class MasterUserRoleConfiguration:
		EntityTypeConfiguration<MasterUserRole>
	{
		public MasterUserRoleConfiguration ()
		{
			// Table name in database.
			this.ToTable("UserRole");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Role).WithMany(p => p.UserRoles).HasForeignKey(p => p.RoleId);
			this.HasRequired(p => p.User).WithMany(p => p.UserRoles).HasForeignKey(p => p.UserId);

			this.Property(p => p.RoleId)
				.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_RoleId_UserId") { Order = 1, IsUnique = true, IsClustered = false, }))
				.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_RoleId_UserId") { Order = 2, IsUnique = true, IsClustered = false, }));
		}
	}
}