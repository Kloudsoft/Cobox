var AffinityDms;
(function (AffinityDms) {
    var Entities;
    (function (Entities) {
        (function (AssignmentState) {
            AssignmentState[AssignmentState["None"] = 0] = "None";
            AssignmentState[AssignmentState["Assigned"] = 1] = "Assigned";
            AssignmentState[AssignmentState["Accepted"] = 2] = "Accepted";
            AssignmentState[AssignmentState["Rejected"] = 3] = "Rejected";
        })(Entities.AssignmentState || (Entities.AssignmentState = {}));
        var AssignmentState = Entities.AssignmentState;
        (function (AuditTrailActionType) {
            AuditTrailActionType[AuditTrailActionType["None"] = 0] = "None";
            AuditTrailActionType[AuditTrailActionType["View"] = 1] = "View";
            AuditTrailActionType[AuditTrailActionType["Add"] = 2] = "Add";
            AuditTrailActionType[AuditTrailActionType["Edit"] = 3] = "Edit";
            AuditTrailActionType[AuditTrailActionType["Delete"] = 4] = "Delete";
            AuditTrailActionType[AuditTrailActionType["Rename"] = 5] = "Rename";
            AuditTrailActionType[AuditTrailActionType["CancelCheckOut"] = 6] = "CancelCheckOut";
            AuditTrailActionType[AuditTrailActionType["CheckIn"] = 7] = "CheckIn";
            AuditTrailActionType[AuditTrailActionType["CheckOut"] = 8] = "CheckOut";
            AuditTrailActionType[AuditTrailActionType["Move"] = 9] = "Move";
            AuditTrailActionType[AuditTrailActionType["AddIndex"] = 10] = "AddIndex";
            AuditTrailActionType[AuditTrailActionType["UpdateVersion"] = 11] = "UpdateVersion";
        })(Entities.AuditTrailActionType || (Entities.AuditTrailActionType = {}));
        var AuditTrailActionType = Entities.AuditTrailActionType;
        (function (BarcodeType) {
            BarcodeType[BarcodeType["None"] = 0] = "None";
            BarcodeType[BarcodeType["_1D"] = 1] = "_1D";
            BarcodeType[BarcodeType["_2D"] = 2] = "_2D";
            BarcodeType[BarcodeType["_3D"] = 3] = "_3D";
        })(Entities.BarcodeType || (Entities.BarcodeType = {}));
        var BarcodeType = Entities.BarcodeType;
        (function (DocumentCorrectiveIndexValueStatus) {
            DocumentCorrectiveIndexValueStatus[DocumentCorrectiveIndexValueStatus["None"] = 0] = "None";
            DocumentCorrectiveIndexValueStatus[DocumentCorrectiveIndexValueStatus["Updated"] = 1] = "Updated";
            DocumentCorrectiveIndexValueStatus[DocumentCorrectiveIndexValueStatus["Submitted"] = 2] = "Submitted";
        })(Entities.DocumentCorrectiveIndexValueStatus || (Entities.DocumentCorrectiveIndexValueStatus = {}));
        var DocumentCorrectiveIndexValueStatus = Entities.DocumentCorrectiveIndexValueStatus;
        (function (DocumentIndexDataType) {
            DocumentIndexDataType[DocumentIndexDataType["Text"] = 0] = "Text";
            DocumentIndexDataType[DocumentIndexDataType["Digits"] = 1] = "Digits";
            DocumentIndexDataType[DocumentIndexDataType["AlphaNumeric"] = 2] = "AlphaNumeric";
            DocumentIndexDataType[DocumentIndexDataType["Date"] = 3] = "Date";
            DocumentIndexDataType[DocumentIndexDataType["Time"] = 4] = "Time";
        })(Entities.DocumentIndexDataType || (Entities.DocumentIndexDataType = {}));
        var DocumentIndexDataType = Entities.DocumentIndexDataType;
        (function (DocumentIndexType) {
            DocumentIndexType[DocumentIndexType["None"] = 0] = "None";
            DocumentIndexType[DocumentIndexType["Auto"] = 1] = "Auto";
            DocumentIndexType[DocumentIndexType["Manual"] = 2] = "Manual";
        })(Entities.DocumentIndexType || (Entities.DocumentIndexType = {}));
        var DocumentIndexType = Entities.DocumentIndexType;
        (function (DocumentQueueType) {
            DocumentQueueType[DocumentQueueType["Auto"] = 0] = "Auto";
            DocumentQueueType[DocumentQueueType["Manual"] = 1] = "Manual";
        })(Entities.DocumentQueueType || (Entities.DocumentQueueType = {}));
        var DocumentQueueType = Entities.DocumentQueueType;
        (function (DocumentWorkflowState) {
            DocumentWorkflowState[DocumentWorkflowState["None"] = 0] = "None";
            DocumentWorkflowState[DocumentWorkflowState["WaitingToBeIndexed"] = 1] = "WaitingToBeIndexed";
            DocumentWorkflowState[DocumentWorkflowState["Verified"] = 2] = "Verified";
        })(Entities.DocumentWorkflowState || (Entities.DocumentWorkflowState = {}));
        var DocumentWorkflowState = Entities.DocumentWorkflowState;
        (function (TemplateEntityState) {
            TemplateEntityState[TemplateEntityState["Web"] = 0] = "Web";
            TemplateEntityState[TemplateEntityState["Mobile"] = 1] = "Mobile";
            TemplateEntityState[TemplateEntityState["Print"] = 2] = "Print";
        })(Entities.TemplateEntityState || (Entities.TemplateEntityState = {}));
        var TemplateEntityState = Entities.TemplateEntityState;
        (function (EntityType) {
            EntityType[EntityType["None"] = 0] = "None";
            EntityType[EntityType["User"] = 1] = "User";
            EntityType[EntityType["Form"] = 2] = "Form";
            EntityType[EntityType["Document"] = 3] = "Document";
            EntityType[EntityType["Template"] = 4] = "Template";
            EntityType[EntityType["Folder"] = 5] = "Folder";
        })(Entities.EntityType || (Entities.EntityType = {}));
        var EntityType = Entities.EntityType;
        (function (SessionMessageType) {
            SessionMessageType[SessionMessageType["None"] = 0] = "None";
            SessionMessageType[SessionMessageType["Information"] = 1] = "Information";
            SessionMessageType[SessionMessageType["Warning"] = 2] = "Warning";
            SessionMessageType[SessionMessageType["Error"] = 3] = "Error";
            SessionMessageType[SessionMessageType["Unknown"] = 4] = "Unknown";
        })(Entities.SessionMessageType || (Entities.SessionMessageType = {}));
        var SessionMessageType = Entities.SessionMessageType;
        (function (ElementDataType) {
            ElementDataType[ElementDataType["None"] = 0] = "None";
            ElementDataType[ElementDataType["Alphabets"] = 1] = "Alphabets";
            ElementDataType[ElementDataType["Password"] = 2] = "Password";
            ElementDataType[ElementDataType["Numeric"] = 3] = "Numeric";
            ElementDataType[ElementDataType["Alphanumeric"] = 4] = "Alphanumeric";
            ElementDataType[ElementDataType["Email"] = 5] = "Email";
        })(Entities.ElementDataType || (Entities.ElementDataType = {}));
        var ElementDataType = Entities.ElementDataType;
        (function (ElementType) {
            ElementType[ElementType["Label"] = 0] = "Label";
            ElementType[ElementType["Textbox"] = 1] = "Textbox";
            ElementType[ElementType["Textarea"] = 2] = "Textarea";
            ElementType[ElementType["Image"] = 3] = "Image";
            ElementType[ElementType["Radio"] = 4] = "Radio";
            ElementType[ElementType["Checkbox"] = 5] = "Checkbox";
            ElementType[ElementType["Rectangle"] = 6] = "Rectangle";
            ElementType[ElementType["HorizontalLine"] = 7] = "HorizontalLine";
            ElementType[ElementType["VerticalLine"] = 8] = "VerticalLine";
            ElementType[ElementType["Circle"] = 9] = "Circle";
            ElementType[ElementType["Barcode2D"] = 10] = "Barcode2D";
            ElementType[ElementType["Table"] = 11] = "Table";
            ElementType[ElementType["TableColumn"] = 12] = "TableColumn";
        })(Entities.ElementType || (Entities.ElementType = {}));
        var ElementType = Entities.ElementType;
        (function (SessionType) {
            SessionType[SessionType["Api"] = 0] = "Api";
            SessionType[SessionType["Mvc"] = 1] = "Mvc";
        })(Entities.SessionType || (Entities.SessionType = {}));
        var SessionType = Entities.SessionType;
        (function (EntityMasterTenantType) {
            EntityMasterTenantType[EntityMasterTenantType["None"] = 0] = "None";
            EntityMasterTenantType[EntityMasterTenantType["Master"] = 1] = "Master";
            EntityMasterTenantType[EntityMasterTenantType["Tenant"] = 2] = "Tenant";
        })(Entities.EntityMasterTenantType || (Entities.EntityMasterTenantType = {}));
        var EntityMasterTenantType = Entities.EntityMasterTenantType;
        (function (SettingsType) {
            SettingsType[SettingsType["None"] = 0] = "None";
            SettingsType[SettingsType["UserManagement"] = 1] = "UserManagement";
            SettingsType[SettingsType["AuditTrail"] = 2] = "AuditTrail";
            SettingsType[SettingsType["General"] = 3] = "General";
            SettingsType[SettingsType["All"] = 4] = "All";
        })(Entities.SettingsType || (Entities.SettingsType = {}));
        var SettingsType = Entities.SettingsType;
        (function (MasterActionType) {
            MasterActionType[MasterActionType["None"] = 0] = "None";
            MasterActionType[MasterActionType["DashboardView"] = 1] = "DashboardView";
            MasterActionType[MasterActionType["UsersAdd"] = 2] = "UsersAdd";
            MasterActionType[MasterActionType["UsersEdit"] = 3] = "UsersEdit";
            MasterActionType[MasterActionType["UsersView"] = 4] = "UsersView";
            MasterActionType[MasterActionType["SubscriptionsAdd"] = 5] = "SubscriptionsAdd";
            MasterActionType[MasterActionType["SubscriptionsEdit"] = 6] = "SubscriptionsEdit";
            MasterActionType[MasterActionType["SubscriptionsView"] = 7] = "SubscriptionsView";
            MasterActionType[MasterActionType["TenantsAdd"] = 8] = "TenantsAdd";
            MasterActionType[MasterActionType["TenantsEdit"] = 9] = "TenantsEdit";
            MasterActionType[MasterActionType["TenantsView"] = 10] = "TenantsView";
            MasterActionType[MasterActionType["TenantSubscriptionsAdd"] = 11] = "TenantSubscriptionsAdd";
            MasterActionType[MasterActionType["TenantSubscriptionsEdit"] = 12] = "TenantSubscriptionsEdit";
            MasterActionType[MasterActionType["TenantSubscriptionsView"] = 13] = "TenantSubscriptionsView";
        })(Entities.MasterActionType || (Entities.MasterActionType = {}));
        var MasterActionType = Entities.MasterActionType;
        (function (TenantActionType) {
            TenantActionType[TenantActionType["None"] = 0] = "None";
            TenantActionType[TenantActionType["DashboardView"] = 1] = "DashboardView";
            TenantActionType[TenantActionType["UsersAdd"] = 2] = "UsersAdd";
            TenantActionType[TenantActionType["UsersEdit"] = 3] = "UsersEdit";
            TenantActionType[TenantActionType["UsersView"] = 4] = "UsersView";
            TenantActionType[TenantActionType["FoldersAdd"] = 5] = "FoldersAdd";
            TenantActionType[TenantActionType["FoldersEdit"] = 6] = "FoldersEdit";
            TenantActionType[TenantActionType["FoldersView"] = 7] = "FoldersView";
            TenantActionType[TenantActionType["TemplatesAdd"] = 8] = "TemplatesAdd";
            TenantActionType[TenantActionType["TemplatesEdit"] = 9] = "TemplatesEdit";
            TenantActionType[TenantActionType["TemplatesView"] = 10] = "TemplatesView";
            TenantActionType[TenantActionType["TemplatesDesign"] = 11] = "TemplatesDesign";
            TenantActionType[TenantActionType["FormsAdd"] = 12] = "FormsAdd";
            TenantActionType[TenantActionType["FormsEdit"] = 13] = "FormsEdit";
            TenantActionType[TenantActionType["FormsView"] = 14] = "FormsView";
            TenantActionType[TenantActionType["FormsDesign"] = 15] = "FormsDesign";
            TenantActionType[TenantActionType["DocumentsAdd"] = 16] = "DocumentsAdd";
            TenantActionType[TenantActionType["DocumentsEdit"] = 17] = "DocumentsEdit";
            TenantActionType[TenantActionType["DocumentsView"] = 18] = "DocumentsView";
            TenantActionType[TenantActionType["DocumentsScan"] = 19] = "DocumentsScan";
            TenantActionType[TenantActionType["DocumentsUpload"] = 20] = "DocumentsUpload";
            TenantActionType[TenantActionType["DocumentsIndex"] = 21] = "DocumentsIndex";
            TenantActionType[TenantActionType["WorkflowAdd"] = 22] = "WorkflowAdd";
            TenantActionType[TenantActionType["WorkflowEdit"] = 23] = "WorkflowEdit";
            TenantActionType[TenantActionType["WorkflowView"] = 24] = "WorkflowView";
            TenantActionType[TenantActionType["WorkflowAction"] = 25] = "WorkflowAction";
            TenantActionType[TenantActionType["WorkflowDesign"] = 26] = "WorkflowDesign";
        })(Entities.TenantActionType || (Entities.TenantActionType = {}));
        var TenantActionType = Entities.TenantActionType;
        (function (DeviceType) {
            DeviceType[DeviceType["None"] = 0] = "None";
            DeviceType[DeviceType["Desktop"] = 1] = "Desktop";
            DeviceType[DeviceType["Mobile"] = 2] = "Mobile";
            DeviceType[DeviceType["Other"] = 3] = "Other";
            DeviceType[DeviceType["Unknown"] = 4] = "Unknown";
        })(Entities.DeviceType || (Entities.DeviceType = {}));
        var DeviceType = Entities.DeviceType;
        (function (DiscussionPostAttachmentType) {
            DiscussionPostAttachmentType[DiscussionPostAttachmentType["None"] = 0] = "None";
            DiscussionPostAttachmentType[DiscussionPostAttachmentType["Template"] = 1] = "Template";
            DiscussionPostAttachmentType[DiscussionPostAttachmentType["Document"] = 2] = "Document";
            DiscussionPostAttachmentType[DiscussionPostAttachmentType["External"] = 3] = "External";
            DiscussionPostAttachmentType[DiscussionPostAttachmentType["Live"] = 4] = "Live";
            DiscussionPostAttachmentType[DiscussionPostAttachmentType["Form"] = 5] = "Form";
        })(Entities.DiscussionPostAttachmentType || (Entities.DiscussionPostAttachmentType = {}));
        var DiscussionPostAttachmentType = Entities.DiscussionPostAttachmentType;
        (function (DocumentEntryState) {
            DocumentEntryState[DocumentEntryState["None"] = 0] = "None";
            DocumentEntryState[DocumentEntryState["Imported"] = 1] = "Imported";
            DocumentEntryState[DocumentEntryState["Queued"] = 2] = "Queued";
            DocumentEntryState[DocumentEntryState["Uploading"] = 3] = "Uploading";
            DocumentEntryState[DocumentEntryState["Uploaded"] = 4] = "Uploaded";
        })(Entities.DocumentEntryState || (Entities.DocumentEntryState = {}));
        var DocumentEntryState = Entities.DocumentEntryState;
        (function (DocumentState) {
            DocumentState[DocumentState["None"] = 0] = "None";
            DocumentState[DocumentState["Uploading"] = 1] = "Uploading";
            DocumentState[DocumentState["QueuedAuto"] = 2] = "QueuedAuto";
            DocumentState[DocumentState["QueuedManual"] = 3] = "QueuedManual";
            DocumentState[DocumentState["Matched"] = 4] = "Matched";
            DocumentState[DocumentState["MatchedMultiple"] = 5] = "MatchedMultiple";
            DocumentState[DocumentState["UnMatched"] = 6] = "UnMatched";
            DocumentState[DocumentState["MatchedUnverified"] = 7] = "MatchedUnverified";
            DocumentState[DocumentState["Verified"] = 8] = "Verified";
            DocumentState[DocumentState["Processing"] = 9] = "Processing";
        })(Entities.DocumentState || (Entities.DocumentState = {}));
        var DocumentState = Entities.DocumentState;
        (function (DocumentType) {
            DocumentType[DocumentType["None"] = 0] = "None";
            DocumentType[DocumentType["Raster"] = 1] = "Raster";
            DocumentType[DocumentType["Digital"] = 2] = "Digital";
            DocumentType[DocumentType["Unknown"] = 3] = "Unknown";
        })(Entities.DocumentType || (Entities.DocumentType = {}));
        var DocumentType = Entities.DocumentType;
        (function (ElementIndexType) {
            ElementIndexType[ElementIndexType["Label"] = 0] = "Label";
            ElementIndexType[ElementIndexType["Value"] = 1] = "Value";
            ElementIndexType[ElementIndexType["LabelValue"] = 2] = "LabelValue";
        })(Entities.ElementIndexType || (Entities.ElementIndexType = {}));
        var ElementIndexType = Entities.ElementIndexType;
        (function (FileFormatType) {
            FileFormatType[FileFormatType["None"] = 0] = "None";
            FileFormatType[FileFormatType["Bmp"] = 1] = "Bmp";
            FileFormatType[FileFormatType["Jpg"] = 2] = "Jpg";
            FileFormatType[FileFormatType["Png"] = 3] = "Png";
            FileFormatType[FileFormatType["Tiff"] = 4] = "Tiff";
            FileFormatType[FileFormatType["Pdf"] = 5] = "Pdf";
            FileFormatType[FileFormatType["Unknown"] = 6] = "Unknown";
        })(Entities.FileFormatType || (Entities.FileFormatType = {}));
        var FileFormatType = Entities.FileFormatType;
        (function (FileType) {
            FileType[FileType["None"] = 0] = "None";
            FileType[FileType["Image"] = 1] = "Image";
            FileType[FileType["Document"] = 2] = "Document";
            FileType[FileType["Unknown"] = 3] = "Unknown";
        })(Entities.FileType || (Entities.FileType = {}));
        var FileType = Entities.FileType;
        (function (FolderType) {
            FolderType[FolderType["None"] = 0] = "None";
            FolderType[FolderType["Root"] = 1] = "Root";
            FolderType[FolderType["EnterpriseRoot"] = 2] = "EnterpriseRoot";
            FolderType[FolderType["EnterpriseChild"] = 3] = "EnterpriseChild";
            FolderType[FolderType["SharedRoot"] = 4] = "SharedRoot";
            FolderType[FolderType["SharedChild"] = 5] = "SharedChild";
            FolderType[FolderType["UserRoot"] = 6] = "UserRoot";
            FolderType[FolderType["UserChild"] = 7] = "UserChild";
            FolderType[FolderType["ProjectRoot"] = 8] = "ProjectRoot";
            FolderType[FolderType["ProjectChild"] = 9] = "ProjectChild";
            FolderType[FolderType["Child"] = 10] = "Child";
        })(Entities.FolderType || (Entities.FolderType = {}));
        var FolderType = Entities.FolderType;
        (function (OcrConfidence) {
            OcrConfidence[OcrConfidence["None"] = 0] = "None";
            OcrConfidence[OcrConfidence["MinimumRecognitionConfidence"] = 60] = "MinimumRecognitionConfidence";
            OcrConfidence[OcrConfidence["MinimumOCRConfidence"] = 80] = "MinimumOCRConfidence";
        })(Entities.OcrConfidence || (Entities.OcrConfidence = {}));
        var OcrConfidence = Entities.OcrConfidence;
        (function (MasterRoleType) {
            MasterRoleType[MasterRoleType["None"] = 0] = "None";
            MasterRoleType[MasterRoleType["Custom"] = 1] = "Custom";
            MasterRoleType[MasterRoleType["Administrator"] = 2] = "Administrator";
            MasterRoleType[MasterRoleType["Reporting"] = 3] = "Reporting";
        })(Entities.MasterRoleType || (Entities.MasterRoleType = {}));
        var MasterRoleType = Entities.MasterRoleType;
        (function (TenantRoleType) {
            TenantRoleType[TenantRoleType["None"] = 0] = "None";
            TenantRoleType[TenantRoleType["Custom"] = 1] = "Custom";
            TenantRoleType[TenantRoleType["Administrator"] = 2] = "Administrator";
            TenantRoleType[TenantRoleType["Scanner"] = 3] = "Scanner";
            TenantRoleType[TenantRoleType["Uploader"] = 4] = "Uploader";
            TenantRoleType[TenantRoleType["Indexer"] = 5] = "Indexer";
            TenantRoleType[TenantRoleType["TemplateCreator"] = 6] = "TemplateCreator";
            TenantRoleType[TenantRoleType["FormCreator"] = 7] = "FormCreator";
            TenantRoleType[TenantRoleType["WorkflowActor"] = 8] = "WorkflowActor";
            TenantRoleType[TenantRoleType["WorkflowCreator"] = 9] = "WorkflowCreator";
            TenantRoleType[TenantRoleType["Reporting"] = 10] = "Reporting";
        })(Entities.TenantRoleType || (Entities.TenantRoleType = {}));
        var TenantRoleType = Entities.TenantRoleType;
        (function (SeparatorType) {
            SeparatorType[SeparatorType["None"] = 0] = "None";
            SeparatorType[SeparatorType["DocumentSet"] = 1] = "DocumentSet";
            SeparatorType[SeparatorType["DocumentType"] = 2] = "DocumentType";
        })(Entities.SeparatorType || (Entities.SeparatorType = {}));
        var SeparatorType = Entities.SeparatorType;
        (function (SourceType) {
            SourceType[SourceType["None"] = 0] = "None";
            SourceType[SourceType["MobileCamera"] = 1] = "MobileCamera";
            SourceType[SourceType["MobileFileSystem"] = 2] = "MobileFileSystem";
            SourceType[SourceType["DesktopCamera"] = 3] = "DesktopCamera";
            SourceType[SourceType["DesktopScanner"] = 4] = "DesktopScanner";
            SourceType[SourceType["DesktopFileSystem"] = 5] = "DesktopFileSystem";
            SourceType[SourceType["BrowserFileSystem"] = 6] = "BrowserFileSystem";
            SourceType[SourceType["Other"] = 7] = "Other";
        })(Entities.SourceType || (Entities.SourceType = {}));
        var SourceType = Entities.SourceType;
        (function (TemplateType) {
            TemplateType[TemplateType["Form"] = 0] = "Form";
            TemplateType[TemplateType["Template"] = 1] = "Template";
        })(Entities.TemplateType || (Entities.TemplateType = {}));
        var TemplateType = Entities.TemplateType;
        (function (AuthenticationType) {
            AuthenticationType[AuthenticationType["None"] = 0] = "None";
            AuthenticationType[AuthenticationType["Local"] = 1] = "Local";
            AuthenticationType[AuthenticationType["ActiveDirectory"] = 2] = "ActiveDirectory";
            AuthenticationType[AuthenticationType["Both"] = 3] = "Both";
            AuthenticationType[AuthenticationType["External"] = 4] = "External";
        })(Entities.AuthenticationType || (Entities.AuthenticationType = {}));
        var AuthenticationType = Entities.AuthenticationType;
        (function (DocumentSortOrder) {
            DocumentSortOrder[DocumentSortOrder["None"] = 0] = "None";
            DocumentSortOrder[DocumentSortOrder["Ascending"] = 1] = "Ascending";
            DocumentSortOrder[DocumentSortOrder["Descending"] = 2] = "Descending";
        })(Entities.DocumentSortOrder || (Entities.DocumentSortOrder = {}));
        var DocumentSortOrder = Entities.DocumentSortOrder;
        (function (DocumentSortMember) {
            DocumentSortMember[DocumentSortMember["Id"] = 0] = "Id";
            DocumentSortMember[DocumentSortMember["Name"] = 1] = "Name";
            DocumentSortMember[DocumentSortMember["None"] = -1] = "None";
        })(Entities.DocumentSortMember || (Entities.DocumentSortMember = {}));
        var DocumentSortMember = Entities.DocumentSortMember;
        (function (IEntitySortMember) {
            IEntitySortMember[IEntitySortMember["Id"] = 0] = "Id";
            IEntitySortMember[IEntitySortMember["None"] = -1] = "None";
        })(Entities.IEntitySortMember || (Entities.IEntitySortMember = {}));
        var IEntitySortMember = Entities.IEntitySortMember;
        (function (IEntitySortOrder) {
            IEntitySortOrder[IEntitySortOrder["None"] = 0] = "None";
            IEntitySortOrder[IEntitySortOrder["Ascending"] = 1] = "Ascending";
            IEntitySortOrder[IEntitySortOrder["Descending"] = 2] = "Descending";
        })(Entities.IEntitySortOrder || (Entities.IEntitySortOrder = {}));
        var IEntitySortOrder = Entities.IEntitySortOrder;
        (function (ApplicationModule) {
            ApplicationModule[ApplicationModule["Library"] = 0] = "Library";
            ApplicationModule[ApplicationModule["Entities"] = 1] = "Entities";
            ApplicationModule[ApplicationModule["DataAccess"] = 2] = "DataAccess";
            ApplicationModule[ApplicationModule["Business"] = 3] = "Business";
            ApplicationModule[ApplicationModule["Console"] = 4] = "Console";
            ApplicationModule[ApplicationModule["LibraryOcrLeadTools"] = 5] = "LibraryOcrLeadTools";
            ApplicationModule[ApplicationModule["Worker"] = 6] = "Worker";
            ApplicationModule[ApplicationModule["Web"] = 7] = "Web";
            ApplicationModule[ApplicationModule["Desktop"] = 8] = "Desktop";
        })(Entities.ApplicationModule || (Entities.ApplicationModule = {}));
        var ApplicationModule = Entities.ApplicationModule;
        var AuditTrailEntry = (function () {
            function AuditTrailEntry() {
            }
            return AuditTrailEntry;
        }());
        Entities.AuditTrailEntry = AuditTrailEntry;
        var Button = (function () {
            function Button() {
            }
            return Button;
        }());
        Entities.Button = Button;
        var Department = (function () {
            function Department() {
            }
            return Department;
        }());
        Entities.Department = Department;
        var Discourse = (function () {
            function Discourse() {
            }
            return Discourse;
        }());
        Entities.Discourse = Discourse;
        var DiscoursePostVersion = (function () {
            function DiscoursePostVersion() {
            }
            return DiscoursePostVersion;
        }());
        Entities.DiscoursePostVersion = DiscoursePostVersion;
        var DiscoursePost = (function () {
            function DiscoursePost() {
            }
            return DiscoursePost;
        }());
        Entities.DiscoursePost = DiscoursePost;
        var DiscoursePostVersionAttachment = (function () {
            function DiscoursePostVersionAttachment() {
            }
            return DiscoursePostVersionAttachment;
        }());
        Entities.DiscoursePostVersionAttachment = DiscoursePostVersionAttachment;
        var DiscourseUser = (function () {
            function DiscourseUser() {
            }
            return DiscourseUser;
        }());
        Entities.DiscourseUser = DiscourseUser;
        var DocumentCorrectiveIndexValue = (function () {
            function DocumentCorrectiveIndexValue() {
            }
            return DocumentCorrectiveIndexValue;
        }());
        Entities.DocumentCorrectiveIndexValue = DocumentCorrectiveIndexValue;
        var DocumentIndex = (function () {
            function DocumentIndex() {
            }
            return DocumentIndex;
        }());
        Entities.DocumentIndex = DocumentIndex;
        var Document = (function () {
            function Document() {
            }
            return Document;
        }());
        Entities.Document = Document;
        var DocumentElement = (function () {
            function DocumentElement() {
            }
            return DocumentElement;
        }());
        Entities.DocumentElement = DocumentElement;
        var DocumentFragment = (function () {
            function DocumentFragment() {
            }
            return DocumentFragment;
        }());
        Entities.DocumentFragment = DocumentFragment;
        var DocumentSearchCriteria = (function () {
            function DocumentSearchCriteria() {
            }
            return DocumentSearchCriteria;
        }());
        Entities.DocumentSearchCriteria = DocumentSearchCriteria;
        var DocumentTag = (function () {
            function DocumentTag() {
            }
            return DocumentTag;
        }());
        Entities.DocumentTag = DocumentTag;
        var DocumentTagUser = (function () {
            function DocumentTagUser() {
            }
            return DocumentTagUser;
        }());
        Entities.DocumentTagUser = DocumentTagUser;
        var DocumentTemplate = (function () {
            function DocumentTemplate() {
            }
            return DocumentTemplate;
        }());
        Entities.DocumentTemplate = DocumentTemplate;
        var DocumentXmlElement = (function () {
            function DocumentXmlElement() {
            }
            return DocumentXmlElement;
        }());
        Entities.DocumentXmlElement = DocumentXmlElement;
        var EntityWorkflowMapping = (function () {
            function EntityWorkflowMapping() {
            }
            return EntityWorkflowMapping;
        }());
        Entities.EntityWorkflowMapping = EntityWorkflowMapping;
        var Folder = (function () {
            function Folder() {
            }
            return Folder;
        }());
        Entities.Folder = Folder;
        var Role = (function () {
            function Role() {
            }
            return Role;
        }());
        Entities.Role = Role;
        var RoleDelegation = (function () {
            function RoleDelegation() {
            }
            return RoleDelegation;
        }());
        Entities.RoleDelegation = RoleDelegation;
        var RoleRight = (function () {
            function RoleRight() {
            }
            return RoleRight;
        }());
        Entities.RoleRight = RoleRight;
        var RuleDetail = (function () {
            function RuleDetail() {
            }
            return RuleDetail;
        }());
        Entities.RuleDetail = RuleDetail;
        var RuleDetailInstance = (function () {
            function RuleDetailInstance() {
            }
            return RuleDetailInstance;
        }());
        Entities.RuleDetailInstance = RuleDetailInstance;
        var ScanSession = (function () {
            function ScanSession() {
            }
            return ScanSession;
        }());
        Entities.ScanSession = ScanSession;
        var Screen = (function () {
            function Screen() {
            }
            return Screen;
        }());
        Entities.Screen = Screen;
        var Session = (function () {
            function Session() {
            }
            return Session;
        }());
        Entities.Session = Session;
        var SessionMessage = (function () {
            function SessionMessage() {
            }
            return SessionMessage;
        }());
        Entities.SessionMessage = SessionMessage;
        var TableHistory = (function () {
            function TableHistory() {
            }
            return TableHistory;
        }());
        Entities.TableHistory = TableHistory;
        var Template = (function () {
            // Type not implemented: [Version].
            //public Version: any;
            function Template() {
            }
            return Template;
        }());
        Entities.Template = Template;
        var TemplateElement = (function () {
            function TemplateElement() {
            }
            return TemplateElement;
        }());
        Entities.TemplateElement = TemplateElement;
        var TemplateElementDetail = (function () {
            function TemplateElementDetail() {
            }
            return TemplateElementDetail;
        }());
        Entities.TemplateElementDetail = TemplateElementDetail;
        var TemplateElementDetailExtended = (function () {
            function TemplateElementDetailExtended() {
            }
            return TemplateElementDetailExtended;
        }());
        Entities.TemplateElementDetailExtended = TemplateElementDetailExtended;
        var TemplateElementValue = (function () {
            function TemplateElementValue() {
            }
            return TemplateElementValue;
        }());
        Entities.TemplateElementValue = TemplateElementValue;
        var TemplateInstance = (function () {
            function TemplateInstance() {
            }
            return TemplateInstance;
        }());
        Entities.TemplateInstance = TemplateInstance;
        var TemplatePage = (function () {
            // Type not implemented: [Byte[]].
            //public ImageBackground: any;
            function TemplatePage() {
            }
            return TemplatePage;
        }());
        Entities.TemplatePage = TemplatePage;
        var TemplateTag = (function () {
            function TemplateTag() {
            }
            return TemplateTag;
        }());
        Entities.TemplateTag = TemplateTag;
        var TemplateTagUser = (function () {
            function TemplateTagUser() {
            }
            return TemplateTagUser;
        }());
        Entities.TemplateTagUser = TemplateTagUser;
        var TemplateVersion = (function () {
            function TemplateVersion() {
            }
            return TemplateVersion;
        }());
        Entities.TemplateVersion = TemplateVersion;
        var User = (function () {
            function User() {
            }
            return User;
        }());
        Entities.User = User;
        var UserDelegation = (function () {
            function UserDelegation() {
            }
            return UserDelegation;
        }());
        Entities.UserDelegation = UserDelegation;
        var UserDocument = (function () {
            function UserDocument() {
            }
            return UserDocument;
        }());
        Entities.UserDocument = UserDocument;
        var UserDocumentLabel = (function () {
            function UserDocumentLabel() {
            }
            return UserDocumentLabel;
        }());
        Entities.UserDocumentLabel = UserDocumentLabel;
        var UserFolder = (function () {
            function UserFolder() {
            }
            return UserFolder;
        }());
        Entities.UserFolder = UserFolder;
        var UserHistory = (function () {
            function UserHistory() {
            }
            return UserHistory;
        }());
        Entities.UserHistory = UserHistory;
        var UserInvite = (function () {
            function UserInvite() {
            }
            return UserInvite;
        }());
        Entities.UserInvite = UserInvite;
        var UserLabel = (function () {
            function UserLabel() {
            }
            return UserLabel;
        }());
        Entities.UserLabel = UserLabel;
        var UserRole = (function () {
            function UserRole() {
            }
            return UserRole;
        }());
        Entities.UserRole = UserRole;
        var UserTemplate = (function () {
            function UserTemplate() {
            }
            return UserTemplate;
        }());
        Entities.UserTemplate = UserTemplate;
        var WorkflowAction = (function () {
            function WorkflowAction() {
            }
            return WorkflowAction;
        }());
        Entities.WorkflowAction = WorkflowAction;
        var WorkflowActor = (function () {
            function WorkflowActor() {
            }
            return WorkflowActor;
        }());
        Entities.WorkflowActor = WorkflowActor;
        var WorkflowActorsInstance = (function () {
            function WorkflowActorsInstance() {
            }
            return WorkflowActorsInstance;
        }());
        Entities.WorkflowActorsInstance = WorkflowActorsInstance;
        var WorkflowInstance = (function () {
            function WorkflowInstance() {
            }
            return WorkflowInstance;
        }());
        Entities.WorkflowInstance = WorkflowInstance;
        var WorkflowMaster = (function () {
            function WorkflowMaster() {
            }
            return WorkflowMaster;
        }());
        Entities.WorkflowMaster = WorkflowMaster;
        var WorkflowRule = (function () {
            function WorkflowRule() {
            }
            return WorkflowRule;
        }());
        Entities.WorkflowRule = WorkflowRule;
        var WorkflowRuleInstance = (function () {
            function WorkflowRuleInstance() {
            }
            return WorkflowRuleInstance;
        }());
        Entities.WorkflowRuleInstance = WorkflowRuleInstance;
        var WorkflowStage = (function () {
            function WorkflowStage() {
            }
            return WorkflowStage;
        }());
        Entities.WorkflowStage = WorkflowStage;
        var WorkflowStagesInstance = (function () {
            function WorkflowStagesInstance() {
            }
            return WorkflowStagesInstance;
        }());
        Entities.WorkflowStagesInstance = WorkflowStagesInstance;
        var WorkflowTemplate = (function () {
            function WorkflowTemplate() {
            }
            return WorkflowTemplate;
        }());
        Entities.WorkflowTemplate = WorkflowTemplate;
        var WorkflowTransaction = (function () {
            function WorkflowTransaction() {
            }
            return WorkflowTransaction;
        }());
        Entities.WorkflowTransaction = WorkflowTransaction;
        var WorkflowUserAction = (function () {
            function WorkflowUserAction() {
            }
            return WorkflowUserAction;
        }());
        Entities.WorkflowUserAction = WorkflowUserAction;
        var WorkflowUserActionInstance = (function () {
            function WorkflowUserActionInstance() {
            }
            return WorkflowUserActionInstance;
        }());
        Entities.WorkflowUserActionInstance = WorkflowUserActionInstance;
        var MasterRole = (function () {
            function MasterRole() {
            }
            return MasterRole;
        }());
        Entities.MasterRole = MasterRole;
        var MasterSession = (function () {
            // Type not implemented: [IPAddress].
            //public IPAddress: any;
            function MasterSession() {
            }
            return MasterSession;
        }());
        Entities.MasterSession = MasterSession;
        var MasterUser = (function () {
            function MasterUser() {
            }
            return MasterUser;
        }());
        Entities.MasterUser = MasterUser;
        var MasterUserRole = (function () {
            function MasterUserRole() {
            }
            return MasterUserRole;
        }());
        Entities.MasterUserRole = MasterUserRole;
        var ApplicationVersion = (function () {
            // Type not implemented: [Version].
            //public Version: any;
            function ApplicationVersion() {
            }
            return ApplicationVersion;
        }());
        Entities.ApplicationVersion = ApplicationVersion;
        var Culture = (function () {
            function Culture() {
            }
            return Culture;
        }());
        Entities.Culture = Culture;
        var AuditLog = (function () {
            function AuditLog() {
            }
            return AuditLog;
        }());
        Entities.AuditLog = AuditLog;
        var Subscription = (function () {
            function Subscription() {
            }
            return Subscription;
        }());
        Entities.Subscription = Subscription;
        var Tenant = (function () {
            function Tenant() {
            }
            return Tenant;
        }());
        Entities.Tenant = Tenant;
        var TenantSubscription = (function () {
            function TenantSubscription() {
            }
            return TenantSubscription;
        }());
        Entities.TenantSubscription = TenantSubscription;
    })(Entities = AffinityDms.Entities || (AffinityDms.Entities = {}));
})(AffinityDms || (AffinityDms = {}));
//# sourceMappingURL=Entities.js.map