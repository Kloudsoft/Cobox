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
    class MasterIndexConfiguration  : 
        EntityTypeConfiguration<MasterIndex>
    {

        public MasterIndexConfiguration()
        {
            // Table name in database.
            this.ToTable("Master_IndexName_List");

            // Primary key.
            this.HasKey<long>(p => p.Id);
            // Auto-increment will be handled by the database.
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }

    }
}
