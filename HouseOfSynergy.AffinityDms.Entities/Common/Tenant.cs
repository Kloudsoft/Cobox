using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Common
{
	public partial class Tenant:
		IEntity<Tenant>
	{
		public virtual long Id { get; set; }

		/// <summary>
		/// Represents the tenant Id in the master database.
		/// This is only significant on the tenant's side since
		/// the Tenant entity is shared across both databases.
		/// This property is not a bound foriegn key and should not be configured as such.
		/// </summary>
		public virtual long MasterTenantId { get; set; }

		/// <summary>
		/// Determines whether the tenant belongs to the master context or a local tenant context.
		/// </summary>
		public virtual EntityMasterTenantType TenantType { get; set; }

		/// <summary>
		/// Represents the tenant token used for authentication by the master.
		/// </summary>
		public virtual string MasterTenantToken { get; set; }

		public virtual AuthenticationType AuthenticationType { get; set; }

		/// <summary>
		/// The unique domain for this tenant.
		/// </summary>
		public virtual string Domain { get; set; }

		/// <summary>
		/// The company name for this tenant.
		/// </summary>
        public virtual string CompanyName { get; set; }

        public virtual string ContactOwnerNameGiven { get; set; }
        public virtual string ContactOwnerNameFamily { get; set; }
        public virtual string ContactOwnerAddress { get; set; }
        public virtual string ContactOwnerCity { get; set; }
        public virtual string ContactOwnerState { get; set; }
        public virtual string ContactOwnerZipCode { get; set; }
        public virtual string ContactOwnerCountry { get; set; }
        public virtual string ContactOwnerPhone { get; set; }
        public virtual string ContactOwnerFax { get; set; }
        public virtual string ContactOwnerEmail { get; set; }
        public virtual string ContactAdministratorNameGiven { get; set; }
        public virtual string ContactAdministratorNameFamily { get; set; }
        public virtual string ContactAdministratorAddress { get; set; }
        public virtual string ContactAdministratorCity { get; set; }
        public virtual string ContactAdministratorState { get; set; }
        public virtual string ContactAdministratorZipCode { get; set; }
        public virtual string ContactAdministratorCountry { get; set; }
        public virtual string ContactAdministratorPhone { get; set; }
        public virtual string ContactAdministratorFax { get; set; }
        public virtual string ContactAdministratorEmail { get; set; }
        public virtual string ContactBillingNameGiven { get; set; }
        public virtual string ContactBillingNameFamily { get; set; }
        public virtual string ContactBillingAddress { get; set; }
        public virtual string ContactBillingCity { get; set; }
        public virtual string ContactBillingState { get; set; }
        public virtual string ContactBillingZipCode { get; set; }
        public virtual string ContactBillingCountry { get; set; }
        public virtual string ContactBillingPhone { get; set; }
        public virtual string ContactBillingFax { get; set; }
        public virtual string ContactBillingEmail { get; set; }
        public virtual string ContactTechnicalNameGiven { get; set; }
        public virtual string ContactTechnicalNameFamily { get; set; }
        public virtual string ContactTechnicalAddress { get; set; }
        public virtual string ContactTechnicalCity { get; set; }
        public virtual string ContactTechnicalState { get; set; }
        public virtual string ContactTechnicalZipCode { get; set; }
        public virtual string ContactTechnicalCountry { get; set; }
        public virtual string ContactTechnicalPhone { get; set; }
        public virtual string ContactTechnicalFax { get; set; }
        public virtual string ContactTechnicalEmail { get; set; }

		/// <summary>
		/// The tenant's RSA key (public).
		/// </summary>
		public virtual string RsaKeyPublic { get; set; } // Read-only with a button to re-generate.

		/// <summary>
		/// The tenant's RSA key (private).
		/// </summary>
		public virtual string RsaKeyPrivate { get; set; }

		/// <summary>
		/// The tenant's API URI.
		/// </summary>
		public virtual string UrlApi { get; set; }

		/// <summary>
		/// The tenant's resource group name on Azure.
		/// </summary>
		public virtual string UrlResourceGroup { get; set; }	// Example: kloudsoft-sea-tenant.

		// Edit only UrlStorage. The rest of the 4 properties should be generated on screen accordingly.
		public virtual string UrlStorage { get; set; }			// Example: storage-account-name.
		public virtual string UrlStorageBlob { get; set; }      // Example: https://storage-account-name.blob.core.windows.net/.
		public virtual string UrlStorageTable { get; set; }     // Example: https://storage-account-name.table.core.windows.net/.
		public virtual string UrlStorageQueue { get; set; }     // Example: https://storage-account-name.queue.core.windows.net/.
		public virtual string UrlStorageFile { get; set; }      // Example: https://storage-account-name.file.core.windows.net/.

		public virtual string StorageAccessKeyPrimary { get; set; }				// Example: 9PeE42dFjtFVWthgi4vC74XyfQyitE/oi+fUPw9Q99NHnQMX6bCInpm9ZioVpbAk/IgfbwIdURWw97jYp+RU2g==.
		public virtual string StorageAccessKeySecondary { get; set; }			// Example: lJEp+ggFP0IbSTZrfHABWmjp0GjOWWnzlKjchdoH6aC6lqVTlgKhfr7w4SAG57aLNm2I8YfCu7KOMLnSJYJ+mA==.
		public virtual string StorageConnectionStringPrimary { get; set; }		// Example: DefaultEndpointsProtocol=https;AccountName=kloudsoft;AccountKey=9PeE42dFjtFVWthgi4vC74XyfQyitE/oi+fUPw9Q99NHnQMX6bCInpm9ZioVpbAk/IgfbwIdURWw97jYp+RU2g==;BlobEndpoint=https://kloudsoft.blob.core.windows.net/;TableEndpoint=https://kloudsoft.table.core.windows.net/;QueueEndpoint=https://kloudsoft.queue.core.windows.net/;FileEndpoint=https://kloudsoft.file.core.windows.net/.
		public virtual string StorageConnectionStringSecondary { get; set; }	// Example: DefaultEndpointsProtocol=https;AccountName=kloudsoft;AccountKey=lJEp+ggFP0IbSTZrfHABWmjp0GjOWWnzlKjchdoH6aC6lqVTlgKhfr7w4SAG57aLNm2I8YfCu7KOMLnSJYJ+mA==;BlobEndpoint=https://kloudsoft.blob.core.windows.net/;TableEndpoint=https://kloudsoft.table.core.windows.net/;QueueEndpoint=https://kloudsoft.queue.core.windows.net/;FileEndpoint=https://kloudsoft.file.core.windows.net/.

		private string _DatabaseConnectionString = "";

		/// <summary>
		/// The tenant's database connection string on Azure.
		/// </summary>
		public virtual string DatabaseConnectionString
		{
			get { return (this._DatabaseConnectionString); }
			set { this._DatabaseConnectionString = (value ?? "").Trim(); }
		}

		private ICollection<User> _Users = null;
		private ICollection<Session> _Sessions = null;
		private ICollection<MasterUser> _MasterUsers = null;
		private ICollection<MasterSession> _MasterSessions = null;
		private ICollection<TenantSubscription> _TenantSubscriptions = null;

		public virtual ICollection<User> Users { get { if (this._Users == null) { this._Users = new List<User>(); } return (this._Users); } protected set { this._Users = value; } }
		public virtual ICollection<Session> Sessions { get { if (this._Sessions == null) { this._Sessions = new List<Session>(); } return (this._Sessions); } protected set { this._Sessions = value; } }
		public virtual ICollection<MasterUser> MasterUsers { get { if (this._MasterUsers == null) { this._MasterUsers = new List<MasterUser>(); } return (this._MasterUsers); } protected set { this._MasterUsers = value; } }
		public virtual ICollection<MasterSession> MasterSessions { get { if (this._MasterSessions == null) { this._MasterSessions = new List<MasterSession>(); } return (this._MasterSessions); } protected set { this._MasterSessions = value; } }
		public virtual ICollection<TenantSubscription> TenantSubscriptions { get { if (this._TenantSubscriptions == null) { this._TenantSubscriptions = new List<TenantSubscription>(); } return (this._TenantSubscriptions); } protected set { this._TenantSubscriptions = value; } }

        public Tenant ()
		{
		}

		public void Initialize ()
		{
		}

		public void SetAllContacts
		(
			string nameGiven,
			string nameFamily,
			string address,
			string city,
			string state,
			string zipCode,
			string country,
			string phone,
			string fax,
			string email
		)
		{
			this.ContactOwnerNameGiven = this.ContactAdministratorNameGiven = this.ContactBillingNameGiven = this.ContactTechnicalNameGiven = nameGiven;
			this.ContactOwnerNameFamily = this.ContactAdministratorNameFamily = this.ContactBillingNameFamily = this.ContactTechnicalNameFamily = nameFamily;
			this.ContactOwnerAddress = this.ContactAdministratorAddress = this.ContactBillingAddress = this.ContactTechnicalAddress = address;
			this.ContactOwnerCity = this.ContactAdministratorCity = this.ContactBillingCity = this.ContactTechnicalCity = city;
			this.ContactOwnerState = this.ContactAdministratorState = this.ContactBillingState = this.ContactTechnicalState = state;
			this.ContactOwnerZipCode = this.ContactAdministratorZipCode = this.ContactBillingZipCode = this.ContactTechnicalZipCode = zipCode;
			this.ContactOwnerCountry = this.ContactAdministratorCountry = this.ContactBillingCountry = this.ContactTechnicalCountry = country;
			this.ContactOwnerPhone = this.ContactAdministratorPhone = this.ContactBillingPhone = this.ContactTechnicalPhone = phone;
			this.ContactOwnerFax = this.ContactAdministratorFax = this.ContactBillingFax = this.ContactTechnicalFax = fax;
			this.ContactOwnerEmail = this.ContactAdministratorEmail = this.ContactBillingEmail = this.ContactTechnicalEmail = email;
		}

		public Tenant Clone ()
		{
			return (new Tenant().CopyFrom(this));
		}

		public Tenant CopyTo (Tenant destination)
		{
			return (destination.CopyFrom(this));
		}

		public Tenant CopyFrom (Tenant source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Tenant FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}