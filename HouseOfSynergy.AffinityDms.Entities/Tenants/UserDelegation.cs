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
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
	public partial class UserDelegation:
		IEntity<UserDelegation>
	{
		public virtual long Id { get; set; }

		public virtual DateTime StartDate { get; set; }
		public virtual DateTime EndDate { get; set; }
		public virtual int ActiveTag { get; set; }
		public virtual int Status { get; set; }

		public virtual long? FormUserId { get; set; }
        public virtual User FromUser { get; set; }

        public virtual long? ToUserId { get; set; }
        public virtual User ToUser { get; set; }

		private ICollection<RoleDelegation> _RoleDelegations = null;
		public virtual ICollection<RoleDelegation> RoleDelegations { get { if (this._RoleDelegations == null) { this._RoleDelegations = new List<RoleDelegation>(); } return (this._RoleDelegations); } protected set { this._RoleDelegations = value; } }

		public UserDelegation ()
		{
		}
	
		public void Initialize()
		{
		}

		public UserDelegation Clone()
		{
			return (new UserDelegation().CopyFrom(this));
		}

		public UserDelegation CopyTo(UserDelegation destination)
		{
			return (destination.CopyFrom(this));
		}

		public UserDelegation CopyFrom(UserDelegation source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public UserDelegation FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}