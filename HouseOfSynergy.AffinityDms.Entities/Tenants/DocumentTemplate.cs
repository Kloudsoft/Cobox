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
	public partial class DocumentTemplate:
		IEntity<DocumentTemplate>
	{
		public virtual long Id { get; set; }

		public virtual long DocumentId { get; set; }
		public virtual Document Document { get; set; }

		public virtual long TemplateId { get; set; }
		public virtual Template Template { get; set; }
        public virtual int? Confidence { get; set; }

		public DocumentTemplate ()
		{
		}
	
		public void Initialize()
		{
		}

		public DocumentTemplate Clone()
		{
			return (new DocumentTemplate().CopyFrom(this));
		}

		public DocumentTemplate CopyTo(DocumentTemplate destination)
		{
			return (destination.CopyFrom(this));
		}

		public DocumentTemplate CopyFrom(DocumentTemplate source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DocumentTemplate FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}