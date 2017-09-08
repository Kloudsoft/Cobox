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
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
	public partial class TemplateElement:
		IEntity<TemplateElement>
	{
		public virtual long Id { get; set; }

        public virtual string ElementId { get; set; }
		public virtual ElementType ElementType { get; set; }					// Raheel: Why not an Enum?
		public virtual ElementDataType ElementDataType { get; set; }				// Raheel: Why not an Enum?
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Text { get; set; }
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public virtual float X2 { get; set; }
        public virtual float Y2 { get; set; }
        public virtual float Radius { get; set; }
        public virtual float Diameter { get; set; }
        public virtual string Width { get; set; }
        public virtual string Height { get; set; }
        public virtual float DivX { get; set; }
        public virtual float DivY { get; set; }
        public virtual string DivWidth { get; set; }
        public virtual string DivHeight { get; set; }
        public virtual string MinHeight { get; set; }
        public virtual string MinWidth { get; set; }
        public virtual string ForegroundColor { get; set; }
        public virtual string BackgroundColor { get; set; }
        public virtual string Hint { get; set; }
        public virtual string MinChar { get; set; }
        public virtual string MaxChar { get; set; }
        public virtual string DateTime { get; set; }
        public virtual string FontName { get; set; }
        public virtual float FontSize { get; set; }
		public virtual int FontStyle { get; set; }							// Raheel: Why not an Enum?
        public virtual string FontColor { get; set; }
		public virtual int BorderStyle { get; set; }						// Raheel: Why not an Enum?
		public virtual BarcodeType BarcodeType { get; set; }						// Raheel: Why not an Enum?
        public virtual string Value { get; set; }
        public virtual string ImageSource { get; set; }
        public virtual int SizeMode { get; set; }
        public virtual string IsSelected { get; set; }
        public virtual int FontGraphicsUnit { get; set; }
        public virtual int ColorForegroundA { get; set; }
        public virtual int ColorForegroundR { get; set; }
        public virtual int ColorForegroundG { get; set; }
        public virtual int ColorForegroundB { get; set; }
        public virtual int ColorBackroundA { get; set; }
        public virtual int ColorBackroundR { get; set; }
        public virtual int ColorBackroundG { get; set; }
        public virtual int ColorBackroundB { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual int ElementMobileOrdinal { get; set; }
		public virtual ElementIndexType ElementIndexType { get; set; }              // Raheel: Why not an Enum?
        public virtual DocumentIndexDataType DocumentIndexDataType { get; set; }              

        public virtual string BarcodeValue { get; set; }
		public virtual string Discriminator { get; set; }

		public virtual long TemplateId { get; set; }
		public virtual Template Template { get; set; }

		private ICollection<TemplateElementDetail> _ElementDetails = null;
		public virtual ICollection<TemplateElementDetail> ElementDetails { get { if (this._ElementDetails == null) { this._ElementDetails = new List<TemplateElementDetail>(); } return (this._ElementDetails); } protected set { this._ElementDetails = value; } }
		private ICollection<TemplateElementValue> _ElementValues = null;
		public virtual ICollection<TemplateElementValue> ElementValues { get { if (this._ElementValues == null) { this._ElementValues = new List<TemplateElementValue>(); } return (this._ElementValues); } protected set { this._ElementValues = value; } }

		private ICollection<DocumentElement> _DocumentElements = null;
		public virtual ICollection<DocumentElement> DocumentElements { get { if (this._DocumentElements == null) { this._DocumentElements = new List<DocumentElement>(); } return (this._DocumentElements); } protected set { this._DocumentElements = value; } }

        private ICollection<DocumentCorrectiveIndexValue> _DocumentCorrectiveIndexValue = null;
        public virtual ICollection<DocumentCorrectiveIndexValue> DocumentCorrectiveIndexValues { get { if (this._DocumentCorrectiveIndexValue == null) { this._DocumentCorrectiveIndexValue = new List<DocumentCorrectiveIndexValue>(); } return (this._DocumentCorrectiveIndexValue); } protected set { this._DocumentCorrectiveIndexValue = value; } }

        public TemplateElement ()
		{
		}
	
		public void Initialize()
		{
		}

		public TemplateElement Clone()
		{
			return (new TemplateElement().CopyFrom(this));
		}

		public TemplateElement CopyTo(TemplateElement destination)
		{
			return (destination.CopyFrom(this));
		}

		public TemplateElement CopyFrom(TemplateElement source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public TemplateElement FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}