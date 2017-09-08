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
	public partial class EntityWorkflowMapping:
		IEntity<EntityWorkflowMapping>
	{
		public virtual long Id { get; set; }

		public virtual long WorkflowMasterId { get; set; }
        public virtual WorkflowMaster WorkflowMaster { get; set; }

        public virtual long WorkflowTemplateId { get; set; }
        public virtual WorkflowTemplate WorkflowTemplate { get; set; }

        public virtual long DocumentId { get; set; }
        public virtual Document Document { get; set; }

		private ICollection<WorkflowStagesInstance> _WorkFlowStagesInstances = null;
		public virtual ICollection<WorkflowStagesInstance> WorkFlowStagesInstances { get { if (this._WorkFlowStagesInstances == null) { this._WorkFlowStagesInstances = new List<WorkflowStagesInstance>(); } return (this._WorkFlowStagesInstances); } protected set { this._WorkFlowStagesInstances = value; } }

		public EntityWorkflowMapping ()
		{
		}
	
		public void Initialize()
		{
		}

		public EntityWorkflowMapping Clone()
		{
			return (new EntityWorkflowMapping().CopyFrom(this));
		}

		public EntityWorkflowMapping CopyTo(EntityWorkflowMapping destination)
		{
			return (destination.CopyFrom(this));
		}

		public EntityWorkflowMapping CopyFrom(EntityWorkflowMapping source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public EntityWorkflowMapping FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}