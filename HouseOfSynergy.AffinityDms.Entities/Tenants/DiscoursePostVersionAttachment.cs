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
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Tenants
{
	public partial class DiscoursePostVersionAttachment:
		IEntity<DiscoursePostVersionAttachment>
	{
		public virtual long Id { get; set; }
		public virtual string Url { get; set; }

		public virtual DiscussionPostAttachmentType AttachmentType { get; set; }

        public virtual long PostVersionId { get; set; }
        public virtual DiscoursePostVersion PostVersion { get; set; }

        public virtual long? TemplateId { get; set; }
        public virtual Template Template { get; set; }

        public virtual long? DocumentId { get; set; }
        public virtual Document Document { get; set; }



        public virtual long? DiscourseId { get; set; }
        public virtual Discourse Discourse { get; set; }

        public virtual string FileNameClient { get; set; }

        /// <summary>
        /// The file name of the document on Azure Blob Storage (without the path).
        /// </summary>
        public virtual string FileNameServer { get; set; }

        public DiscoursePostVersionAttachment ()
		{
		}
	
		public void Initialize()
		{
		}

		public DiscoursePostVersionAttachment Clone()
		{
			return (new DiscoursePostVersionAttachment().CopyFrom(this));
		}

		public DiscoursePostVersionAttachment CopyTo(DiscoursePostVersionAttachment destination)
		{
			return (destination.CopyFrom(this));
		}

		public DiscoursePostVersionAttachment CopyFrom(DiscoursePostVersionAttachment source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DiscoursePostVersionAttachment FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}