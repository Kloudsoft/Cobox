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
	public partial class DocumentTagUser:
		IEntity<DocumentTagUser>
	{
		public virtual long Id { get; set; }

		public virtual string Value { get; set; }

		public virtual long DocumentId { get; set; }
		public virtual Document Document { get; set; }

		public virtual long UserId { get; set; }
		public virtual User User { get; set; }

		public DocumentTagUser ()
		{
		}
	
		public void Initialize()
		{
		}

		public DocumentTagUser Clone()
		{
			return (new DocumentTagUser().CopyFrom(this));
		}

		public DocumentTagUser CopyTo(DocumentTagUser destination)
		{
			return (destination.CopyFrom(this));
		}

		public DocumentTagUser CopyFrom(DocumentTagUser source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DocumentTagUser FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}