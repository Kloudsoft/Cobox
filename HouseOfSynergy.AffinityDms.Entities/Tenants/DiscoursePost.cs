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
	public partial class DiscoursePost:
		IEntity<DiscoursePost>
	{
		public virtual long Id { get; set; }

		public virtual long DiscourseId { get; set; }
        public virtual Discourse Discourse { get; set; }

        public virtual long UserId { get; set; }
        public virtual User User { get; set; }

		private ICollection<DiscoursePostVersion> _Versions = null;
		public virtual ICollection<DiscoursePostVersion> Versions { get { if (this._Versions == null) { this._Versions = new List<DiscoursePostVersion> (); } return (this._Versions); } protected set { this._Versions = value; } }

		public DiscoursePost ()
		{
        }
	
		public void Initialize()
		{
		}

		public DiscoursePost Clone()
		{
			return (new DiscoursePost().CopyFrom(this));
		}

		public DiscoursePost CopyTo(DiscoursePost destination)
		{
			return (destination.CopyFrom(this));
		}

		public DiscoursePost CopyFrom(DiscoursePost source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public DiscoursePost FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}