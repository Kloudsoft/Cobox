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
    public class UserTemplate:
        IEntity<UserTemplate>
    {
        public virtual long Id { get; set; }

        public virtual User User { get; set; }
        public virtual long UserId { get; set; }
        public bool IsActive { get; set; }
        public virtual Template Template { get; set; }
        public virtual long TemplateId { get; set; }

        public UserTemplate()
        {
        }

        public void Initialize()
        {
        }

        public UserTemplate Clone()
        {
            return (new UserTemplate().CopyFrom(this));
        }

        public UserTemplate CopyTo(UserTemplate destination)
        {
            return (destination.CopyFrom(this));
        }

        public UserTemplate CopyFrom(UserTemplate source)
        {
            return (ReflectionUtilities.Copy(source, this));
        }

        public XmlElement ToXmlElement(XmlDocument document)
        {
            var element = ReflectionUtilities.ToXmlElement(document, this);

            return (element);
        }

        public UserTemplate FromXmlElement(XmlElement element)
        {
            ReflectionUtilities.FromXmlElement(this, element);

            return (this);
        }
    }
}