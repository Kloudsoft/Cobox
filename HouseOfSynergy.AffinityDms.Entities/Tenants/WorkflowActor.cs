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
	public partial class WorkflowActor:
		IEntity<WorkflowActor>
	{
		public virtual long Id { get; set; }

        public virtual long WorkflowStageId { get; set; }
        public virtual WorkflowStage WorkflowStage { get; set; }

        public virtual long RoleId { get; set; }

		private ICollection<WorkflowAction> _WorkflowActions = null;
		public virtual ICollection<WorkflowAction> WorkflowActions { get { if (this._WorkflowActions == null) { this._WorkflowActions = new List<WorkflowAction>(); } return (this._WorkflowActions); } protected set { this._WorkflowActions = value; } }

		public WorkflowActor ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowActor Clone()
		{
			return (new WorkflowActor().CopyFrom(this));
		}

		public WorkflowActor CopyTo(WorkflowActor destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowActor CopyFrom(WorkflowActor source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowActor FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}