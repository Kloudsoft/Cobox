using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Master
{
    public partial class MasterUser:
		IEntity<MasterUser>
	{
		public virtual long Id { get; set; }

		public virtual string Email { get; set; }
		public virtual string UserName { get; set; }
		public virtual string PasswordHash { get; set; }
		public virtual string PasswordSalt { get; set; }

		public virtual string NameGiven { get; set; }
		public virtual string NameFamily { get; set; }
		public virtual string Address1 { get; set; }
		public virtual string Address2 { get; set; }
		public virtual string City { get; set; }
		public virtual string ZipOrPostCode { get; set; }
		public virtual string Country { get; set; }
		public virtual string PhoneWork { get; set; }
		public virtual string PhoneMobile { get; set; }
		public virtual DateTime DateTimeCreated { get; set; }

		/// <summary>
		/// User Type: Local, ActiveDirectory or External.
		/// </summary>
		public virtual AuthenticationType AuthenticationType { get; set; }
		public virtual string ActiveDirectory_NameDisplay { get; set; }
		public virtual Guid ActiveDirectory_ObjectId { get; set; }
		public virtual string ActiveDirectory_UsageLocation { get; set; }
		public virtual string ActiveDirectory_JobTitle { get; set; }
		public virtual string ActiveDirectory_Department { get; set; }
		public virtual Guid ActiveDirectory_ManagerId { get; set; }
		public virtual string ActiveDirectory_AuthenticationPhone { get; set; }
		public virtual string ActiveDirectory_AuthenticationPhoneAlternate { get; set; }
		public virtual string ActiveDirectory_AuthenticationEmail { get; set; }
		public virtual string ActiveDirectory_RoleDisplayName { get; set; }

		/// <summary>
		/// The ActiveDirectory Id associated with this user (if any).
		/// </summary>
		public virtual string ActiveDirectoryId { get; set; }

		private ICollection<MasterRole> _Roles = null;
		private ICollection<MasterSession> _Sessions = null;
		private ICollection<MasterUserRole> _UserRoles = null;

		public virtual ICollection<MasterRole> Roles { get { if (this._Roles == null) { this._Roles = new List<MasterRole>(); } return (this._Roles); } protected set { this._Roles = value; } }
		public virtual ICollection<MasterSession> Sessions { get { if (this._Sessions == null) { this._Sessions = new List<MasterSession>(); } return (this._Sessions); } protected set { this._Sessions = value; } }
		public virtual ICollection<MasterUserRole> UserRoles { get { if (this._UserRoles == null) { this._UserRoles = new List<MasterUserRole>(); } return (this._UserRoles); } protected set { this._UserRoles = value; } }

		public MasterUser ()
		{
		}

		[NotMapped]
		public string NameFull { get { return (this.NameGiven + " " + this.NameFamily); } }

		public void Initialize ()
		{
		}

		public MasterUser Clone ()
		{
			return (new MasterUser().CopyFrom(this));
		}

		public MasterUser CopyTo (MasterUser destination)
		{
			return (destination.CopyFrom(this));
		}

		public MasterUser CopyFrom (MasterUser source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public MasterUser FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}