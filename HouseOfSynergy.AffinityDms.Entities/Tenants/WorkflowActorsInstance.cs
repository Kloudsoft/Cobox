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
	public partial class WorkflowActorsInstance:
		IEntity<WorkflowActorsInstance>
	{
		public virtual long Id { get; set; }

        public virtual long WorkFlowStagesInstanceId { get; set; }
        public virtual WorkflowStagesInstance WorkFlowStagesInstances { get; set; }

        public virtual long UserId { get; set; }
        public virtual User User { get; set; }

		public WorkflowActorsInstance ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowActorsInstance Clone()
		{
			return (new WorkflowActorsInstance().CopyFrom(this));
		}

		public WorkflowActorsInstance CopyTo(WorkflowActorsInstance destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowActorsInstance CopyFrom(WorkflowActorsInstance source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowActorsInstance FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}