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
    public partial class Template :
		IEntity<Template>
	{
		public virtual long Id { get; set; }
		public virtual TemplateEntityState EntityState { get; set; }
		public virtual string Title { get; set; }
        public virtual string Description { get; set; }
		public virtual TemplateType TemplateType { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsFinalized { get; set; }
        public virtual byte[] TemplateImage { get; set; }

        public virtual long TemplateOriginalId { get; set; }
        public virtual long? TemplateParent { get; set; }
        /// <summary>
        /// Used only for DTO purposes. Yuck!!!!
        /// TODO: Remove later and use model.
        /// </summary>
        public virtual int VersionCount { get; set; }
        /// <summary>
        /// Used only for DTO purposes. Yuck!!!!
        /// TODO: Remove later and use model.
        /// </summary>
        public virtual bool CheckedOut{ get; set; }
        [NotMapped]
        public virtual bool IsSelected { get; set; }

        public virtual int VersionMajor { get; set; }
        public virtual int VersionMinor { get; set; }

        public virtual long CheckedOutByUserId { get; set; }
        public virtual User CheckedOutByUser { get; set; }
        public DateTime CheckedOutDateTime { get; set; }

        [NotMapped]
        public bool IsVisible { get; set; }

        public virtual long UserId { get; set; }
        public virtual User User { get; set; }

        private ICollection<Document> _Documents = null;
		private ICollection<RuleDetail> _RuleDetails = null;
		private ICollection<TemplateElement> _Elements = null;
		private ICollection<TemplateTag> _TemplateTags = null;
		private ICollection<TemplateTagUser> _TemplateTagUsers = null;
		private ICollection<TemplateVersion> _TemplateVersions = null;
		private ICollection<DocumentTemplate> _DocumentTemplates = null;
		private ICollection<RuleDetailInstance> _RuleDetailInstances = null;
		private ICollection<DiscoursePostVersionAttachment> _DiscussionPostDocuments = null;
        private ICollection<UserTemplate> _TemplateUsers = null;

        public virtual ICollection<Document> Documents { get { if (this._Documents == null) { this._Documents = new List<Document>(); } return (this._Documents); } protected set { this._Documents = value; } }
		public virtual ICollection<RuleDetail> RuleDetails { get { if (this._RuleDetails == null) { this._RuleDetails = new List<RuleDetail>(); } return (this._RuleDetails); } protected set { this._RuleDetails = value; } }
		public virtual ICollection<TemplateElement> Elements { get { if (this._Elements == null) { this._Elements = new List<TemplateElement>(); } return (this._Elements); } protected set { this._Elements = value; } }
		public virtual ICollection<TemplateTag> TemplateTags { get { if (this._TemplateTags == null) { this._TemplateTags = new List<TemplateTag>(); } return (this._TemplateTags); } protected set { this._TemplateTags = value; } }
		public virtual ICollection<TemplateTagUser> TemplateTagUsers { get { if (this._TemplateTagUsers == null) { this._TemplateTagUsers = new List<TemplateTagUser>(); } return (this._TemplateTagUsers); } protected set { this._TemplateTagUsers = value; } }
		public virtual ICollection<TemplateVersion> TemplateVersions { get { if (this._TemplateVersions == null) { this._TemplateVersions = new List<TemplateVersion>(); } return (this._TemplateVersions); } protected set { this._TemplateVersions = value; } }
		public virtual ICollection<DocumentTemplate> DocumentTemplates { get { if (this._DocumentTemplates == null) { this._DocumentTemplates = new List<DocumentTemplate>(); } return (this._DocumentTemplates); } protected set { this._DocumentTemplates = value; } }
		public virtual ICollection<RuleDetailInstance> RuleDetailInstances { get { if (this._RuleDetailInstances == null) { this._RuleDetailInstances = new List<RuleDetailInstance>(); } return (this._RuleDetailInstances); } protected set { this._RuleDetailInstances = value; } }
		public virtual ICollection<DiscoursePostVersionAttachment> DiscussionPostDocuments { get { if (this._DiscussionPostDocuments == null) { this._DiscussionPostDocuments = new List<DiscoursePostVersionAttachment>(); } return (this._DiscussionPostDocuments); } protected set { this._DiscussionPostDocuments = value; } }

        public virtual ICollection<UserTemplate> TemplateUsers { get { if (this._TemplateUsers == null) { this._TemplateUsers = new List<UserTemplate>(); } return (this._TemplateUsers); } protected set { this._TemplateUsers = value; } }

        public Template ()
		{
        }

        [NotMapped]
        public Version Version { get { return (new Version (this.VersionMajor, this.VersionMinor)); } set { this.VersionMajor = value.Major; this.VersionMinor = value.Minor; } }

        public void Initialize()
		{
		}

		public Template Clone()
		{
			return (new Template().CopyFrom(this));
		}

		public Template CopyTo(Template destination)
		{
			return (destination.CopyFrom(this));
		}

		public Template CopyFrom(Template source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Template FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}