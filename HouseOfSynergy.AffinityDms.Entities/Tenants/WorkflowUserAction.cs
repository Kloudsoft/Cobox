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
	public partial class WorkflowUserAction:
		IEntity<WorkflowUserAction>
	{
		public virtual long Id { get; set; }

        public virtual long WorkflowActionId { get; set; }
        public virtual WorkflowAction WorkflowAction { get; set; }

        public virtual long WorkflowStagesId { get; set; }
        public virtual WorkflowStage WorkflowStage { get; set; }

		public WorkflowUserAction ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowUserAction Clone()
		{
			return (new WorkflowUserAction().CopyFrom(this));
		}

		public WorkflowUserAction CopyTo(WorkflowUserAction destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowUserAction CopyFrom(WorkflowUserAction source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowUserAction FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}