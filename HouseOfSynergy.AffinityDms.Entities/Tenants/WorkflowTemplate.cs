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
	public partial class WorkflowTemplate:
		IEntity<WorkflowTemplate>
	{
		public virtual long Id { get; set; }

		public virtual string Description { get; set; }
		public virtual int NoOfStages { get; set; }
		public virtual bool IsCompleted { get; set; }

		private ICollection<EntityWorkflowMapping> _EntityWorkflowMappings = null;
		public virtual ICollection<EntityWorkflowMapping> EntityWorkflowMappings { get { if (this._EntityWorkflowMappings == null) { this._EntityWorkflowMappings = new List<EntityWorkflowMapping>(); } return (this._EntityWorkflowMappings); } protected set { this._EntityWorkflowMappings = value; } }
		private ICollection<WorkflowInstance> _WorkFlowInstances = null;
		public virtual ICollection<WorkflowInstance> WorkFlowInstances { get { if (this._WorkFlowInstances == null) { this._WorkFlowInstances = new List<WorkflowInstance>(); } return (this._WorkFlowInstances); } protected set { this._WorkFlowInstances = value; } }
		private ICollection<WorkflowStage> _WorkflowStages = null;
		public virtual ICollection<WorkflowStage> WorkflowStages { get { if (this._WorkflowStages == null) { this._WorkflowStages = new List<WorkflowStage>(); } return (this._WorkflowStages); } protected set { this._WorkflowStages = value; } }

		public WorkflowTemplate ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowTemplate Clone()
		{
			return (new WorkflowTemplate().CopyFrom(this));
		}

		public WorkflowTemplate CopyTo(WorkflowTemplate destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowTemplate CopyFrom(WorkflowTemplate source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowTemplate FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}