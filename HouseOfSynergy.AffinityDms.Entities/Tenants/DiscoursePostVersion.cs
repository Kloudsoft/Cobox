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
	public partial class DiscoursePostVersion:
		IEntity<DiscoursePostVersion>
	{
		public virtual long Id { get; set; }

		public virtual DateTime DateTime { get; set; }
		public virtual string Comments { get; set; }
		public virtual long PostId { get; set; }
        public virtual DiscoursePost Post { get; set; }
        public virtual long DiscourseId { get; set; }
        public virtual Discourse Discourse { get; set; }
        private ICollection<DiscoursePostVersionAttachment> _Attachments = null;
		public virtual ICollection<DiscoursePostVersionAttachment> Attachments { get { if (this._Attachments == null) { this._Attachments = new List<DiscoursePostVersionAttachment>(); } return (this._Attachments); } protected set { this._Attachments = value; } }

		public DiscoursePostVersion ()
		{
        }
	
		public void Initialize()
		{
		}

		public DiscoursePostVersion Clone ()
		{
			return (new DiscoursePostVersion ().CopyFrom(this));
		}

		public DiscoursePostVersion CopyTo (DiscoursePostVersion destination)
		{
			return (destination.CopyFrom(this));
		}

		public DiscoursePostVersion CopyFrom (DiscoursePostVersion source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DiscoursePostVersion FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}