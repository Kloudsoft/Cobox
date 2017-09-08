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
	public partial class DocumentElement:
		IEntity<DocumentElement>
	{
		public virtual long Id { get; set; }

		public virtual string OcrXml { get; set; }
		public virtual string OcrText { get; set; }

		public virtual long DocumentId { get; set; }
		public virtual Document Document { get; set; }

		public virtual long TemplateElementId { get; set; }
		public virtual TemplateElement TemplateElement { get; set; }
        public virtual long Confidience { get; set; }
        public virtual long? TemplateElementDetailId { get; set; }
		public virtual TemplateElementDetail TemplateElementDetail { get; set; }

		public DocumentElement ()
		{
			var document = new Document(); // Currently processing document.
			var template = new Template(); // Matched template against the above document.

			foreach (var templateElement in template.Elements)
			{
				var documentElement = new DocumentElement();

				documentElement.Document = document;
				documentElement.DocumentId = document.Id;

				documentElement.TemplateElement = templateElement;
				documentElement.TemplateElementId = templateElement.Id;

				// Don't know what this does.
				documentElement.TemplateElementDetail = null;
				documentElement.TemplateElementDetailId = null;

				documentElement.OcrXml = ""; // Xml zone result from LeadTools.
				documentElement.OcrText = ""; // Text-only result from LeadTools.
			}
		}
	
		public void Initialize()
		{
		}

		public DocumentElement Clone()
		{
			return (new DocumentElement().CopyFrom(this));
		}

		public DocumentElement CopyTo(DocumentElement destination)
		{
			return (destination.CopyFrom(this));
		}

		public DocumentElement CopyFrom(DocumentElement source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DocumentElement FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}