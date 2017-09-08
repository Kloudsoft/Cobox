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
	public partial class TemplateElementDetailConfiguration:
		EntityTypeConfiguration<TemplateElementDetail>
	{
		public TemplateElementDetailConfiguration ()
		{
			// Table name in database.
			this.ToTable("TemplateElementDetail");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.


			this.HasRequired(p => p.Element).WithMany(p => p.ElementDetails).HasForeignKey(p => p.ElementId);

			// Class property validation.
			this.Property(p => p.ElementType).IsRequired();

			this.Property(p => p.ElementDetailId).IsRequired();
			this.Property(p => p.X).IsRequired();
			this.Property(p => p.Y).IsRequired();
			this.Property(p => p.Width).IsRequired();
			this.Property(p => p.Height).IsRequired();
			//this.Property(p => p.DivX).IsOptional();
			//this.Property(p => p.DivY).IsOptional();
			//this.Property(p => p.DivWidth).IsOptional();
			//this.Property(p => p.DivHeight).IsOptional();
			//this.Property(p => p.FontSize).IsOptional();
			//this.Property(p => p.FontStyle).IsOptional();
			this.Property(p => p.Text).IsOptional();
			this.Property(p => p.Name).IsOptional();
			this.Property(p => p.Description).IsOptional();
			//this.Property(p => p.BarcodeType).IsOptional();
			this.Property(p => p.BorderStyle).IsOptional();
			this.Property(p => p.ForegroundColor).IsOptional();
			this.Property(p => p.BackgroundColor).IsOptional();
			this.Property(p => p.SizeMode).IsOptional();
			this.Property(p => p.Value).IsOptional();
			//this.Property(p => p.Discriminator).IsOptional().HasMaxLength(128);
			//this.Property(p => p.ElementMobileOrdinal).IsOptional();
			//this.Property(p => p.X2).IsOptional();
			//this.Property(p => p.Y2).IsOptional();
			//this.Property(p => p.Diameter).IsOptional();
			//this.Property(p => p.Radius).IsOptional();
		}
	}
}