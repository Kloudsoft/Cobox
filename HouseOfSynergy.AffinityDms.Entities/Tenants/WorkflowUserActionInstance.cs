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
	public partial class WorkflowUserActionInstance:
		IEntity<WorkflowUserActionInstance>
	{
		public virtual long Id { get; set; }

        public virtual long WorkflowStageInstanceId { get; set; }
        public virtual WorkflowStagesInstance WorkFlowStagesInstance { get; set; }

        public virtual long WorkflowActionId { get; set; }
        public virtual WorkflowAction WorkflowAction { get; set; }

		public WorkflowUserActionInstance ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowUserActionInstance Clone()
		{
			return (new WorkflowUserActionInstance().CopyFrom(this));
		}

		public WorkflowUserActionInstance CopyTo(WorkflowUserActionInstance destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowUserActionInstance CopyFrom(WorkflowUserActionInstance source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowUserActionInstance FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}