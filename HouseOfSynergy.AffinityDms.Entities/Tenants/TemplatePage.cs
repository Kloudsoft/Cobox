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
	public partial class TemplatePage:
		IEntity<TemplatePage>
	{
		public virtual long Id { get; set; }

        public virtual long? TemplateVersionId { get; set; }
        public virtual TemplateVersion TemplateVersion { get; set; }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual long Number { get; set; }
        public virtual float Width { get; set; }
        public virtual float Height { get; set; }
        public virtual byte[] ImageBackground { get; set; }

		public TemplatePage ()
		{
		}
	
		public void Initialize()
		{
		}

		public TemplatePage Clone()
		{
			return (new TemplatePage().CopyFrom(this));
		}

		public TemplatePage CopyTo(TemplatePage destination)
		{
			return (destination.CopyFrom(this));
		}

		public TemplatePage CopyFrom(TemplatePage source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TemplatePage FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}