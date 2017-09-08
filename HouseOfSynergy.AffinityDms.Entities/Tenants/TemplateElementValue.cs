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
	public partial class TemplateElementValue:
		IEntity<TemplateElementValue>
	{
		public virtual long Id { get; set; }


        public virtual long? DocumentId { get; set; }
        public virtual Document Document { get; set; }

        public virtual long ElementId { get; set; }
        public virtual TemplateElement Element { get; set; }

        public virtual string Value { get; set; }

		public TemplateElementValue ()
		{

		}
	
		public void Initialize()
		{
		}

		public TemplateElementValue Clone()
		{
			return (new TemplateElementValue().CopyFrom(this));
		}

		public TemplateElementValue CopyTo(TemplateElementValue destination)
		{
			return (destination.CopyFrom(this));
		}

		public TemplateElementValue CopyFrom(TemplateElementValue source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TemplateElementValue FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}