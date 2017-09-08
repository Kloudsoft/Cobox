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
	public partial class TemplateInstance:
		IEntity<TemplateInstance>
	{
		public virtual long Id { get; set; }

        public virtual long TemplateVersionId { get; set; }
        public virtual TemplateVersion TemplateVersion { get; set; }

        public virtual string Number { get; set; }
        
		public TemplateInstance ()
		{

		}
	
		public void Initialize()
		{
		}

		public TemplateInstance Clone()
		{
			return (new TemplateInstance().CopyFrom(this));
		}

		public TemplateInstance CopyTo(TemplateInstance destination)
		{
			return (destination.CopyFrom(this));
		}

		public TemplateInstance CopyFrom(TemplateInstance source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TemplateInstance FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}