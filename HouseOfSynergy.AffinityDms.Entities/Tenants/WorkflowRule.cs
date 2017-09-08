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
	public partial class WorkflowRule:
		IEntity<WorkflowRule>
	{
		public virtual long Id { get; set; }

		public virtual string Description { get; set; }

        public virtual long WorkflowStageId { get; set; }
        public virtual WorkflowStage WorkflowStage { get; set; }

		private ICollection<RuleDetail> _RuleDetails = null;
		public virtual ICollection<RuleDetail> RuleDetails { get { if (this._RuleDetails == null) { this._RuleDetails = new List<RuleDetail>(); } return (this._RuleDetails); } protected set { this._RuleDetails = value; } }

		public WorkflowRule ()
		{
            this.RuleDetails = new List<RuleDetail>();
		}
	
		public void Initialize()
		{
		}

		public WorkflowRule Clone()
		{
			return (new WorkflowRule().CopyFrom(this));
		}

		public WorkflowRule CopyTo(WorkflowRule destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowRule CopyFrom(WorkflowRule source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowRule FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}