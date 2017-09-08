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
	public partial class WorkflowActorsInstanceConfiguration:
		EntityTypeConfiguration<WorkflowActorsInstance>
	{
		public WorkflowActorsInstanceConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowActorsInstance");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.WorkFlowStagesInstances).WithMany(p => p.WorkFlowActorsInstances).HasForeignKey(p => p.WorkFlowStagesInstanceId);
			this.HasRequired(p => p.User).WithMany(p => p.WorkFlowActorsInstances).HasForeignKey(p => p.UserId);
			// Class property validation.
		}
	}
}