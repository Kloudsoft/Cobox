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
	public partial class TemplateTag:
		IEntity<TemplateTag>
	{
		public virtual long Id { get; set; }

		public virtual string Value { get; set; }

		public virtual long TemplateId { get; set; }
		public virtual Template Template { get; set; }

		public TemplateTag ()
		{
		}
	
		public void Initialize()
		{
		}

		public TemplateTag Clone()
		{
			return (new TemplateTag().CopyFrom(this));
		}

		public TemplateTag CopyTo(TemplateTag destination)
		{
			return (destination.CopyFrom(this));
		}

		public TemplateTag CopyFrom(TemplateTag source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TemplateTag FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}