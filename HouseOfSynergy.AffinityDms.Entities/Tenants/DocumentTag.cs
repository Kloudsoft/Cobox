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
	public partial class DocumentTag:
		IEntity<DocumentTag>
	{
		public virtual long Id { get; set; }

		public virtual string Value { get; set; }

		public virtual long DocumentId { get; set; }
		public virtual Document Document { get; set; }

		public DocumentTag ()
		{
		}
	
		public void Initialize()
		{
		}

		public DocumentTag Clone()
		{
			return (new DocumentTag().CopyFrom(this));
		}

		public DocumentTag CopyTo(DocumentTag destination)
		{
			return (destination.CopyFrom(this));
		}

		public DocumentTag CopyFrom(DocumentTag source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DocumentTag FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}