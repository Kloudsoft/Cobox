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
	public partial class Discourse:
		IEntity<Discourse>
	{
		public virtual long Id { get; set; }

        public virtual string Topic { get; set; }
        public virtual string Description { get; set; }

		private ICollection<DiscoursePost> _Posts = null;
		private ICollection<DiscourseUser> _Users = null;
        private ICollection<DiscoursePostVersion> _PostVersion = null;
        private ICollection<DiscoursePostVersionAttachment> _PostVersionAttachments = null;


        public virtual ICollection<DiscoursePost> Posts { get { if (this._Posts == null) { this._Posts = new List<DiscoursePost> (); } return (this._Posts); } protected set { this._Posts = value; } }
		public virtual ICollection<DiscourseUser> Users { get { if (this._Users == null) { this._Users = new List<DiscourseUser>(); } return (this._Users); } protected set { this._Users = value; } }

        public virtual ICollection<DiscoursePostVersion> PostVersion { get { if (this._PostVersion == null) { this._PostVersion = new List<DiscoursePostVersion>(); } return (this._PostVersion); } protected set { this._PostVersion = value; } }

        public virtual ICollection<DiscoursePostVersionAttachment> PostVersionAttachments { get { if (this._PostVersionAttachments == null) { this._PostVersionAttachments = new List<DiscoursePostVersionAttachment>(); } return (this._PostVersionAttachments); } protected set { this._PostVersionAttachments = value; } }


        public Discourse ()
		{
		}
	
		public void Initialize()
		{
		}

		public Discourse Clone()
		{
			return (new Discourse().CopyFrom(this));
		}

		public Discourse CopyTo(Discourse destination)
		{
			return (destination.CopyFrom(this));
		}

		public Discourse CopyFrom(Discourse source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Discourse FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}