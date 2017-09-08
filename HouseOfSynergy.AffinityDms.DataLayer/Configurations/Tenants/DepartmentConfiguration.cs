using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public class DepartmentConfiguration:
		EntityTypeConfiguration<Department>
	{
		public DepartmentConfiguration ()
		{
			// Table name in database.
			this.ToTable("Department");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //ASK RAHEEL BHAI
            this.HasMany(p => p.Users).WithOptional(p => p.Department).HasForeignKey(p => p.DepartmentId);
			this.HasMany(p => p.Folders).WithOptional(p => p.Department).HasForeignKey(p => p.DepartmentId);

			this.Property(p => p.Name).IsRequired().HasMaxLength(999);
			this.Property(p => p.Name).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Name") { Order = 1, IsUnique = true, IsClustered = false, }));
		}
	}
}
