using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public partial class UserRoleConfiguration:
		EntityTypeConfiguration<UserRole>
	{
		public UserRoleConfiguration ()
		{
			// Table name in database.
			this.ToTable("UserRole");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.Role).WithMany(p => p.UserRoles).HasForeignKey(p => p.RoleId);
			this.HasRequired(p => p.User).WithMany(p => p.UserRoles).HasForeignKey(p => p.UserId);

            //Comment out because foreign key exception is occurring, Its need to be rectify later
            //this.Property(p => p.RoleId)
            //.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_RoleId_UserId") { Order = 1, IsUnique = true, IsClustered = false, }))
            //.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_RoleId_UserId") { Order = 2, IsUnique = true, IsClustered = false, }));
        }
    }
}