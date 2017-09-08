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
	public partial class RuleDetail:
		IEntity<RuleDetail>
	{
		public virtual long Id { get; set; }

		public virtual string Condition { get; set; }
        public virtual string Value { get; set; }
        public virtual string Action { get; set; }
        public virtual long TemplateField { get; set; }


        public virtual long WorkflowRuleId { get; set; }
        public virtual WorkflowRule WorkFlowRule { get; set; }

        public virtual long TemplateId { get; set; }
        public virtual Template Template { get; set; }

        public virtual long UserId { get; set; }
        public virtual User User { get; set; }

		public RuleDetail ()
		{
		}
	
		public void Initialize()
		{
		}

		public RuleDetail Clone()
		{
			return (new RuleDetail().CopyFrom(this));
		}

		public RuleDetail CopyTo(RuleDetail destination)
		{
			return (destination.CopyFrom(this));
		}

		public RuleDetail CopyFrom(RuleDetail source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public RuleDetail FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}