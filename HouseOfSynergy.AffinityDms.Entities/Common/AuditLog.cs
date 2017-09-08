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
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Common
{
	public partial class AuditLog:
		IEntity<AuditLog>
	{
		public virtual long Id { get; set; }
		public virtual Screen Screen { get; set; }
		public virtual DateTime LogTime { get; set; }
		public virtual int ActiveUserId { get; set; }
		public virtual int ActualUserId { get; set; }
		public virtual int ScreenId { get; set; }
		public virtual string FieldName { get; set; }
		public virtual string PreviousValue { get; set; }
		public virtual string NewValue { get; set; }
		public virtual int Variance { get; set; }

		public AuditLog ()
		{
		}

		public void Initialize ()
		{
		}

		public AuditLog Clone()
		{
			return (new AuditLog().CopyFrom(this));
		}

		public AuditLog CopyTo(AuditLog destination)
		{
			return (destination.CopyFrom(this));
		}

		public AuditLog CopyFrom(AuditLog source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public AuditLog FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}