using HouseOfSynergy.AffinityDms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
    public class TemplateElementListAndElementDetailListViewModel
    {
        public Template template { get; set; }//{ if (this.template == null) { this.template = new Template(); } return (this.template); } set { this.template = value; }
        public List<TemplateElement> elements { get; set; }
        public List<TemplateElementDetail> elementsdetails { get; set; }
    }
}