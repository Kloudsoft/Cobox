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
	public partial class RoleDelegation:
		IEntity<RoleDelegation>
	{
		public virtual long Id { get; set; }

		public virtual long UserId { get; set; }
        public virtual User User { get; set; }

        public virtual long UserDelegationId { get; set; }
        public virtual UserDelegation UserDelegations { get; set; }

		public RoleDelegation ()
		{
		}
	
		public void Initialize()
		{
		}

		public RoleDelegation Clone()
		{
			return (new RoleDelegation().CopyFrom(this));
		}

		public RoleDelegation CopyTo(RoleDelegation destination)
		{
			return (destination.CopyFrom(this));
		}

		public RoleDelegation CopyFrom(RoleDelegation source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public RoleDelegation FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}