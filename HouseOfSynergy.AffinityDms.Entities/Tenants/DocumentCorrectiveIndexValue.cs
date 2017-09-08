using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
    public partial class DocumentCorrectiveIndexValue:
        IEntity<DocumentCorrectiveIndexValue>
    {
        public virtual long Id { get; set; }
        public virtual Document Document { get; set; }
        public virtual long DocumentId { get; set; }
        public TemplateElement IndexElement { get; set; }
        public long IndexElementId { get; set; }
        public virtual User Indexer { get; set; }
        public long IndexerId { get; set; }
        public string IndexValue { get; set; }
        public DocumentCorrectiveIndexValueStatus Status { get; set; }

        public DocumentCorrectiveIndexValue()
        {
        }

        public void Initialize()
        {
        }

        public DocumentCorrectiveIndexValue Clone()
        {
            return (new DocumentCorrectiveIndexValue().CopyFrom(this));
        }

        public DocumentCorrectiveIndexValue CopyTo(DocumentCorrectiveIndexValue destination)
        {
            return (destination.CopyFrom(this));
        }

        public DocumentCorrectiveIndexValue CopyFrom(DocumentCorrectiveIndexValue source)
        {
            return (ReflectionUtilities.Copy(source, this));
        }

        public XmlElement ToXmlElement(XmlDocument document)
        {
            var element = ReflectionUtilities.ToXmlElement(document, this);

            return (element);
        }

        public DocumentCorrectiveIndexValue FromXmlElement(XmlElement element)
        {
            ReflectionUtilities.FromXmlElement(this, element);

            return (this);
        }


    }
}
