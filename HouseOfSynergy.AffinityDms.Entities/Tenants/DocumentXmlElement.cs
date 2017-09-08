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
	public partial class DocumentXmlElement:
		IEntity<DocumentXmlElement>
	{
		public virtual long Id { get; set; }

		public virtual string OcrXml { get; set; }
		public virtual string OcrText { get; set; }

		public virtual long DocumentId { get; set; }
		public virtual Document Document { get; set; }

		public DocumentXmlElement ()
		{
		}
	
		public void Initialize()
		{
		}

		public DocumentXmlElement Clone()
		{
			return (new DocumentXmlElement().CopyFrom(this));
		}

		public DocumentXmlElement CopyTo(DocumentXmlElement destination)
		{
			return (destination.CopyFrom(this));
		}

		public DocumentXmlElement CopyFrom(DocumentXmlElement source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DocumentXmlElement FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}