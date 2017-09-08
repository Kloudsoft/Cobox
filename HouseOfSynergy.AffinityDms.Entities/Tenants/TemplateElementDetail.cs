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
	public partial class TemplateElementDetail:
		IEntity<TemplateElementDetail>
	{
		public virtual long Id { get; set; }
        public virtual long ElementId { get; set; }
        public virtual TemplateElement Element { get; set; }
		private ICollection<DocumentElement> _DocumentElements = null;
		public virtual ICollection<DocumentElement> DocumentElements { get { if (this._DocumentElements == null) { this._DocumentElements = new List<DocumentElement>(); } return (this._DocumentElements); } protected set { this._DocumentElements = value; } }
        public virtual string ElementDetailId { get; set; } // ElementDetailDivId
		public virtual int ElementType { get; set; }							
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Text { get; set; }
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public virtual string Width { get; set; }
        public virtual string Height { get; set; }
        public virtual string ForegroundColor { get; set; }
        public virtual string BackgroundColor { get; set; }
		public virtual int BorderStyle { get; set; }							// Raheel: Why not an Enum?
        public virtual string Value { get; set; }
		public virtual int SizeMode { get; set; }								// Raheel: Why not an Enum?
        public TemplateElementDetail ()
		{
        
		}
	
		public void Initialize()
		{
		}

		public TemplateElementDetail Clone()
		{
			return (new TemplateElementDetail().CopyFrom(this));
		}

		public TemplateElementDetail CopyTo(TemplateElementDetail destination)
		{
			return (destination.CopyFrom(this));
		}

		public TemplateElementDetail CopyFrom(TemplateElementDetail source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TemplateElementDetail FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}