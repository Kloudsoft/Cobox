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
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Common
{
    public partial class Subscription:
		IEntity<Subscription>
    {
		public virtual long Id { get; set; }

		/// <summary>
		/// Represents the entity Id in the master database.
		/// This is only significant on the tenant's side since
		/// the entity is shared across both databases.
		/// This property is not a bound foriegn key and should not be configured as such.
		/// </summary>
		public virtual long MasterSubscriptionId { get; set; }

		/// <summary>
		/// Determines whether this entity belongs to the master context or a local tenant context.
		/// </summary>
		public virtual EntityMasterTenantType SubscriptionType { get; set; }

		public virtual bool IsDemo { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual string Description { get; set; }

		public virtual int NumberOfFormsAllowed { get; set; }
		public virtual int NumberOfPagesAllowed { get; set; }
		public virtual int NumberOfUsersAllowed { get; set; }
		public virtual int NumberOfTemplatesAllowed { get; set; }

		public virtual int NumberOfFormsUsed { get; set; }
		public virtual int NumberOfPagesUsed { get; set; }
		public virtual int NumberOfUsersUsed { get; set; }
		public virtual int NumberOfTemplatesUsed { get; set; }

		public virtual bool AllowScanning { get; set; }
		public virtual bool AllowBranding { get; set; }
		public virtual bool AllowTemplateWorkflows { get; set; }

		private ICollection<TenantSubscription> _TenantSubscriptions = null;
		public virtual ICollection<TenantSubscription> TenantSubscriptions { get { if (this._TenantSubscriptions == null) { this._TenantSubscriptions = new List<TenantSubscription>(); } return (this._TenantSubscriptions); } protected set { this._TenantSubscriptions = value; } }

		public Subscription ()
		{
		}
    
		public void Initialize()
		{
		}

		public Subscription Clone()
		{
			return (new Subscription().CopyFrom(this));
		}

		public Subscription CopyTo(Subscription destination)
		{
			return (destination.CopyFrom(this));
		}

		public Subscription CopyFrom(Subscription source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Subscription FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}