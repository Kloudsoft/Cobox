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
	public partial class WorkflowStagesInstanceConfiguration:
		EntityTypeConfiguration<WorkflowStagesInstance>
	{
		public WorkflowStagesInstanceConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowStagesInstance");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasMany(p => p.WorkFlowRuleInstances).WithRequired(p => p.WorkFlowStagesInstance).HasForeignKey(p => p.WorkFlowStagesInstanceId);
			this.HasMany(p => p.WorkFlowUserActionInstances).WithRequired(p => p.WorkFlowStagesInstance).HasForeignKey(p => p.WorkflowStageInstanceId);
			this.HasMany(p => p.WorkFlowActorsInstances).WithRequired(p => p.WorkFlowStagesInstances).HasForeignKey(p => p.WorkFlowStagesInstanceId);
			// Class property validation.
			this.Property(p => p.StageDescription).HasMaxLength(100);
			this.Property(p => p.StageNo).IsRequired();
		}
	}
}