using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public partial class TableHistoryConfiguration:
		EntityTypeConfiguration<TableHistory>
	{
		public TableHistoryConfiguration ()
		{
			// Table name in database.
			this.ToTable("TableHistory");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.

			// Class property validation.
			this.Property(p => p.TableName).HasMaxLength(EntityConstants.LengthSQLTableName);
		}
	}
}