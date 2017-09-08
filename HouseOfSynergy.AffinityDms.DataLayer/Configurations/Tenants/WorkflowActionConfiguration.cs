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
	public partial class WorkflowActionConfiguration:
		EntityTypeConfiguration<WorkflowAction>
	{
		public WorkflowActionConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowAction");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasMany(p => p.WorkflowUserActions).WithRequired(p => p.WorkflowAction).HasForeignKey(p => p.WorkflowActionId);
			this.HasRequired(p => p.WorkflowActor).WithMany(p => p.WorkflowActions).HasForeignKey(p => p.WorkflowActorId);
			// Class property validation.
			this.Property(p => p.ActionDescription).HasMaxLength(50);
		}
	}
}