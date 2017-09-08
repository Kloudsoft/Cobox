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
	public partial class WorkflowMasterConfiguration:
		EntityTypeConfiguration<WorkflowMaster>
	{
		public WorkflowMasterConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowMaster");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasMany(p => p.EntityWorkflowMappings).WithRequired(p => p.WorkflowMaster).HasForeignKey(p => p.WorkflowMasterId);
			this.HasMany(p => p.WorkflowStages).WithRequired(p => p.WorkflowMaster).HasForeignKey(p => p.WorkflowMasterId);
			// Class property validation.
			this.Property(p => p.Description).HasMaxLength(200);

		}
	}
}