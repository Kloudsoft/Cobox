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
	public partial class WorkflowInstanceConfiguration:
		EntityTypeConfiguration<WorkflowInstance>
	{
		public WorkflowInstanceConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowInstance");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.WorkflowTemplate).WithMany(p => p.WorkFlowInstances).HasForeignKey(p => p.WorkflowTemplateId);
			this.HasRequired(p => p.Document).WithMany(p => p.WorkFlowInstances).HasForeignKey(p => p.DocumentId).WillCascadeOnDelete(false);
			// Class property validation.
		}
	}
}