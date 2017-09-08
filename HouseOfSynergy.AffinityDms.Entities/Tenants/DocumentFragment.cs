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
	public partial class DocumentFragment:
		IEntity<DocumentFragment>
	{
		public virtual long Id { get; set; }

		public virtual string FullTextOcr { get; set; }
        public virtual long DocumentId { get; set; }
        public virtual Document Document { get; set; }

		public DocumentFragment ()
		{
		}
	
		public void Initialize()
		{
		}

		public DocumentFragment Clone()
		{
			return (new DocumentFragment().CopyFrom(this));
		}

		public DocumentFragment CopyTo(DocumentFragment destination)
		{
			return (destination.CopyFrom(this));
		}

		public DocumentFragment CopyFrom(DocumentFragment source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DocumentFragment FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}