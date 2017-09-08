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
	public partial class WorkflowRuleConfiguration:
		EntityTypeConfiguration<WorkflowRule>
	{
		public WorkflowRuleConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowRule");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.WorkflowStage).WithMany(p => p.WorkFlowRules).HasForeignKey(p => p.WorkflowStageId);
			this.HasMany(p => p.RuleDetails).WithRequired(p => p.WorkFlowRule).HasForeignKey(p => p.WorkflowRuleId);
			// Class property validation.
			this.Property(p => p.Description).HasMaxLength(200);

		}
	}
}