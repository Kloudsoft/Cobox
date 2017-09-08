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
	public partial class UserRole:
		IEntity<UserRole>
	{
		public virtual long Id { get; set; }

        public virtual long RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual long UserId { get; set; }
        public virtual User User { get; set; }

		public UserRole ()
		{
		}
	
		public void Initialize()
		{
		}

		public UserRole Clone()
		{
			return (new UserRole().CopyFrom(this));
		}

		public UserRole CopyTo(UserRole destination)
		{
			return (destination.CopyFrom(this));
		}

		public UserRole CopyFrom(UserRole source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public UserRole FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}