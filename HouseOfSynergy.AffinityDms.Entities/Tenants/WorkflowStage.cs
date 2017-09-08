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
	public partial class WorkflowStage:
		IEntity<WorkflowStage>
	{
		public virtual long Id { get; set; }

		public virtual long StageNo { get; set; }
		public virtual string StageDescription { get; set; }
		public virtual bool IsVoting { get; set; }

		public virtual long WorkflowMasterId { get; set; }
        public virtual WorkflowMaster WorkflowMaster { get; set; }

        public virtual long WorkflowTemplateId { get; set; }
        public virtual WorkflowTemplate WorkflowTemplate { get; set; }

		private ICollection<WorkflowActor> _WorkflowActors = null;
		public virtual ICollection<WorkflowActor> WorkflowActors { get { if (this._WorkflowActors == null) { this._WorkflowActors = new List<WorkflowActor>(); } return (this._WorkflowActors); } protected set { this._WorkflowActors = value; } }
		private ICollection<WorkflowRule> _WorkFlowRules = null;
		public virtual ICollection<WorkflowRule> WorkFlowRules { get { if (this._WorkFlowRules == null) { this._WorkFlowRules = new List<WorkflowRule>(); } return (this._WorkFlowRules); } protected set { this._WorkFlowRules = value; } }
		private ICollection<WorkflowUserAction> _WorkflowUserActions = null;
		public virtual ICollection<WorkflowUserAction> WorkflowUserActions { get { if (this._WorkflowUserActions == null) { this._WorkflowUserActions = new List<WorkflowUserAction>(); } return (this._WorkflowUserActions); } protected set { this._WorkflowUserActions = value; } }

		public WorkflowStage ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowStage Clone()
		{
			return (new WorkflowStage().CopyFrom(this));
		}

		public WorkflowStage CopyTo(WorkflowStage destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowStage CopyFrom(WorkflowStage source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowStage FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}