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
	public partial class WorkflowActorConfiguration:
		EntityTypeConfiguration<WorkflowActor>
	{
		public WorkflowActorConfiguration ()
		{
			// Table name in database.
			this.ToTable("WorkflowActor");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasRequired(p => p.WorkflowStage).WithMany(p => p.WorkflowActors).HasForeignKey(p => p.WorkflowStageId);
			this.HasMany(p => p.WorkflowActions).WithRequired(p => p.WorkflowActor).HasForeignKey(p => p.WorkflowActorId);
			// Class property validation.
		}
	}
}