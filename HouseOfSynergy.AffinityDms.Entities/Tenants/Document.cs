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
	public partial class Document:
		IEntity<Document>
	{
		public virtual long Id { get; set; }

		public virtual long? TemplateId { get; set; }
		public virtual Template Template { get; set; }

		public virtual Folder Folder { get; set; }
		public virtual long? FolderId { get; set; }

		public virtual User User { get; set; }
		public virtual long UserId { get; set; }

		/// <summary>
		/// Size of the file in bytes.
		/// </summary>
		public virtual long Length { get; set; }

		/// <summary>
		/// The SHA512 computed hash of the file.
		/// </summary>
		public virtual string Hash { get; set; }

		/// <summary>
		/// Determines whether a file has been uploaded against this document entry.
		/// </summary>
		public virtual bool Uploaded { get; set; }

		/// <summary>
		/// The name of the document.
		/// This is the same as FilemameClient without the path.
		/// </summary>
		public virtual string Name { get; set; }

        public virtual string Namepdf { get; set; }

        public virtual string vendorname { get; set; }

        /// <summary>
        /// Determines whether a document can be viewed by other users.
        /// </summary>
        public virtual bool IsPrivate { get; set; }

		/// <summary>
		/// The source of the document (Desktop, Mobile, Scanner, Etcetera).
		/// </summary>
		public virtual SourceType SourceType { get; set; }

		/// <summary>
		/// The XML results of a Full Text operation.
		/// </summary>
		public virtual string FullTextOCRXML { get; set; }

		/// <summary>
		/// Determines whether the source document is 
		/// </summary>
		public virtual bool IsDigital { get; set; }

		/// <summary>
		/// The name of the device this document originated from (if applicable).
		/// </summary>
		public virtual string DeviceName { get; set; }

		/// <summary>
		/// The full file name of the file on the client (including path).
		/// </summary>
		public virtual string FileNameClient { get; set; }

		/// <summary>
		/// The file name of the document on Azure Blob Storage (without the path).
		/// </summary>
		public virtual string FileNameServer { get; set; }

		/// <summary>
		/// A unique value identifying the scanning sessionfor document grouping.
		/// </summary>
		public virtual long? ScanSessionId { get; set; }
		public virtual ScanSession ScanSession { get; set; }

		/// <summary>
		/// Determines if a document upload was cancelled.
		/// </summary>
		public virtual bool IsCancelled { get; set; }

		/// <summary>
		/// Depricated. Do not use.
		/// </summary>
		public virtual bool IsInTransit { get; set; }

		/// <summary>
		/// Determines the type of document (Raster, Digital, Etcetera).
		/// </summary>
		public virtual DocumentType DocumentType { get; set; }

		/// <summary>
		/// Determines the type of processing (Auto, Manual).
		/// </summary>
		public virtual DocumentQueueType DocumentQueueType { get; set; }

		/// <summary>
		/// Determines whether this document has been indexed or not. If yes, determines if indexing has been auto or manual.
		/// </summary>
		public virtual DocumentIndexType DocumentIndexType { get; set; }

		/// <summary>
		/// Determines the current state of a document
		/// (None, Uploading, QueuedAuto, QueuedManual, Matched, MatchedMultiple, UnMatched).
		/// </summary>
		public virtual DocumentState State { get; set; }

		/// <summary>
		/// Date/time of creation.
		/// </summary>
		public virtual DateTime DateTime { get; set; }

		/// <summary>
		/// Determines the overall confidence of the document against OCR operations.
		/// </summary>
        public virtual int? Confidence { get; set; }

        public virtual int CountAttemptOcr { get; set; }
		/// <summary>
		/// Document Versioning
		/// </summary>
		public virtual bool IsFinalized { get; set; }
		public virtual long DocumentOriginalId { get; set; }
		public virtual long? DocumentParent { get; set; }
		/// <summary>
		/// Used only for DTO purposes. Yuck!!!!
		/// TODO: Remove later and use model.
		/// </summary>
		public virtual int VersionCount { get; set; }
		

		public virtual int VersionMajor { get; set; }
		public virtual int VersionMinor { get; set; }

		public virtual long CheckedOutByUserId { get; set; }
		public virtual User CheckedOutByUser { get; set; }
		public DateTime CheckedOutDateTime { get; set; }

        public DocumentWorkflowState WorkflowState { get; set; }
        public virtual Guid? ProcessInstanceId { get; set; }
        public virtual string SchemeCode { get; set; }


        public int IndexingLevel { get; set; }
        public int IndexingIteration { get; set; }
        public virtual long? AssignedToUserId { get; set; }
        public virtual User AssignedToUser { get; set; }
        public virtual long? AssignedByUserId { get; set; }
        public virtual User AssignedByUser { get; set; }
        public virtual AssignmentState AssignmentState { get; set; }
        public virtual DateTime? AssignedDate { get; set; }
        public virtual DateTime? AcceptAssignmentDate { get; set; }
        public long LatestCheckedOutByUserId { get; set; }

        public int ClassificationID { get; set; }

        /// <summary>
        /// Used only for DTO purposes. Yuck!!!!
        /// TODO: Remove later and use model.
        /// </summary>
        //[NotMapped]
        //public virtual bool CheckedOut { get; set; }
        //[NotMapped]
        //public bool IsVisible { get; set; }


        [NotMapped]
        public bool IsSelected { get; set; }
        
        //[NotMapped]
        //public long CurrentLoginUser { get; set; }

        private ICollection<UserDocument> _DocumentUsers = null;
        private ICollection<DocumentTag> _DocumentTags = null;
		private ICollection<DocumentTagUser> _DocumentTagUsers = null;
		private ICollection<DocumentElement> _DocumentElements = null;
		private ICollection<DocumentFragment> _DocumentFragments = null;
		private ICollection<TemplateElementValue> _ElementValues = null;
		private ICollection<DocumentTemplate> _DocumentTemplates = null;
		private ICollection<WorkflowInstance> _WorkFlowInstances = null;
		private ICollection<UserDocumentLabel> _UserDocumentLabels = null;
		private ICollection<DocumentXmlElement> _DocumentXmlElements = null;
		private ICollection<EntityWorkflowMapping> _EntityWorkflowMappings = null;
		private ICollection<DiscoursePostVersionAttachment> _DiscussionPostDocuments = null;
        private ICollection<DocumentIndex> _DocumentIndexs = null;
        private ICollection<DocumentCorrectiveIndexValue> _DocumentCorrectiveIndexValue = null;

        public virtual ICollection<DocumentTag> DocumentTags { get { if (this._DocumentTags == null) { this._DocumentTags = new List<DocumentTag>(); } return (this._DocumentTags); } protected set { this._DocumentTags = value; } }
		public virtual ICollection<DocumentTagUser> DocumentTagUsers { get { if (this._DocumentTagUsers == null) { this._DocumentTagUsers = new List<DocumentTagUser>(); } return (this._DocumentTagUsers); } protected set { this._DocumentTagUsers = value; } }
		public virtual ICollection<DocumentElement> DocumentElements { get { if (this._DocumentElements == null) { this._DocumentElements = new List<DocumentElement>(); } return (this._DocumentElements); } protected set { this._DocumentElements = value; } }
		public virtual ICollection<DocumentFragment> DocumentFragments { get { if (this._DocumentFragments == null) { this._DocumentFragments = new List<DocumentFragment>(); } return (this._DocumentFragments); } protected set { this._DocumentFragments = value; } }
		public virtual ICollection<TemplateElementValue> ElementValues { get { if (this._ElementValues == null) { this._ElementValues = new List<TemplateElementValue>(); } return (this._ElementValues); } protected set { this._ElementValues = value; } }
		public virtual ICollection<DocumentTemplate> DocumentTemplates { get { if (this._DocumentTemplates == null) { this._DocumentTemplates = new List<DocumentTemplate>(); } return (this._DocumentTemplates); } protected set { this._DocumentTemplates = value; } }
		public virtual ICollection<WorkflowInstance> WorkFlowInstances { get { if (this._WorkFlowInstances == null) { this._WorkFlowInstances = new List<WorkflowInstance>(); } return (this._WorkFlowInstances); } protected set { this._WorkFlowInstances = value; } }
		public virtual ICollection<UserDocumentLabel> UserDocumentLabels { get { if (this._UserDocumentLabels == null) { this._UserDocumentLabels = new List<UserDocumentLabel>(); } return (this._UserDocumentLabels); } protected set { this._UserDocumentLabels = value; } }
		public virtual ICollection<DocumentXmlElement> DocumentXmlElements { get { if (this._DocumentXmlElements == null) { this._DocumentXmlElements = new List<DocumentXmlElement>(); } return (this._DocumentXmlElements); } protected set { this._DocumentXmlElements = value; } }
		public virtual ICollection<EntityWorkflowMapping> EntityWorkflowMappings { get { if (this._EntityWorkflowMappings == null) { this._EntityWorkflowMappings = new List<EntityWorkflowMapping>(); } return (this._EntityWorkflowMappings); } protected set { this._EntityWorkflowMappings = value; } }
		public virtual ICollection<DiscoursePostVersionAttachment> DiscussionPostDocuments { get { if (this._DiscussionPostDocuments == null) { this._DiscussionPostDocuments = new List<DiscoursePostVersionAttachment>(); } return (this._DiscussionPostDocuments); } protected set { this._DiscussionPostDocuments = value; } }
        //public virtual ICollection<DocumentIndexedVersion> DocumentIndexedVersions { get { if (this._DocumentIndexedVersions == null) { this._DocumentIndexedVersions = new List<DocumentIndexedVersion>(); } return (this._DocumentIndexedVersions); } protected set { this._DocumentIndexedVersions = value; } }

        public virtual ICollection<DocumentIndex> DocumentIndexs { get { if (this._DocumentIndexs == null) { this._DocumentIndexs = new List<DocumentIndex>(); } return (this._DocumentIndexs); } protected set { this._DocumentIndexs = value; } }
        /// <summary>
        /// The list of users with permissions for this document.
        /// </summary>
        public virtual ICollection<UserDocument> DocumentUsers { get { if (this._DocumentUsers == null) { this._DocumentUsers = new List<UserDocument> (); } return (this._DocumentUsers); } protected set { this._DocumentUsers = value; } }


        public virtual ICollection<DocumentCorrectiveIndexValue> DocumentCorrectiveIndexValues { get { if (this._DocumentCorrectiveIndexValue == null) { this._DocumentCorrectiveIndexValue = new List<DocumentCorrectiveIndexValue>(); } return (this._DocumentCorrectiveIndexValue); } protected set { this._DocumentCorrectiveIndexValue = value; } }
        public Document ()
		{
		}

		public void Initialize ()
		{
		}

		public Document Clone ()
		{
			return (new Document().CopyFrom(this));
		}

		public Document CopyTo (Document destination)
		{
			return (destination.CopyFrom(this));
		}

		public Document CopyFrom (Document source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Document FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}

        public static implicit operator Document(List<Document> v)
        {
            throw new NotImplementedException();
        }
    }
}