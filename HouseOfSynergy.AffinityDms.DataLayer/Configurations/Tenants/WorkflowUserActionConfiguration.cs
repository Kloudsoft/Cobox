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
	public partial class WorkflowUserActionConfiguration:
		EntityTypeConfiguration<WorkflowUserAction>
	{
		public WorkflowUserActionConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowUserAction");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.WorkflowAction).WithMany(p => p.WorkflowUserActions).HasForeignKey(p => p.WorkflowActionId).WillCascadeOnDelete(false);
			this.HasRequired(p => p.WorkflowStage).WithMany(p => p.WorkflowUserActions).HasForeignKey(p => p.WorkflowStagesId).WillCascadeOnDelete(false);
			// Class property validation.
		}
	}
}