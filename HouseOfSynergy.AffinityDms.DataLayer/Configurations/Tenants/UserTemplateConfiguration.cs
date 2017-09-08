using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
    public class UserTemplateConfiguration :
        EntityTypeConfiguration<UserTemplate>
    {
        public UserTemplateConfiguration()
        {
            // Table name in database.
            this.ToTable("UserTemplate");

            // Primary Key.
            this.HasKey<long>(p => p.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(p => p.Template).WithMany(p => p.TemplateUsers).HasForeignKey(p => p.TemplateId).WillCascadeOnDelete(false);
        }
    }
}
