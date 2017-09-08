using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
	public class SessionMessage:
		IEntity<SessionMessage>
	{
		public long Id { get; set; }

		public SessionMessageType SessionMessageType { get; set; }

		public virtual Session Session { get; set; }
		public virtual long SessionId { get; set; }

		public virtual string Value { get; set; }

		public void Initialize ()
		{
		}

		public SessionMessage Clone ()
		{
			return (new SessionMessage().CopyFrom(this));
		}

		public SessionMessage CopyTo (SessionMessage destination)
		{
			return (destination.CopyFrom(this));
		}

		public SessionMessage CopyFrom (SessionMessage source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public SessionMessage FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}