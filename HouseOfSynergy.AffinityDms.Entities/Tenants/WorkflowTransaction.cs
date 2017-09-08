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
	public partial class WorkflowTransaction:
		IEntity<WorkflowTransaction>
	{
		public virtual long Id { get; set; }

        public virtual long AssignedUserId { get; set; }
        public virtual string Action { get; set; }
        public virtual string Comments { get; set; }
        public virtual bool IsCommentsHide { get; set; }
        

		public WorkflowTransaction ()
		{
		}
	
		public void Initialize()
		{
		}

		public WorkflowTransaction Clone()
		{
			return (new WorkflowTransaction().CopyFrom(this));
		}

		public WorkflowTransaction CopyTo(WorkflowTransaction destination)
		{
			return (destination.CopyFrom(this));
		}

		public WorkflowTransaction CopyFrom(WorkflowTransaction source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public WorkflowTransaction FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}