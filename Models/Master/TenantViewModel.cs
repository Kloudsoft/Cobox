using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Master
{
	public class TenantViewModel
	{
		public virtual long Id { get; set; }

        public virtual string Domain { get; set; }
        public virtual string CompanyName { get; set; }

        public virtual string OwnerNameGiven { get; set; }
        public virtual string OwnerNameFamily { get; set; }
        public virtual string OwnerAddress { get; set; }
        public virtual string OwnerCity { get; set; }
        public virtual string OwnerState { get; set; }
        public virtual string OwnerZipCode { get; set; }
        public virtual string OwnerCountry { get; set; }
        public virtual string OwnerPhone { get; set; }
        public virtual string OwnerFax { get; set; }
        public virtual string OwnerEmail { get; set; }
        public virtual string AdministratorNameGiven { get; set; }
        public virtual string AdministratorNameFamily { get; set; }
        public virtual string AdministratorAddress { get; set; }
        public virtual string AdministratorCity { get; set; }
        public virtual string AdministratorState { get; set; }
        public virtual string AdministratorZipCode { get; set; }
        public virtual string AdministratorCountry { get; set; }
        public virtual string AdministratorPhone { get; set; }
        public virtual string AdministratorFax { get; set; }
        public virtual string AdministratorEmail { get; set; }
        public virtual string BillingNameGiven { get; set; }
        public virtual string BillingNameFamily { get; set; }
        public virtual string BillingAddress { get; set; }
        public virtual string BillingCity { get; set; }
        public virtual string BillingState { get; set; }
        public virtual string BillingZipCode { get; set; }
        public virtual string BillingCountry { get; set; }
        public virtual string BillingPhone { get; set; }
        public virtual string BillingFax { get; set; }
        public virtual string BillingEmail { get; set; }
        public virtual string TechnicalNameGiven { get; set; }
        public virtual string TechnicalNameFamily { get; set; }
        public virtual string TechnicalAddress { get; set; }
        public virtual string TechnicalCity { get; set; }
        public virtual string TechnicalState { get; set; }
        public virtual string TechnicalZipCode { get; set; }
        public virtual string TechnicalCountry { get; set; }
        public virtual string TechnicalPhone { get; set; }
        public virtual string TechnicalFax { get; set; }
        public virtual string TechnicalEmail { get; set; }

		public virtual string RsaKeyPublic { get; set; }
		public virtual string RsaKeyPrivate { get; set; }

		public virtual string UrlApi { get; set; }
		public virtual string UrlResourceGroup { get; set; }	// Example: kloudsoft-sea-tenant.

		public virtual string UrlStorage { get; set; }			// Example: kloudsoft.
		public virtual string UrlStorageBlob { get; set; }		// Example: https://kloudsoft.blob.core.windows.net/.
		public virtual string UrlStorageTable { get; set; }		// Example: https://kloudsoft.table.core.windows.net/.
		public virtual string UrlStorageQueue { get; set; }		// Example: https://kloudsoft.queue.core.windows.net/.
		public virtual string UrlStorageFile { get; set; }		// Example: https://kloudsoft.file.core.windows.net/.

		public virtual string StorageAccessKeyPrimary { get; set; }				// Example: 9PeE42dFjtFVWthgi4vC74XyfQyitE/oi+fUPw9Q99NHnQMX6bCInpm9ZioVpbAk/IgfbwIdURWw97jYp+RU2g==.
		public virtual string StorageAccessKeySecondary { get; set; }			// Example: lJEp+ggFP0IbSTZrfHABWmjp0GjOWWnzlKjchdoH6aC6lqVTlgKhfr7w4SAG57aLNm2I8YfCu7KOMLnSJYJ+mA==.
		public virtual string StorageConnectionStringPrimary { get; set; }		// Example: DefaultEndpointsProtocol=https;AccountName=kloudsoft;AccountKey=9PeE42dFjtFVWthgi4vC74XyfQyitE/oi+fUPw9Q99NHnQMX6bCInpm9ZioVpbAk/IgfbwIdURWw97jYp+RU2g==;BlobEndpoint=https://kloudsoft.blob.core.windows.net/;TableEndpoint=https://kloudsoft.table.core.windows.net/;QueueEndpoint=https://kloudsoft.queue.core.windows.net/;FileEndpoint=https://kloudsoft.file.core.windows.net/.
		public virtual string StorageConnectionStringSecondary { get; set; }	// Example: DefaultEndpointsProtocol=https;AccountName=kloudsoft;AccountKey=lJEp+ggFP0IbSTZrfHABWmjp0GjOWWnzlKjchdoH6aC6lqVTlgKhfr7w4SAG57aLNm2I8YfCu7KOMLnSJYJ+mA==;BlobEndpoint=https://kloudsoft.blob.core.windows.net/;TableEndpoint=https://kloudsoft.table.core.windows.net/;QueueEndpoint=https://kloudsoft.queue.core.windows.net/;FileEndpoint=https://kloudsoft.file.core.windows.net/.

		public TenantViewModel ()
		{
		}
	}
}