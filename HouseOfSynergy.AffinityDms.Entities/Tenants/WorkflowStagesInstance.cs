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
	public partial class WorkflowStagesInstance:
		IEntity<WorkflowStagesInstance>
	{
		public virtual long Id { get; set; }

		public virtual int StageNo { get; set; }
		public virtual string StageDescription { get; set; }
		public virtual bool IsVoting { get; set; }

		public virtual long EntityWorkflowMappingId { get; set; }
        public virtual EntityWorkflowMapping EntityWorkflowMappings { get; set; }

		private ICollection<WorkflowActorsInstance> _WorkFlowActorsInstances = null;
		public virtual ICollection<WorkflowActorsInstance> WorkFlowActorsInstances { get { if (this._WorkFlowActorsInstances == null) { this._WorkFlowActorsInstances = new List<WorkflowActorsInstance>(); } return (this._WorkFlowActorsInstances); } protected set { this._WorkFlowActorsInstances = value; } }
		private ICollection<WorkflowRuleInstance> _WorkFlowRuleInstances = null;
		public virtual ICollection<WorkflowRuleInstance> WorkFlowRuleInstances { get { if (this._WorkFlowRuleInstances == null) { this._WorkFlowRuleInstances = new List<WorkflowRuleInstance>(); } return (this._WorkFlowRuleInstances); } protected set { this._WorkFlowRuleInstances = value; } }
		private ICollection<WorkflowUserActionInstance> _WorkFlowUserActionInstances = null;
		public virtual ICollection<WorkflowUserActionInstance> WorkFlowUserActionInstances { get { if (this._WorkFlowUserActionInstances == null) { this._WorkFlowUserActionInstances = new List<WorkflowUserActionInstance>(); } return (this._WorkFlowUserActionInstances); } protected set { this._WorkFlowUserActionInstances = value; } }

		public WorkflowStagesInstance ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowStagesInstance Clone()
		{
			return (new WorkflowStagesInstance().CopyFrom(this));
		}

		public WorkflowStagesInstance CopyTo(WorkflowStagesInstance destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowStagesInstance CopyFrom(WorkflowStagesInstance source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowStagesInstance FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}