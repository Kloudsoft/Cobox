using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Common
{
	public partial class CultureConfiguration:
		EntityTypeConfiguration<Culture>
	{
		public CultureConfiguration ()
		{
			// Table name in database.
			this.ToTable("Culture");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.Property(p => p.Name).IsRequired().HasMaxLength(EntityConstants.LengthFullTextSearchMaximum);
			this.Property(p => p.NameNative).IsRequired().IsMaxLength();
			this.Property(p => p.NameDisplay).IsRequired().IsMaxLength();
			this.Property(p => p.NameEnglish).IsRequired().IsMaxLength();
			this.Property(p => p.NameIsoTwoLetter).IsRequired().IsMaxLength();
			this.Property(p => p.NameIsoThreeLetter).IsRequired().IsMaxLength();
			this.Property(p => p.NameWindowsThreeLetter).IsRequired().IsMaxLength();

			this.Property(p => p.Name).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Name") { Order = 1, IsUnique = true, IsClustered = false, }));
		}
	}
}