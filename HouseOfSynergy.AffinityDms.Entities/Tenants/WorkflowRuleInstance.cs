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
	public partial class WorkflowRuleInstance:
		IEntity<WorkflowRuleInstance>
	{
		public virtual long Id { get; set; }

		public virtual string Description { get; set; }

		public virtual long WorkFlowStagesInstanceId { get; set; }
		public virtual WorkflowStagesInstance WorkFlowStagesInstance { get; set; }

		private ICollection<RuleDetailInstance> _RuleDetailInstances = null;
		public virtual ICollection<RuleDetailInstance> RuleDetailInstances { get { if (this._RuleDetailInstances == null) { this._RuleDetailInstances = new List<RuleDetailInstance>(); } return (this._RuleDetailInstances); } protected set { this._RuleDetailInstances = value; } }

		public WorkflowRuleInstance ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowRuleInstance Clone()
		{
			return (new WorkflowRuleInstance().CopyFrom(this));
		}

		public WorkflowRuleInstance CopyTo(WorkflowRuleInstance destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowRuleInstance CopyFrom(WorkflowRuleInstance source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowRuleInstance FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}