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
	public partial class WorkflowTemplateConfiguration:
		EntityTypeConfiguration<WorkflowTemplate>
	{
		public WorkflowTemplateConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowTemplate");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.

			this.HasMany(p => p.EntityWorkflowMappings).WithRequired(p => p.WorkflowTemplate).HasForeignKey(p => p.WorkflowTemplateId);
			this.HasMany(p => p.WorkflowStages).WithRequired(p => p.WorkflowTemplate).HasForeignKey(p => p.WorkflowTemplateId);

			// Class property validation.
			this.Property(p => p.Description).HasMaxLength(100);
		}
	}
}