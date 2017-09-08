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
	public partial class TemplateVersion:
		IEntity<TemplateVersion>
	{
		public virtual long Id { get; set; }

        public virtual long TemplateCurrentId { get; set; }
        public virtual Template TemplateCurrent { get; set; }

        public virtual long TemplateOriginalId { get; set; }
        public virtual Template TemplateOriginal { get; set; }
        
        public virtual long? TemplateParentId { get; set; }
        public virtual Template TemplateParent { get; set; }

        //public virtual int VersionNumber { get; set; }
        public virtual int VersionMajor { get; set; }
        public virtual int VersionMinor { get; set; }

		public TemplateVersion ()
		{
		}
	
		public void Initialize()
		{
		}

		public TemplateVersion Clone()
		{
			return (new TemplateVersion().CopyFrom(this));
		}

		public TemplateVersion CopyTo(TemplateVersion destination)
		{
			return (destination.CopyFrom(this));
		}

		public TemplateVersion CopyFrom(TemplateVersion source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TemplateVersion FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}