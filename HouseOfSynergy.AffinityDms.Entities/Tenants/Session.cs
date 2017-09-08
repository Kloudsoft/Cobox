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

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
    public partial class Session:
		IEntity<Session>
	{
		public virtual long Id { get; set; }

		public virtual User User { get; set; }
		public virtual long UserId { get; set; }

		public virtual Tenant Tenant { get; set; }
		public virtual long TenantId { get; set; }

		/// <summary>
		/// A Guid unique across all tenant sessions.
		/// </summary>
		public virtual Guid Guid { get; set; }

		/// <summary>
		/// Stores the ASP .NET Server Session Id.
		/// </summary>
		public virtual string SessionId { get; set; }

		/// <summary>
		/// Determines whether this session originated from Mvc or Web Api controllers.
		/// </summary>
		public virtual SessionType SessionType { get; set; }

		public virtual DateTime DateTimeCreated { get; set; }
		public virtual DateTime DateTimeExpiration { get; set; }

		public virtual string Token { get; set; }
		public virtual string UserAgent { get; set; }

		public virtual DeviceType DeviceType { get; set; }
		public virtual string IPAddressString { get; set; }

		public virtual string RsaKeyPublic { get; set; }
		public virtual string RsaKeyPrivate { get; set; }

		public virtual string RijndaelKey { get; set; }
		public virtual string RijndaelInitializationVector { get; set; }

		public virtual string CultureName { get; set; }

		[NotMapped]
		public IPAddress IPAddress { get; set; }

		private ICollection<SessionMessage> _SessionMessages = null;
		public virtual ICollection<SessionMessage> SessionMessages { get { if (this._SessionMessages == null) { this._SessionMessages = new List<SessionMessage>(); } return (this._SessionMessages); } protected set { this._SessionMessages = value; } }

		public Session ()
		{
		}

		public void Initialize ()
		{
		}

		public Session Clone ()
		{
			return (new Session().CopyFrom(this));
		}

		public Session CopyTo (Session destination)
		{
			return (destination.CopyFrom(this));
		}

		public Session CopyFrom (Session source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Session FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}