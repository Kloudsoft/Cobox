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
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
    public partial class Vendor 
    {
        
        public virtual long Id { get; set; }

        public virtual long VendorId { get; set; }

        [Required(ErrorMessage ="Enter Vendor Name")]
        [DisplayName("Vendor Name")]
        public virtual string VendorName { get; set; }

        [Required(ErrorMessage = "Enter GST")]
        [DisplayName("GST")]
        public virtual string Gst { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [DisplayName("Address")]
        public virtual string Address { get; set; }

        [Required(ErrorMessage = "Enter Phone")]
        [DisplayName("Phone")]
        public virtual string Phone { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [DisplayName("Email")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "Enter Contact Person")]
        [DisplayName("Contact Person")]
        public virtual string ContactPerson { get; set; }

        //public virtual string CreateOn { get; set; }

        public Vendor()
        {
        }

        public void Initialize()
        {
        }

        public Vendor Clone()
        {
            return (new Vendor().CopyFrom(this));
        }

        public Vendor CopyTo(Vendor destination)
        {
            return (destination.CopyFrom(this));
        }

        public Vendor CopyFrom(Vendor source)
        {
            return (ReflectionUtilities.Copy(source, this));
        }

        //public XmlElement ToXmlElement(XmlDocument document)
        //{
        //    var element = ReflectionUtilities.ToXmlElement(document, this);

        //    return (element);
        //}

        //public Vendor FromXmlElement(XmlElement element)
        //{
        //    ReflectionUtilities.FromXmlElement(this, element);

        //    return (this);
        //}
    }

}
