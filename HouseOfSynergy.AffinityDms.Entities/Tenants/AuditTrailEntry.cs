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
	public class AuditTrailEntry:
		IEntity<AuditTrailEntry>
	{
		public virtual long Id { get; set; }
		public virtual long? TransactionId { get; set; }
		public virtual DateTime DateTime { get; set; } = DateTime.UtcNow;
		public virtual EntityType EntityType { get; set; }
        public virtual long EntityTypeId { get; set; }
        public virtual AuditTrailActionType AuditTrailActionType { get; set; }
		public virtual string Description { get; set; }

		public virtual long UserId { get; set; }
		public virtual User User { get; set; }

		public virtual long? EntityUserId { get; set; }
		public virtual User EntityUser { get; set; }

		public virtual long? EntityDocumentOriginalId { get; set; }
		public virtual long? EntityDocumentParentId { get; set; }

		public virtual long? EntityDiscourseId { get; set; }
		public virtual long? EntityDiscoursePostId { get; set; }
		public virtual long? EntityDiscoursePostVersionId { get; set; }
		public virtual long? EntityDiscoursePostVersionAttachmentId { get; set; }


		public AuditTrailEntry ()
		{
		}

		public AuditTrailEntry
		(
			EntityType entityType,
			AuditTrailActionType auditTrailActionType,
			string description,
			long userId
		)
		{
			this.DateTime = DateTime.UtcNow;
			this.EntityType = entityType;
			this.AuditTrailActionType = auditTrailActionType;
			this.Description = description;
			this.UserId = userId;
		}

		public AuditTrailEntry
		(
			DateTime dateTime,
			EntityType entityType,
			AuditTrailActionType auditTrailActionType,
			string description,
			long userId
		)
		{
			this.DateTime = dateTime;
			this.EntityType = entityType;
			this.AuditTrailActionType = auditTrailActionType;
			this.Description = description;
			this.UserId = userId;
		}

		public void Initialize()
		{
		}

		public AuditTrailEntry Clone()
		{
			return (new AuditTrailEntry().CopyFrom(this));
		}

		public AuditTrailEntry CopyTo(AuditTrailEntry destination)
		{
			return (destination.CopyFrom(this));
		}

		public AuditTrailEntry CopyFrom(AuditTrailEntry source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public AuditTrailEntry FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}