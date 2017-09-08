using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.PowerTools.Library.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
    public class UserDocument:
        IEntity<UserDocument>
    {
        public virtual long Id { get; set; }

        public virtual User User { get; set; }
        public virtual long UserId { get; set; }
        public bool IsActive { get; set; }
        public virtual Document Document { get; set; }
        public virtual long DocumentId { get; set; }

        public UserDocument()
        {
        }

        public void Initialize()
        {
        }

        public UserDocument Clone()
        {
            return (new UserDocument().CopyFrom(this));
        }

        public UserDocument CopyTo(UserDocument destination)
        {
            return (destination.CopyFrom(this));
        }

        public UserDocument CopyFrom(UserDocument source)
        {
            return (ReflectionUtilities.Copy(source, this));
        }

        public XmlElement ToXmlElement(XmlDocument document)
        {
            var element = ReflectionUtilities.ToXmlElement(document, this);

            return (element);
        }

        public UserDocument FromXmlElement(XmlElement element)
        {
            ReflectionUtilities.FromXmlElement(this, element);

            return (this);
        }
    }
}