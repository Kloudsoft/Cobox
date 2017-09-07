module AffinityDms.Entities
{
	export interface IEntity
	{
		Id: number;
	}

	export enum AssignmentState
	{
		None = 0,
		Assigned = 1,
		Accepted = 2,
		Rejected = 3,
	}

	export enum AuditTrailActionType
	{
		None = 0,
		View = 1,
		Add = 2,
		Edit = 3,
		Delete = 4,
		Rename = 5,
		CancelCheckOut = 6,
		CheckIn = 7,
		CheckOut = 8,
		Move = 9,
		AddIndex = 10,
		UpdateVersion = 11,
	}

	export enum BarcodeType
	{
		None = 0,
		_1D = 1,
		_2D = 2,
		_3D = 3,
	}

	export enum DocumentCorrectiveIndexValueStatus
	{
		None = 0,
		Updated = 1,
		Submitted = 2,
	}

	export enum DocumentIndexDataType
	{
		Text = 0,
		Digits = 1,
		AlphaNumeric = 2,
		Date = 3,
		Time = 4,
	}

	export enum DocumentIndexType
	{
		None = 0,
		Auto = 1,
		Manual = 2,
	}

	export enum DocumentQueueType
	{
		Auto = 0,
		Manual = 1,
	}

	export enum DocumentWorkflowState
	{
		None = 0,
		WaitingToBeIndexed = 1,
		Verified = 2,
	}

	export enum TemplateEntityState
	{
		Web = 0,
		Mobile = 1,
		Print = 2,
	}

	export enum EntityType
	{
		None = 0,
		User = 1,
		Form = 2,
		Document = 3,
		Template = 4,
		Folder = 5,
	}

	export enum SessionMessageType
	{
		None = 0,
		Information = 1,
		Warning = 2,
		Error = 3,
		Unknown = 4,
	}

	export enum ElementDataType
	{
		None = 0,
		Alphabets = 1,
		Password = 2,
		Numeric = 3,
		Alphanumeric = 4,
		Email = 5,
	}

	export enum ElementType
	{
		Label = 0,
		Textbox = 1,
		Textarea = 2,
		Image = 3,
		Radio = 4,
		Checkbox = 5,
		Rectangle = 6,
		HorizontalLine = 7,
		VerticalLine = 8,
		Circle = 9,
		Barcode2D = 10,
		Table = 11,
		TableColumn = 12,
	}

	export enum SessionType
	{
		Api = 0,
		Mvc = 1,
	}

	export enum EntityMasterTenantType
	{
		None = 0,
		Master = 1,
		Tenant = 2,
	}

	export enum SettingsType
	{
		None = 0,
		UserManagement = 1,
		AuditTrail = 2,
		General = 3,
		All = 4,
	}

	export enum MasterActionType
	{
		None = 0,
		DashboardView = 1,
		UsersAdd = 2,
		UsersEdit = 3,
		UsersView = 4,
		SubscriptionsAdd = 5,
		SubscriptionsEdit = 6,
		SubscriptionsView = 7,
		TenantsAdd = 8,
		TenantsEdit = 9,
		TenantsView = 10,
		TenantSubscriptionsAdd = 11,
		TenantSubscriptionsEdit = 12,
		TenantSubscriptionsView = 13,
	}

	export enum TenantActionType
	{
		None = 0,
		DashboardView = 1,
		UsersAdd = 2,
		UsersEdit = 3,
		UsersView = 4,
		FoldersAdd = 5,
		FoldersEdit = 6,
		FoldersView = 7,
		TemplatesAdd = 8,
		TemplatesEdit = 9,
		TemplatesView = 10,
		TemplatesDesign = 11,
		FormsAdd = 12,
		FormsEdit = 13,
		FormsView = 14,
		FormsDesign = 15,
		DocumentsAdd = 16,
		DocumentsEdit = 17,
		DocumentsView = 18,
		DocumentsScan = 19,
		DocumentsUpload = 20,
		DocumentsIndex = 21,
		WorkflowAdd = 22,
		WorkflowEdit = 23,
		WorkflowView = 24,
		WorkflowAction = 25,
		WorkflowDesign = 26,
	}

	export enum DeviceType
	{
		None = 0,
		Desktop = 1,
		Mobile = 2,
		Other = 3,
		Unknown = 4,
	}

	export enum DiscussionPostAttachmentType
	{
		None = 0,
		Template = 1,
		Document = 2,
		External = 3,
		Live = 4,
		Form = 5,
	}

	export enum DocumentEntryState
	{
		None = 0,
		Imported = 1,
		Queued = 2,
		Uploading = 3,
		Uploaded = 4,
	}

	export enum DocumentState
	{
		None = 0,
		Uploading = 1,
		QueuedAuto = 2,
		QueuedManual = 3,
		Matched = 4,
		MatchedMultiple = 5,
		UnMatched = 6,
		MatchedUnverified = 7,
		Verified = 8,
		Processing = 9,
	}

	export enum DocumentType
	{
		None = 0,
		Raster = 1,
		Digital = 2,
		Unknown = 3,
	}

	export enum ElementIndexType
	{
		Label = 0,
		Value = 1,
		LabelValue = 2,
	}

	export enum FileFormatType
	{
		None = 0,
		Bmp = 1,
		Jpg = 2,
		Png = 3,
		Tiff = 4,
		Pdf = 5,
		Unknown = 6,
	}

	export enum FileType
	{
		None = 0,
		Image = 1,
		Document = 2,
		Unknown = 3,
	}

	export enum FolderType
	{
		None = 0,
		Root = 1,
		EnterpriseRoot = 2,
		EnterpriseChild = 3,
		SharedRoot = 4,
		SharedChild = 5,
		UserRoot = 6,
		UserChild = 7,
		ProjectRoot = 8,
		ProjectChild = 9,
		Child = 10,
	}

	export enum OcrConfidence
	{
		None = 0,
		MinimumRecognitionConfidence = 60,
		MinimumOCRConfidence = 80,
	}

	export enum MasterRoleType
	{
		None = 0,
		Custom = 1,
		Administrator = 2,
		Reporting = 3,
	}

	export enum TenantRoleType
	{
		None = 0,
		Custom = 1,
		Administrator = 2,
		Scanner = 3,
		Uploader = 4,
		Indexer = 5,
		TemplateCreator = 6,
		FormCreator = 7,
		WorkflowActor = 8,
		WorkflowCreator = 9,
		Reporting = 10,
	}

	export enum SeparatorType
	{
		None = 0,
		DocumentSet = 1,
		DocumentType = 2,
	}

	export enum SourceType
	{
		None = 0,
		MobileCamera = 1,
		MobileFileSystem = 2,
		DesktopCamera = 3,
		DesktopScanner = 4,
		DesktopFileSystem = 5,
		BrowserFileSystem = 6,
		Other = 7,
	}

	export enum TemplateType
	{
		Form = 0,
		Template = 1,
	}

	export enum AuthenticationType
	{
		None = 0,
		Local = 1,
		ActiveDirectory = 2,
		Both = 3,
		External = 4,
	}

	export enum DocumentSortOrder
	{
		None = 0,
		Ascending = 1,
		Descending = 2,
	}

	export enum DocumentSortMember
	{
		Id = 0,
		Name = 1,
		None = -1,
	}

	export enum IEntitySortMember
	{
		Id = 0,
		None = -1,
	}

	export enum IEntitySortOrder
	{
		None = 0,
		Ascending = 1,
		Descending = 2,
	}

	export enum ApplicationModule
	{
		Library = 0,
		Entities = 1,
		DataAccess = 2,
		Business = 3,
		Console = 4,
		LibraryOcrLeadTools = 5,
		Worker = 6,
		Web = 7,
		Desktop = 8,
	}

	export class AuditTrailEntry
		implements IEntity
	{
		public Id: number;
		public TransactionId: number; // Nullable.
		public DateTime: Date;
		public EntityType: EntityType;
		public EntityTypeId: number;
		public AuditTrailActionType: AuditTrailActionType;
		public Description: string;
		public UserId: number;
		public User: User;
		public EntityUserId: number; // Nullable.
		public EntityUser: User;
		public EntityDocumentOriginalId: number; // Nullable.
		public EntityDocumentParentId: number; // Nullable.
		public EntityDiscourseId: number; // Nullable.
		public EntityDiscoursePostId: number; // Nullable.
		public EntityDiscoursePostVersionId: number; // Nullable.
		public EntityDiscoursePostVersionAttachmentId: number; // Nullable.

		constructor ()
		{
		}
	}

	export class Button
		implements IEntity
	{
		public Id: number;
		public ScreenId: number;
		public Screen: Screen;
		public Name: string;
		public RoleRights: Array<RoleRight>;

		constructor ()
		{
		}
	}

	export class Department
		implements IEntity
	{
		public Id: number;
		public Name: string;
		public Users: Array<User>;
		public Folders: Array<Folder>;

		constructor ()
		{
		}
	}

	export class Discourse
		implements IEntity
	{
		public Id: number;
		public Topic: string;
		public Description: string;
		public Posts: Array<DiscoursePost>;
		public Users: Array<DiscourseUser>;
		public PostVersion: Array<DiscoursePostVersion>;
		public PostVersionAttachments: Array<DiscoursePostVersionAttachment>;

		constructor ()
		{
		}
	}

	export class DiscoursePostVersion
		implements IEntity
	{
		public Id: number;
		public DateTime: Date;
		public Comments: string;
		public PostId: number;
		public Post: DiscoursePost;
		public DiscourseId: number;
		public Discourse: Discourse;
		public Attachments: Array<DiscoursePostVersionAttachment>;

		constructor ()
		{
		}
	}

	export class DiscoursePost
		implements IEntity
	{
		public Id: number;
		public DiscourseId: number;
		public Discourse: Discourse;
		public UserId: number;
		public User: User;
		public Versions: Array<DiscoursePostVersion>;

		constructor ()
		{
		}
	}

	export class DiscoursePostVersionAttachment
		implements IEntity
	{
		public Id: number;
		public Url: string;
		public AttachmentType: DiscussionPostAttachmentType;
		public PostVersionId: number;
		public PostVersion: DiscoursePostVersion;
		public TemplateId: number; // Nullable.
		public Template: Template;
		public DocumentId: number; // Nullable.
		public Document: Document;
		public DiscourseId: number; // Nullable.
		public Discourse: Discourse;
		public FileNameClient: string;
		public FileNameServer: string;

		constructor ()
		{
		}
	}

	export class DiscourseUser
		implements IEntity
	{
		public Id: number;
		public DiscourseId: number;
		public Discourse: Discourse;
		public IsActive: boolean;
		public UserId: number;
		public User: User;

		constructor ()
		{
		}
	}

	export class DocumentCorrectiveIndexValue
		implements IEntity
	{
		public Id: number;
		public Document: Document;
		public DocumentId: number;
		public IndexElement: TemplateElement;
		public IndexElementId: number;
		public Indexer: User;
		public IndexerId: number;
		public IndexValue: string;
		public Status: DocumentCorrectiveIndexValueStatus;

		constructor ()
		{
		}
	}

	export class DocumentIndex
		implements IEntity
	{
		public Id: number;
		public Document: Document;
		public DocumentId: number;
		public Name: string;
		public Value: string;
		public DataType: DocumentIndexDataType;

		constructor ()
		{
		}
	}

	export class Document
		implements IEntity
	{
		public Id: number;
		public TemplateId: number; // Nullable.
		public Template: Template;
		public Folder: Folder;
		public FolderId: number; // Nullable.
		public User: User;
		public UserId: number;
		public Length: number;
		public Hash: string;
		public Uploaded: boolean;
		public Name: string;
		public IsPrivate: boolean;
		public SourceType: SourceType;
		public FullTextOCRXML: string;
		public IsDigital: boolean;
		public DeviceName: string;
		public FileNameClient: string;
		public FileNameServer: string;
		public ScanSessionId: number; // Nullable.
		public ScanSession: ScanSession;
		public IsCancelled: boolean;
		public IsInTransit: boolean;
		public DocumentType: DocumentType;
		public DocumentQueueType: DocumentQueueType;
		public DocumentIndexType: DocumentIndexType;
		public State: DocumentState;
		public DateTime: Date;
		public Confidence: number; // Nullable.
		public CountAttemptOcr: number;
		public IsFinalized: boolean;
		public DocumentOriginalId: number;
		public DocumentParent: number; // Nullable.
		public VersionCount: number;
		public VersionMajor: number;
		public VersionMinor: number;
		public CheckedOutByUserId: number;
		public CheckedOutByUser: User;
		public CheckedOutDateTime: Date;
		public WorkflowState: DocumentWorkflowState;
		public SchemeCode: string;
		public IndexingLevel: number;
		public IndexingIteration: number;
		public AssignedToUserId: number; // Nullable.
		public AssignedToUser: User;
		public AssignedByUserId: number; // Nullable.
		public AssignedByUser: User;
		public AssignmentState: AssignmentState;
		public LatestCheckedOutByUserId: number;
		public IsSelected: boolean;
		public DocumentTags: Array<DocumentTag>;
		public DocumentTagUsers: Array<DocumentTagUser>;
		public DocumentElements: Array<DocumentElement>;
		public DocumentFragments: Array<DocumentFragment>;
		public ElementValues: Array<TemplateElementValue>;
		public DocumentTemplates: Array<DocumentTemplate>;
		public WorkFlowInstances: Array<WorkflowInstance>;
		public UserDocumentLabels: Array<UserDocumentLabel>;
		public DocumentXmlElements: Array<DocumentXmlElement>;
		public EntityWorkflowMappings: Array<EntityWorkflowMapping>;
		public DiscussionPostDocuments: Array<DiscoursePostVersionAttachment>;
		public DocumentIndexs: Array<DocumentIndex>;
		public DocumentUsers: Array<UserDocument>;
		public DocumentCorrectiveIndexValues: Array<DocumentCorrectiveIndexValue>;

		constructor ()
		{
		}
	}

	export class DocumentElement
		implements IEntity
	{
		public Id: number;
		public OcrXml: string;
		public OcrText: string;
		public DocumentId: number;
		public Document: Document;
		public TemplateElementId: number;
		public TemplateElement: TemplateElement;
		public Confidience: number;
		public TemplateElementDetailId: number; // Nullable.
		public TemplateElementDetail: TemplateElementDetail;

		constructor ()
		{
		}
	}

	export class DocumentFragment
		implements IEntity
	{
		public Id: number;
		public FullTextOcr: string;
		public DocumentId: number;
		public Document: Document;

		constructor ()
		{
		}
	}

	export class DocumentSearchCriteria
		implements IEntity
	{
		public Id: number;
		public Name: string;
		public UserId: number;
		public User: User;
		public TagsUser: string;
		public TagsGlobal: string;
		public Content: string;
		public Filename: string;
		public FolderName: string;
		public TemplateName: string;

		constructor ()
		{
		}
	}

	export class DocumentTag
		implements IEntity
	{
		public Id: number;
		public Value: string;
		public DocumentId: number;
		public Document: Document;

		constructor ()
		{
		}
	}

	export class DocumentTagUser
		implements IEntity
	{
		public Id: number;
		public Value: string;
		public DocumentId: number;
		public Document: Document;
		public UserId: number;
		public User: User;

		constructor ()
		{
		}
	}

	export class DocumentTemplate
		implements IEntity
	{
		public Id: number;
		public DocumentId: number;
		public Document: Document;
		public TemplateId: number;
		public Template: Template;
		public Confidence: number; // Nullable.

		constructor ()
		{
		}
	}

	export class DocumentXmlElement
		implements IEntity
	{
		public Id: number;
		public OcrXml: string;
		public OcrText: string;
		public DocumentId: number;
		public Document: Document;

		constructor ()
		{
		}
	}

	export class EntityWorkflowMapping
		implements IEntity
	{
		public Id: number;
		public WorkflowMasterId: number;
		public WorkflowMaster: WorkflowMaster;
		public WorkflowTemplateId: number;
		public WorkflowTemplate: WorkflowTemplate;
		public DocumentId: number;
		public Document: Document;
		public WorkFlowStagesInstances: Array<WorkflowStagesInstance>;

		constructor ()
		{
		}
	}

	export class Folder
		implements IEntity
	{
		public Id: number;
		public Name: string;
		public FolderType: FolderType;
		public DateTimeCreated: Date;
		public UserCreatedBy: User;
		public UserCreatedById: number;
		public Parent: Folder;
		public ParentId: number; // Nullable.
		public DepartmentId: number; // Nullable.
		public Department: Department;
		public FolderUsers: Array<UserFolder>;
		public Folders: Array<Folder>;
		public Documents: Array<Document>;
		public HasChildren: boolean;
		public CanContainFiles: boolean;

		constructor ()
		{
		}
	}

	export class Role
		implements IEntity
	{
		public Id: number;
		public Name: string;
		public Description: string;
		public RoleType: TenantRoleType;
		public RoleRights: Array<RoleRight>;
		public UserRoles: Array<UserRole>;
		public Users: Array<User>;

		constructor ()
		{
		}
	}

	export class RoleDelegation
		implements IEntity
	{
		public Id: number;
		public UserId: number;
		public User: User;
		public UserDelegationId: number;
		public UserDelegations: UserDelegation;

		constructor ()
		{
		}
	}

	export class RoleRight
		implements IEntity
	{
		public Id: number;
		public RoleId: number;
		public Roles: Role;
		public ScreenId: number;
		public Screen: Screen;
		public ButtonId: number; // Nullable.
		public Button: Button;

		constructor ()
		{
		}
	}

	export class RuleDetail
		implements IEntity
	{
		public Id: number;
		public Condition: string;
		public Value: string;
		public Action: string;
		public TemplateField: number;
		public WorkflowRuleId: number;
		public WorkFlowRule: WorkflowRule;
		public TemplateId: number;
		public Template: Template;
		public UserId: number;
		public User: User;

		constructor ()
		{
		}
	}

	export class RuleDetailInstance
		implements IEntity
	{
		public Id: number;
		public Condition: string;
		public Value: string;
		public Action: string;
		public WorkFlowRuleInstanceId: number;
		public WorkFlowRuleInstance: WorkflowRuleInstance;
		public TemplateId: number;
		public Template: Template;
		public UserId: number;
		public User: User;

		constructor ()
		{
		}
	}

	export class ScanSession
		implements IEntity
	{
		public Id: number;
		public Name: string;
		public Description: string;
		public DateTimeCreated: Date;
		// Type not implemented: [Guid].
		//public Guid: any;
		public Finalized: boolean;
		public UserId: number;
		public User: User;
		public Documents: Array<Document>;

		constructor ()
		{
		}
	}

	export class Screen
		implements IEntity
	{
		public Id: number;
		public Name: string;
		public Parent: Screen;
		public ParentId: number; // Nullable.
		public Screens: Array<Screen>;
		public Buttons: Array<Button>;
		public RoleRights: Array<RoleRight>;

		constructor ()
		{
		}
	}

	export class Session
		implements IEntity
	{
		public Id: number;
		public User: User;
		public UserId: number;
		public Tenant: Tenant;
		public TenantId: number;
		// Type not implemented: [Guid].
		//public Guid: any;
		public SessionId: string;
		public SessionType: SessionType;
		public DateTimeCreated: Date;
		public DateTimeExpiration: Date;
		public Token: string;
		public UserAgent: string;
		public DeviceType: DeviceType;
		public IPAddressString: string;
		public RsaKeyPublic: string;
		public RsaKeyPrivate: string;
		public RijndaelKey: string;
		public RijndaelInitializationVector: string;
		public CultureName: string;
		// Type not implemented: [IPAddress].
		//public IPAddress: any;
		public SessionMessages: Array<SessionMessage>;

		constructor ()
		{
		}
	}

	export class SessionMessage
		implements IEntity
	{
		public Id: number;
		public SessionMessageType: SessionMessageType;
		public Session: Session;
		public SessionId: number;
		public Value: string;

		constructor ()
		{
		}
	}

	export class TableHistory
		implements IEntity
	{
		public Id: number;
		public TableName: string;
		public Action: string;
		public ModifiedBy: string;
		public DateTime: Date;
		public RowId: string;
		public DataType: string;
		public ValueOldBinary: string;
		public ValueOldText: string;
		public ValueOldGeographic: string;
		public ValueNewBinary: string;
		public ValueNewGeographic: string;
		public ValueNewText: string;

		constructor ()
		{
		}
	}

	export class Template
		implements IEntity
	{
		public Id: number;
		public EntityState: TemplateEntityState;
		public Title: string;
		public Description: string;
		public TemplateType: TemplateType;
		public IsActive: boolean;
		public IsFinalized: boolean;
		// Type not implemented: [Byte[]].
		//public TemplateImage: any;
		public TemplateOriginalId: number;
		public TemplateParent: number; // Nullable.
		public VersionCount: number;
		public CheckedOut: boolean;
		public IsSelected: boolean;
		public VersionMajor: number;
		public VersionMinor: number;
		public CheckedOutByUserId: number;
		public CheckedOutByUser: User;
		public CheckedOutDateTime: Date;
		public IsVisible: boolean;
		public UserId: number;
		public User: User;
		public Documents: Array<Document>;
		public RuleDetails: Array<RuleDetail>;
		public Elements: Array<TemplateElement>;
		public TemplateTags: Array<TemplateTag>;
		public TemplateTagUsers: Array<TemplateTagUser>;
		public TemplateVersions: Array<TemplateVersion>;
		public DocumentTemplates: Array<DocumentTemplate>;
		public RuleDetailInstances: Array<RuleDetailInstance>;
		public DiscussionPostDocuments: Array<DiscoursePostVersionAttachment>;
		public TemplateUsers: Array<UserTemplate>;
		// Type not implemented: [Version].
		//public Version: any;

		constructor ()
		{
		}
	}

	export class TemplateElement
		implements IEntity
	{
		public Id: number;
		public ElementId: string;
		public ElementType: ElementType;
		public ElementDataType: ElementDataType;
		public Name: string;
		public Description: string;
		public Text: string;
		public X: number;
		public Y: number;
		public X2: number;
		public Y2: number;
		public Radius: number;
		public Diameter: number;
		public Width: string;
		public Height: string;
		public DivX: number;
		public DivY: number;
		public DivWidth: string;
		public DivHeight: string;
		public MinHeight: string;
		public MinWidth: string;
		public ForegroundColor: string;
		public BackgroundColor: string;
		public Hint: string;
		public MinChar: string;
		public MaxChar: string;
		public DateTime: string;
		public FontName: string;
		public FontSize: number;
		public FontStyle: number;
		public FontColor: string;
		public BorderStyle: number;
		public BarcodeType: BarcodeType;
		public Value: string;
		public ImageSource: string;
		public SizeMode: number;
		public IsSelected: string;
		public FontGraphicsUnit: number;
		public ColorForegroundA: number;
		public ColorForegroundR: number;
		public ColorForegroundG: number;
		public ColorForegroundB: number;
		public ColorBackroundA: number;
		public ColorBackroundR: number;
		public ColorBackroundG: number;
		public ColorBackroundB: number;
		// Type not implemented: [Byte[]].
		//public Data: any;
		public ElementMobileOrdinal: number;
		public ElementIndexType: ElementIndexType;
		public DocumentIndexDataType: DocumentIndexDataType;
		public BarcodeValue: string;
		public Discriminator: string;
		public TemplateId: number;
		public Template: Template;
		public ElementDetails: Array<TemplateElementDetail>;
		public ElementValues: Array<TemplateElementValue>;
		public DocumentElements: Array<DocumentElement>;
		public DocumentCorrectiveIndexValues: Array<DocumentCorrectiveIndexValue>;

		constructor ()
		{
		}
	}

	export class TemplateElementDetail
		implements IEntity
	{
		public Id: number;
		public ElementId: number;
		public Element: TemplateElement;
		public DocumentElements: Array<DocumentElement>;
		public ElementDetailId: string;
		public ElementType: number;
		public Name: string;
		public Description: string;
		public Text: string;
		public X: number;
		public Y: number;
		public Width: string;
		public Height: string;
		public ForegroundColor: string;
		public BackgroundColor: string;
		public BorderStyle: number;
		public Value: string;
		public SizeMode: number;

		constructor ()
		{
		}
	}

	export class TemplateElementDetailExtended
		implements IEntity
	{
		public Id: number;
		public ElementDivId: string;
		public TemplateId: number;
		public ElementId: number;
		public Element: TemplateElement;
		public DocumentElements: Array<DocumentElement>;
		public ElementDetailId: string;
		public ElementType: number;
		public Name: string;
		public Description: string;
		public Text: string;
		public X: number;
		public Y: number;
		public Width: string;
		public Height: string;
		public ForegroundColor: string;
		public BackgroundColor: string;
		public BorderStyle: number;
		public Value: string;
		public SizeMode: number;

		constructor ()
		{
		}
	}

	export class TemplateElementValue
		implements IEntity
	{
		public Id: number;
		public DocumentId: number; // Nullable.
		public Document: Document;
		public ElementId: number;
		public Element: TemplateElement;
		public Value: string;

		constructor ()
		{
		}
	}

	export class TemplateInstance
		implements IEntity
	{
		public Id: number;
		public TemplateVersionId: number;
		public TemplateVersion: TemplateVersion;
		public Number: string;

		constructor ()
		{
		}
	}

	export class TemplatePage
		implements IEntity
	{
		public Id: number;
		public TemplateVersionId: number; // Nullable.
		public TemplateVersion: TemplateVersion;
		public Name: string;
		public Description: string;
		public Number: number;
		public Width: number;
		public Height: number;
		// Type not implemented: [Byte[]].
		//public ImageBackground: any;

		constructor ()
		{
		}
	}

	export class TemplateTag
		implements IEntity
	{
		public Id: number;
		public Value: string;
		public TemplateId: number;
		public Template: Template;

		constructor ()
		{
		}
	}

	export class TemplateTagUser
		implements IEntity
	{
		public Id: number;
		public Value: string;
		public TemplateId: number;
		public Template: Template;
		public UserId: number;
		public User: User;

		constructor ()
		{
		}
	}

	export class TemplateVersion
		implements IEntity
	{
		public Id: number;
		public TemplateCurrentId: number;
		public TemplateCurrent: Template;
		public TemplateOriginalId: number;
		public TemplateOriginal: Template;
		public TemplateParentId: number; // Nullable.
		public TemplateParent: Template;
		public VersionMajor: number;
		public VersionMinor: number;

		constructor ()
		{
		}
	}

	export class User
		implements IEntity
	{
		public Id: number;
		public TenantId: number;
		public Tenant: Tenant;
		public Email: string;
		public UserName: string;
		public PasswordHash: string;
		public PasswordSalt: string;
		public NameGiven: string;
		public NameFamily: string;
		public Address1: string;
		public Address2: string;
		public City: string;
		public ZipOrPostCode: string;
		public Country: string;
		public PhoneWork: string;
		public PhoneMobile: string;
		public DateTimeCreated: Date;
		public InviteUrl: string;
		public IsUserSelected: boolean;
		public AuthenticationType: AuthenticationType;
		public DepartmentId: number;
		public Department: Department;
		public ActiveDirectory_NameDisplay: string;
		// Type not implemented: [Guid].
		//public ActiveDirectory_ObjectId: any;
		public ActiveDirectory_UsageLocation: string;
		public ActiveDirectory_JobTitle: string;
		public ActiveDirectory_Department: string;
		// Type not implemented: [Guid].
		//public ActiveDirectory_ManagerId: any;
		public ActiveDirectory_AuthenticationPhone: string;
		public ActiveDirectory_AuthenticationPhoneAlternate: string;
		public ActiveDirectory_AuthenticationEmail: string;
		public ActiveDirectory_RoleDisplayName: string;
		public CheckedOutDocuments: Array<Document>;
		public CheckedOutTemplates: Array<Template>;
		public DocumentsAssignedTo: Array<Document>;
		public DocumentsAssignedBy: Array<Document>;
		public Templates: Array<Template>;
		public Roles: Array<Role>;
		public Sessions: Array<Session>;
		public Documents: Array<Document>;
		public UserRoles: Array<UserRole>;
		public UserLabels: Array<UserLabel>;
		public RuleDetails: Array<RuleDetail>;
		public UserInvites: Array<UserInvite>;
		public DiscoursePosts: Array<DiscoursePost>;
		public RoleDelegations: Array<RoleDelegation>;
		public ScanSessions: Array<ScanSession>;
		public DocumentTagUsers: Array<DocumentTagUser>;
		public TemplateTagUsers: Array<TemplateTagUser>;
		public ToUserDelegations: Array<UserDelegation>;
		public FormUserDelegations: Array<UserDelegation>;
		public RuleDetailInstances: Array<RuleDetailInstance>;
		public WorkFlowActorsInstances: Array<WorkflowActorsInstance>;
		public DocumentSearchCriterias: Array<DocumentSearchCriteria>;
		public Folders: Array<Folder>;
		public UserFolders: Array<UserFolder>;
		public UserDocuments: Array<UserDocument>;
		public UserTemplates: Array<UserTemplate>;
		public DocumentCorrectiveIndexValues: Array<DocumentCorrectiveIndexValue>;
		public NameFull: string;

		constructor ()
		{
		}
	}

	export class UserDelegation
		implements IEntity
	{
		public Id: number;
		public StartDate: Date;
		public EndDate: Date;
		public ActiveTag: number;
		public Status: number;
		public FormUserId: number; // Nullable.
		public FromUser: User;
		public ToUserId: number; // Nullable.
		public ToUser: User;
		public RoleDelegations: Array<RoleDelegation>;

		constructor ()
		{
		}
	}

	export class UserDocument
		implements IEntity
	{
		public Id: number;
		public User: User;
		public UserId: number;
		public IsActive: boolean;
		public Document: Document;
		public DocumentId: number;

		constructor ()
		{
		}
	}

	export class UserDocumentLabel
		implements IEntity
	{
		public Id: number;
		public DocumentId: number;
		public Document: Document;
		public LabelId: number;

		constructor ()
		{
		}
	}

	export class UserFolder
		implements IEntity
	{
		public Id: number;
		public User: User;
		public UserId: number;
		public IsActive: boolean;
		public Folder: Folder;
		public FolderId: number;

		constructor ()
		{
		}
	}

	export class UserHistory
		implements IEntity
	{
		public Id: number;
		public UserId: number;
		public User: User;
		public FieldName: string;
		public PreviousValue: string;
		public NewValue: string;
		public DateTime: Date;
		public ModifiedBy: string;

		constructor ()
		{
		}
	}

	export class UserInvite
		implements IEntity
	{
		public Id: number;
		public InviterUserId: number;
		public User: User;
		public InviteeUserId: number;
		public URL: string;
		public URLExpiryDate: Date;
		public URLCreatedDate: Date;
		public IsActive: boolean;

		constructor ()
		{
		}
	}

	export class UserLabel
		implements IEntity
	{
		public Id: number;
		public UserId: number;
		public User: User;
		public LabelName: string;

		constructor ()
		{
		}
	}

	export class UserRole
		implements IEntity
	{
		public Id: number;
		public RoleId: number;
		public Role: Role;
		public UserId: number;
		public User: User;

		constructor ()
		{
		}
	}

	export class UserTemplate
		implements IEntity
	{
		public Id: number;
		public User: User;
		public UserId: number;
		public IsActive: boolean;
		public Template: Template;
		public TemplateId: number;

		constructor ()
		{
		}
	}

	export class WorkflowAction
		implements IEntity
	{
		public Id: number;
		public ActionDescription: string;
		public WorkflowActorId: number;
		public WorkflowActor: WorkflowActor;
		public WorkflowUserActions: Array<WorkflowUserAction>;

		constructor ()
		{
		}
	}

	export class WorkflowActor
		implements IEntity
	{
		public Id: number;
		public WorkflowStageId: number;
		public WorkflowStage: WorkflowStage;
		public RoleId: number;
		public WorkflowActions: Array<WorkflowAction>;

		constructor ()
		{
		}
	}

	export class WorkflowActorsInstance
		implements IEntity
	{
		public Id: number;
		public WorkFlowStagesInstanceId: number;
		public WorkFlowStagesInstances: WorkflowStagesInstance;
		public UserId: number;
		public User: User;

		constructor ()
		{
		}
	}

	export class WorkflowInstance
		implements IEntity
	{
		public Id: number;
		public WorkflowTemplateId: number;
		public WorkflowTemplate: WorkflowTemplate;
		public TemplateId: number;
		public Template: Template;
		public DocumentId: number;
		public Document: Document;

		constructor ()
		{
		}
	}

	export class WorkflowMaster
		implements IEntity
	{
		public Id: number;
		public Description: string;
		public NoofStages: number;
		public IsCompleted: boolean;
		public EntityWorkflowMappings: Array<EntityWorkflowMapping>;
		public WorkflowStages: Array<WorkflowStage>;

		constructor ()
		{
		}
	}

	export class WorkflowRule
		implements IEntity
	{
		public Id: number;
		public Description: string;
		public WorkflowStageId: number;
		public WorkflowStage: WorkflowStage;
		public RuleDetails: Array<RuleDetail>;

		constructor ()
		{
		}
	}

	export class WorkflowRuleInstance
		implements IEntity
	{
		public Id: number;
		public Description: string;
		public WorkFlowStagesInstanceId: number;
		public WorkFlowStagesInstance: WorkflowStagesInstance;
		public RuleDetailInstances: Array<RuleDetailInstance>;

		constructor ()
		{
		}
	}

	export class WorkflowStage
		implements IEntity
	{
		public Id: number;
		public StageNo: number;
		public StageDescription: string;
		public IsVoting: boolean;
		public WorkflowMasterId: number;
		public WorkflowMaster: WorkflowMaster;
		public WorkflowTemplateId: number;
		public WorkflowTemplate: WorkflowTemplate;
		public WorkflowActors: Array<WorkflowActor>;
		public WorkFlowRules: Array<WorkflowRule>;
		public WorkflowUserActions: Array<WorkflowUserAction>;

		constructor ()
		{
		}
	}

	export class WorkflowStagesInstance
		implements IEntity
	{
		public Id: number;
		public StageNo: number;
		public StageDescription: string;
		public IsVoting: boolean;
		public EntityWorkflowMappingId: number;
		public EntityWorkflowMappings: EntityWorkflowMapping;
		public WorkFlowActorsInstances: Array<WorkflowActorsInstance>;
		public WorkFlowRuleInstances: Array<WorkflowRuleInstance>;
		public WorkFlowUserActionInstances: Array<WorkflowUserActionInstance>;

		constructor ()
		{
		}
	}

	export class WorkflowTemplate
		implements IEntity
	{
		public Id: number;
		public Description: string;
		public NoOfStages: number;
		public IsCompleted: boolean;
		public EntityWorkflowMappings: Array<EntityWorkflowMapping>;
		public WorkFlowInstances: Array<WorkflowInstance>;
		public WorkflowStages: Array<WorkflowStage>;

		constructor ()
		{
		}
	}

	export class WorkflowTransaction
		implements IEntity
	{
		public Id: number;
		public AssignedUserId: number;
		public Action: string;
		public Comments: string;
		public IsCommentsHide: boolean;

		constructor ()
		{
		}
	}

	export class WorkflowUserAction
		implements IEntity
	{
		public Id: number;
		public WorkflowActionId: number;
		public WorkflowAction: WorkflowAction;
		public WorkflowStagesId: number;
		public WorkflowStage: WorkflowStage;

		constructor ()
		{
		}
	}

	export class WorkflowUserActionInstance
		implements IEntity
	{
		public Id: number;
		public WorkflowStageInstanceId: number;
		public WorkFlowStagesInstance: WorkflowStagesInstance;
		public WorkflowActionId: number;
		public WorkflowAction: WorkflowAction;

		constructor ()
		{
		}
	}

	export class MasterRole
		implements IEntity
	{
		public Id: number;
		public Name: string;
		public Description: string;
		public RoleType: MasterRoleType;
		public Users: Array<MasterUser>;
		public UserRoles: Array<MasterUserRole>;

		constructor ()
		{
		}
	}

	export class MasterSession
		implements IEntity
	{
		public Id: number;
		public UserId: number;
		public User: MasterUser;
		// Type not implemented: [Guid].
		//public Guid: any;
		public SessionId: string;
		public SessionType: SessionType;
		public DateTimeCreated: Date;
		public DateTimeExpiration: Date;
		public Token: string;
		public UserAgent: string;
		public DeviceType: DeviceType;
		public IPAddressString: string;
		public RsaKeyPublic: string;
		public RsaKeyPrivate: string;
		public RijndaelKey: string;
		public RijndaelInitializationVector: string;
		public CultureName: string;
		// Type not implemented: [IPAddress].
		//public IPAddress: any;

		constructor ()
		{
		}
	}

	export class MasterUser
		implements IEntity
	{
		public Id: number;
		public Email: string;
		public UserName: string;
		public PasswordHash: string;
		public PasswordSalt: string;
		public NameGiven: string;
		public NameFamily: string;
		public Address1: string;
		public Address2: string;
		public City: string;
		public ZipOrPostCode: string;
		public Country: string;
		public PhoneWork: string;
		public PhoneMobile: string;
		public DateTimeCreated: Date;
		public AuthenticationType: AuthenticationType;
		public ActiveDirectory_NameDisplay: string;
		// Type not implemented: [Guid].
		//public ActiveDirectory_ObjectId: any;
		public ActiveDirectory_UsageLocation: string;
		public ActiveDirectory_JobTitle: string;
		public ActiveDirectory_Department: string;
		// Type not implemented: [Guid].
		//public ActiveDirectory_ManagerId: any;
		public ActiveDirectory_AuthenticationPhone: string;
		public ActiveDirectory_AuthenticationPhoneAlternate: string;
		public ActiveDirectory_AuthenticationEmail: string;
		public ActiveDirectory_RoleDisplayName: string;
		public ActiveDirectoryId: string;
		public Roles: Array<MasterRole>;
		public Sessions: Array<MasterSession>;
		public UserRoles: Array<MasterUserRole>;
		public NameFull: string;

		constructor ()
		{
		}
	}

	export class MasterUserRole
		implements IEntity
	{
		public Id: number;
		public RoleId: number;
		public Role: MasterRole;
		public UserId: number;
		public User: MasterUser;

		constructor ()
		{
		}
	}

	export class ApplicationVersion
		implements IEntity
	{
		public Id: number;
		public ApplicationModule: ApplicationModule;
		public VersionMajor: number;
		public VersionMinor: number;
		public VersionBuild: number;
		public VersionRevision: number;
		// Type not implemented: [Version].
		//public Version: any;

		constructor ()
		{
		}
	}

	export class Culture
		implements IEntity
	{
		public Id: number;
		public Name: string;
		public LocaleId: number;
		public NameNative: string;
		public NameDisplay: string;
		public NameEnglish: string;
		public NameIsoTwoLetter: string;
		public NameIsoThreeLetter: string;
		public NameWindowsThreeLetter: string;

		constructor ()
		{
		}
	}

	export class AuditLog
		implements IEntity
	{
		public Id: number;
		public Screen: Screen;
		public LogTime: Date;
		public ActiveUserId: number;
		public ActualUserId: number;
		public ScreenId: number;
		public FieldName: string;
		public PreviousValue: string;
		public NewValue: string;
		public Variance: number;

		constructor ()
		{
		}
	}

	export class Subscription
		implements IEntity
	{
		public Id: number;
		public MasterSubscriptionId: number;
		public SubscriptionType: EntityMasterTenantType;
		public IsDemo: boolean;
		public IsActive: boolean;
		public Description: string;
		public NumberOfFormsAllowed: number;
		public NumberOfPagesAllowed: number;
		public NumberOfUsersAllowed: number;
		public NumberOfTemplatesAllowed: number;
		public NumberOfFormsUsed: number;
		public NumberOfPagesUsed: number;
		public NumberOfUsersUsed: number;
		public NumberOfTemplatesUsed: number;
		public AllowScanning: boolean;
		public AllowBranding: boolean;
		public AllowTemplateWorkflows: boolean;
		public TenantSubscriptions: Array<TenantSubscription>;

		constructor ()
		{
		}
	}

	export class Tenant
		implements IEntity
	{
		public Id: number;
		public MasterTenantId: number;
		public TenantType: EntityMasterTenantType;
		public MasterTenantToken: string;
		public AuthenticationType: AuthenticationType;
		public Domain: string;
		public CompanyName: string;
		public ContactOwnerNameGiven: string;
		public ContactOwnerNameFamily: string;
		public ContactOwnerAddress: string;
		public ContactOwnerCity: string;
		public ContactOwnerState: string;
		public ContactOwnerZipCode: string;
		public ContactOwnerCountry: string;
		public ContactOwnerPhone: string;
		public ContactOwnerFax: string;
		public ContactOwnerEmail: string;
		public ContactAdministratorNameGiven: string;
		public ContactAdministratorNameFamily: string;
		public ContactAdministratorAddress: string;
		public ContactAdministratorCity: string;
		public ContactAdministratorState: string;
		public ContactAdministratorZipCode: string;
		public ContactAdministratorCountry: string;
		public ContactAdministratorPhone: string;
		public ContactAdministratorFax: string;
		public ContactAdministratorEmail: string;
		public ContactBillingNameGiven: string;
		public ContactBillingNameFamily: string;
		public ContactBillingAddress: string;
		public ContactBillingCity: string;
		public ContactBillingState: string;
		public ContactBillingZipCode: string;
		public ContactBillingCountry: string;
		public ContactBillingPhone: string;
		public ContactBillingFax: string;
		public ContactBillingEmail: string;
		public ContactTechnicalNameGiven: string;
		public ContactTechnicalNameFamily: string;
		public ContactTechnicalAddress: string;
		public ContactTechnicalCity: string;
		public ContactTechnicalState: string;
		public ContactTechnicalZipCode: string;
		public ContactTechnicalCountry: string;
		public ContactTechnicalPhone: string;
		public ContactTechnicalFax: string;
		public ContactTechnicalEmail: string;
		public RsaKeyPublic: string;
		public RsaKeyPrivate: string;
		public UrlApi: string;
		public UrlResourceGroup: string;
		public UrlStorage: string;
		public UrlStorageBlob: string;
		public UrlStorageTable: string;
		public UrlStorageQueue: string;
		public UrlStorageFile: string;
		public StorageAccessKeyPrimary: string;
		public StorageAccessKeySecondary: string;
		public StorageConnectionStringPrimary: string;
		public StorageConnectionStringSecondary: string;
		public DatabaseConnectionString: string;
		public Users: Array<User>;
		public Sessions: Array<Session>;
		public MasterUsers: Array<MasterUser>;
		public MasterSessions: Array<MasterSession>;
		public TenantSubscriptions: Array<TenantSubscription>;

		constructor ()
		{
		}
	}

	export class TenantSubscription
		implements IEntity
	{
		public Id: number;
		public MasterTenantSubscriptionId: number;
		public TenantSubscriptionType: EntityMasterTenantType;
		public IsDemo: boolean;
		public IsActive: boolean;
		public DateTimeStart: Date;
		public DateTimeExpires: Date;
		public NumberOfFormsAllowed: number;
		public NumberOfPagesAllowed: number;
		public NumberOfUsersAllowed: number;
		public NumberOfTemplatesAllowed: number;
		public NumberOfFormsUsed: number;
		public NumberOfPagesUsed: number;
		public NumberOfUsersUsed: number;
		public NumberOfTemplatesUsed: number;
		public AllowScanning: boolean;
		public AllowBranding: boolean;
		public AllowTemplateWorkflows: boolean;
		public RequireDelegationAcceptance: boolean;
		public Time: Date;
		public TenantId: number;
		public Tenant: Tenant;
		public SubscriptionId: number;
		public Subscription: Subscription;
		public NumberOfPagesRemaining: number;
		public NumberOfFormsRemaining: number;
		public NumberOfTemplatesRemaining: number;

		constructor ()
		{
		}
	}
}