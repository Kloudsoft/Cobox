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
	public partial class TenantSubscription:
		IEntity<TenantSubscription>
	{
		public virtual long Id { get; set; }

		/// <summary>
		/// Represents the entity Id in the master database.
		/// This is only significant on the tenant's side since
		/// the entity is shared across both databases.
		/// This property is not a bound foriegn key and should not be configured as such.
		/// </summary>
		public virtual long MasterTenantSubscriptionId { get; set; }

		/// <summary>
		/// Determines whether this entity belongs to the master context or a local tenant context.
		/// </summary>
		public virtual EntityMasterTenantType TenantSubscriptionType { get; set; }

		public virtual bool IsDemo { get; set; }
        public virtual bool IsActive { get; set; }

		public virtual DateTime DateTimeStart { get; set; }
		public virtual DateTime DateTimeExpires { get; set; }

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

		public virtual bool RequireDelegationAcceptance { get; set; }

		/// <summary>
		/// Deprecated.
		/// </summary>
		public virtual DateTime Time { get; set; }

		public virtual long TenantId { get; set; }
		public virtual Tenant Tenant { get; set; }

		public virtual long SubscriptionId { get; set; }
		public virtual Subscription Subscription { get; set; }

		public TenantSubscription ()
		{
		}

		[NotMapped]
		public virtual int NumberOfPagesRemaining { get { return (this.NumberOfPagesAllowed - this.NumberOfPagesUsed); } }
		[NotMapped]
		public virtual int NumberOfFormsRemaining { get { return (this.NumberOfFormsAllowed - this.NumberOfFormsUsed); } }
		[NotMapped]
		public virtual int NumberOfTemplatesRemaining { get { return (this.NumberOfTemplatesAllowed - this.NumberOfTemplatesUsed); } }
	
		public void Initialize()
		{
		}

		public TenantSubscription Clone()
		{
			return (new TenantSubscription().CopyFrom(this));
		}

		public TenantSubscription CopyTo(TenantSubscription destination)
		{
			return (destination.CopyFrom(this));
		}

		public TenantSubscription CopyFrom(TenantSubscription source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TenantSubscription FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}