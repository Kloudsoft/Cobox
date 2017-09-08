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

namespace HouseOfSynergy.AffinityDms.Entities.Master
{
	public partial class MasterUserRole:
		IEntity<MasterUserRole>
	{
		public virtual long Id { get; set; }

        public virtual long RoleId { get; set; }
		public virtual MasterRole Role { get; set; }

        public virtual long UserId { get; set; }
		public virtual MasterUser User { get; set; }

		public MasterUserRole ()
		{
		}
	
		public void Initialize()
		{
		}

		public MasterUserRole Clone()
		{
			return (new MasterUserRole().CopyFrom(this));
		}

		public MasterUserRole CopyTo(MasterUserRole destination)
		{
			return (destination.CopyFrom(this));
		}

		public MasterUserRole CopyFrom(MasterUserRole source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public MasterUserRole FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}