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
	public partial class WorkflowRuleInstanceConfiguration:
		EntityTypeConfiguration<WorkflowRuleInstance>
	{
		public WorkflowRuleInstanceConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowRuleInstance");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			HasRequired(p => p.WorkFlowStagesInstance).WithMany(p => p.WorkFlowRuleInstances).HasForeignKey(p => p.WorkFlowStagesInstanceId);
			HasMany(p => p.RuleDetailInstances).WithRequired(p => p.WorkFlowRuleInstance).HasForeignKey(p => p.WorkFlowRuleInstanceId);
			// Class property validation.
			this.Property(p => p.Description).HasMaxLength(200);
		}
	}
}