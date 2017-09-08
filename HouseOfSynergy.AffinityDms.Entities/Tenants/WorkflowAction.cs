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
	public partial class WorkflowAction:
		IEntity<WorkflowAction>
	{
		public virtual long Id { get; set; }

        public virtual string ActionDescription { get; set; }

        public virtual long WorkflowActorId { get; set; }
        public virtual WorkflowActor WorkflowActor { get; set; }

		private ICollection<WorkflowUserAction> _WorkflowUserActions = null;
		public virtual ICollection<WorkflowUserAction> WorkflowUserActions { get { if (this._WorkflowUserActions == null) { this._WorkflowUserActions = new List<WorkflowUserAction>(); } return (this._WorkflowUserActions); } protected set { this._WorkflowUserActions = value; } }

		public WorkflowAction ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowAction Clone()
		{
			return (new WorkflowAction().CopyFrom(this));
		}

		public WorkflowAction CopyTo(WorkflowAction destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowAction CopyFrom(WorkflowAction source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowAction FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}