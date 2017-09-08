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
    public partial class DocumentCorrectiveIndexValueConfiguration :
        EntityTypeConfiguration<DocumentCorrectiveIndexValue>
    {
        public DocumentCorrectiveIndexValueConfiguration()
        {
            // Table name in database.
            this.ToTable("DocumentCorrectiveIndexValue");

            // Primary Key.
            this.HasKey<long>(p => p.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Foriegn Keys.
            ////this.HasRequired(p => p.Document).WithMany(p => p.DocumentCorrectiveIndexValues).HasForeignKey(p => p.DocumentId);
            ////this.HasMany(p => p.DocumentId).WithRequired(p => p.DocumentIndexedVersion).HasForeignKey(p => p.DocumentIndexedVersionId);
            // Class property validation.

            this.HasRequired(p => p.Document).WithMany(p => p.DocumentCorrectiveIndexValues).HasForeignKey(p => p.DocumentId);
            this.HasRequired(p => p.IndexElement).WithMany(p => p.DocumentCorrectiveIndexValues).HasForeignKey(p => p.IndexElementId);
            this.HasRequired(p => p.Indexer).WithMany(p => p.DocumentCorrectiveIndexValues).HasForeignKey(p => p.IndexerId);

        }
    }
}