using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
	public sealed class TenantUserSession:
		IEntity<TenantUserSession>
	{
		public long Id { get; set; }

		public User User { get; set; }
		public Tenant Tenant { get; set; }
		public Session Session { get; set; }

		public TenantUserSession ()
		{
			this.User = new User();
			this.Tenant = new Tenant();
			this.Session = new Session();
		}

		public TenantUserSession (Tenant tenant)
			: this()
		{
			this.Tenant = tenant;
		}

		public TenantUserSession (Tenant tenant, User user)
			: this()
		{
			this.User = user;
			this.Tenant = tenant;
		}

		public TenantUserSession (Tenant tenant, User user, Session session)
			: this()
		{
			this.User = user;
			this.Tenant = tenant;
			this.Session = session;
		}

		public void Initialize ()
		{
		}

		public TenantUserSession Clone ()
		{
			return (new TenantUserSession().CopyFrom(this));
		}

		public TenantUserSession CopyTo (TenantUserSession destination)
		{
			return (destination.CopyFrom(this));
		}

		public TenantUserSession CopyFrom (TenantUserSession source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TenantUserSession FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}