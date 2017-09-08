using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.DataLayer.Configurations.Tenants
{
	public partial class TenantConfiguration:
		EntityTypeConfiguration<Tenant>
	{
		public TenantConfiguration ()
		{
			// Table name in database.
			this.ToTable("Tenant");

			// Primary Key.
			this.HasKey<long>(p => p.Id);
			this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			// Foriegn Keys.
			this.HasMany(p => p.Users).WithRequired(p => p.Tenant).HasForeignKey(p => p.TenantId);
			this.HasMany(p => p.Sessions).WithRequired(p => p.Tenant).HasForeignKey(p => p.TenantId).WillCascadeOnDelete(false);
			this.HasMany(p => p.TenantSubscriptions).WithRequired(p => p.Tenant).HasForeignKey(p => p.TenantId);

			// Class property validation.
			this.Property(p => p.CompanyName).IsRequired().HasMaxLength(EntityConstants.LengthCompanyName);
			this.Property(p => p.Domain).HasMaxLength(EntityConstants.LengthDomainNameMaximum);
			this.Property(p => p.ContactOwnerNameGiven).HasMaxLength(256);
			this.Property(p => p.ContactOwnerNameFamily).HasMaxLength(256);
			this.Property(p => p.ContactOwnerAddress).HasMaxLength(EntityConstants.LengthAddress);
			this.Property(p => p.ContactOwnerCity).HasMaxLength(256);
			this.Property(p => p.ContactOwnerState).HasMaxLength(256);
			this.Property(p => p.ContactOwnerZipCode).HasMaxLength(100);
			this.Property(p => p.ContactOwnerCountry).HasMaxLength(256);
			this.Property(p => p.ContactOwnerPhone).HasMaxLength(100);
			this.Property(p => p.ContactOwnerFax).HasMaxLength(100);
			this.Property(p => p.ContactOwnerEmail).HasMaxLength(EntityConstants.LengthEmail);
			this.Property(p => p.ContactAdministratorNameGiven).HasMaxLength(256);
			this.Property(p => p.ContactAdministratorNameFamily).HasMaxLength(256);
			this.Property(p => p.ContactAdministratorAddress).HasMaxLength(EntityConstants.LengthAddress);
			this.Property(p => p.ContactAdministratorCity).HasMaxLength(256);
			this.Property(p => p.ContactAdministratorState).HasMaxLength(256);
			this.Property(p => p.ContactAdministratorZipCode).HasMaxLength(100);
			this.Property(p => p.ContactAdministratorCountry).HasMaxLength(256);
			this.Property(p => p.ContactAdministratorPhone).HasMaxLength(100);
			this.Property(p => p.ContactAdministratorFax).HasMaxLength(100);
			this.Property(p => p.ContactAdministratorEmail).HasMaxLength(EntityConstants.LengthEmail);
			this.Property(p => p.ContactBillingNameGiven).HasMaxLength(256);
			this.Property(p => p.ContactBillingNameFamily).HasMaxLength(256);
			this.Property(p => p.ContactBillingAddress).HasMaxLength(EntityConstants.LengthAddress);
			this.Property(p => p.ContactBillingCity).HasMaxLength(256);
			this.Property(p => p.ContactBillingState).HasMaxLength(256);
			this.Property(p => p.ContactBillingZipCode).HasMaxLength(100);
			this.Property(p => p.ContactBillingCountry).HasMaxLength(256);
			this.Property(p => p.ContactBillingPhone).HasMaxLength(100);
			this.Property(p => p.ContactBillingFax).HasMaxLength(100);
			this.Property(p => p.ContactBillingEmail).HasMaxLength(EntityConstants.LengthEmail);
			this.Property(p => p.ContactTechnicalNameGiven).HasMaxLength(256);
			this.Property(p => p.ContactTechnicalNameFamily).HasMaxLength(256);
			this.Property(p => p.ContactTechnicalAddress).HasMaxLength(EntityConstants.LengthAddress);
			this.Property(p => p.ContactTechnicalCity).HasMaxLength(256);
			this.Property(p => p.ContactTechnicalState).HasMaxLength(256);
			this.Property(p => p.ContactTechnicalZipCode).HasMaxLength(100);
			this.Property(p => p.ContactTechnicalCountry).HasMaxLength(256);
			this.Property(p => p.ContactTechnicalPhone).HasMaxLength(100);
			this.Property(p => p.ContactTechnicalFax).HasMaxLength(100);
			this.Property(p => p.ContactTechnicalEmail).HasMaxLength(EntityConstants.LengthEmail);

			this.Ignore (p => p.MasterUsers);
			this.Ignore (p => p.MasterSessions);
		}
	}
}