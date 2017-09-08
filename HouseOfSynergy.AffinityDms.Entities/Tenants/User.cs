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
    public partial class User:
		IEntity<User>
	{
		public virtual long Id { get; set; }

		public virtual long TenantId { get; set; }
		public virtual Tenant Tenant { get; set; }

		public virtual string Email { get; set; }
		public virtual string UserName { get; set; }
		public virtual string PasswordHash { get; set; }
		public virtual string PasswordSalt { get; set; }

		public virtual string NameGiven { get; set; }
		public virtual string NameFamily { get; set; }
		public virtual string Address1 { get; set; }
		public virtual string Address2 { get; set; }
		public virtual string City { get; set; }
		public virtual string ZipOrPostCode { get; set; }
		public virtual string Country { get; set; }
		public virtual string PhoneWork { get; set; }
		public virtual string PhoneMobile { get; set; }
		public virtual DateTime DateTimeCreated { get; set; }
        public virtual string InviteUrl{ get; set; }
        public virtual Guid? InviteGuid { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// (IsUserSelected) is Used For DTO Purpose
        /// </summary>
        [NotMapped]
        public virtual bool IsUserSelected { get; set; }
		/// <summary>
		/// User Type: Local, ActiveDirectory or External.
		/// </summary>
		public virtual AuthenticationType AuthenticationType { get; set; }

		public virtual long? DepartmentId { get; set; }
		public virtual Department Department { get; set; }

		/// <summary>
		/// The ActiveDirectory Id associated with this user (if any).
		/// </summary>
		public virtual string ActiveDirectory_NameDisplay { get; set; }
		public virtual Guid ActiveDirectory_ObjectId { get; set; }
		public virtual string ActiveDirectory_UsageLocation { get; set; }
		public virtual string ActiveDirectory_JobTitle { get; set; }
		public virtual string ActiveDirectory_Department { get; set; }
		public virtual Guid ActiveDirectory_ManagerId { get; set; }
		public virtual string ActiveDirectory_AuthenticationPhone { get; set; }
		public virtual string ActiveDirectory_AuthenticationPhoneAlternate { get; set; }
		public virtual string ActiveDirectory_AuthenticationEmail { get; set; }
		public virtual string ActiveDirectory_RoleDisplayName { get; set; }

		private ICollection<Role> _Roles = null;
		private ICollection<Session> _Sessions = null;
		private ICollection<Document> _Documents = null;
        private ICollection<UserFolder> _UserFolders = null;
        private ICollection<Folder> _Folders = null;
        private ICollection<UserRole> _UserRoles = null;
		private ICollection<UserLabel> _UserLabels = null;
		private ICollection<RuleDetail> _RuleDetails = null;
		private ICollection<UserInvite> _UserInvites = null;
        private ICollection<UserDocument> _UserDocuments = null;
		//private ICollection<ScanningJob> _ScanningJobs = null;
		private ICollection<DiscoursePost> _DiscoursePosts = null;
		private ICollection<RoleDelegation> _RoleDelegations = null;
		//private ICollection<ScannerSession> _ScannerSessions = null;
		private ICollection<ScanSession> _ScanSessions = null;
		private ICollection<DocumentTagUser> _DocumentTagUsers = null;
		private ICollection<TemplateTagUser> _TemplateTagUsers = null;
		private ICollection<UserDelegation> _ToUserDelegations = null;
		private ICollection<UserDelegation> _FormUserDelegations = null;
		private ICollection<RuleDetailInstance> _RuleDetailInstances = null;
		private ICollection<WorkflowActorsInstance> _WorkFlowActorsInstances = null;
		private ICollection<DocumentSearchCriteria> _DocumentSearchCriterias = null;
		private ICollection<Document> _CheckedOutDocuments = null;
        private ICollection<Document> _DocumentsAssignedTo = null;
        private ICollection<Document> _DocumentsAssignedBy = null;


        private ICollection<UserTemplate> _UserTemplates = null;
        private ICollection<Template> _CheckedOutTemplates = null;
        private ICollection<Template> _Templates = null;

        public virtual ICollection<Document> CheckedOutDocuments { get { if (this._CheckedOutDocuments == null) { this._CheckedOutDocuments = new List<Document> (); } return (this._CheckedOutDocuments); } protected set { this._CheckedOutDocuments = value; } }
        public virtual ICollection<Template> CheckedOutTemplates { get { if (this._CheckedOutTemplates == null) { this._CheckedOutTemplates = new List<Template>(); } return (this._CheckedOutTemplates); } protected set { this._CheckedOutTemplates = value; } }
        public virtual ICollection<Document> DocumentsAssignedTo { get { if (this._DocumentsAssignedTo == null) { this._DocumentsAssignedTo = new List<Document>(); } return (this._DocumentsAssignedTo); } protected set { this._DocumentsAssignedTo = value; } }
        public virtual ICollection<Document> DocumentsAssignedBy { get { if (this._DocumentsAssignedBy == null) { this._DocumentsAssignedBy = new List<Document>(); } return (this._DocumentsAssignedBy); } protected set { this._DocumentsAssignedBy = value; } }



        public virtual ICollection<Template> Templates { get { if (this._Templates == null) { this._Templates = new List<Template>(); } return (this._Templates); } protected set { this._Templates = value; } }
        public virtual ICollection<Role> Roles { get { if (this._Roles == null) { this._Roles = new List<Role>(); } return (this._Roles); } protected set { this._Roles = value; } }
		public virtual ICollection<Session> Sessions { get { if (this._Sessions == null) { this._Sessions = new List<Session>(); } return (this._Sessions); } protected set { this._Sessions = value; } }
		public virtual ICollection<Document> Documents { get { if (this._Documents == null) { this._Documents = new List<Document>(); } return (this._Documents); } protected set { this._Documents = value; } }
		public virtual ICollection<UserRole> UserRoles { get { if (this._UserRoles == null) { this._UserRoles = new List<UserRole>(); } return (this._UserRoles); } protected set { this._UserRoles = value; } }
		public virtual ICollection<UserLabel> UserLabels { get { if (this._UserLabels == null) { this._UserLabels = new List<UserLabel>(); } return (this._UserLabels); } protected set { this._UserLabels = value; } }
		public virtual ICollection<RuleDetail> RuleDetails { get { if (this._RuleDetails == null) { this._RuleDetails = new List<RuleDetail>(); } return (this._RuleDetails); } protected set { this._RuleDetails = value; } }
		public virtual ICollection<UserInvite> UserInvites { get { if (this._UserInvites == null) { this._UserInvites = new List<UserInvite>(); } return (this._UserInvites); } protected set { this._UserInvites = value; } }
		//public virtual ICollection<ScanningJob> ScanningJobs { get { if (this._ScanningJobs == null) { this._ScanningJobs = new List<ScanningJob>(); } return (this._ScanningJobs); } protected set { this._ScanningJobs = value; } }
		public virtual ICollection<DiscoursePost> DiscoursePosts { get { if (this._DiscoursePosts == null) { this._DiscoursePosts = new List<DiscoursePost>(); } return (this._DiscoursePosts); } protected set { this._DiscoursePosts = value; } }
		public virtual ICollection<RoleDelegation> RoleDelegations { get { if (this._RoleDelegations == null) { this._RoleDelegations = new List<RoleDelegation>(); } return (this._RoleDelegations); } protected set { this._RoleDelegations = value; } }
		//public virtual ICollection<ScannerSession> ScannerSessions { get { if (this._ScannerSessions == null) { this._ScannerSessions = new List<ScannerSession>(); } return (this._ScannerSessions); } protected set { this._ScannerSessions = value; } }
		public virtual ICollection<ScanSession> ScanSessions { get { if (this._ScanSessions == null) { this._ScanSessions = new List<ScanSession>(); } return (this._ScanSessions); } protected set { this._ScanSessions = value; } }
		public virtual ICollection<DocumentTagUser> DocumentTagUsers { get { if (this._DocumentTagUsers == null) { this._DocumentTagUsers = new List<DocumentTagUser>(); } return (this._DocumentTagUsers); } protected set { this._DocumentTagUsers = value; } }
		public virtual ICollection<TemplateTagUser> TemplateTagUsers { get { if (this._TemplateTagUsers == null) { this._TemplateTagUsers = new List<TemplateTagUser>(); } return (this._TemplateTagUsers); } protected set { this._TemplateTagUsers = value; } }
		public virtual ICollection<UserDelegation> ToUserDelegations { get { if (this._ToUserDelegations == null) { this._ToUserDelegations = new List<UserDelegation>(); } return (this._ToUserDelegations); } protected set { this._ToUserDelegations = value; } }
		public virtual ICollection<UserDelegation> FormUserDelegations { get { if (this._FormUserDelegations == null) { this._FormUserDelegations = new List<UserDelegation>(); } return (this._FormUserDelegations); } protected set { this._FormUserDelegations = value; } }
		public virtual ICollection<RuleDetailInstance> RuleDetailInstances { get { if (this._RuleDetailInstances == null) { this._RuleDetailInstances = new List<RuleDetailInstance>(); } return (this._RuleDetailInstances); } protected set { this._RuleDetailInstances = value; } }
		public virtual ICollection<WorkflowActorsInstance> WorkFlowActorsInstances { get { if (this._WorkFlowActorsInstances == null) { this._WorkFlowActorsInstances = new List<WorkflowActorsInstance>(); } return (this._WorkFlowActorsInstances); } protected set { this._WorkFlowActorsInstances = value; } }
		public virtual ICollection<DocumentSearchCriteria> DocumentSearchCriterias { get { if (this._DocumentSearchCriterias == null) { this._DocumentSearchCriterias = new List<DocumentSearchCriteria>(); } return (this._DocumentSearchCriterias); } protected set { this._DocumentSearchCriterias = value; } }

        /// <summary>
        /// The folders created by this user.
        /// </summary>
        public virtual ICollection<Folder> Folders { get { if (this._Folders == null) { this._Folders = new List<Folder> (); } return (this._Folders); } protected set { this._Folders = value; } }
        /// <summary>
        /// The list of folders which this user has permissions for.
        /// </summary>
        public virtual ICollection<UserFolder> UserFolders { get { if (this._UserFolders == null) { this._UserFolders = new List<UserFolder> (); } return (this._UserFolders); } protected set { this._UserFolders = value; } }
        /// <summary>
        /// The list of documents which this user has permissions for.
        /// </summary>
        public virtual ICollection<UserDocument> UserDocuments { get { if (this._UserDocuments == null) { this._UserDocuments = new List<UserDocument> (); } return (this._UserDocuments); } protected set { this._UserDocuments = value; } }

        public virtual ICollection<UserTemplate> UserTemplates { get { if (this._UserTemplates == null) { this._UserTemplates = new List<UserTemplate>(); } return (this._UserTemplates); } protected set { this._UserTemplates = value; } }

        private ICollection<DocumentCorrectiveIndexValue> _DocumentCorrectiveIndexValue = null;
        public virtual ICollection<DocumentCorrectiveIndexValue> DocumentCorrectiveIndexValues { get { if (this._DocumentCorrectiveIndexValue == null) { this._DocumentCorrectiveIndexValue = new List<DocumentCorrectiveIndexValue>(); } return (this._DocumentCorrectiveIndexValue); } protected set { this._DocumentCorrectiveIndexValue = value; } }

        public User ()
		{
		}

		[NotMapped]
		public string NameFull => ((this.NameGiven ?? "") + (" ") + (this.NameFamily ?? "")).Trim();
		//public string NameFull => ((this.NameFamily ?? "") + (" ") + (this.NameGiven ?? "")).Trim();

		public void Initialize ()
		{
		}

		public User Clone ()
		{
			return (new User().CopyFrom(this));
		}

		public User CopyTo (User destination)
		{
			return (destination.CopyFrom(this));
		}

		public User CopyFrom (User source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public User FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
        public User CleanDataGetIdAndName(User user)
        {
            User userObj = new User();
            userObj.Id = user.Id;
            userObj.NameFamily = user.NameFamily;
            userObj.NameGiven = user.NameGiven;
            return userObj;
        }
        public User CleanData(User user,bool basicInfo,bool password, bool email,bool ActiveDirectoryData,bool tenant,bool folders,bool roles,bool documents,bool templates, bool session, bool labels,bool rules,bool invites,bool scanning,bool discourse,bool deligations,bool workflow, bool searchCriteria)
        {
            User userObj = new User();
            
            if (!basicInfo) {
                userObj.Id = user.Id;
                userObj.InviteGuid = user.InviteGuid;
                userObj.InviteUrl = user.InviteUrl;
                userObj.IsUserSelected = user.IsUserSelected;
                userObj.NameFamily = user.NameFamily;
                userObj.NameGiven = user.NameGiven;
                userObj.PhoneMobile = user.PhoneMobile;
                userObj.PhoneWork = user.PhoneWork;
                userObj.Address1 = user.Address1;
                userObj.Address2 = user.Address2;
                userObj.AuthenticationType = user.AuthenticationType;
                userObj.City = user.City;
                userObj.Country = user.Country;
                userObj.DateTimeCreated = user.DateTimeCreated;
                userObj.Department = user.Department;
                userObj.DepartmentId = user.DepartmentId;
                userObj.Email = user.Email;
                userObj.UserName = user.UserName;
                userObj.ZipOrPostCode = user.ZipOrPostCode;
                userObj.TenantId = user.TenantId;
                userObj.DepartmentId = user.DepartmentId;
                userObj.ActiveDirectory_ManagerId= user.ActiveDirectory_ManagerId;
                userObj.ActiveDirectory_ObjectId = user.ActiveDirectory_ObjectId;
            }
            if (!folders)
            {
                userObj.Folders = user.Folders;
                userObj.UserFolders = user.UserFolders;
            }
            if (!password)
            {
                userObj.PasswordHash = user.PasswordHash;
                userObj.PasswordSalt = user.PasswordSalt;
            }
            if (!email)
            {
                userObj.Email = user.Email;
            }
            if (!tenant)
            {
                userObj.Tenant = user.Tenant;
                user.TenantId = user.TenantId;
            }
            if (!ActiveDirectoryData)
            {
                userObj.ActiveDirectory_AuthenticationEmail = user.ActiveDirectory_AuthenticationEmail;
                userObj.ActiveDirectory_AuthenticationPhone = user.ActiveDirectory_AuthenticationPhone;
                user.ActiveDirectory_AuthenticationPhoneAlternate = user.ActiveDirectory_AuthenticationPhoneAlternate;
                userObj.ActiveDirectory_Department = user.ActiveDirectory_Department;
                userObj.ActiveDirectory_JobTitle = user.ActiveDirectory_JobTitle;
                userObj.ActiveDirectory_ManagerId = user.ActiveDirectory_ManagerId;
                userObj.ActiveDirectory_NameDisplay = user.ActiveDirectory_NameDisplay;
                userObj.ActiveDirectory_ObjectId = user.ActiveDirectory_ObjectId;
                userObj.ActiveDirectory_RoleDisplayName = user.ActiveDirectory_RoleDisplayName;
                userObj.ActiveDirectory_UsageLocation = user.ActiveDirectory_UsageLocation;
            }
            if (!roles)
            {
                userObj.RoleDelegations = user.RoleDelegations;
                userObj.Roles = user.Roles;
                userObj.UserRoles = user.UserRoles;
            }
            if (!documents)
            {
                userObj.Documents = user.Documents;
                userObj.CheckedOutDocuments = user.CheckedOutDocuments;
                userObj.UserDocuments = user.UserDocuments;
                userObj.DocumentTagUsers = user.DocumentTagUsers;
            }
            if (!templates)
            {
                userObj.TemplateTagUsers = user.TemplateTagUsers;
                userObj.UserTemplates = user.UserTemplates;
            }
            if (!session) {
                userObj.Sessions = user.Sessions;
            }
            if (!labels)
            {
                userObj.UserLabels = user.UserLabels;
            }
            if (!rules) {
                userObj.RuleDetailInstances = user.RuleDetailInstances;
                userObj.RuleDetails = user.RuleDetails;
            }
            if (!invites)
            {
                userObj.UserInvites = user.UserInvites;
            }
            if (!scanning) {
                //userObj.ScannerSessions = user.ScannerSessions;
                //userObj.ScanningJobs = user.ScanningJobs;
            }
            if (!discourse) {
                userObj.DiscoursePosts = user.DiscoursePosts;
            }
            if (!deligations) {
                userObj.FormUserDelegations = user.FormUserDelegations;
                userObj.ToUserDelegations = user.ToUserDelegations;
                userObj.RoleDelegations = user.RoleDelegations;
            }
            if (!workflow)
            {
                userObj.WorkFlowActorsInstances = user.WorkFlowActorsInstances;
            }
            if (!searchCriteria)
            {
                userObj.DocumentSearchCriterias = user.DocumentSearchCriterias;
            }
            return userObj;
        }
        
    }
}