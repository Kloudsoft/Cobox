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
	public partial class DiscourseUser:
		IEntity<DiscourseUser>
	{
		public virtual long Id { get; set; }

		public virtual long DiscourseId { get; set; }
        public virtual Discourse Discourse { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual long UserId { get; set; }
        public virtual User User { get; set; }

		public DiscourseUser ()
		{
		}
	
		public void Initialize()
		{
		}

		public DiscourseUser Clone()
		{
			return (new DiscourseUser().CopyFrom(this));
		}

		public DiscourseUser CopyTo(DiscourseUser destination)
		{
			return (destination.CopyFrom(this));
		}

		public DiscourseUser CopyFrom(DiscourseUser source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DiscourseUser FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}