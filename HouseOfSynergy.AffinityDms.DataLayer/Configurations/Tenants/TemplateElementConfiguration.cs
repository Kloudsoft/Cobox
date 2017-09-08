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
	public partial class TemplateElementConfiguration:
		EntityTypeConfiguration<TemplateElement>
	{
		public TemplateElementConfiguration ()
		{
			this.ToTable("TemplateElement");

			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.HasRequired (p => p.Template).WithMany (p => p.Elements).HasForeignKey (p => p.TemplateId);
            this.HasMany(p => p.DocumentCorrectiveIndexValues).WithRequired(p => p.IndexElement).HasForeignKey(p => p.IndexElementId);
            this.HasMany(p => p.ElementValues).WithRequired(p => p.Element).HasForeignKey(p => p.ElementId);

			this.Property(p => p.ElementType).IsRequired();
			this.Property(p => p.Name).IsRequired().HasMaxLength(500);
			this.Property(p => p.Description).HasMaxLength(50);
			//this.Property(p => p.Text).IsRequired();
			this.Property(p => p.X).IsRequired();
			this.Property(p => p.Y).IsRequired();
			this.Property(p => p.Width).IsRequired();
			this.Property(p => p.Height).IsRequired();
			this.Property(p => p.DivX).IsOptional();
			this.Property(p => p.DivY).IsOptional();
			this.Property(p => p.DivWidth).IsOptional();
			this.Property(p => p.DivHeight).IsOptional();
			this.Property(p => p.FontSize).IsOptional();
			this.Property(p => p.FontStyle).IsOptional();
			this.Property(p => p.ColorBackroundA).IsOptional();
			this.Property(p => p.ColorBackroundB).IsOptional();
			this.Property(p => p.ColorBackroundG).IsOptional();
			this.Property(p => p.ColorBackroundR).IsOptional();
			this.Property(p => p.ColorForegroundA).IsOptional();
			this.Property(p => p.ColorForegroundB).IsOptional();
			this.Property(p => p.ColorForegroundG).IsOptional();
			this.Property(p => p.ColorForegroundR).IsOptional();
			this.Property(p => p.BarcodeType).IsOptional();
			this.Property(p => p.BorderStyle).IsOptional();
			this.Property(p => p.Discriminator).IsOptional().HasMaxLength(128);
			this.Property(p => p.ElementMobileOrdinal).IsOptional();
			this.Property(p => p.X2).IsOptional();
			this.Property(p => p.Y2).IsOptional();
			this.Property(p => p.Diameter).IsOptional();
			this.Property(p => p.Radius).IsOptional();
			this.Property(p => p.ElementIndexType).IsOptional();
			this.Property(p => p.BarcodeValue).IsOptional();
		}
	}
}