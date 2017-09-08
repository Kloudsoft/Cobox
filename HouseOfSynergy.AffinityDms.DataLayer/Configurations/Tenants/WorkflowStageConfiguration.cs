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
	public partial class WorkflowStageConfiguration:
		EntityTypeConfiguration<WorkflowStage>
	{
		public WorkflowStageConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowStage");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.WorkflowMaster).WithMany(p => p.WorkflowStages).HasForeignKey(p => p.WorkflowMasterId);
			this.HasMany(p => p.WorkflowUserActions).WithRequired(p => p.WorkflowStage).HasForeignKey(p => p.WorkflowStagesId);
			this.HasRequired(p => p.WorkflowTemplate).WithMany(p => p.WorkflowStages).HasForeignKey(p => p.WorkflowTemplateId);
			this.HasMany(p => p.WorkflowActors).WithRequired(p => p.WorkflowStage).HasForeignKey(p => p.WorkflowStageId);
			this.HasMany(p => p.WorkFlowRules).WithRequired(p => p.WorkflowStage).HasForeignKey(p => p.WorkflowStageId);

			// Class property validation.
			this.Property(p => p.StageDescription).HasMaxLength(200);
			this.Property(p => p.StageNo).IsRequired();
		}
	}
}