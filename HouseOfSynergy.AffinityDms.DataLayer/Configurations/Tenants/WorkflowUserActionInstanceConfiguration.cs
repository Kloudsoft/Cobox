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
	public partial class WorkflowUserActionInstanceConfiguration:
		EntityTypeConfiguration<WorkflowUserActionInstance>
	{
		public WorkflowUserActionInstanceConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowUserActionInstance");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.WorkFlowStagesInstance).WithMany(p => p.WorkFlowUserActionInstances).HasForeignKey(p => p.WorkflowStageInstanceId).WillCascadeOnDelete(false);
			// Class property validation.
		}
	}
}