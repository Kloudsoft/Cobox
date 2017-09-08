using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Master;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Master
{
	public sealed class MasterUserSession:
		IEntity<MasterUserSession>
	{
		public long Id { get; set; }

		public MasterUser User { get;  set; }
		public MasterSession Session { get; private set; }

		public MasterUserSession ()
		{
		}

		public MasterUserSession (MasterUser user)
		{
			this.User = user;
		}

		public MasterUserSession (MasterUser user, MasterSession session)
		{
			this.User = user;
			this.Session = session;
		}

		public void Initialize ()
		{
		}

		public MasterUserSession Clone ()
		{
			return (new MasterUserSession().CopyFrom(this));
		}

		public MasterUserSession CopyTo (MasterUserSession destination)
		{
			return (destination.CopyFrom(this));
		}

		public MasterUserSession CopyFrom (MasterUserSession source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public MasterUserSession FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}