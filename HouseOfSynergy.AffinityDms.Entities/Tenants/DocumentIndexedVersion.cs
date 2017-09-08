//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Net;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;
//using HouseOfSynergy.AffinityDms.Entities.Common;
//using HouseOfSynergy.PowerTools.Library.Extensions;
//using HouseOfSynergy.PowerTools.Library.Interfaces;
//using HouseOfSynergy.PowerTools.Library.Utility;

//namespace HouseOfSynergy.AffinityDms.Entities.Tenants
//{
//    public partial class DocumentIndexedVersion :
//        IEntity<DocumentIndexedVersion>
//    {
//        public virtual long Id { get; set; }

//        public virtual DateTime DateTime { get; set; }
//        public virtual string Version { get; set; }
//        public virtual long UserId { get; set; }
//        public virtual bool VersionSource { get; set; }
//        public virtual bool IsFinalized { get; set; }

//        public virtual long DocumentId { get; set; }
//        public virtual Document Document { get; set; }

//        private ICollection<TemplateElementValue> _ElementValues = null;
//        public virtual ICollection<TemplateElementValue> ElementValues { get { if (this._ElementValues == null) { this._ElementValues = new List<TemplateElementValue>(); } return (this._ElementValues); } protected set { this._ElementValues = value; } }

//        public DocumentIndexedVersion()
//        {
//        }

//        public void Initialize()
//        {
//        }

//        public DocumentIndexedVersion Clone()
//        {
//            return (new DocumentIndexedVersion().CopyFrom(this));
//        }

//        public DocumentIndexedVersion CopyTo(DocumentIndexedVersion destination)
//        {
//            return (destination.CopyFrom(this));
//        }

//        public DocumentIndexedVersion CopyFrom(DocumentIndexedVersion source)
//        {
//            return (ReflectionUtilities.Copy(source, this));
//        }

//        public XmlElement ToXmlElement(XmlDocument document)
//        {
//            var element = ReflectionUtilities.ToXmlElement(document, this);

//            return (element);
//        }

//        public DocumentIndexedVersion FromXmlElement(XmlElement element)
//        {
//            ReflectionUtilities.FromXmlElement(this, element);

//            return (this);
//        }
//    }
//}