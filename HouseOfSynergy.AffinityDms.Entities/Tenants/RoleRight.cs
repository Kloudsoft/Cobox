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
	public partial class RoleRight:
		IEntity<RoleRight>
	{
		public virtual long Id { get; set; }

		public virtual long RoleId { get; set; }
        public virtual Role Roles { get; set; }

        public virtual long ScreenId { get; set; }
        public virtual Screen Screen { get; set; }

        public virtual long? ButtonId { get; set; }
        public virtual Button Button { get; set; }

		public RoleRight ()
		{
		}
	
		public void Initialize()
		{
		}

		public RoleRight Clone()
		{
			return (new RoleRight().CopyFrom(this));
		}

		public RoleRight CopyTo(RoleRight destination)
		{
			return (destination.CopyFrom(this));
		}

		public RoleRight CopyFrom(RoleRight source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public RoleRight FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}