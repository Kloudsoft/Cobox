using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System.Data.Entity.Core.Objects;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public static class AuditTrailManagement
    {
        public static void Add(ContextTenant context, AuditTrailEntry auditTrailEntry)
        {
            if (auditTrailEntry != null && context != null)
            {
                if (auditTrailEntry.EntityType == EntityType.None) { throw (new ArgumentException($"The argument [{nameof(auditTrailEntry)}.{nameof(auditTrailEntry.EntityType)}] has an invalid value of [{auditTrailEntry}].", $"{nameof(auditTrailEntry)}.{nameof(auditTrailEntry.EntityType)}")); }
                if (!Enum.IsDefined(typeof(EntityType), auditTrailEntry.EntityType)) { throw (new ArgumentException($"The argument [{nameof(auditTrailEntry)}.{nameof(auditTrailEntry.EntityType)}] has an invalid value of [{auditTrailEntry.EntityType}].", $"{nameof(auditTrailEntry)}.{nameof(auditTrailEntry.EntityType)}")); }

                if (auditTrailEntry.AuditTrailActionType == AuditTrailActionType.None) { throw (new ArgumentException($"The argument [{nameof(auditTrailEntry)}.{nameof(auditTrailEntry.AuditTrailActionType)}] has an invalid value of [{auditTrailEntry.AuditTrailActionType}].", $"{nameof(auditTrailEntry)}.{nameof(auditTrailEntry.AuditTrailActionType)}")); }
                if (!Enum.IsDefined(typeof(AuditTrailActionType), auditTrailEntry.AuditTrailActionType)) { throw (new ArgumentException($"The argument [{nameof(auditTrailEntry)}.{nameof(auditTrailEntry.AuditTrailActionType)}] has an invalid value of [{auditTrailEntry.AuditTrailActionType}].", $"{nameof(auditTrailEntry)}.{nameof(auditTrailEntry.AuditTrailActionType)}")); }

                if (auditTrailEntry.DateTime < DateTime.UtcNow.Subtract(TimeSpan.FromDays(1))) { auditTrailEntry.DateTime = DateTime.UtcNow; }

                auditTrailEntry.User = null;
                auditTrailEntry.EntityUser = null;

                context.AuditTrailEntries.Add(auditTrailEntry);

                context.SaveChanges();
            }

        }

        #region AuditTrailFiller

        public static AuditTrailEntry CreateForRetrievingDocument(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            if (document == null) { return null; }
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieve(tenantUserSession, EntityType.Document, document.Id, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Retrieve;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForRenamingDocument(TenantUserSession tenantUserSession, Document document, string oldDocumentName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRename(tenantUserSession, EntityType.Document, document.Id, document.DocumentOriginalId, document.DocumentParent, oldDocumentName, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Rename;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.RenameDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", oldDocumentName).Replace("{ItemNameNew}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingDocumentVersions(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            if (document == null) { return null; }
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrievingVersions(tenantUserSession, EntityType.Document, document.Id, document.DocumentOriginalId, document.DocumentParent, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.RetrieveVersions;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingAllDocuments(TenantUserSession tenantUserSession)//, List<Document> document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieve(tenantUserSession, EntityType.Document, 0, "List", null);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.RetrieveAll;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", "List").ToString();
            //auditTrail.EntityTypeId = 0;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;// document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = null;// document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForMovingDocument(TenantUserSession tenantUserSession, string oldFolderName, Document document, long? transactionId = null)
        {
            if (document == null) { return null; }
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForMove(tenantUserSession, EntityType.Document, document.Id, document.DocumentOriginalId, document.DocumentParent, document.Name, oldFolderName, document.Folder.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Move;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.MoveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).Replace("{ItemMoveFrom}", oldFolderName).Replace("{ItemMoveTo}", document.Folder.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForDocumentCancelCheckout(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForCancelCheckout(tenantUserSession, EntityType.Document, document.Id, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.CancelCheckOut;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.CancelCheckOutDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);

        }
        public static AuditTrailEntry CreateForRetrievingDocumentUsers(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrievingUsers(tenantUserSession, EntityType.Document, document.Id, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.RetrieveAccessRights;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.RetrieveAccessRightsDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForUpdateDocumentRights(TenantUserSession tenantUserSession, Document document, User user, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForEditingUserRights(tenantUserSession, EntityType.Document, document.Id, document.DocumentOriginalId, document.DocumentParent, document.Name, user, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.UpdateAccessRights;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.UpdateAccessRightsDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).Replace("{AssignedToUser}", user.NameFull).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = user.Id;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForCheckingoutDocument(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.CheckOut;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = EntityType.Document;
            auditTrail.Description = AuditTrailConstants.CheckOutDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            auditTrail.EntityTypeId = document.Id;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            auditTrail.EntityDocumentParentId = document.DocumentParent;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForCheckinginDocument(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.CheckIn;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = EntityType.Document;
            auditTrail.Description = AuditTrailConstants.CheckInDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            auditTrail.EntityTypeId = document.Id;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            auditTrail.EntityDocumentParentId = document.DocumentParent;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForEditingDocumentIndex(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForEdit(tenantUserSession, EntityType.DocumentIndex, document.Id, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.CheckIn;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.CheckInDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForEnablingDocumentManualClassification(TenantUserSession tenantUserSession, Document document, Template template, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.EnableManualClassification;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = EntityType.Document;
            auditTrail.Description = AuditTrailConstants.EnableManualClassificationDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).Replace("{TemplateName}", template.Title).ToString();
            auditTrail.EntityTypeId = document.Id;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            auditTrail.EntityDocumentParentId = document.DocumentParent;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForAddingDocument(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForAdd(tenantUserSession, EntityType.Document, document.Id, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Add;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.AddDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForFinalizingDocument(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.FinalizeDocument;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = EntityType.Document;
            auditTrail.Description = AuditTrailConstants.FinalizeDocumentDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            auditTrail.EntityTypeId = document.Id;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            auditTrail.EntityDocumentParentId = document.DocumentParent;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForDeletingDocument(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailFoDelete(tenantUserSession, EntityType.Document, document.Id, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Delete;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.DeleteDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingAllDocumentsByUser(TenantUserSession tenantUserSession, long userId)//, List<Document> document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieveByUser(tenantUserSession, EntityType.Document, 0, "List", userId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.RetrieveAll;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", "List").ToString();
            //auditTrail.EntityTypeId = 0;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;
            //auditTrail.EntityDocumentParentId = null;
            //auditTrail.EntityUserId = userId;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingAllRelatedDocuments(TenantUserSession tenantUserSession, Document document)//, List<Document> document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.RetrieveAll;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = null;//transactionId;
            auditTrail.EntityType = EntityType.Document;
            auditTrail.Description = AuditTrailConstants.RetrieveAllRelatedDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", "List").ToString();
            auditTrail.EntityTypeId = document.Id;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForUpdatingDocumentVersion(TenantUserSession tenantUserSession, string newVersionNumber, Document document)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.UpdateVersion;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = null;//transactionId;
            auditTrail.EntityType = EntityType.Document;
            auditTrail.Description = AuditTrailConstants.UpdateVersionDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).Replace("{ItemOldVersion}", (document.VersionMajor + "." + document.VersionMinor)).Replace("{ItemNewVersion}", newVersionNumber).ToString();
            auditTrail.EntityTypeId = document.Id;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForEditingDocument(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForEdit(tenantUserSession, EntityType.Document, document.Id, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Edit;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.EditDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;
            //auditTrail.EntityDocumentParentId = null;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForMarkingDocumentPrivate(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForMarkingPrivate(tenantUserSession, EntityType.Document, document.Id, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.MarkPrivate;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.MarkPrivateDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;
            //auditTrail.EntityDocumentParentId = null;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForMarkingDocumentPublic(TenantUserSession tenantUserSession, Document document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForMarkingPublic(tenantUserSession, EntityType.Document, document.Id, document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.MarkPublic;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.MarkPublicDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;
            //auditTrail.EntityDocumentParentId = null;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForSearchingDocument(TenantUserSession tenantUserSession, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForSearch(tenantUserSession, EntityType.Document, 0, "List", "", transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Search;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.MarkPublicDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", "List").ToString();
            //auditTrail.EntityTypeId = 0;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;
            //auditTrail.EntityDocumentParentId = null;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForAddingDocumentFragments(TenantUserSession tenantUserSession, DocumentFragment documentFragment, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForAdd(tenantUserSession, EntityType.DocumentFragment, documentFragment.Id, documentFragment.Document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.AddFragments;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.DocumentFragment;
            //auditTrail.Description = AuditTrailConstants.AddDocumentFragmentsDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", documentFragment.Document.Name).ToString();
            //auditTrail.EntityTypeId = documentFragment.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;
            //auditTrail.EntityDocumentParentId = null;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForConfirmingDocumentCorrectiveIndexing(TenantUserSession tenantUserSession, Document document)//, List<Document> document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForConfirm(tenantUserSession, EntityType.DocumentCorrectiveIndexValue, 0, document.Name);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.AddCorrectiveIndexing;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.PerformCorrectiveIndexDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;// document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = null;// document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForEditingDocumentCorrectiveIndexing(TenantUserSession tenantUserSession, DocumentCorrectiveIndexValue documentCorrectiveIndexValue, long? transactionId = null)//, List<Document> document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForEdit(tenantUserSession, EntityType.DocumentCorrectiveIndexValue, documentCorrectiveIndexValue.Id, documentCorrectiveIndexValue.Document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Edit;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.DocumentCorrectiveIndexValue;
            //auditTrail.Description = AuditTrailConstants.EditDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;// document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = null;// document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingDocumentCorrectiveIndexValues(TenantUserSession tenantUserSession)//, List<Document> document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieve(tenantUserSession, EntityType.DocumentCorrectiveIndexValue, 0, "List", null);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Retrieve;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.DocumentCorrectiveIndexValue;
            //auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;// document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = null;// document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingDocumentCorrectiveIndexValues(TenantUserSession tenantUserSession, DocumentCorrectiveIndexValue documentCorrectiveIndexValue, long? transactionId = null)//, List<Document> document, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieve(tenantUserSession, EntityType.DocumentCorrectiveIndexValue, documentCorrectiveIndexValue.Id, documentCorrectiveIndexValue.Document.Name, transactionId);
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingAllDocumentElements(TenantUserSession tenantUserSession)//, List<Document> document, long? transactionId = null)
        {

            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieve(tenantUserSession, EntityType.DocumentElement, 0, "List", null);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Retrieve;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.DocumentElement;
            //auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", "List").ToString();
            //auditTrail.EntityTypeId = 0;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;// document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = null;// document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForAddingDocumentElement(TenantUserSession tenantUserSession, DocumentElement documentElement, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForAdd(tenantUserSession, EntityType.DocumentElement, documentElement.Id, documentElement.Document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Add;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.DocumentElement;
            //auditTrail.Description = AuditTrailConstants.AddDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", documentElement.Document.Name).ToString();
            //auditTrail.EntityTypeId = documentElement.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;// document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = null;// document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForEditingDocumentElement(TenantUserSession tenantUserSession, DocumentElement documentElement, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForEdit(tenantUserSession, EntityType.DocumentElement, documentElement.Id, documentElement.Document.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Edit;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.DocumentElement;
            //auditTrail.Description = AuditTrailConstants.EditDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", documentElement.Document.Name).ToString();
            //auditTrail.EntityTypeId = documentElement.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;// document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = null;// document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingAllDocumentIndex(TenantUserSession tenantUserSession)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieve(tenantUserSession, EntityType.DocumentIndex, 0, "List");
            return (auditTrail);
            //AuditTrailEntry auditTrail = new AuditTrailEntry();
            //auditTrail.AuditTrailActionType = AuditTrailActionType.Retrieve;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.DocumentIndex;
            //auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", "List").ToString();
            //auditTrail.EntityTypeId = 0;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;// document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = null;// document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }
        public static AuditTrailEntry CreateForAddingDocumentTemplate(TenantUserSession tenantUserSession, DocumentTemplate documentTemplate)
        {
            if (documentTemplate == null) { throw (new ArgumentNullException("documentTemplate", "Unable to log the following entry")); }
            if (documentTemplate.Document == null) { throw (new ArgumentNullException("Unable to log the following entry", new Exception("Document cant not be found"))); }

            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForAdd(tenantUserSession, EntityType.DocumentTemplate, documentTemplate.Id, documentTemplate.Document.Name);
            return (auditTrail);

            //AuditTrailEntry auditTrail = new AuditTrailEntry();
            //auditTrail.AuditTrailActionType = AuditTrailActionType.Add;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = null;//transactionId;
            //auditTrail.EntityType = EntityType.DocumentTemplate;
            //auditTrail.Description = AuditTrailConstants.AddDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", documentTemplate.Document.Name).ToString();
            //auditTrail.EntityTypeId = documentTemplate.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = null;// document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = null;// document.DocumentParent;
            //auditTrail.EntityUserId = null;

        }
        public static AuditTrailEntry CreateForAddingFolder(TenantUserSession tenantUserSession, Folder folder)
        {
            if (folder == null) { throw (new ArgumentNullException("folder", "Unable to log the following entry")); }
            //if (documentTemplate.Document == null) { throw (new ArgumentNullException("Unable to log the following entry", new Exception("Document cant not be found"))); }

            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForAdd(tenantUserSession, EntityType.Folder, folder.Id, folder.Name);
            return (auditTrail);
        }

        public static AuditTrailEntry CreateForRetrievingFolder(TenantUserSession tenantUserSession, Folder folder)
        {
            if (folder == null) { throw (new ArgumentNullException("folder", "Unable to log the following entry")); }
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieve(tenantUserSession, EntityType.Folder, folder.Id, folder.Name);
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingFoldersUnderRootfolder(TenantUserSession tenantUserSession, Folder folder)
        {
            if (folder == null) { throw (new ArgumentNullException("folder", "Unable to log the following entry")); }
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieve(tenantUserSession, EntityType.Folder, (long)folder.ParentId, "List");
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForRetrievingDocumentsUnderFolder(TenantUserSession tenantUserSession, Folder folder)
        {
            //NEED TO ADD FOLDER ID IN AUDIT TRAIL TO KEEP TRACK OF FOLDER CONTAINING DOCUMENTS
            //Temporary uses retreiving method.
            if (folder == null) { throw (new ArgumentNullException("folder", "Unable to log the following entry")); }
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRetrieve(tenantUserSession, EntityType.Folder, folder.Id, folder.Name);
            return (auditTrail);
        }
        public static AuditTrailEntry CreateForRenamingFolder(TenantUserSession tenantUserSession, Folder folder, string oldFolderName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = AuditTrailManagement.GetAuditTrailForRename(tenantUserSession, EntityType.Folder, folder.Id, null, folder.ParentId, oldFolderName, folder.Name, transactionId);
            return (auditTrail);

            //auditTrail.AuditTrailActionType = AuditTrailActionType.Rename;
            //auditTrail.DateTime = DateTime.UtcNow;
            //auditTrail.TransactionId = transactionId;
            //auditTrail.EntityType = EntityType.Document;
            //auditTrail.Description = AuditTrailConstants.RenameDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", oldDocumentName).Replace("{ItemNameNew}", document.Name).ToString();
            //auditTrail.EntityTypeId = document.Id;
            //auditTrail.UserId = tenantUserSession.User.Id;
            //auditTrail.EntityDiscourseId = null;
            //auditTrail.EntityDiscoursePostId = null;
            //auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            //auditTrail.EntityDiscoursePostVersionId = null;
            //auditTrail.EntityDocumentOriginalId = document.DocumentOriginalId;
            //auditTrail.EntityDocumentParentId = document.DocumentParent;
            //auditTrail.EntityUserId = null;
            //return (auditTrail);
        }


        #endregion AuditTrailFiller

        #region Helper Methods
        private static AuditTrailEntry GetAuditTrailForAdd(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.Add;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.AddDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForRetrieve(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.Retrieve;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForRetrievingUsers(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.RetrieveAccessRights;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.RetrieveAccessRightsDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForCancelCheckout(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.CancelCheckOut;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.CancelCheckOutDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForRetrieveByUser(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long retrieveByUserId, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.Retrieve;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = null;//transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = retrieveByUserId;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForEditingUserRights(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, long? entityTypeOriginalId, long? entityTypeParentlId, string itemName, User rightsAssignedTo, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.UpdateAccessRights;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = EntityType.Document;
            auditTrail.Description = AuditTrailConstants.UpdateAccessRightsDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).Replace("{AssignedToUser}", rightsAssignedTo.NameFull).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = entityTypeOriginalId;
            auditTrail.EntityDocumentParentId = entityTypeParentlId;
            auditTrail.EntityUserId = rightsAssignedTo.Id;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForEdit(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.Edit;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.EditDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailFoDelete(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.Delete;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.DeleteDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForConfirm(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.Confirm;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.ConfirmDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForSearch(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, string searchQueryStr, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.Search;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.SearchDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).Replace("{SearchItemQurey}", searchQueryStr).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForMarkingPublic(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.MarkPublic;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.MarkPublicDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForMarkingPrivate(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.MarkPrivate;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.MarkPrivateDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = null;
            auditTrail.EntityDocumentParentId = null;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForMove(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, long? entityTypeOriginalId, long? entityTypeParentlId, string itemName, string moveFrom, string moveTo, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.Move;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.MoveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).Replace("{ItemMoveFrom}", moveFrom).Replace("{ItemMoveTo}", moveTo).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = entityTypeOriginalId;
            auditTrail.EntityDocumentParentId = entityTypeParentlId;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForRetrievingVersions(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, long? entityTypeOriginalId, long? entityTypeParentlId, string itemName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.RetrieveVersions;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.RetrieveDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", itemName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = entityTypeOriginalId;
            auditTrail.EntityDocumentParentId = entityTypeParentlId;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }
        private static AuditTrailEntry GetAuditTrailForRename(TenantUserSession tenantUserSession, EntityType entityType, long entityTypeId, long? entityTypeOriginalId, long? entityTypeParentlId, string oldName, string newName, long? transactionId = null)
        {
            AuditTrailEntry auditTrail = new AuditTrailEntry();
            auditTrail.AuditTrailActionType = AuditTrailActionType.Rename;
            auditTrail.DateTime = DateTime.UtcNow;
            auditTrail.TransactionId = transactionId;
            auditTrail.EntityType = entityType;
            auditTrail.Description = AuditTrailConstants.RenameDescription.Replace("{user}", tenantUserSession.User.UserName).Replace("{item}", auditTrail.EntityType.ToString()).Replace("{ItemName}", oldName).Replace("{ItemNameNew}", newName).ToString();
            auditTrail.EntityTypeId = entityTypeId;
            auditTrail.UserId = tenantUserSession.User.Id;
            auditTrail.EntityDiscourseId = null;
            auditTrail.EntityDiscoursePostId = null;
            auditTrail.EntityDiscoursePostVersionAttachmentId = null;
            auditTrail.EntityDiscoursePostVersionId = null;
            auditTrail.EntityDocumentOriginalId = entityTypeOriginalId;
            auditTrail.EntityDocumentParentId = entityTypeParentlId;
            auditTrail.EntityUserId = null;
            return (auditTrail);
        }

        #endregion Helper Methods



        public static void GetAllAuditTrail(TenantUserSession tenantUserSession, ContextTenant context, out List<AuditTrailEntry> audittrailentry)
        {
            audittrailentry = context.AuditTrailEntries.Include(t => t.User).ToList();
        }
        public static void GetAuditTrailByDateRange(TenantUserSession tenantUserSession, ContextTenant context, DateTime dateTo, DateTime dateFrom, out List<AuditTrailEntry> audittrailentry)
        {
            dateTo = dateTo.Date;
            dateFrom = dateFrom.Date;

            audittrailentry = context
                .AuditTrailEntries
                .Include(t => t.User)
                .Where
                (
                    t =>
                    (
                        (DbFunctions.TruncateTime(t.DateTime) >= dateFrom)
                        &&
                        (DbFunctions.TruncateTime(t.DateTime) <= dateTo)
                    )
                )
                .ToList();
        }
        public static long GetMaxAuditTrailTransactionId(ContextTenant context)
        {
            var MaxAuditTrailId = (context.AuditTrailEntries.Max(x => x.TransactionId)) ?? 0;
            return (MaxAuditTrailId + 1);
        }



    }
}