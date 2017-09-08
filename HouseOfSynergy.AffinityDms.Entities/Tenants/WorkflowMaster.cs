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
	public partial class WorkflowMaster:
		IEntity<WorkflowMaster>
	{
		public virtual long Id { get; set; }

		public virtual string Description { get; set; }
		public virtual int NoofStages { get; set; }
		public virtual bool IsCompleted { get; set; }

		private ICollection<EntityWorkflowMapping> _EntityWorkflowMappings = null;
		public virtual ICollection<EntityWorkflowMapping> EntityWorkflowMappings { get { if (this._EntityWorkflowMappings == null) { this._EntityWorkflowMappings = new List<EntityWorkflowMapping>(); } return (this._EntityWorkflowMappings); } protected set { this._EntityWorkflowMappings = value; } }
		private ICollection<WorkflowStage> _WorkflowStages = null;
		public virtual ICollection<WorkflowStage> WorkflowStages { get { if (this._WorkflowStages == null) { this._WorkflowStages = new List<WorkflowStage>(); } return (this._WorkflowStages); } protected set { this._WorkflowStages = value; } }

		public WorkflowMaster ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowMaster Clone()
		{
			return (new WorkflowMaster().CopyFrom(this));
		}

		public WorkflowMaster CopyTo(WorkflowMaster destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowMaster CopyFrom(WorkflowMaster source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowMaster FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}