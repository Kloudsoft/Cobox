using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Utility;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Utilities;
using System.Text.RegularExpressions;
using HouseOfSynergy.AffinityDms.BusinessLayer.Lookup;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public static class DocumentManagement
    {
        internal static IQueryable<Document> GetQueryDocuments
        (
            TenantUserSession tenantUserSession,
            ContextTenant context,
            long? folderId,
            long? documentId,
            DocumentIdType? documentIdType,
            bool includeDiscourse = false,
            bool includeDocumentElements = false,
            bool includeDocumentFragments = false,
            bool includeCreatorUser = false,
            bool includeCheckedOutUser = false,
            bool includeFolders = false,
            bool includeDocumentUsers = false,
            bool includeDocumentIndexes = false,
            bool includeDocumentTags = false,
            bool includeDocumentTagUsers = false,
            bool includeDocumentTemplates = false,
            bool includeDocumentCorretiveIndexValues = false,
            bool? isFinalized = null,
            int? skipRows = null,
            int? takeRows = null
        )
        {
            if (documentId.HasValue != documentIdType.HasValue) { throw (new ArgumentException("The arguments [documentId] and [documentIdType] should either both be null or both have values.", "documentId, documentIdType")); }

            var userId = tenantUserSession.User.Id;
            var query = context.Documents.AsQueryable();

            if (documentIdType.HasValue)
            {
                switch (documentIdType)
                {
                    case DocumentIdType.Id: { if (documentId.HasValue) { query = query.Where(d => (d.Id == documentId.Value)); } break; }
                    case DocumentIdType.OriginalId: { if (documentId.HasValue) { query = query.Where(d => (d.DocumentOriginalId == documentId.Value)); } break; }
                    case DocumentIdType.ParentId: { if (documentId.HasValue) { query = query.Where(d => (d.DocumentParent == documentId.Value)); } break; }
                    default: { throw (new NotImplementedException()); }
                }
            }

            if (includeCreatorUser) { query = query.Include(d => d.User); }
            if (includeCheckedOutUser) { query = query.Include(d => d.CheckedOutByUser); }
            if (includeDocumentUsers) { query = query.Include(d => d.DocumentUsers).Include(d => d.DocumentUsers.Select(x => x.User)); }
            if (includeFolders) { query = query.Include(d => d.Folder).Include(d => d.Folder.FolderUsers); }
            if (includeDocumentIndexes) { query = query.Include(x => x.DocumentIndexs); }
            if (includeDocumentTags) { query = query.Include(x => x.DocumentTags); }
            if (includeDocumentTagUsers) { query = query.Include(x => x.DocumentTagUsers); }
            if (includeDocumentTemplates) { query = query.Include(x => x.DocumentTemplates); }

            if (includeDiscourse)
            {
                query = query
                    .Include(x => x.DiscussionPostDocuments)
                    .Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse))
                    .Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse.Posts))
                    .Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse.Posts.Select(z => z.Versions)))
                    .Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse.Posts.Select(z => z.Versions.Select(a => a.Attachments))))
                    .Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse.Posts.Select(z => z.Versions.Select(a => a.Attachments.Select(b => b.Template)))));
            }

            if (includeDocumentElements)
            {
                query = query
                    .Include(x => x.DocumentElements)
                    .Include(x => x.DocumentElements.Select(y => y.TemplateElement))
                    .Include(x => x.DocumentElements.Select(y => y.TemplateElementDetail));
            }

            if (includeDocumentCorretiveIndexValues)
            {
                query = query
                    .Include(x => x.DocumentCorrectiveIndexValues)
                    .Include(x => x.DocumentCorrectiveIndexValues.Select(y => y.Document))
                    .Include(x => x.DocumentCorrectiveIndexValues.Select(y => y.IndexElement))
                    .Include(x => x.DocumentCorrectiveIndexValues.Select(y => y.Indexer));
            }
            //   var folders = context.Folders.ToList();
            var folderShared = context.Folders.FirstOrDefault(f => ((f.ParentId == 1) && (f.Name == "Shared")));
            if (folderShared.Id == folderId)
            {
                //(!(FolderManagement.ValidateUserFolderRightsHirarchy((long)d.FolderId,folders,userId)))
                query = query
                    .Where
                    (
                        d =>
                        (
                            (d.UserId != userId)
                            &&
                            (!d.IsPrivate)
                            &&
                            (d.DocumentUsers.Any(du => ((du.UserId == userId) && du.IsActive)))
                            //&&
                            //(!(d.Folder.FolderUsers.Any(fu => ((fu.UserId == userId) && (fu.IsActive)))))


                            //&&
                            //(d.LatestCheckedOutByUserId != userId)
                            &&
                            (d.LatestCheckedOutByUserId <= 0)
                            &&
                            (d.IsFinalized)
                        )
                    );
            }
            else
            {
                if (folderId.HasValue) { query = query.Where(d => (d.FolderId.Value == folderId.Value)); }

                query = query
                    .Where
                    (
                        d =>
                        (
                            (
                                (d.UserId != userId)
                                && (!d.IsPrivate)
                                && (d.IsFinalized)
                            )
                            ||
                            (
                                (d.UserId == userId)
                                || (d.LatestCheckedOutByUserId == userId)/*CheckedOutByUserId == userId*/
                                || (d.DocumentUsers.Any(du => du.UserId == userId))
                            )
                        )
                    );
            }

            if (includeFolders)
            {
                if (folderId.HasValue)
                {
                    if (folderShared != null)
                    {
                        if (folderId.Value == folderShared.Id)
                        {
                            query = query.Where
                            (
                                d =>
                                (
                                    (!d.Folder.FolderUsers.Any(u => u.Id == userId))
                                )
                            );
                        }
                        else
                        {
                            query = query.Where
                            (
                                d =>
                                (
                                    (d.Folder.FolderUsers.Any(u => u.Id == userId))
                                )
                            );
                        }
                    }
                }
            }

            if (skipRows != null) { query = query.Skip((int)skipRows); }
            if (takeRows != null) { query = query.Take((int)takeRows); }

            return (query);
        }

        public static List<Document> GetDocuments
        (
            TenantUserSession tenantUserSession,
            long? documentId,
            DocumentIdType? documentIdType,
            long? folderId,
            DocumentResultVersionType documentResultVersionType,
            bool includeDiscourse = false,
            bool includeDocumentElements = false,
            bool includeDocumentFragments = false,
            bool includeCreatorUser = false,
            bool includeCheckedOutUser = false,
            //	bool includeFolders = false,
            bool includeDocumentUsers = false,
            bool includeDocumentIndexes = false,
            bool includeDocumentTags = false,
            bool includeDocumentTagUsers = false,
            bool includeDocumentTemplates = false,
            bool includeDocumentCorrectiveIndexValues = false,
            bool? isFinalized = null,
            int? skipRows = null,
            int? takeRows = null
        )
        {

            var documents = DocumentManagement.GetDocuments
            (
                tenantUserSession: tenantUserSession,
                context: null,
                folderId: folderId,
                documentId: documentId,
                documentIdType: documentIdType,
                documentResultVersionType: documentResultVersionType,
                includeDiscourse: includeDiscourse,
                includeDocumentElements: includeDocumentElements,
                includeDocumentFragments: includeDocumentFragments,
                includeCreatorUser: includeCreatorUser,
                includeCheckedOutUser: includeCheckedOutUser,
                //includeFolders: includeFolders,
                includeDocumentUsers: includeDocumentUsers,
                includeDocumentTags: includeDocumentTags,
                includeDocumentTagUsers: includeDocumentTagUsers,
                includeDocumentTemplates: includeDocumentTemplates,
                includeDocumentCorretiveIndexValues: includeDocumentCorrectiveIndexValues,
                isFinalized: isFinalized,
                skipRows: skipRows,
                takeRows: takeRows
            );

            //var documents = query.ToList();

            documents = DocumentManagement.GetDocumentsVersions(tenantUserSession, documents, documentId, documentResultVersionType);

            return (documents);

        }

        internal static List<Document> GetDocuments
        (
            TenantUserSession tenantUserSession,
            ContextTenant context,
            long? documentId,
            DocumentIdType? documentIdType,
            long? folderId,
            DocumentResultVersionType documentResultVersionType,
            bool includeDiscourse = false,
            bool includeDocumentElements = false,
            bool includeDocumentFragments = false,
            bool includeCreatorUser = false,
            bool includeCheckedOutUser = false,
            bool includeFolders = false,
            bool includeDocumentUsers = false,
            bool includeDocumentIndexes = false,
            bool includeDocumentTags = false,
            bool includeDocumentTagUsers = false,
            bool includeDocumentTemplates = false,
            bool includeDocumentCorretiveIndexValues = false,
            bool? isFinalized = null,
            int? skipRows = null,
            int? takeRows = null
        )
        {
            long? documentOriginalId = null;

            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString, proxyCreationEnabled: false, lazyLoadingEnabled: true))
                {
                    if (documentId.HasValue)
                    {
                        switch (documentIdType)
                        {
                            case DocumentIdType.Id: { documentOriginalId = context.Documents.SingleOrDefault(d => d.Id == documentId.Value)?.DocumentOriginalId; break; }
                            case DocumentIdType.OriginalId: { documentOriginalId = documentId; break; }
                            case DocumentIdType.ParentId: { documentOriginalId = context.Documents.FirstOrDefault(d => d.DocumentParent == documentId.Value)?.DocumentOriginalId; break; }
                        }
                    }

                    var query = DocumentManagement.GetQueryDocuments
                    (
                        tenantUserSession: tenantUserSession,
                        context: context,
                        folderId: folderId,
                        documentId: documentOriginalId,
                        documentIdType: documentOriginalId.HasValue ? DocumentIdType.OriginalId : ((DocumentIdType?)null),
                        includeDiscourse: includeDiscourse,
                        includeDocumentElements: includeDocumentElements,
                        includeDocumentFragments: includeDocumentFragments,
                        includeCreatorUser: includeCreatorUser,
                        includeCheckedOutUser: includeCheckedOutUser,
                        includeDocumentUsers: includeDocumentUsers,
                        includeDocumentIndexes: includeDocumentIndexes,
                        includeDocumentTags: includeDocumentTags,
                        includeDocumentTagUsers: includeDocumentTagUsers,
                        includeDocumentTemplates: includeDocumentTemplates,
                        includeDocumentCorretiveIndexValues: includeDocumentCorretiveIndexValues,
                        includeFolders: includeFolders,
                        isFinalized: isFinalized,
                        skipRows: skipRows,
                        takeRows: takeRows
                    );

                    var documents = query.ToList();
                    var folders = context.Folders.Include(x => x.FolderUsers).ToList();
                    documents = DocumentManagement.GetSharedFolderDocuments(tenantUserSession, folderId, documents, folders);
                    documents = DocumentManagement.GetDocumentsVersions(tenantUserSession, documents, documentId, documentResultVersionType);

                    return (documents);
                }
            }
            else
            {
                if (documentId.HasValue)
                {
                    switch (documentIdType)
                    {
                        case DocumentIdType.Id: { documentOriginalId = context.Documents.SingleOrDefault(d => d.Id == documentId.Value)?.DocumentOriginalId; break; }
                        case DocumentIdType.OriginalId: { documentOriginalId = documentId; break; }
                        case DocumentIdType.ParentId: { documentOriginalId = context.Documents.FirstOrDefault(d => d.DocumentParent == documentId.Value)?.DocumentOriginalId; break; }
                    }
                }

                var query = DocumentManagement.GetQueryDocuments
                (
                    tenantUserSession: tenantUserSession,
                    context: context,
                    folderId: folderId,
                    documentId: documentOriginalId,
                    documentIdType: documentOriginalId.HasValue ? DocumentIdType.OriginalId : ((DocumentIdType?)null),
                    includeDiscourse: includeDiscourse,
                    includeDocumentElements: includeDocumentElements,
                    includeDocumentFragments: includeDocumentFragments,
                    includeCreatorUser: includeCreatorUser,
                    includeCheckedOutUser: includeCheckedOutUser,
                    includeFolders: includeFolders,
                    includeDocumentUsers: includeDocumentUsers,
                    includeDocumentTags: includeDocumentTags,
                    includeDocumentTagUsers: includeDocumentTagUsers,
                    includeDocumentIndexes: includeDocumentIndexes,
                    includeDocumentTemplates: includeDocumentTemplates,
                    includeDocumentCorretiveIndexValues: includeDocumentCorretiveIndexValues,
                    isFinalized: isFinalized,
                    skipRows: skipRows,
                    takeRows: takeRows
                );

                var documents = query.ToList();
                var folders = context.Folders.Include(x => x.FolderUsers).ToList();
                documents = DocumentManagement.GetSharedFolderDocuments(tenantUserSession, folderId, documents, folders);
                documents = DocumentManagement.GetDocumentsVersions(tenantUserSession, documents, documentId, documentResultVersionType);

                return (documents);
            }
        }
        internal static List<Document> GetSharedFolderDocuments(TenantUserSession tenantUserSession, long? selectedFolderId, List<Document> documents, List<Folder> folders)
        {
            var isSharedFolder = false;
            if (selectedFolderId == null) { return documents; }
            if (folders != null)
            {
                var folderShared = folders.FirstOrDefault(f => ((f.ParentId == 1) && (f.Name == "Shared")));
                if (folderShared.Id == (long)selectedFolderId)
                {
                    isSharedFolder = true;
                }
            }
            if (isSharedFolder)
            {
                foreach (var document in documents.ToList())
                {
                    var result = FolderManagement.ValidateUserFolderRightsHirarchy((long)document.FolderId, folders, tenantUserSession.User.Id);
                    //If user have rights to every parent folder in which the docuemnt exist. then the document will be excluded because user can view it in the enterprise folder.
                    if (result) { documents.Remove(document); }
                }
            }
            return documents;
        }
        internal static List<Document> GetDocumentsVersions
        (
            TenantUserSession tenantUserSession,
            List<Document> documents,
            long? documentId,
            DocumentResultVersionType documentResultVersionType
        )
        {
            if ((!documentId.HasValue) && (documentResultVersionType == DocumentResultVersionType.Exact)) { throw (new ArgumentException($"The argument [{nameof(documentId)}] can only have a value if the argument [{nameof(documentResultVersionType)}] is [{nameof(DocumentResultVersionType.Exact)}].", $"{nameof(documentId)}, {nameof(documentResultVersionType)}")); }

            switch (documentResultVersionType)
            {
                case DocumentResultVersionType.Minimum:
                {
                    documents = documents.OrderBy(d => d.Id).ThenBy(d => d.DocumentOriginalId).ThenBy(d => d.DocumentParent).Take(1).ToList();

                    break;
                }
                case DocumentResultVersionType.Maximum:
                {
                    documents = documents.OrderByDescending(d => d.Id).ThenByDescending(d => d.DocumentOriginalId).ThenByDescending(d => d.DocumentParent).Take(1).ToList();

                    break;
                }
                case DocumentResultVersionType.Parent:
                {
                    documents = documents.Where(x => x.DocumentParent == x.Id).OrderBy(d => d.Id).ThenBy(d => d.DocumentOriginalId).ThenBy(d => d.DocumentParent).ToList();

                    break;
                }
                case DocumentResultVersionType.Exact:
                {
                    documents = documents.Where(x => x.Id == documentId.Value).ToList();

                    break;
                }
                default:
                {
                    documents = documents.OrderBy(d => d.Id).ThenBy(d => d.DocumentOriginalId).ThenBy(d => d.DocumentParent).ToList();

                    break;
                }
            }

            return (documents);
        }



        #region ACLImplemented

        #region Documents

        #region GetDocumentByHash
        /// <summary>
        /// Returns the document containing the supplied hash.
        /// </summary>
        /// <param name="hash">The SHA512 hash value as a hex string.</param>
        /// <param name="document">The document to be returned.</param>
        /// <param name="exception">The exception generated (if any).</param>
        /// <returns>True if the function succeeded. False in case of an exception.</returns>

        public static bool GetDocumentByHash(TenantUserSession tenantUserSession, string hash, out Document document, out Exception exception)
        {
            var result = false;
            document = null;
            exception = null;
            try
            {
                if (string.IsNullOrWhiteSpace(hash)) { throw (new ArgumentException("The argument [hash] is empty.", "hash")); }
                if (!(result = DocumentManagement.GetDocumentByHash(tenantUserSession: tenantUserSession, context: null, hash: hash, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingDocument(tenantUserSession: tenantUserSession, document: document));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        internal static bool GetDocumentByHash(TenantUserSession tenantUserSession, ContextTenant context, string hash, out Document document, out Exception exception)
        {
            var result = false;

            document = null;
            exception = null;

            try
            {
                if (string.IsNullOrWhiteSpace(hash)) { throw (new ArgumentException("The argument [hash] is empty.", "hash")); }

                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = DocumentManagement.GetDocumentByHash(tenantUserSession: tenantUserSession, context: context, hash: hash, document: out document);
                    }
                }
                else
                {
                    result = DocumentManagement.GetDocumentByHash(tenantUserSession: tenantUserSession, context: context, hash: hash, document: out document);
                }

            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        private static bool GetDocumentByHash(TenantUserSession tenantUserSession, ContextTenant context, string hash, out Document document)
        {
            var result = false;

            document = null;

            if (string.IsNullOrWhiteSpace(hash)) { throw (new ArgumentException("The argument [hash] is empty.", "hash")); }

            document = context.Documents.Where(d => d.Hash.ToLower() == hash.ToLower()).SingleOrDefault();
            //DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: null, documentIdType: null, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeCreatorUser: true, includeCheckedOutUser: true, includeDocumentUsers: true, includeFolders: true);//GetDocuments(tenantUserSession: tenantUserSession, documentId: null, documentIdType: null, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDiscourse: false, includeDocumentElements: false, includeDocumentFragments: false, includeCreatorUser: false, includeCheckedOutUser: false, includeFolders: false, includeDocumentUsers: false);
            //document = documents.SingleOrDefault(d => (d.Hash.ToLower() == hash.ToLower()));
            result = true;

            return (result);
        }

        #endregion GetDocumentByHash

        #region GetMaxVersionDocumentByDocumentId

        public static bool GetMaxVersionDocumentByDocumentId(TenantUserSession tenantUserSession, long id, out Document document, out Exception exception)
        {
            var result = false;
            document = null;
            exception = null;
            //Document curentDocument = null;
            try
            {
                //using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                //{
                //	curentDocument = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: id, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault(); //context.Documents.Where(x => x.Id == id).FirstOrDefault();//
                //	if (curentDocument == null) { throw (new Exception("Unable to find the following document")); }
                //	document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: curentDocument.DocumentOriginalId, documentIdType: DocumentIdType.OriginalId, folderId: null, documentResultVersionType: DocumentResultVersionType.Maximum, includeFolders: true, includeCreatorUser: true, includeCheckedOutUser: true).FirstOrDefault();
                //	result = true;
                //}

                if (!(result = GetMaxVersionDocumentByDocumentId(tenantUserSession: tenantUserSession, context: null, id: id, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingDocument(tenantUserSession: tenantUserSession, document: document));
                //context.Documents.Include(x => x.Folder).Include(x => x.User).Where(x => x.DocumentOriginalId == curentDocument.DocumentOriginalId).ToList();//.Max();
                //document = documents.Where(x => x.Id == documents.Select(y => y.Id).ToList().Max(y => y)).FirstOrDefault();
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        internal static bool GetMaxVersionDocumentByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long id, out Document document, out Exception exception)
        {
            var result = false;
            document = null;
            exception = null;
            Document curentDocument = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetMaxVersionDocumentByDocumentId(tenantUserSession: tenantUserSession, context: context, id: id, document: out document);
                    }
                }
                else
                {
                    result = GetMaxVersionDocumentByDocumentId(tenantUserSession: tenantUserSession, context: context, id: id, document: out document);
                }


                //context.Documents.Include(x => x.Folder).Include(x => x.User).Where(x => x.DocumentOriginalId == curentDocument.DocumentOriginalId).ToList();//.Max();
                //document = documents.Where(x => x.Id == documents.Select(y => y.Id).ToList().Max(y => y)).FirstOrDefault();
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        private static bool GetMaxVersionDocumentByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long id, out Document document)
        {
            var result = false;
            document = null;
            Document curentDocument = null;

            curentDocument = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: id, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault(); //context.Documents.Where(x => x.Id == id).FirstOrDefault();//
            if (curentDocument == null) { throw (new Exception("Unable to find the following document")); }
            document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: curentDocument.DocumentOriginalId, documentIdType: DocumentIdType.OriginalId, folderId: null, documentResultVersionType: DocumentResultVersionType.Maximum, includeCreatorUser: true, includeCheckedOutUser: true, includeFolders: true).FirstOrDefault();
            result = true;

            return (result);
        }
        #endregion GetMaxVersionDocumentByDocumentId

        #region GetDocumentDiscourseById
        public static bool GetDocumentDiscourseById(TenantUserSession tenantUserSession, long? id, out Document document, out Exception exception)
        {
            var result = false;
            document = null;
            exception = null;

            try
            {
                if (!(result = GetDocumentDiscourseById(tenantUserSession: tenantUserSession, context: null, id: id, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingDocument(tenantUserSession: tenantUserSession, document: document));

                //using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                //{
                //	//document = context
                //	//	.Documents
                //	//	.Include(x => x.Folder)
                //	//	.Include(x => x.User)
                //	//	.Include(x => x.DiscussionPostDocuments)
                //	//	.Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse))
                //	//	.Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse.Posts))
                //	//	.Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse.Posts.Select(z => z.Versions)))
                //	//	.Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse.Posts.Select(z => z.Versions.Select(a => a.Attachments))))
                //	//	.Include(x => x.DiscussionPostDocuments.Select(y => y.Discourse.Posts.Select(z => z.Versions.Select(a => a.Attachments.Select(b => b.Template)))))

                //	document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: id, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();//context.Documents.Where(x => x.Id == id).FirstOrDefault();
                //	if (document == null) { throw (new Exception("Unable to Find the Following Document")); }
                //	document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: document.DocumentOriginalId, documentIdType: DocumentIdType.OriginalId, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDiscourse: true, includeCreatorUser: true, includeFolders: true).FirstOrDefault();
                //	result = true;
                //}
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        internal static bool GetDocumentDiscourseById(TenantUserSession tenantUserSession, ContextTenant context, long? id, out Document document, out Exception exception)
        {
            var result = false;
            document = null;
            exception = null;

            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetDocumentDiscourseById(tenantUserSession: tenantUserSession, context: context, id: id, document: out document);
                    }
                }
                else
                {
                    result = GetDocumentDiscourseById(tenantUserSession: tenantUserSession, context: context, id: id, document: out document);
                }

            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        private static bool GetDocumentDiscourseById(TenantUserSession tenantUserSession, ContextTenant context, long? id, out Document document)
        {
            var result = false;
            document = null;

            document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: id, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();//context.Documents.Where(x => x.Id == id).FirstOrDefault();
            if (document == null) { throw (new Exception("Unable to Find the Following Document")); }
            document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: document.DocumentOriginalId, documentIdType: DocumentIdType.OriginalId, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDiscourse: true, includeCreatorUser: true, includeFolders: true).FirstOrDefault();

            result = true;

            return (result);
        }



        #endregion GetDocumentDiscourseById

        #region RenameDocumentById
        public static bool RenameDocumentById(TenantUserSession tenantUserSession, long documentId, string documentName, out Exception exception)
        {
            bool result = false;
            exception = null;
            string previousDocumentName = "";
            List<Document> documents = null;
            try
            {
                if (!(result = RenameDocumentById(tenantUserSession: tenantUserSession, context: null, documentId: documentId, documentName: documentName, previousDocumentName: out previousDocumentName, documents: out documents, exception: out exception))) { if (exception != null) { throw (exception); } }
                if (documents != null)
                {
                    var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var maxTransId = AuditTrailManagement.GetMaxAuditTrailTransactionId(context);
                        foreach (var doc in documents)
                        {
                            AuditTrailManagement.Add(context, AuditTrailManagement.CreateForRenamingDocument(tenantUserSession: tenantUserSession, document: doc, oldDocumentName: previousDocumentName, transactionId: maxTransId));
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool RenameDocumentById(TenantUserSession tenantUserSession, ContextTenant context, long documentId, string documentName, out string previousDocumentName, out List<Document> documents, out Exception exception)
        {
            bool result = false;
            exception = null;
            previousDocumentName = "";
            documents = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = RenameDocumentById(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentName: documentName, previousDocumentName: out previousDocumentName, documents: out documents);
                    }
                }
                else
                {

                    result = RenameDocumentById(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentName: documentName, previousDocumentName: out previousDocumentName, documents: out documents);
                }

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool RenameDocumentById(TenantUserSession tenantUserSession, ContextTenant context, long documentId, string documentName, out string previousDocumentName, out List<Document> documents)
        {
            bool result = false;
            Document document = null;
            documents = null;
            //if (string.IsNullOrEmpty((documentName = Path.GetFileNameWithoutExtension(documentName).Trim()))) { throw (new Exception("Document name is required")); } 
            if (string.IsNullOrEmpty(documentName.Trim())) { throw (new Exception("Document name is required")); }
            document = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();
            documentName += Path.GetExtension(document.FileNameClient);
            if (document.Name == documentName) { throw (new Exception("Can not rename the following document with same name")); }
           
            documents = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: document.DocumentOriginalId, documentIdType: DocumentIdType.OriginalId, folderId: null, documentResultVersionType: DocumentResultVersionType.All);

            using (var transaction = context.Database.BeginTransaction())
            {
                var documentNameOld = documents.FirstOrDefault()?.Name ?? "";
                if (string.IsNullOrEmpty(documentNameOld.Trim()))
                {
                    // This should never happen. 
                    throw (new InvalidOperationException("Dev bug!"));
                }
                previousDocumentName = documentNameOld;
                foreach (var d in documents)
                {
                    d.Name = documentName;
                    //context.Documents.Attach(d);
                    context.SaveChanges();
                }
                transaction.Commit();

                result = true;
            }

            return result;
        }
        #endregion RenameDocumentById

        #region GetDocumentVersionsByDocumentId
        public static bool GetDocumentVersionsByDocumentId(TenantUserSession tenantUserSession, long documentId, out List<Document> documentVersions, out Exception exception)
        {
            documentVersions = null;
            exception = null;
            bool result = false;
            try
            {
                if (!(result = GetDocumentVersionsByDocumentId(tenantUserSession: tenantUserSession, context: null, documentId: documentId, documentVersions: out documentVersions, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingDocumentVersions(tenantUserSession: tenantUserSession, document: documentVersions.Where(x => x.Id == documentId).FirstOrDefault()));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool GetDocumentVersionsByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out List<Document> documentVersions, out Exception exception)
        {
            documentVersions = null;
            exception = null;
            bool result = false;
            try
            {
                if (context == null)
                {

                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetDocumentVersionsByDocumentId(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentVersions: out documentVersions);
                    }
                }
                else
                {
                    result = GetDocumentVersionsByDocumentId(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentVersions: out documentVersions);
                }

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool GetDocumentVersionsByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out List<Document> documentVersions)
        {
            documentVersions = null;
            bool result = false;
            // Document document = null;
            //document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();//context.Documents.Where(y => y.Id == documentId).FirstOrDefault();
            //if (document == null) { throw (new Exception("Unable to Find the Following Document")); }
            documentVersions = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeCheckedOutUser: true, includeDocumentUsers: true, includeDocumentElements: true);// include creator User too if exception occurs
            result = true;
            return result;
        }
        #endregion GetDocumentVersionsByDocumentId

        #region GetAllDocuments
        public static bool GetAllDocuments(TenantUserSession tenantUserSession, out List<Document> documents, out Exception exception)
        {
            bool result = false;

            exception = null;
            documents = null;
            try
            {
                if (!(result = GetAllDocuments(tenantUserSession: tenantUserSession, context: null, documents: out documents, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingAllDocuments(tenantUserSession: tenantUserSession));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return result;
        }
        internal static bool GetAllDocuments(TenantUserSession tenantUserSession, ContextTenant context, out List<Document> documents, out Exception exception)
        {
            bool result = false;

            exception = null;
            documents = null;

            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetAllDocuments(tenantUserSession: tenantUserSession, context: context, documents: out documents);
                    }
                }
                else
                {
                    result = GetAllDocuments(tenantUserSession: tenantUserSession, context: context, documents: out documents);
                }

                //using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                //{
                //	var user = context.Users.Single(u => u.Id == tenantUserSession.User.Id);

                //	documents = context.UserDocuments
                //		.Include(d => d.User)
                //		.Include(d => d.Document)
                //		.Include(d => d.Document.Folder)
                //		.Where(d => d.UserId == user.Id)
                //		.ToList()
                //		.ConvertAll(d => d.Document);

                //	result = true;
                //}
            }
            catch (Exception e)
            {
                exception = e;
            }

            return result;
        }
        private static bool GetAllDocuments(TenantUserSession tenantUserSession, ContextTenant context, out List<Document> documents)
        {
            bool result = false;
            documents = null;

            documents = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: null, documentIdType: null, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDocumentUsers: true, includeCreatorUser: true, includeFolders: true);




            result = true;

            return result;
        }
        #endregion GetAllDocuments

        #region GetAllDocumentsFirstVersion
        public static bool GetAllDocumentsFirstVersion(TenantUserSession tenantUserSession, out List<Document> documents, out Exception exception)
        {
            bool result = false;

            exception = null;
            documents = null;

            try
            {
                if (!(result = GetAllDocumentsFirstVersion(tenantUserSession: tenantUserSession, context: null, documents: out documents, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingAllDocuments(tenantUserSession: tenantUserSession));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return result;
        }
        internal static bool GetAllDocumentsFirstVersion(TenantUserSession tenantUserSession, ContextTenant context, out List<Document> documents, out Exception exception)
        {
            bool result = false;

            exception = null;
            documents = null;

            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetAllDocumentsFirstVersion(tenantUserSession: tenantUserSession, context: context, documents: out documents);
                    }
                }
                else
                {
                    result = GetAllDocumentsFirstVersion(tenantUserSession: tenantUserSession, context: context, documents: out documents);
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return result;
        }

        private static bool GetAllDocumentsFirstVersion(TenantUserSession tenantUserSession, ContextTenant context, out List<Document> documents)
        {
            bool result = false;
            documents = null;

            documents = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: null, documentIdType: null, folderId: null, documentResultVersionType: DocumentResultVersionType.Parent, includeDocumentUsers: true, includeCreatorUser: true, isFinalized: true, includeFolders: true);

            result = true;

            return result;
        }

        #endregion GetAllDocumentsFirstVersion

        #region MoveDocumentIntoFolder
        public static bool MoveDocumentIntoFolder(TenantUserSession tenantUserSession, long documentId, long folderId, out Exception exception)
        {
            var result = false;
            exception = null;
            try
            {
                List<Document> documents = null;
                string oldFolderName = "";
                if (!(result = MoveDocumentIntoFolder(tenantUserSession: tenantUserSession, context: null, documentId: documentId, folderId: folderId, documents: out documents, oldFolderName: out oldFolderName, exception: out exception))) { if (exception != null) { throw (exception); } }
                var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                using (var transaction = context.Database.BeginTransaction())
                {
                    var maxTransId = AuditTrailManagement.GetMaxAuditTrailTransactionId(context);
                    foreach (var doc in documents)
                    {
                        AuditTrailManagement.Add(context, AuditTrailManagement.CreateForMovingDocument(tenantUserSession: tenantUserSession, oldFolderName: oldFolderName, document: doc, transactionId: maxTransId));
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool MoveDocumentIntoFolder(TenantUserSession tenantUserSession, ContextTenant context, long documentId, long folderId, out List<Document> documents, out string oldFolderName, out Exception exception)
        {
            var result = false;
            exception = null;
            documents = null;
            oldFolderName = "";
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = MoveDocumentIntoFolder(tenantUserSession: tenantUserSession, context: context, documentId: documentId, folderId: folderId, documents: out documents, oldFolderName: out oldFolderName);
                    }
                }
                else
                {
                    result = MoveDocumentIntoFolder(tenantUserSession: tenantUserSession, context: context, documentId: documentId, folderId: folderId, documents: out documents, oldFolderName: out oldFolderName);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool MoveDocumentIntoFolder(TenantUserSession tenantUserSession, ContextTenant context, long documentId, long folderId, out List<Document> documents, out string oldFolderName)
        {
            var result = false;
            Document document = null;
            oldFolderName = "";
            //Folder id 1 is the root folder. no document can be moved here
            //folder id <1 is invalid.
            if (folderId <= 1) { new Exception("Document can not be moved in to the following folder"); }
            var sharedAndPrivateFolders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: null, folderIdType: null, documentId: null, folderResultType: FolderResultType.All).ToList().Where(f => ((f.ParentId == 1) && ((f.Name == "Shared") || (f.Name == "Private")))).ToList();
            if (sharedAndPrivateFolders.Any(x => x.Id == folderId)) { throw (new Exception("Document can not be moved to the following folder")); }
            document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();//context.Documents.SingleOrDefault(d => d.Id == documentId);
            if (document == null) { throw (new Exception("Unable to Find the Following Document")); }
            documents = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: document.DocumentOriginalId, documentIdType: DocumentIdType.OriginalId, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeFolders: true);//  context.Documents.Where(d => d.DocumentOriginalId == ).ToList();
            oldFolderName = documents.First().Folder.Name;
            using (var transaction = context.Database.BeginTransaction())
            {
                foreach (var d in documents)
                {
                    d.Folder = null;

                    context.Documents.Attach(d);
                    d.FolderId = folderId;
                    context.SaveChanges();
                }
                transaction.Commit();

                result = true;
            }
            return result;
        }
        #endregion MoveDocumentIntoFolder

        #region CancelCheckoutDocument
        public static bool CancelCheckoutDocument(TenantUserSession tenantUserSession, long id, out Exception exception)
        {
            bool result = false;
            exception = null;
            Document documentFirstVersion = null;
            try
            {
                if (!(result = CancelCheckoutDocument(tenantUserSession: tenantUserSession, context: null, id: id, documentFirstVersion: out documentFirstVersion, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForDocumentCancelCheckout(tenantUserSession: tenantUserSession, document: documentFirstVersion));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool CancelCheckoutDocument(TenantUserSession tenantUserSession, ContextTenant context, long id, out Document documentFirstVersion, out Exception exception)
        {
            bool result = false;
            exception = null;
            documentFirstVersion = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = CancelCheckoutDocument(tenantUserSession: tenantUserSession, context: context, id: id, documentFirstVersion: out documentFirstVersion);
                    }
                }
                else
                {
                    result = CancelCheckoutDocument(tenantUserSession: tenantUserSession, context: context, id: id, documentFirstVersion: out documentFirstVersion);
                }

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool CancelCheckoutDocument(TenantUserSession tenantUserSession, ContextTenant context, long id, out Document documentFirstVersion)
        {
            bool result = false;
            Exception exception = null;
            using (var contextTrans = context.Database.BeginTransaction())
            {
                if (id <= 0) { throw (new Exception("Unable to find the following document")); }

                var documents = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: id, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDocumentIndexes: true);
                documentFirstVersion = documents.OrderBy(x => x.Id).First();
                var document = DocumentManagement.GetDocumentsVersions(tenantUserSession, documents, id, DocumentResultVersionType.Exact).FirstOrDefault();
                if (document == null) { throw (new Exception("Unable to find the following document")); }
                if (document.IsFinalized) { throw (new Exception("Finalized documents can not be canceled")); }
                else
                {

                    if (document.DocumentIndexs != null)
                    {
                        foreach (var documentindex in document.DocumentIndexs.ToList())
                        {
                            //context.DocumentIndexes.Attach(documentindex);
                            context.Entry(documentindex).State = EntityState.Deleted;
                            //context.DocumentIndexes.Remove(documentindex);
                            context.SaveChanges();
                        }
                    }
                    documents.Remove(document);
                    context.Documents.Remove(document);
                    context.SaveChanges();
                    foreach (var doc in documents)
                    {
                        //if (doc.Id != document.Id)
                        //{
                        doc.LatestCheckedOutByUserId = 0;
                        //}
                    }
                    context.SaveChanges();
                    {

                        AzureCloudStorageAccountHelper azureCS = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant);
                        bool azresult = azureCS.RemoveFile(tenantUserSession, document.Id, DiscussionPostAttachmentType.Document, new System.Threading.CancellationToken(), out exception);
                        if (exception != null) { throw exception; }
                        if (azresult)
                        {
                            contextTrans.Commit();
                            result = true;
                        }
                    }

                }



            }

            return result;
        }
        #endregion CancelCheckoutDocument

        #region GetDocumentUsersByDocumentId
        public static bool GetDocumentUsersByDocumentId(TenantUserSession tenantUserSession, long documentId, out List<UserDocument> userDocuments, out Exception exception)
        {
            exception = null;
            bool result = false;
            userDocuments = new List<UserDocument>();
            try
            {
                if (!(result = GetDocumentUsersByDocumentId(tenantUserSession: tenantUserSession, context: null, documentId: documentId, userDocuments: out userDocuments, exception: out exception))) { if (exception != null) { throw (exception); } }
                if (userDocuments.Count > 0)
                {
                    AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingDocumentUsers(tenantUserSession: tenantUserSession, document: userDocuments.FirstOrDefault().Document));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool GetDocumentUsersByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out List<UserDocument> userDocuments, out Exception exception)
        {
            exception = null;
            bool result = false;
            userDocuments = new List<UserDocument>();
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetDocumentUsersByDocumentId(tenantUserSession: tenantUserSession, context: context, documentId: documentId, userDocuments: out userDocuments);
                    }
                }
                else
                {
                    result = GetDocumentUsersByDocumentId(tenantUserSession: tenantUserSession, context: context, documentId: documentId, userDocuments: out userDocuments);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool GetDocumentUsersByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out List<UserDocument> userDocuments)
        {
            bool result = false;
            userDocuments = new List<UserDocument>();
            if (documentId <= 0) { throw (new Exception("Unable to find the following document")); }
            var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, documentResultVersionType: DocumentResultVersionType.Minimum, folderId: null, includeDocumentUsers: true).FirstOrDefault();
            if (document == null) { throw (new Exception("Unable to find the following document")); }
            if (document.DocumentUsers != null)
            {
                userDocuments = document.DocumentUsers.Where(x => x.IsActive).ToList();
                document.DocumentUsers.Clear();
                userDocuments.ForEach(x => x.Document = document);
            }






            return result;
        }
        #endregion GetDocumentUsersByDocumentId

        #region AddRemoveDocumentUsers
        public static bool AddRemoveDocumentUsers(TenantUserSession tenantUserSession, long documentId, List<User> users, out Exception exception)
        {
            exception = null;
            bool result = false;
            Document orginalDoc = null;
            try
            {
                if (!(result = AddRemoveDocumentUsers(tenantUserSession: tenantUserSession, context: null, documentId: documentId, users: users, orginalDoc: out orginalDoc, exception: out exception))) { if (exception != null) { throw (exception); } }

                var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString);
                using (var transaction = context.Database.BeginTransaction())
                {
                    var maxTransId = AuditTrailManagement.GetMaxAuditTrailTransactionId(context);
                    foreach (var user in users)
                    {
                        AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForUpdateDocumentRights(tenantUserSession: tenantUserSession, document: orginalDoc, user: user));
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool AddRemoveDocumentUsers(TenantUserSession tenantUserSession, ContextTenant context, long documentId, List<User> users, out Document orginalDoc, out Exception exception)
        {
            exception = null;
            bool result = false;
            orginalDoc = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = AddRemoveDocumentUsers(tenantUserSession: tenantUserSession, context: context, documentId: documentId, users: users, orginalDoc: out orginalDoc);
                    }
                }
                else
                {
                    result = AddRemoveDocumentUsers(tenantUserSession: tenantUserSession, context: context, documentId: documentId, users: users, orginalDoc: out orginalDoc);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool AddRemoveDocumentUsers(TenantUserSession tenantUserSession, ContextTenant context, long documentId, List<User> users, out Document orginalDoc)
        {
            bool result = false;

            using (var contextTrans = context.Database.BeginTransaction())
            {
                var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();// context.Documents.SingleOrDefault(d => d.Id == documentId);
                if (document == null) { throw (new Exception("Unable to find the following document")); }

                var originalDocument = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: document.DocumentOriginalId, documentIdType: DocumentIdType.OriginalId, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum, includeDocumentUsers: true).FirstOrDefault();// context.Documents.SingleOrDefault(d => d.Id == documentId);
                if (originalDocument == null) { throw (new Exception("Unable to find the following document")); }
                orginalDoc = originalDocument;
                var userIds = users.ConvertAll(u => u.Id);
                var userDocs = context.UserDocuments.Where(ud => ((ud.DocumentId == originalDocument.Id) && (ud.UserId != originalDocument.UserId))).ToList();
                context.UserDocuments.RemoveRange(userDocs);
                context.SaveChanges();

                foreach (var user in users)
                {
                    context.UserDocuments.Add(new UserDocument() { DocumentId = originalDocument.Id, UserId = user.Id, IsActive = true, });
                }
                context.SaveChanges();

                contextTrans.Commit();

                result = true;
            }
            return result;
        }

        #endregion AddRemoveDocumentUsers

        #region CheckoutDoucmentAndMakeNewVersion
        public static bool CheckoutDoucmentAndMakeNewVersion(TenantUserSession tenantUserSession, long documentId, out Document newDocument, out Exception exception)
        {
            bool result = false;
            exception = null;
            Document document = null;
            newDocument = null;
            List<DocumentIndex> documentIndexList = null;
            try
            {
                if (!(result = CheckoutDoucmentAndMakeNewVersion(tenantUserSession: tenantUserSession, context: null, documentId: documentId, newDocument: out newDocument, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.CreateForCheckingoutDocument(tenantUserSession, newDocument);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool CheckoutDoucmentAndMakeNewVersion(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out Document newDocument, out Exception exception)
        {
            bool result = false;
            exception = null;
            newDocument = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = CheckoutDoucmentAndMakeNewVersion(tenantUserSession: tenantUserSession, context: context, documentId: documentId, newDocument: out newDocument);
                    }
                }
                else
                {
                    result = CheckoutDoucmentAndMakeNewVersion(tenantUserSession: tenantUserSession, context: context, documentId: documentId, newDocument: out newDocument);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool CheckoutDoucmentAndMakeNewVersion(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out Document newDocument)
        {
            bool result = false;
            Document document = null;
            newDocument = null;
            List<DocumentIndex> documentIndexList = null;

            //var documents = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDocumentIndexes: true);
            //var documents2 = Get(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDocumentIndexes: true);
            ////document = DocumentManagement.GetmaDocumentsVersions(tenantUserSession, documents, documentId, DocumentResultVersionType.Exact).LastOrDefault();
            ////document = context.Documents.Where(x=>x.DocumentParent == documentId).Max(x=>x.VersionMajor) 
            //document = context.Documents.Where(x => x.VersionMajor == context.Documents.Max(y => y.VersionMajor)).First();

            var documents = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDocumentIndexes: true);

            document = documents.OrderByDescending(x=>x.VersionMajor).First();// DocumentManagement.GetDocumentsVersions(tenantUserSession, documents, documentId, DocumentResultVersionType.Exact).Max(x=>x.VersionMajor);




            if (document.LatestCheckedOutByUserId > 0)
            {
                try
                {
                    document.LatestCheckedOutByUserId = 0;
                    context.Entry(document).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception exx) {
                    throw (new Exception("Document is already checkout"));
                }

                

            }
            if (document == null) { throw (new Exception("Unable to find the following document")); }
            using (var contextTrans = context.Database.BeginTransaction())
            {
                //update version count in checkout document
               // document.VersionCount += 1;
                document.WorkflowState = HouseOfSynergy.AffinityDms.Entities.Lookup.DocumentWorkflowState.Draft;
                if (document.AssignedToUserId == tenantUserSession.User.Id)
                {
                    document.AcceptAssignmentDate = DateTime.UtcNow;
                    document.AssignmentState = AssignmentState.Accepted;
                }
                context.Entry(document).State = EntityState.Modified;
                context.SaveChanges();

                //Adding document in DB
                var copyDocument = document.Clone();
                copyDocument.FolderId = document.FolderId;
                copyDocument.Id = 0;
                copyDocument.DocumentParent = document.Id;
                copyDocument.DocumentOriginalId = document.DocumentOriginalId;
                copyDocument.VersionCount = document.VersionCount + 1;
                copyDocument.VersionMajor = document.VersionMajor + 1;
                copyDocument.VersionMinor = 0;
                copyDocument.CheckedOutByUserId = tenantUserSession.User.Id;
                copyDocument.CheckedOutDateTime = DateTime.UtcNow;
                copyDocument.IsFinalized = false;
                copyDocument.AcceptAssignmentDate = null;
                copyDocument.AssignedByUser = null;
                copyDocument.AssignedDate = null;
                copyDocument.AssignedToUser = null;
                copyDocument.AssignmentState = AssignmentState.None;
                copyDocument.Confidence = document.Confidence;
                copyDocument.LatestCheckedOutByUserId = tenantUserSession.User.Id;
                context.Documents.Add(copyDocument);
                context.SaveChanges();

                //Adding document Index in DB
                if (document.DocumentIndexs != null)
                {
                    foreach (var item in document.DocumentIndexs.ToList())
                    {
                        var indexNew = item.Clone();
                        indexNew.Id = 0;
                        indexNew.DocumentId = copyDocument.Id;
                        context.DocumentIndexes.Add(indexNew);
                        context.SaveChanges();
                    }
                }
                foreach (var doc in documents)
                {
                    doc.LatestCheckedOutByUserId = tenantUserSession.User.Id;
                }
                context.SaveChanges();
                contextTrans.Commit();
                newDocument = copyDocument;
            }


            return result;
        }
        #endregion CheckoutDoucmentAndMakeNewVersion

        #region UpdateDocumentForManualClassifcation
        public static bool UpdateDocumentForManualClassifcation(TenantUserSession tenantUserSession, long documentId, long templateId, out Exception exception)
        {
            bool result = false;
            exception = null;
            Document document = null;
            try
            {
                if (!(result = UpdateDocumentForManualClassifcation(tenantUserSession: tenantUserSession, context: null, documentId: documentId, templateId: templateId, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var template = context.Templates.Where(x => x.Id == templateId).FirstOrDefault();
                    AuditTrailManagement.Add(context, AuditTrailManagement.CreateForEnablingDocumentManualClassification(tenantUserSession: tenantUserSession, document: document, template: template));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool UpdateDocumentForManualClassifcation(TenantUserSession tenantUserSession, ContextTenant context, long documentId, long templateId, out Document document, out Exception exception)
        {
            document = null;
            bool result = false;
            exception = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = UpdateDocumentForManualClassifcation(tenantUserSession: tenantUserSession, context: context, documentId: documentId, templateId: templateId, document: out document);
                    }
                }
                else
                {
                    result = UpdateDocumentForManualClassifcation(tenantUserSession: tenantUserSession, context: context, documentId: documentId, templateId: templateId, document: out document);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool UpdateDocumentForManualClassifcation(TenantUserSession tenantUserSession, ContextTenant context, long documentId, long templateId, out Document document)
        {
            bool result = false;

            document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum, includeDocumentElements: true, includeDocumentFragments: true).FirstOrDefault();
            if (document != null)
            {
                using (var contextTrans = context.Database.BeginTransaction())
                {
                    foreach (var docElements in document.DocumentElements.ToList())
                    {
                        context.DocumentElements.Remove(docElements);
                        context.SaveChanges();
                    }
                    foreach (var docFragments in document.DocumentFragments.ToList())
                    {
                        context.DocumentFragments.Remove(docFragments);
                        context.SaveChanges();
                    }
                    document.TemplateId = templateId;
                    document.DocumentQueueType = DocumentQueueType.Manual;
                    document.State = DocumentState.QueuedManual;
                    document.CountAttemptOcr = 0;
                    document.FullTextOCRXML = "";
                    context.Entry(document).State = EntityState.Modified;
                    context.SaveChanges();
                    contextTrans.Commit();
                    result = true;
                }
            }

            return result;
        }
        #endregion UpdateDocumentForManualClassifcation

        #region GetDocumentById
        public static bool GetDocumentById(TenantUserSession tenantUserSession, long id, out Document document, out Exception exception)
        {
            var result = false;

            document = null;
            exception = null;

            try
            {
                if (!(result = GetDocumentById(tenantUserSession: tenantUserSession, context: null, id: id, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingDocument(tenantUserSession: tenantUserSession, document: document));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        internal static bool GetDocumentById(TenantUserSession tenantUserSession, ContextTenant context, long id, out Document document, out Exception exception)
        {
            var result = false;

            document = null;
            exception = null;

            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetDocumentById(tenantUserSession: tenantUserSession, context: context, id: id, document: out document);
                    }
                }
                else
                {
                    result = GetDocumentById(tenantUserSession: tenantUserSession, context: context, id: id, document: out document);
                }

            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        private static bool GetDocumentById(TenantUserSession tenantUserSession, ContextTenant context, long id, out Document document)
        {
            var result = false;
            document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: id, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Exact, includeCreatorUser: true, includeFolders: true, includeDocumentUsers: true, includeDocumentTemplates: true).SingleOrDefault();
            result = true;
            return (result);
        }
        #endregion GetDocumentById

        #region CreateDocumentEntry
        /// <summary>
        /// Creates a new document.
        /// </summary>
        /// <param name="filename">The filename of the document without path.</param>
        /// <param name="hash">The SHA512 hash value as a hex string.</param>
        /// <param name="size">The size of the file in bytes.</param>
        /// <param name="folderId">The cloud folder the document belongs in (if any).</param>
        /// <param name="document">The document to be returned.</param>
        /// <param name="exception">The exception generated (if any).</param>
        /// <returns>True if the function succeeded. False in case of an exception.</returns>
        public static bool CreateDocumentEntry(TenantUserSession tenantUserSession, string filename, string hash, long size, long? folderId,long? scanSessionId, User user, out Document document, out Exception exception)
        {
            var result = false;

            document = null;
            exception = null;

            scanSessionId = scanSessionId == 0 ? ((long?)null) : scanSessionId;


            try
            {
                if (!(result = CreateDocumentEntry(tenantUserSession: tenantUserSession, context: null, filename: filename, hash: hash, size: size, folderId: folderId,scanSessionId:scanSessionId, user: user, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForAddingDocument(tenantUserSession: tenantUserSession, document: document));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        internal static bool CreateDocumentEntry(TenantUserSession tenantUserSession, ContextTenant context, string filename, string hash, long size, long? folderId, long? scanSessionId, User user, out Document document, out Exception exception)
        {
            var result = false;

            document = null;
            exception = null;

            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = CreateDocumentEntry(tenantUserSession: tenantUserSession, context: context, filename: filename, hash: hash, size: size, folderId: folderId, scanSessionId: scanSessionId, user: user, document: out document);
                    }
                }
                else
                {
                    result = CreateDocumentEntry(tenantUserSession: tenantUserSession, context: context, filename: filename, hash: hash, size: size, folderId: folderId, scanSessionId: scanSessionId, user: user, document: out document);
                }

            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        private static bool CreateDocumentEntry(TenantUserSession tenantUserSession, ContextTenant context, string filename, string hash, long size, long? folderId, long? scanSessionId, User user, out Document document)
        {
            var result = false;
            document = null;
            if (size <= 0) { throw (new ArgumentException("The argument [length] is zero.", "length")); }
            if (string.IsNullOrWhiteSpace(hash)) { throw (new ArgumentException("The argument [hash] is empty.", "hash")); }
            if (string.IsNullOrWhiteSpace(filename)) { throw (new ArgumentException("The argument [filename] is empty.", "filename")); }
            Exception exception = null;
            result = GetDocumentByHash(tenantUserSession: tenantUserSession, context: context, hash: hash, document: out document, exception: out exception);
            if (exception != null) { throw (exception); }
            if (result)
            {
                result = false;
                if (document == null)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var file = new FileInfo(filename);

                            document = new Document();

                            document.Name = file.Name;
                            document.Hash = hash;
                            document.Length = size;
                            document.DateTime = DateTime.UtcNow;
                            document.SourceType = SourceType.DesktopFileSystem;
                            document.DeviceName = "";
                            document.FullTextOCRXML = "";
                            document.FileNameServer = file.Name;
                            document.FileNameClient = file.FullName;
                            document.State = DocumentState.Uploading;
                            document.IsFinalized = true;
                            document.CheckedOutDateTime = DateTime.UtcNow;
                            document.CheckedOutByUserId = tenantUserSession.User.Id;
                            document.VersionCount = 0;
                            document.VersionMajor = 1;
                            document.VersionMinor = 0;
                            document.ScanSessionId = scanSessionId;
                            if (false)
                            {
                                ///Raheel Bhai Code: Document FileType Checking. TODO
                                var type = DocumentUtilities.GetFileType(filename);
                                var format = DocumentUtilities.GetFileFormatType(filename);

                                if (type == FileType.Document)
                                {
                                    if (format != FileFormatType.Pdf) { throw (new DocumentTypeException()); }

                                    document.DocumentType = DocumentType.Raster;
                                }
                                else if (type == FileType.Image)
                                {
                                    if ((format != FileFormatType.Bmp) && (format != FileFormatType.Jpg) && (format != FileFormatType.Png) && (format != FileFormatType.Tiff))
                                    {
                                        throw (new DocumentTypeException());
                                    }

                                    document.DocumentType = DocumentType.Raster;
                                }
                                else
                                {
                                    throw (new DocumentTypeException());
                                }
                            }
                            else
                            {
                                document.DocumentType = DocumentType.Raster;
                            }

                            document.UserId = user.Id;

                            if (folderId == null)
                            {
                                throw (new Exception("Please provide a folder location for the document to be uploaded."));
                                ////Faraz Get Approval
                                //document.Folder = context.Folders.Single(f => (f.FolderType == FolderType.Root));
                                //document.FolderId = document.Folder.Id;
                            }
                            else
                            {
                                ////Faraz Get Approval
                                //if (document != null)
                                //{
                                var folder = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: folderId, folderIdType: FolderIdType.Id, documentId: null, folderResultType: FolderResultType.Exact, includeFolderUsers: true).FirstOrDefault();
                                if (folder == null)
                                {
                                    throw (new Exception("Unable to find the following folder."));
                                }
                                document.Folder = folder;
                                document.FolderId = folderId;


                                //}
                                //if (document.Folder == null)
                                //{
                                //	//Faraz Get Approval
                                //	document.Folder = context.Folders.Single(f => (f.FolderType == FolderType.Root));
                                //	document.FolderId = document.Folder.Id;
                                //}
                                //else
                                //{
                                //	document.FolderId = document.Folder.Id;
                                //}
                            }
                            document.LatestCheckedOutByUserId = 0;

                            context.Documents.Add(document);
                            context.SaveChanges();

                            document.DocumentOriginalId = document.Id;
                            document.DocumentParent = document.Id;

                            var id = document.Id;

                            document.FileNameServer = document.Id.ToString() + "." + GlobalConstants.FileExtensionCloud;
                            context.SaveChanges();

                            var userDocument = new UserDocument() { UserId = tenantUserSession.User.Id, DocumentId = document.DocumentOriginalId, IsActive = true };
                            context.UserDocuments.Add(userDocument);
                            context.SaveChanges();

                            GetDocumentById(tenantUserSession: tenantUserSession, context: context, id: id, document: out document, exception: out exception);
                            if (exception != null) { throw exception; }
                            result = true;
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            document = null;
                            transaction.Rollback();
                            exception = ex;
                        }
                    }
                }
                else
                {
                    throw (new DocumentAlreadyExistsException(document, "A document with the same hash value already exists in the system."));
                }
            }


            return (result);
        }

        public static bool CreateDocumentEntry(TenantUserSession tenantUserSession, string filename, long size, long? folderId, User user, out Document document, out Exception exception)
        {
            var result = false;

            document = null;
            exception = null;

            try
            {
                if (!(result = CreateDocumentEntry(tenantUserSession: tenantUserSession, context: null, filename: filename, size: size, folderId: folderId, user: user, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForAddingDocument(tenantUserSession: tenantUserSession, document: document));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        internal static bool CreateDocumentEntry(TenantUserSession tenantUserSession, ContextTenant context, string filename, long size, long? folderId, User user, out Document document, out Exception exception)
        {
            var result = false;

            document = null;
            exception = null;

            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = CreateDocumentEntry(tenantUserSession: tenantUserSession, context: context, filename: filename, size: size, folderId: folderId, user: user, document: out document);
                    }
                }
                else
                {
                    result = CreateDocumentEntry(tenantUserSession: tenantUserSession, context: context, filename: filename, size: size, folderId: folderId, user: user, document: out document);
                }

            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        private static bool CreateDocumentEntry(TenantUserSession tenantUserSession, ContextTenant context, string filename, long size, long? folderId, User user, out Document document)
        {
            var result = false;
            document = null;
            if (size <= 0) { throw (new ArgumentException("The argument [length] is zero.", "length")); }
            if (string.IsNullOrWhiteSpace(filename)) { throw (new ArgumentException("The argument [filename] is empty.", "filename")); }
            Exception exception = null;
            result = false;
            if (document == null)
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var file = new FileInfo(filename);

                        document = new Document();

                        document.Name = file.Name;
                        document.Hash = string.Empty;
                        document.Length = size;
                        document.DateTime = DateTime.UtcNow;
                        document.SourceType = SourceType.DesktopFileSystem;
                        document.DeviceName = "";
                        document.FullTextOCRXML = "";
                        document.FileNameServer = file.Name;
                        document.FileNameClient = file.FullName;
                        document.State = DocumentState.Uploading;
                        document.IsFinalized = true;
                        document.CheckedOutDateTime = DateTime.UtcNow;
                        document.CheckedOutByUserId = tenantUserSession.User.Id;
                        document.VersionCount = 0;
                        document.VersionMajor = 1;
                        document.VersionMinor = 0;
                        if (false)
                        {
                            ///Raheel Bhai Code: Document FileType Checking. TODO
                            var type = DocumentUtilities.GetFileType(filename);
                            var format = DocumentUtilities.GetFileFormatType(filename);

                            if (type == FileType.Document)
                            {
                                if (format != FileFormatType.Pdf) { throw (new DocumentTypeException()); }

                                document.DocumentType = DocumentType.Raster;
                            }
                            else if (type == FileType.Image)
                            {
                                if ((format != FileFormatType.Bmp) && (format != FileFormatType.Jpg) && (format != FileFormatType.Png) && (format != FileFormatType.Tiff))
                                {
                                    throw (new DocumentTypeException());
                                }

                                document.DocumentType = DocumentType.Raster;
                            }
                            else
                            {
                                throw (new DocumentTypeException());
                            }
                        }
                        else
                        {
                            document.DocumentType = DocumentType.Raster;
                        }

                        document.UserId = user.Id;



                        document.IsPrivate = true;
                        document.FolderId = 1;

                        document.LatestCheckedOutByUserId = 0;

                        context.Documents.Add(document);
                        context.SaveChanges();

                        document.DocumentOriginalId = document.Id;
                        document.DocumentParent = document.Id;

                        var id = document.Id;

                        document.FileNameServer = document.Id.ToString() + "." + GlobalConstants.FileExtensionCloud;
                        context.SaveChanges();

                        var userDocument = new UserDocument() { UserId = tenantUserSession.User.Id, DocumentId = document.DocumentOriginalId, IsActive = true };
                        context.UserDocuments.Add(userDocument);
                        context.SaveChanges();

                        GetDocumentById(tenantUserSession: tenantUserSession, context: context, id: id, document: out document, exception: out exception);
                        if (exception != null) { throw exception; }
                        result = true;
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        document = null;
                        transaction.Rollback();
                        exception = ex;
                    }
                }
            }
            else
            {
                throw (new DocumentAlreadyExistsException(document, "A document with the same hash value already exists in the system."));
            }


            return (result);
        }



        #endregion CreateDocumentEntry

        #region DocumentEntryFinalize
        /// <summary>
        /// Finalizes a new document entry after the file has been uploaded as a blob on Azure storage.
        /// </summary>
        /// <param name="filename">The filename of the document without path.</param>
        /// <param name="hash">The SHA512 hash value as a hex string.</param>
        /// <param name="size">The size of the file in bytes.</param>
        /// <param name="folderId">The cloud folder the document belongs in (if any).</param>
        /// <param name="document">The document to be returned.</param>
        /// <param name="exception">The exception generated (if any).</param>
        /// <returns>True if the function succeeded. False in case of an exception.</returns>
        public static bool DocumentEntryFinalize(TenantUserSession tenantUserSession, long documentId, out Document document, out Exception exception)
        {
            var result = false;

            document = null;
            exception = null;

            try
            {
                if (!(result = DocumentEntryFinalize(tenantUserSession: tenantUserSession, context: null, documentId: documentId, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForFinalizingDocument(tenantUserSession: tenantUserSession, document: document));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        internal static bool DocumentEntryFinalize(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out Document document, out Exception exception)
        {
            var result = false;

            document = null;
            exception = null;

            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = DocumentEntryFinalize(tenantUserSession: tenantUserSession, context: context, documentId: documentId, document: out document);
                    }
                }
                else
                {
                    result = DocumentEntryFinalize(tenantUserSession: tenantUserSession, context: context, documentId: documentId, document: out document);
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        private static bool DocumentEntryFinalize(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out Document document)
        {
            var result = false;

            document = null;


            document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();
            if (document == null)
            {
                throw (new RowNotFoundException());
            }
            else
            {
                if (document.State == DocumentState.QueuedAuto)
                {
                    result = true;
                }
                else if (document.State == DocumentState.Uploading)
                {
                    document.IsInTransit = false;
                    document.State = DocumentState.QueuedAuto;

                    context.SaveChanges();

                    //document = context.Documents.AsNoTracking().SingleOrDefault(d => (d.Id == documentId));

                    result = true;
                }
                else
                {
                    throw (new DocumentStateException());
                }
            }
            return (result);
        }
        #endregion DocumentEntryFinalize

        #region DeleteDocument
        public static bool DeleteDocument(TenantUserSession tenantUserSession, Document document, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (!(result = DeleteDocument(tenantUserSession: tenantUserSession, context: null, document: document))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForDeletingDocument(tenantUserSession: tenantUserSession, document: document));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool DeleteDocument(TenantUserSession tenantUserSession, ContextTenant context, Document document, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = DeleteDocument(tenantUserSession: tenantUserSession, context: context, document: document);
                    }
                }
                else
                {
                    result = DeleteDocument(tenantUserSession: tenantUserSession, context: context, document: document);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool DeleteDocument(TenantUserSession tenantUserSession, ContextTenant context, Document document)
        {
            bool result = false;
            if (document != null)
            {
                var doc = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: document.Id, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();
                if (doc != null)
                {
                    //context.Documents.Remove(document);
                    context.Entry(document).State = EntityState.Deleted;
                    context.SaveChanges();
                    result = true;
                }
            }
            else
            {
                throw (new Exception("Unable to find the following document"));
            }
            return result;
        }
        #endregion DeleteDocument

        #region GetDocumentsByUser
        public static bool GetDocumentsByUser(TenantUserSession tenantUserSession, long userId, int pageNumber, int pageRowCount, out List<Document> documents, out Exception exception)
        {
            var result = false;

            exception = null;
            documents = new List<Document>();
            try
            {
                if (!(result = GetDocumentsByUser(tenantUserSession: tenantUserSession, context: null, userId: userId, pageNumber: pageNumber, pageRowCount: pageRowCount, documents: out documents, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingAllDocumentsByUser(tenantUserSession: tenantUserSession, userId: userId));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        public static bool GetDocumentsByUser(TenantUserSession tenantUserSession, ContextTenant context, long userId, int pageNumber, int pageRowCount, out List<Document> documents, out Exception exception)
        {
            var result = false;

            exception = null;
            documents = new List<Document>();
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetDocumentsByUser(tenantUserSession: tenantUserSession, context: context, userId: userId, pageNumber: pageNumber, pageRowCount: pageRowCount, documents: out documents);
                    }
                }
                else
                {
                    result = GetDocumentsByUser(tenantUserSession: tenantUserSession, context: context, userId: userId, pageNumber: pageNumber, pageRowCount: pageRowCount, documents: out documents);
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        public static bool GetDocumentsByUser(TenantUserSession tenantUserSession, ContextTenant context, long userId, int pageNumber, int pageRowCount, out List<Document> documents)
        {
            var result = false;
            documents = new List<Document>();

            if (pageNumber < GlobalConstants.WebViewPageNumberMinimum) { throw (new ArgumentException("The argument [pageNumber] cannot be less than " + GlobalConstants.WebViewPageNumberMinimum.ToString() + ".", "pageNumber")); }
            if (pageRowCount < GlobalConstants.WebViewPageRowCountMinimum) { throw (new ArgumentException("The argument [pageRowCount] cannot be less than " + GlobalConstants.WebViewPageRowCountMinimum.ToString() + ".", "pageRowCount")); }
            if (pageRowCount > GlobalConstants.WebViewPageRowCountMaximum) { throw (new ArgumentException("The argument [pageRowCount] cannot be greater than " + GlobalConstants.WebViewPageRowCountMaximum.ToString() + ".", "pageRowCount")); }
            documents = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: null, documentIdType: null, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeCheckedOutUser: true, includeCreatorUser: true, includeDocumentFragments: true, includeDocumentTags: true, includeDocumentTagUsers: true, includeDocumentTemplates: true, includeFolders: true);
            documents = documents.Where(x => (x.UserId == userId) || (!x.IsPrivate)).Skip(pageRowCount * pageNumber).Take(pageRowCount).ToList();
            //documents = context
            //		.Documents
            //		.AsNoTracking()
            //		.Where(d => ((d.UserId == userId) || (!d.IsPrivate)))
            //		.OrderBy(d => d.Id)
            //		.Skip(pageRowCount * pageNumber)
            //		.Take(pageRowCount)
            //		.Include(p => p.Folder)
            //		.Include(p => p.DocumentFragments)
            //		.Include(p => p.DocumentTags)
            //		.Include(p => p.DocumentTagUsers)
            //		.Include(d => d.DocumentTemplates)
            //		.Include(x => x.User)
            //		.Include(x => x.CheckedOutByUser)
            //		.ToList();

            documents = DocumentManagement.GetMaxVersionDocuments(tenantUserSession, documents);

            result = true;
            return (result);
        }
        #endregion GetDocumentsByUser

        #region GetDocumentsRelated
        /*
		public static bool GetDocumentsRelated(TenantUserSession tenantUserSession, long sourceDocumentId, out List<Document> documents, out Exception exception, int? count = null)
		{
			//var documentVersion = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: sourceDocumentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();
			//var documentOriginal = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentVersion.DocumentOriginalId, documentIdType: DocumentIdType.OriginalId, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum, includeDocumentIndexes: true);
			documents = new List<Document>();
			exception = null;
			bool result = false;
			try
			{
				using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
				{
					var ids = new List<long>();
					var documentVersion = context
						.Documents
						.Include(d => d.DocumentIndexs)
						.SingleOrDefault(d => d.Id == sourceDocumentId);

					var documentOriginal = context
					   .Documents
					   .Include(d => d.DocumentIndexs)
					   .SingleOrDefault(d => d.Id == documentVersion.DocumentOriginalId);

					if ((documentVersion != null) && (documentOriginal != null))
					{
						if (documentVersion.Id == documentOriginal.Id) { documentVersion = documentOriginal; }

						var indexes = context
							.DocumentIndexes
							.ToList();

						foreach (var documentIndex in documentVersion.DocumentIndexs)
						{
							foreach (var index in indexes)
							{
								if (index.DocumentId != documentVersion.Id)
								{
									if (string.Compare((index.Name ?? "").Trim(), (documentIndex.Name ?? "").Trim(), StringComparison.OrdinalIgnoreCase) == 0)
									{
										if (string.Compare((index.Value ?? "").Trim(), (documentIndex.Value ?? "").Trim(), StringComparison.OrdinalIgnoreCase) == 0)
										{
											var indice = ids.BinarySearch(index.DocumentId);
											if (indice < 0) { ids.Insert(~indice, index.DocumentId); }
										}
									}
								}
							}
						}

						if (ids.Any())
						{
							documents = context.Documents.Where(d => ids.Contains(d.Id)).ToList();
							// Filter out documents based on user ACL.
							var userDocuments = context.UserDocuments.Where(u => u.UserId == tenantUserSession.User.Id).ToList();
							documents = documents.Where(d => userDocuments.Any(ud => ud.DocumentId == d.DocumentOriginalId)).ToList();
						}
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			return (result);
		}

		 */
        public static bool GetDocumentsRelated(TenantUserSession tenantUserSession, long sourceDocumentId, out List<Document> documents, out Exception exception, int? count = null)
        {
            documents = new List<Document>();
            exception = null;
            bool result = false;
            Document sourceDocument = null;
            try
            {
                if (!(result = GetDocumentsRelated(tenantUserSession: tenantUserSession, context: null, sourceDocumentId: sourceDocumentId, sourceDocument: out sourceDocument, documents: out documents, exception: out exception, count: count))) { if (exception != null) { throw exception; } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingAllRelatedDocuments(tenantUserSession: tenantUserSession, document: sourceDocument));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }
        internal static bool GetDocumentsRelated(TenantUserSession tenantUserSession, ContextTenant context, long sourceDocumentId, out Document sourceDocument, out List<Document> documents, out Exception exception, int? count = null)
        {
            documents = new List<Document>();
            exception = null;
            bool result = false;
            sourceDocument = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetDocumentsRelated(tenantUserSession: tenantUserSession, context: context, sourceDocumentId: sourceDocumentId, sourceDocument: out sourceDocument, documents: out documents, count: count);
                    }
                }
                else
                {
                    result = GetDocumentsRelated(tenantUserSession: tenantUserSession, context: context, sourceDocumentId: sourceDocumentId, sourceDocument: out sourceDocument, documents: out documents, count: count);
                }
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return (result);
        }
        private static bool GetDocumentsRelated(TenantUserSession tenantUserSession, ContextTenant context, long sourceDocumentId, out Document sourceDocument, out List<Document> documents, int? count = null)
        {
            documents = new List<Document>();
            bool result = false;
            var ids = new List<long>();
            sourceDocument = null;
            var allDocuments = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: null, documentIdType: null, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDocumentUsers: true, includeDocumentIndexes: true);
            var documentVersion = allDocuments.Where(x => x.Id == sourceDocumentId).FirstOrDefault();
            sourceDocument = documentVersion;
            if (documentVersion != null)
            {
                var documentOriginal = allDocuments.Where(x => x.Id == documentVersion.DocumentOriginalId).FirstOrDefault();
                if ((documentOriginal != null))
                {
                    if (documentVersion.Id == documentOriginal.Id) { documentVersion = documentOriginal; }

                    List<DocumentIndex> indexes = new List<DocumentIndex>();
                    var maxVersionDocuments = GetMaxVersionDocuments(tenantUserSession, allDocuments);
                    maxVersionDocuments.Remove(maxVersionDocuments.Where(x => x.DocumentOriginalId == documentVersion.DocumentOriginalId).FirstOrDefault());
                    indexes = maxVersionDocuments.SelectMany(x => x.DocumentIndexs).ToList();
                    foreach (var documentIndex in documentVersion.DocumentIndexs)
                    {
                        foreach (var index in indexes)
                        {
                            if (index.DocumentId != documentVersion.Id)
                            {
                                if (string.Compare((index.Name ?? "").Trim(), (documentIndex.Name ?? "").Trim(), StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    if (string.Compare((index.Value ?? "").Trim(), (documentIndex.Value ?? "").Trim(), StringComparison.OrdinalIgnoreCase) == 0)
                                    {
                                        var indice = ids.BinarySearch(index.DocumentId);
                                        if (indice < 0) { ids.Insert(~indice, index.DocumentId); }
                                    }
                                }
                            }
                        }
                    }

                    if (ids.Any())
                    {
                        documents = allDocuments.Where(d => ids.Contains(d.Id)).ToList();
                        // Filter out documents based on user ACL.
                        var userDocuments = allDocuments.SelectMany(x => x.DocumentUsers).Where(x => x.UserId == tenantUserSession.User.Id).ToList(); //context.UserDocuments.Where(u => u.UserId == tenantUserSession.User.Id).ToList();
                        documents = documents.Where(d => userDocuments.Any(ud => ud.DocumentId == d.DocumentOriginalId)).ToList();
                    }
                }
            }
            return (result);
        }
        #endregion GetDocumentsRelated

        #region UpdateVersion
        public static bool UpdateVersion(TenantUserSession tenantUserSession, long documentId, string VersionNumber, out Exception exception)
        {
            exception = null;
            var result = false;
            Document oldDocument = null;
            try
            {
                if (!(result = UpdateVersion(tenantUserSession: tenantUserSession, context: null, documentId: documentId, VersionNumber: VersionNumber, oldDocument: out oldDocument, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForUpdatingDocumentVersion(tenantUserSession: tenantUserSession, newVersionNumber: VersionNumber, document: oldDocument));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool UpdateVersion(TenantUserSession tenantUserSession, ContextTenant context, long documentId, string VersionNumber, out Document oldDocument, out Exception exception)
        {
            exception = null;
            var result = false;
            oldDocument = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = UpdateVersion(tenantUserSession: tenantUserSession, context: context, documentId: documentId, VersionNumber: VersionNumber, oldDocument: out oldDocument);
                    }
                }
                else
                {
                    result = UpdateVersion(tenantUserSession: tenantUserSession, context: context, documentId: documentId, VersionNumber: VersionNumber, oldDocument: out oldDocument);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool UpdateVersion(TenantUserSession tenantUserSession, ContextTenant context, long documentId, string VersionNumber, out Document oldDocument)
        {
            var result = false;
            if (!Regex.Match(VersionNumber, @"\d+\.\d+").Success) { throw (new Exception("Version Number is Invalid")); }
            var versions = VersionNumber.Split('.');
            int verMajor = Convert.ToInt16(versions[0]), verMinor = Convert.ToInt16(versions[1]);
            int major = 1, minor = 0;

            var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Maximum).FirstOrDefault();// context.Documents.Where(t => t.Id == documentId).SingleOrDefault();
            oldDocument = document;
            if (document != null)
            {
                Exception exception = null;
                if (VerifyVersion(document, verMajor, verMinor, out major, out minor, out exception, false))
                {
                    document.VersionMajor = major;
                    document.VersionMinor = minor;
                    context.Entry(document).State = EntityState.Modified;
                    context.SaveChanges();








                }
                if (exception != null) { throw (exception); }
            }

            return result;
        }

        #endregion UpdateVersion

        #region UpdateDocument
        /// <summary>
        /// Updates an existing document
        /// </summary>
        /// <param name="document">Provide document to update</param>
        /// <param name="exception">Excpetion occurred</param>
        /// <returns>returns true if record is successfully updated</returns>
        public static bool UpdateDocument(TenantUserSession tenantUserSession, Document document, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (!(UpdateDocument(tenantUserSession: tenantUserSession, context: null, document: document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForEditingDocument(tenantUserSession: tenantUserSession, document: document));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;

        }
        internal static bool UpdateDocument(TenantUserSession tenantUserSession, ContextTenant context, Document document, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = UpdateDocument(tenantUserSession: tenantUserSession, context: context, document: document);
                    }
                }
                else
                {
                    result = UpdateDocument(tenantUserSession: tenantUserSession, context: context, document: document);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;

        }
        private static bool UpdateDocument(TenantUserSession tenantUserSession, ContextTenant context, Document document)
        {
            bool result = false;
            //if ((GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: document.Id, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault()) == null) { throw (new Exception("Unable to proccess the following request.")); }
            //document.Folder = null;
            //document.AssignedByUser = null;
            //document.AssignedToUser = null;
            //document.CheckedOutByUser = null;
            //document.ScanSession = null;
            //document.Template = null;
            //document.User = null;
            //document.DiscussionPostDocuments.Clear();
            //document.DocumentCorrectiveIndexValues.Clear();
            //document.DocumentElements.Clear();
            //document.DocumentFragments.Clear();
            //document.DocumentIndexs.Clear();
            //document.DocumentTags.Clear();
            //document.DocumentTagUsers.Clear();
            //document.DocumentTemplates.Clear();
            //document.DocumentUsers.Clear();
            //document.DocumentXmlElements.Clear();
            //document.ElementValues.Clear();
            //document.EntityWorkflowMappings.Clear();
            //document.WorkFlowInstances.Clear();
            // context.ObjectContext.AcceptAllChanges();

            context.Documents.Attach(document);
            context.Entry(document).State = EntityState.Modified;
            context.SaveChanges();
            result = true;
            return result;
        }
        #endregion UpdateDocument







        #region UpdateDocument
        public static Document UpdateDocumentEntry(TenantUserSession tenantUserSession, long documentId, long? folderId, string hash, long? fileLength, ContextTenant context = null)
        {
            Document document = new Document();
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    document = UpdateDocumentEntryPrivate(tenantUserSession: tenantUserSession, documentId: documentId, folderId: folderId, hash: hash, fileLength: fileLength, context: context);
                }
            }
            else
            {
                document = UpdateDocumentEntryPrivate(tenantUserSession: tenantUserSession, documentId: documentId, folderId: folderId, hash: hash, fileLength: fileLength, context: context);
            }
            AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForEditingDocument(tenantUserSession: tenantUserSession, document: document));
            return (document);
        }
        private static Document UpdateDocumentEntryPrivate(TenantUserSession tenantUserSession, long documentId, long? folderId, string hash, long? fileLength, ContextTenant context)
        {
            if (context == null) { throw (new ArgumentNullException("context")); }
            Document document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Exact).FirstOrDefault();
            if (fileLength != null)
            {
                if (document.Length != fileLength) { throw (new Exception("File was corrupted while uploading")); }
            }
            if (string.IsNullOrEmpty(hash.Trim())) { throw (new Exception("Unable to recognize the uploaded document")); }
            context.Documents.Attach(document);
            if (folderId.HasValue)
            {
                document.FolderId = folderId;
                document.Folder = null;
            }
            document.Hash = hash;
            document.IsPrivate = false;
            context.SaveChanges();
            return (document);
        }
        #endregion UpdateDocument

        #region MarkPrivate
        public static void MarkPrivate(TenantUserSession tenantUserSession, long documentId, ContextTenant context = null)
        {
            var documents = new List<Document>();
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    documents = MarkPrivate_Private(tenantUserSession: tenantUserSession, documentId: documentId, context: context);
                }
            }
            else
            {
                documents = MarkPrivate_Private(tenantUserSession: tenantUserSession, documentId: documentId, context: context);
            }
            using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var maxtransId = AuditTrailManagement.GetMaxAuditTrailTransactionId(context);
                    foreach (var document in documents)
                    {
                        AuditTrailManagement.Add(context, AuditTrailManagement.CreateForMarkingDocumentPrivate(tenantUserSession: tenantUserSession, document: document, transactionId: maxtransId));
                    }
                }
            }
        }
        private static List<Document> MarkPrivate_Private(TenantUserSession tenantUserSession, long documentId, ContextTenant context)
        {
            if (context == null) { throw (new ArgumentNullException("context")); }
            List<Document> documents = new List<Document>();
            bool changed = false;
            using (var transaction = context.Database.BeginTransaction())
            {
                documents = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeDocumentUsers: true);
                if (documents == null) { throw (new Exception("Unable to proccess the following request")); }

                foreach (var document in documents)
                {
                    if (!document.IsPrivate) { changed = true; document.IsPrivate = true; }
                    foreach (var docUsers in document.DocumentUsers.ToList())
                    {
                        context.UserDocuments.Remove(docUsers);
                        context.SaveChanges();
                    }
                }
                if (changed)
                {
                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            return (documents);
        }


        #endregion MarkPrivate

        #region MarkPublic

        public static void MarkPublic(TenantUserSession tenantUserSession, long documentId, ContextTenant context = null)
        {
            var documents = new List<Document>();
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    documents = MarkPublicPrivate(tenantUserSession: tenantUserSession, documentId: documentId, context: context);
                }
            }
            else
            {
                documents = MarkPublicPrivate(tenantUserSession: tenantUserSession, documentId: documentId, context: context);
            }
            using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var maxtransId = AuditTrailManagement.GetMaxAuditTrailTransactionId(context);
                    foreach (var document in documents)
                    {
                        AuditTrailManagement.Add(context, AuditTrailManagement.CreateForMarkingDocumentPublic(tenantUserSession: tenantUserSession, document: document, transactionId: maxtransId));
                    }
                }
            }
        }
        private static List<Document> MarkPublicPrivate(TenantUserSession tenantUserSession, long documentId, ContextTenant context)
        {
            if (context == null) { throw (new ArgumentNullException("context")); }
            var changed = false;
            List<Document> documents = new List<Document>();
            using (var transaction = context.Database.BeginTransaction())
            {
                documents = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.All);
                if (documents == null) { throw (new Exception("Unable to proccess the following request.")); }

                foreach (var document in documents)
                {
                    if (document.IsPrivate) { changed = true; document.IsPrivate = false; }
                }

                if (changed) { context.SaveChanges(); transaction.Commit(); }
            }
            return (documents);
        }
        #endregion MarkPublic

        #region GetDocumentsByDocumentSearchCriteria
        public static List<Document> GetDocumentsByDocumentSearchCriteria(TenantUserSession tenantUserSession, long userId, DocumentSearchCriteria documentSearchCriteria, int pageNumber = GlobalConstants.WebViewPageNumberMinimum, int pageRowCount = GlobalConstants.WebViewPageRowCountDefault, ContextTenant context = null)
        {
            var documents = new List<Document>();
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    documents = GetDocumentsByDocumentSearchCriteriaPrivate(tenantUserSession: tenantUserSession, userId: userId, documentSearchCriteria: documentSearchCriteria, pageNumber: GlobalConstants.WebViewPageNumberMinimum, pageRowCount: GlobalConstants.WebViewPageRowCountDefault, context: context);
                }
            }
            else
            {
                documents = GetDocumentsByDocumentSearchCriteriaPrivate(tenantUserSession: tenantUserSession, userId: userId, documentSearchCriteria: documentSearchCriteria, pageNumber: GlobalConstants.WebViewPageNumberMinimum, pageRowCount: GlobalConstants.WebViewPageRowCountDefault, context: context);
            }
            return documents;
        }
        private static List<Document> GetDocumentsByDocumentSearchCriteriaPrivate(TenantUserSession tenantUserSession, long userId, DocumentSearchCriteria documentSearchCriteria, ContextTenant context, int pageNumber = GlobalConstants.WebViewPageNumberMinimum, int pageRowCount = GlobalConstants.WebViewPageRowCountDefault)
        {
            if (context == null) { throw (new ArgumentNullException("context")); }
            var documents = new List<Document>();


            if (pageNumber < GlobalConstants.WebViewPageNumberMinimum) { throw (new ArgumentException("The argument [pageNumber] cannot be less than " + GlobalConstants.WebViewPageNumberMinimum.ToString() + ".", "pageNumber")); }
            if (pageRowCount < GlobalConstants.WebViewPageRowCountMinimum) { throw (new ArgumentException("The argument [pageRowCount] cannot be less than " + GlobalConstants.WebViewPageRowCountMinimum.ToString() + ".", "pageRowCount")); }
            if (pageRowCount > GlobalConstants.WebViewPageRowCountMaximum) { throw (new ArgumentException("The argument [pageRowCount] cannot be greater than " + GlobalConstants.WebViewPageRowCountMaximum.ToString() + ".", "pageRowCount")); }


            var taken = 0;
            var skipped = 0;
            var take = pageRowCount;
            var skip = (pageRowCount * pageNumber);
            var documentslist = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: null, documentIdType: null, folderId: null, documentResultVersionType: DocumentResultVersionType.All, includeCheckedOutUser: true, includeCreatorUser: true, includeDocumentFragments: true, includeDocumentTags: true, includeDocumentTagUsers: true, includeDocumentTemplates: true, includeFolders: true);
            //////////documentslist = documentslist.Where
            //////////                    (
            //////////                        d =>
            //////////                        (
            //////////                            (
            //////////                                (d.UserId == tenantUserSession.User.Id)
            //////////                                ||
            //////////                                (!d.IsPrivate)
            //////////                            )
            //////////                            &&
            //////////                            (
            //////////                                (d.State == DocumentState.UnMatched) // 5
            //////////                                ||
            //////////                                (d.State == DocumentState.Matched) // 3
            //////////                                ||
            //////////                                (d.State == DocumentState.MatchedMultiple) // 4
            //////////                            )
            //////////                        )
            //////////                    )
            //////////                    .ToList();
            //query = context
            //	.Documents
            //	.AsNoTracking()
            //	.Where
            //	(
            //		d =>
            //		(
            //			(
            //				(d.UserId == tenantUserSession.User.Id)
            //				||
            //				(!d.IsPrivate)
            //			)
            //			&&
            //			(
            //				(d.State == DocumentState.UnMatched) // 5
            //				||
            //				(d.State == DocumentState.Matched) // 3
            //				||
            //				(d.State == DocumentState.MatchedMultiple) // 4
            //			)
            //		)
            //	)
            //	.Include(p => p.Folder)
            //	.Include(p => p.DocumentFragments)
            //	.Include(p => p.DocumentTags)
            //	.Include(p => p.DocumentTagUsers)
            //	.Include(d => d.DocumentTemplates)
            //	.Include(x => x.User)
            //	.Include(x => x.CheckedOutByUser);

            var considerSearchCriteria =
            (
                (false)
                || (documentSearchCriteria.DateTimeFrom.HasValue)
                || (documentSearchCriteria.DateTimeUpTo.HasValue)
                || (!string.IsNullOrWhiteSpace(documentSearchCriteria.TagsUser))
                || (!string.IsNullOrWhiteSpace(documentSearchCriteria.TagsGlobal))
                || (!string.IsNullOrWhiteSpace(documentSearchCriteria.Content))
                || (!string.IsNullOrWhiteSpace(documentSearchCriteria.Filename))
                || (!string.IsNullOrWhiteSpace(documentSearchCriteria.FolderName))
                || (!string.IsNullOrWhiteSpace(documentSearchCriteria.TemplateName))
            );

            foreach (var document in documentslist.ToList())
            {
                //var matched = false;
                var matched = !considerSearchCriteria;

                matched |= ((matched) || ((documentSearchCriteria.DateTimeFrom.HasValue) && (document.DateTime >= documentSearchCriteria.DateTimeFrom)));
                matched |= ((matched) || ((documentSearchCriteria.DateTimeUpTo.HasValue) && (document.DateTime <= documentSearchCriteria.DateTimeUpTo)));

                if (!matched)
                {
                    if (!string.IsNullOrWhiteSpace(documentSearchCriteria.Filename))
                    {
                        var words = StringUtilities.BreakSearchCriteria(documentSearchCriteria.Filename);

                        matched |= ((matched) || (words.Any(word => document.Name.ToLower().Contains(word))));
                    }
                }

                if (!matched)
                {
                    if (!string.IsNullOrWhiteSpace(documentSearchCriteria.FolderName))
                    {
                        var words = StringUtilities.BreakSearchCriteria(documentSearchCriteria.FolderName);

                        matched |= ((matched) || ((document.FolderId.HasValue) && (words.Any(word => document.Folder.Name.ToLower().Contains(word)))));
                    }
                }

                if (!matched)
                {
                    if (!string.IsNullOrWhiteSpace(documentSearchCriteria.TemplateName))
                    {
                        var words = StringUtilities.BreakSearchCriteria(documentSearchCriteria.TemplateName);

                        matched |= ((matched) || ((document.TemplateId.HasValue) && (words.Any(word => document.Template.Title.ToLower().Contains(word)))));
                    }
                }

                if (!matched)
                {
                    if (!string.IsNullOrWhiteSpace(documentSearchCriteria.Content))
                    {
                        var words = StringUtilities.BreakSearchCriteria(documentSearchCriteria.Content);

                        if (words.Any())
                        {
                            foreach (var word in words)
                            {
                                matched |= ((matched) || (document.DocumentFragments.Any(fragment => fragment.FullTextOcr.ToLower().Contains(word))));
                            }
                        }
                    }
                }

                if (!matched)
                {
                    if (!string.IsNullOrWhiteSpace(documentSearchCriteria.TagsGlobal))
                    {
                        var words = StringUtilities.BreakSearchCriteria(documentSearchCriteria.TagsGlobal);

                        if (words.Any())
                        {
                            foreach (var word in words)
                            {
                                matched |= ((matched) || (document.DocumentTags.Any(tag => tag.Value.ToLower().Contains(word))));
                            }
                        }
                    }
                }

                if (!matched)
                {
                    if (!string.IsNullOrWhiteSpace(documentSearchCriteria.TagsUser))
                    {
                        var words = StringUtilities.BreakSearchCriteria(documentSearchCriteria.TagsUser);

                        if (words.Any())
                        {
                            foreach (var word in words)
                            {
                                matched |= ((matched) || (document.DocumentTagUsers.Any(tag => tag.Value.ToLower().Contains(word))));
                            }
                        }
                    }
                }

                if (matched)
                {
                    if (skipped < skip)
                    {
                        skipped++;
                    }
                    else
                    {
                        if (taken < take)
                        {
                            taken++;
                            documents.Add(document);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            documents = DocumentManagement.GetMaxVersionDocuments(tenantUserSession, documents);
            return documents;
        }
        #endregion GetDocumentsByDocumentSearchCriteria

        #region AddDocumentUser
        public static void AddDocumentUser(TenantUserSession tenantUserSession, long documentId, List<User> users, ContextTenant context = null)
        {
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    AddDocumentUserPrivate(tenantUserSession, documentId, users, context);
                }
            }
            else
            {
                AddDocumentUserPrivate(tenantUserSession, documentId, users, context);
            }
        }
        private static void AddDocumentUserPrivate(TenantUserSession tenantUserSession, long documentId, List<User> users, ContextTenant context)
        {
            if (context == null) { throw (new ArgumentNullException("context")); }

            using (var transaction = context.Database.BeginTransaction())
            {
                Document document = null;
                DocumentManagement.GetDocumentById(tenantUserSession, context, documentId, out document);
                if (document == null) { throw (new Exception("Unable to find the following document")); }
                foreach (var user in users)
                {
                    var documentuser = document.DocumentUsers.Where(x => x.UserId == user.Id).FirstOrDefault();
                    if (documentuser != null)
                    {
                        documentuser.IsActive = true;
                        context.UserDocuments.Attach(documentuser);
                        context.SaveChanges();
                    }
                    else
                    {
                        document.DocumentUsers.Add(new UserDocument() { DocumentId = documentId, UserId = user.Id, IsActive = true });
                        context.SaveChanges();
                    }
                }
                transaction.Commit();
            }
        }
        #endregion AddDocumentUser

        #region Checkin
        public static void Checkin(TenantUserSession tenantUserSession, long documentid, ContextTenant context = null)
        {


            Document document = null;
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    document = CheckinPrivate(tenantUserSession: tenantUserSession, context: context, documentid: documentid);
                }
            }
            else
            {
                document = CheckinPrivate(tenantUserSession: tenantUserSession, context: context, documentid: documentid);
            }
            AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForCheckinginDocument(tenantUserSession: tenantUserSession, document: document));
        }
        private static Document CheckinPrivate(TenantUserSession tenantUserSession, long documentid, ContextTenant context)
        {
            Document document = null;
            using (var transaction = context.Database.BeginTransaction())
            {
                var documents = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentid, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.All);

                document = DocumentManagement.GetDocumentsVersions(tenantUserSession, documents, null, DocumentResultVersionType.Maximum).SingleOrDefault();

                if (documentid != document.Id) { throw (new Exception("The document check in attempt is not authorized.")); }
                //This shall never happen.
                if (documents.First().LatestCheckedOutByUserId != document.CheckedOutByUserId) { throw (new Exception("Dev Bug.!! Invalid Document")); }


                if (document != null)
                {
                    if (document.LatestCheckedOutByUserId <= 0)
                    {
                        throw (new Exception("The requested document is already checked in."));
                    }
                    else
                    {
                        document.IsFinalized = true;
                        context.Entry(document).State = EntityState.Modified;
                        context.SaveChanges();

                        foreach (var doc in documents)
                        {
                            //if (document.Id != documentLastest.Id)
                            //{
                            doc.LatestCheckedOutByUserId = 0;
                            //context.Documents.Attach(document);
                            context.SaveChanges();
                            //}
                        }

                        transaction.Commit();

                    }
                }
                else
                {
                    throw (new Exception("Unable to Find the Following Document"));
                }
            }
            return document;
        }
        #endregion Checkin

        #region SaveNewAndDeleteOldDocumentIndex
        public static void SaveNewAndDeleteOldDocumentIndex(TenantUserSession tenantUserSession, int documentId, List<DocumentIndex> documentIndexList, ContextTenant context = null)
        {
            Document document = null;
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    document = SaveNewAndDeleteOldDocumentIndexPrivate(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIndexList: documentIndexList);
                }
            }
            else
            {
                document = SaveNewAndDeleteOldDocumentIndexPrivate(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIndexList: documentIndexList);
            }
            AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForEditingDocumentIndex(tenantUserSession: tenantUserSession, document: document));
        }
        private static Document SaveNewAndDeleteOldDocumentIndexPrivate(TenantUserSession tenantUserSession, int documentId, List<DocumentIndex> documentIndexList, ContextTenant context)
        {
            if (context == null) { throw (new ArgumentNullException("context")); }

            Document document = null;
            using (var contextTrans = context.Database.BeginTransaction())
            {
                document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Maximum, includeDocumentIndexes: true).FirstOrDefault();
                if (document == null) { throw (new Exception("Unable to find the following document")); }
                if (tenantUserSession.User.Id != document.LatestCheckedOutByUserId) { throw (new Exception("The document check in attempt is not authorized.")); }
                if (document.DocumentIndexs != null)
                {
                    foreach (var documentindex in document.DocumentIndexs.ToList())
                    {
                        context.DocumentIndexes.Remove(documentindex);
                        //	context.Entry(documentindex).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
                foreach (var documentIndex in documentIndexList)
                {
                    documentIndex.DocumentId = documentId;
                    context.DocumentIndexes.Add(documentIndex);
                    context.SaveChanges();
                }
                document.DocumentIndexType = DocumentIndexType.Manual;
                context.Entry(document).State = EntityState.Modified;
                context.SaveChanges();
                contextTrans.Commit();
            }
            return document;
        }
        #endregion SaveNewAndDeleteOldDocumentIndex



        #endregion Documents

        #region DocumentFragment
        #region AddDocumentFragment
        /// <summary>
        /// Creates new Document Fragment
        /// </summary>
        /// <param name="documentfragment">Document Fragment Data</param>
        /// <param name="exception">Exception Occured</param>
        /// <returns>Returns true if successfull</returns>
        public static bool AddDocumentFragment(TenantUserSession tenantUserSession, DocumentFragment documentfragment, out Exception exception)
        {
            exception = null;
            bool result = false;
            exception = null;
            DocumentFragment addedDocumentFragement = null;
            try
            {
                if (!(result = AddDocumentFragment(tenantUserSession: tenantUserSession, context: null, documentfragment: documentfragment, addedDocumentFragement: out addedDocumentFragement, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForAddingDocumentFragments(tenantUserSession: tenantUserSession, documentFragment: addedDocumentFragement));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool AddDocumentFragment(TenantUserSession tenantUserSession, ContextTenant context, DocumentFragment documentfragment, out DocumentFragment addedDocumentFragement, out Exception exception)
        {
            exception = null;
            bool result = false;
            addedDocumentFragement = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = AddDocumentFragment(tenantUserSession: tenantUserSession, context: context, documentfragment: documentfragment, addedDocumentFragement: out addedDocumentFragement);
                    }
                }
                else
                {
                    result = AddDocumentFragment(tenantUserSession: tenantUserSession, context: context, documentfragment: documentfragment, addedDocumentFragement: out addedDocumentFragement);
                }

                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                return result;
            }
        }
        private static bool AddDocumentFragment(TenantUserSession tenantUserSession, ContextTenant context, DocumentFragment documentfragment, out DocumentFragment addedDocumentFragement)
        {
            bool result = false;
            var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentfragment.DocumentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();
            if (document == null) { throw (new Exception("Unable to proccess the following request")); }
            addedDocumentFragement = context.DocumentFragments.Add(documentfragment);
            context.SaveChanges();
            context.Dispose();
            addedDocumentFragement.Document = document;
            result = true;
            return result;
        }
        #endregion AddDocumentFragment
        #endregion DocumentFragment

        #region DocumentCorrectiveIndexValues

        #region GetDocumentCorrectiveIndexValueById
        public static bool GetDocumentCorrectiveIndexValueById(TenantUserSession tenantUserSession, long id, out DocumentCorrectiveIndexValue documentCorrectiveIndexValue, out Exception exception)
        {
            var result = false;

            documentCorrectiveIndexValue = null;
            exception = null;

            try
            {
                if (!(result = GetDocumentCorrectiveIndexValueById(tenantUserSession: tenantUserSession, context: null, id: id, documentCorrectiveIndexValue: out documentCorrectiveIndexValue))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingDocumentCorrectiveIndexValues(tenantUserSession: tenantUserSession, documentCorrectiveIndexValue: documentCorrectiveIndexValue));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        internal static bool GetDocumentCorrectiveIndexValueById(TenantUserSession tenantUserSession, ContextTenant context, long id, out DocumentCorrectiveIndexValue documentCorrectiveIndexValue, out Exception exception)
        {
            var result = false;

            documentCorrectiveIndexValue = null;
            exception = null;

            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetDocumentCorrectiveIndexValueById(tenantUserSession: tenantUserSession, context: context, id: id, documentCorrectiveIndexValue: out documentCorrectiveIndexValue);
                    }
                }
                else
                {
                    result = GetDocumentCorrectiveIndexValueById(tenantUserSession: tenantUserSession, context: context, id: id, documentCorrectiveIndexValue: out documentCorrectiveIndexValue);
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        private static bool GetDocumentCorrectiveIndexValueById(TenantUserSession tenantUserSession, ContextTenant context, long id, out DocumentCorrectiveIndexValue documentCorrectiveIndexValue)
        {
            var result = false;

            documentCorrectiveIndexValue = context
                                            .DocumentCorrectiveIndexValues
                                            .Include(x => x.Document)
                                            .SingleOrDefault(d => (d.Id == id));
            var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentCorrectiveIndexValue.DocumentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();
            if (document != null)
            {
                documentCorrectiveIndexValue = null;
                throw (new Exception("Unable to proccess the following request"));
            }
            result = true;
            return (result);
        }
        #endregion GetDocumentCorrectiveIndexValueById

        #region ConfirmDocumentCorrectiveIndexValues
        //FARAZ VERIFY WHAT IS THIS ID PARAMETER
        public static bool ConfirmDocumentCorrectiveIndexValues(TenantUserSession tenantUserSession, long id, long documentId, out Exception exception)
        {
            //--Requires Verification--Faraz
            bool result = false;
            exception = null;
            Document document = null;
            try
            {
                if (!(result = ConfirmDocumentCorrectiveIndexValues(tenantUserSession: tenantUserSession, context: null, documentId: documentId, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForConfirmingDocumentCorrectiveIndexing(tenantUserSession: tenantUserSession, document: document));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool ConfirmDocumentCorrectiveIndexValues(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out Document document, out Exception exception)
        {
            //--Requires Verification--Faraz
            bool result = false;
            exception = null;
            document = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = ConfirmDocumentCorrectiveIndexValues(tenantUserSession: tenantUserSession, context: context, documentId: documentId, document: out document);
                    }
                }
                else
                {
                    result = ConfirmDocumentCorrectiveIndexValues(tenantUserSession: tenantUserSession, context: context, documentId: documentId, document: out document);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool ConfirmDocumentCorrectiveIndexValues(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out Document document)//, long id, long documentId)
        {
            bool result = false;
            using (var contextTrans = context.Database.BeginTransaction())
            {
                document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum, includeDocumentElements: true, includeDocumentCorretiveIndexValues: true).FirstOrDefault();// context.Documents.Where(x => x.Id == documentId).FirstOrDefault();
                if (document == null) { throw (new Exception("Unable to find the following document.")); }
                var correctiveIndexList = document.DocumentCorrectiveIndexValues.ToList();
                if (correctiveIndexList.Count <= 0) { throw (new Exception("Unable to Find the document element")); }
                if (correctiveIndexList.Where(x => x.IndexerId == tenantUserSession.User.Id).FirstOrDefault() == null) { throw (new Exception("You are not authorized to submit the following document")); }
                var limitIndexLevel = correctiveIndexList.Select(x => x.IndexerId).Distinct().Count();
                document.IndexingLevel += 1;
                context.Entry(document).State = EntityState.Modified;
                context.SaveChanges();
                if (document.IndexingLevel <= limitIndexLevel)
                {
                    var submitedIndexs = correctiveIndexList.Where(x => x.IndexerId == tenantUserSession.User.Id).ToList();
                    foreach (var submitedIndex in submitedIndexs)
                    {
                        submitedIndex.Status = DocumentCorrectiveIndexValueStatus.Submitted;
                        context.Entry(submitedIndex).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    if (document.IndexingLevel == limitIndexLevel)
                    {
                        document.IndexingIteration += 1;
                        context.Entry(document).State = EntityState.Modified;
                        context.SaveChanges();

                        try
                        {
                            foreach (var submitedIndex in submitedIndexs)
                            {
                                var correctiveIndexListByIndexerElement = correctiveIndexList.Where(x => (x.IndexElementId == submitedIndex.IndexElementId) && (x.DocumentId == documentId)).ToList();
                                correctiveIndexListByIndexerElement.ForEach((x) =>
                                {
                                    if (x.IndexValue != submitedIndex.IndexValue) { throw (new InvalidDataException("Index values did not match")); }
                                });
                            }
                            var documentElements = document.DocumentElements.ToList();
                            if (documentElements.Count <= 0) { throw (new Exception("Unable to find document eleemnts")); }
                            foreach (var submitedIndex in submitedIndexs)
                            {
                                documentElements.ForEach((x) =>
                                {
                                    if (x.Id == submitedIndex.IndexElementId)
                                    {
                                        x.OcrText = submitedIndex.IndexValue;
                                        context.Entry(x).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }
                                });
                            }
                            document.State = DocumentState.Verified;
                            document.WorkflowState = DocumentWorkflowState.Verified;
                            context.Entry(document).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        catch (Exception invalidDataEx)
                        {
                            document.IndexingLevel = 0;
                            context.Entry(document).State = EntityState.Modified;
                            context.SaveChanges();
                            foreach (var correctiveIndex in correctiveIndexList)
                            {
                                correctiveIndex.Status = DocumentCorrectiveIndexValueStatus.Updated;
                                context.Entry(correctiveIndex).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            throw;
                        }

                    }
                }

                contextTrans.Commit();
                result = true;
            }
            return result;
        }
        #endregion ConfirmDocumentCorrectiveIndexValues

        #region UpdateDocumentCorrectiveIndexValues
        public static bool UpdateDocumentCorrectiveIndexValues(TenantUserSession tenantUserSession, List<DocumentCorrectiveIndexValue> documentCorrectiveIndexValues, out Exception exception)
        {
            bool result = false;
            exception = null;
            Document document = null;
            var updatedCorrectiveIndexList = new List<DocumentCorrectiveIndexValue>();
            try
            {
                if (!(result = UpdateDocumentCorrectiveIndexValues(tenantUserSession: tenantUserSession, context: null, documentCorrectiveIndexValues: documentCorrectiveIndexValues, document: out document, updatedCorrectiveIndexList: out updatedCorrectiveIndexList, exception: out exception))) { if (exception != null) { throw (exception); } }
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        var maxAuditTrailTransId = AuditTrailManagement.GetMaxAuditTrailTransactionId(context);
                        foreach (var docCorrectiveIndexVal in updatedCorrectiveIndexList)
                        {
                            AuditTrailManagement.Add(context, AuditTrailManagement.CreateForEditingDocumentCorrectiveIndexing(tenantUserSession: tenantUserSession, documentCorrectiveIndexValue: docCorrectiveIndexVal, transactionId: maxAuditTrailTransId));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool UpdateDocumentCorrectiveIndexValues(TenantUserSession tenantUserSession, ContextTenant context, List<DocumentCorrectiveIndexValue> documentCorrectiveIndexValues, out Document document, out List<DocumentCorrectiveIndexValue> updatedCorrectiveIndexList, out Exception exception)
        {
            bool result = false;
            exception = null;
            document = null;
            updatedCorrectiveIndexList = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = UpdateDocumentCorrectiveIndexValues(tenantUserSession: tenantUserSession, context: context, documentCorrectiveIndexValues: documentCorrectiveIndexValues, document: out document, updatedCorrectiveIndexList: out updatedCorrectiveIndexList);
                    }
                }
                else
                {
                    result = UpdateDocumentCorrectiveIndexValues(tenantUserSession: tenantUserSession, context: context, documentCorrectiveIndexValues: documentCorrectiveIndexValues, document: out document, updatedCorrectiveIndexList: out updatedCorrectiveIndexList);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool UpdateDocumentCorrectiveIndexValues(TenantUserSession tenantUserSession, ContextTenant context, List<DocumentCorrectiveIndexValue> documentCorrectiveIndexValues, out Document document, out List<DocumentCorrectiveIndexValue> updatedCorrectiveIndexList)
        {
            bool result = false;
            document = null;
            updatedCorrectiveIndexList = new List<DocumentCorrectiveIndexValue>();
            using (var contextTrans = context.Database.BeginTransaction())
            {
                foreach (var correctiveDoc in documentCorrectiveIndexValues.ToList())
                {
                    Exception exception = null;
                    DocumentCorrectiveIndexValue currentDocCorrectiveIndexVal = null;
                    document = currentDocCorrectiveIndexVal.Document;
                    GetDocumentCorrectiveIndexValueById(tenantUserSession, context, correctiveDoc.Id, out currentDocCorrectiveIndexVal, out exception);
                    if (exception != null) { throw exception; }
                    if (currentDocCorrectiveIndexVal == null) { throw (new UnauthorizedAccessException("Unable to Find the document element")); }
                    if (currentDocCorrectiveIndexVal.Status == DocumentCorrectiveIndexValueStatus.Submitted) { throw (new UnauthorizedAccessException("Unable to edit a document once submitted.")); }
                    if (currentDocCorrectiveIndexVal.IndexerId != tenantUserSession.User.Id) { throw (new UnauthorizedAccessException("You are not authorized to index the following document")); }

                    currentDocCorrectiveIndexVal.IndexValue = correctiveDoc.IndexValue;
                    currentDocCorrectiveIndexVal.Status = DocumentCorrectiveIndexValueStatus.Updated;
                    context.Entry(currentDocCorrectiveIndexVal).State = EntityState.Modified;
                    context.SaveChanges();
                    updatedCorrectiveIndexList.Add(currentDocCorrectiveIndexVal);
                }
                contextTrans.Commit();
                result = true;
            }
            return result;
        }
        #endregion UpdateDocumentCorrectiveIndexValues

        #region GetDocumentCorrectiveIndexValuesByDocumentId
        public static bool GetDocumentCorrectiveIndexValuesByDocumentId(TenantUserSession tenantUserSession, long documentId, out List<DocumentCorrectiveIndexValue> documentCorrectiveIndexList, out Exception exception)
        {
            exception = null;
            bool result = false;
            documentCorrectiveIndexList = null;
            Document document = null;
            try
            {
                if (!(result = GetDocumentCorrectiveIndexValuesByDocumentId(tenantUserSession: tenantUserSession, context: null, documentId: documentId, documentCorrectiveIndexList: out documentCorrectiveIndexList, document: out document, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingDocumentCorrectiveIndexValues(tenantUserSession: tenantUserSession));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            result = true;
            return result;
        }
        internal static bool GetDocumentCorrectiveIndexValuesByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out List<DocumentCorrectiveIndexValue> documentCorrectiveIndexList, out Document document, out Exception exception)
        {
            exception = null;
            bool result = false;
            documentCorrectiveIndexList = null;
            document = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetDocumentCorrectiveIndexValuesByDocumentId(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentCorrectiveIndexList: out documentCorrectiveIndexList, document: out document);
                    }
                }
                else
                {
                    result = GetDocumentCorrectiveIndexValuesByDocumentId(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentCorrectiveIndexList: out documentCorrectiveIndexList, document: out document);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool GetDocumentCorrectiveIndexValuesByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out List<DocumentCorrectiveIndexValue> documentCorrectiveIndexList, out Document document)
        {
            bool result = false;
            documentCorrectiveIndexList = new List<DocumentCorrectiveIndexValue>();
            document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum, includeDocumentCorretiveIndexValues: true).FirstOrDefault();
            if (document == null) { throw (new Exception("Unable to proccess the following request")); }
            documentCorrectiveIndexList = document.DocumentCorrectiveIndexValues.ToList();
            if (documentCorrectiveIndexList.Count <= 0) { throw (new Exception("There are no elements to be indexed")); }
            if ((documentCorrectiveIndexList.First().Document.State == DocumentState.Verified) && (documentCorrectiveIndexList.First().Document.WorkflowState == DocumentWorkflowState.Verified)) { throw (new Exception("Verified documents can not be indexed")); }
            if (documentCorrectiveIndexList.Where(x => x.IndexerId == tenantUserSession.User.Id).FirstOrDefault() == null) { throw (new Exception("You are not assigned to index the following document")); }
            if (documentCorrectiveIndexList.Where(x => x.IndexerId == tenantUserSession.User.Id).First().Status == DocumentCorrectiveIndexValueStatus.Submitted) { throw (new Exception($"Indexing was already performed by {tenantUserSession.User.NameFull} on the following document")); }
            result = true;
            return result;
        }
        #endregion GetDocumentCorrectiveIndexValuesByDocumentId

        #endregion DocumentCorrectiveIndexValues

        #region DocumentElement

        #region GetAllDocumentElementsById
        public static bool GetAllDocumentElementsById(TenantUserSession tenantUserSession, long ID, out List<DocumentElement> documentElements, out Exception exception)
        {
            documentElements = null;
            exception = null;
            bool result = false;
            try
            {
                if (!(result = GetAllDocumentElementsById(tenantUserSession: tenantUserSession, context: null, ID: ID, documentElements: out documentElements, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingAllDocumentElements(tenantUserSession: tenantUserSession));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool GetAllDocumentElementsById(TenantUserSession tenantUserSession, ContextTenant context, long ID, out List<DocumentElement> documentElements, out Exception exception)
        {
            documentElements = null;
            exception = null;
            bool result = false;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetAllDocumentElementsById(tenantUserSession: tenantUserSession, context: context, ID: ID, documentElements: out documentElements);
                    }
                }
                else
                {
                    result = GetAllDocumentElementsById(tenantUserSession: tenantUserSession, context: context, ID: ID, documentElements: out documentElements);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool GetAllDocumentElementsById(TenantUserSession tenantUserSession, ContextTenant context, long ID, out List<DocumentElement> documentElements)
        {
            documentElements = null;
            bool result = false;
            var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: ID, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Exact, includeDocumentElements: true).FirstOrDefault();
            documentElements = document.DocumentElements.ToList();
            result = true;
            return result;
        }
        #endregion GetAllDocumentElementsById

        #region AddDocumentElement
        public static bool AddDocumentElement(TenantUserSession tenantUserSession, DocumentElement documentElement, out Exception exception)
        {
            exception = null;
            bool result = false;
            DocumentElement addedDocumentElement = null;
            try
            {
                if (!(result = AddDocumentElement(tenantUserSession: tenantUserSession, context: null, documentElement: documentElement, addedDocumentElement: out addedDocumentElement, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForAddingDocumentElement(tenantUserSession: tenantUserSession, documentElement: addedDocumentElement));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool AddDocumentElement(TenantUserSession tenantUserSession, ContextTenant context, DocumentElement documentElement, out DocumentElement addedDocumentElement, out Exception exception)
        {
            exception = null;
            bool result = false;
            addedDocumentElement = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = AddDocumentElement(tenantUserSession: tenantUserSession, context: context, documentElement: documentElement, addedDocumentElement: out addedDocumentElement);
                    }
                }
                else
                {
                    result = AddDocumentElement(tenantUserSession: tenantUserSession, context: context, documentElement: documentElement, addedDocumentElement: out addedDocumentElement);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool AddDocumentElement(TenantUserSession tenantUserSession, ContextTenant context, DocumentElement documentElement, out DocumentElement addedDocumentElement)
        {
            bool result = false;
            var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentElement.DocumentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();
            if (document == null) { throw (new Exception("Unable to proccess the following request")); }
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            context.DocumentElements.Add(documentElement);
            context.SaveChanges();
            context.Dispose();
            documentElement.Document = document;
            addedDocumentElement = documentElement;
            result = true;
            return result;
        }
        #endregion AddDocumentElement

        #region UpdateDocumentElementText
        /// <summary>
        /// Updates a document element text.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="documentElementId">Document element id, to change the elements text</param>
        /// <param name="text">Text to be changed of the element found by the document element id</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        public static bool UpdateDocumentElementText(TenantUserSession tenantUserSession, long documentElementId, string text, out Exception exception)
        {
            bool result = false;
            exception = null;
            DocumentElement documentElement = null;
            try
            {
                if (!(result = UpdateDocumentElementText(tenantUserSession: tenantUserSession, context: null, documentElementId: documentElementId, text: text, documentElement: out documentElement, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForEditingDocumentElement(tenantUserSession: tenantUserSession, documentElement: documentElement));

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Updates a document element text.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="documentElementId">Document element id, to change the elements text</param>
        /// <param name="text">Text to be changed of the element found by the document element id</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        internal static bool UpdateDocumentElementText(TenantUserSession tenantUserSession, ContextTenant context, long documentElementId, string text, out DocumentElement documentElement, out Exception exception)
        {
            bool result = false;
            exception = null;
            documentElement = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = UpdateDocumentElementText(tenantUserSession: tenantUserSession, context: context, documentElementId: documentElementId, text: text, documentElement: out documentElement);
                    }
                }
                else
                {
                    result = UpdateDocumentElementText(tenantUserSession: tenantUserSession, context: context, documentElementId: documentElementId, text: text, documentElement: out documentElement);
                }

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Updates a document element text. Notice! this should be called only through its internal function.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="documentElementId">Document element id, to change the elements text</param>
        /// <param name="text">Text to be changed of the element found by the document element id</param>
        /// <returns>
        /// Returns true if operation was successfull else returns false if failed. 
        /// Exception is to be handled in it's calling function. 
        /// </returns>
        private static bool UpdateDocumentElementText(TenantUserSession tenantUserSession, ContextTenant context, long documentElementId, string text, out DocumentElement documentElement)
        {
            bool result = false;
            documentElement = context.DocumentElements.Where(x => x.Id == documentElementId).SingleOrDefault();
            if (documentElement != null)
            {
                var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentElement.DocumentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();
                if (document == null) { throw (new Exception("Unable to proccess the following request")); }
                documentElement.OcrText = text;
                context.SaveChanges();
                documentElement.Document = document;
                result = true;
            }
            else
            {
                throw (new Exception("Unable to find the following document"));
            }
            return result;
        }
        #endregion UpdateDocumentElementText


        #endregion DocumentElement

        #region DocumentIndex

        #region GetAllDocumentIndexByDocumentId
        /// <summary>
        /// Gets document index by document id.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="documentId">Document id to get document indexes</param>
        /// <param name="documentIndex">Sends out the list of document indexes with respect to the document id</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        public static bool GetAllDocumentIndexByDocumentId(TenantUserSession tenantUserSession, long documentId, out List<DocumentIndex> documentIndex, out Exception exception)
        {
            documentIndex = null;
            exception = null;
            bool result = false;
            try
            {
                if (!(result = GetAllDocumentIndexByDocumentId(tenantUserSession: tenantUserSession, context: null, documentId: documentId, documentIndex: out documentIndex, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingAllDocumentIndex(tenantUserSession: tenantUserSession));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Gets document index by document id.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="documentId">Document id to get document indexes</param>
        /// <param name="documentIndex">Sends out the list of document indexes with respect to the document id</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        internal static bool GetAllDocumentIndexByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out List<DocumentIndex> documentIndex, out Exception exception)
        {
            documentIndex = null;
            exception = null;
            bool result = false;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetAllDocumentIndexByDocumentId(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIndex: out documentIndex);
                    }
                }
                else
                {
                    result = GetAllDocumentIndexByDocumentId(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIndex: out documentIndex);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Gets document index by document id. Notice! this should be called only through its internal function.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="documentId">Document id to get document indexes</param>
        /// <param name="documentIndex">Sends out the list of document indexes with respect to the document id</param>
        /// <returns>
        /// Returns true if operation was successfull else returns false if failed. 
        /// Exception is to be handled in it's calling function. 
        /// </returns>
        private static bool GetAllDocumentIndexByDocumentId(TenantUserSession tenantUserSession, ContextTenant context, long documentId, out List<DocumentIndex> documentIndex)
        {
            documentIndex = null;
            bool result = false;
            var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Exact, includeDocumentIndexes: true).FirstOrDefault();
            documentIndex = document.DocumentIndexs.ToList();
            result = true;
            return result;
        }
        #endregion GetAllDocumentIndexByDocumentId

        #endregion DocumentIndex

        #region DocumentTemplate 
        #region AddDocumentTemplate
        /// <summary>
        /// Request to add a new document template.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="documenttemplate">Document template to be added</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        public static bool AddDocumentTemplate(TenantUserSession tenantUserSession, DocumentTemplate documenttemplate, out Exception exception)
        {
            var result = false;
            exception = null;
            DocumentTemplate addedDocumentTemplate = null;
            try
            {
                if (!(result = AddDocumentTemplate(tenantUserSession: tenantUserSession, context: null, documenttemplate: documenttemplate, addedDocumentTemplate: out addedDocumentTemplate, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForAddingDocumentTemplate(tenantUserSession: tenantUserSession, documentTemplate: addedDocumentTemplate));
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        /// <summary>
        /// Request to add a new document template.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="documenttemplate">Document template to be added</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        public static bool AddDocumentTemplate(TenantUserSession tenantUserSession, ContextTenant context, DocumentTemplate documenttemplate, out DocumentTemplate addedDocumentTemplate, out Exception exception)
        {
            var result = false;
            exception = null;
            addedDocumentTemplate = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = AddDocumentTemplate(tenantUserSession: tenantUserSession, context: context, documenttemplate: documenttemplate, addedDocumentTemplate: out addedDocumentTemplate);
                    }
                }
                else
                {
                    result = AddDocumentTemplate(tenantUserSession: tenantUserSession, context: context, documenttemplate: documenttemplate, addedDocumentTemplate: out addedDocumentTemplate);
                }

            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        /// <summary>
        /// Request to add a new document template. Notice! this should be called only through its internal function.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="documenttemplate">Document template to be added</param>
        /// <returns>
        /// Returns true if operation was successfull else returns false if failed. 
        /// Exception is to be handled in it's calling function. 
        /// </returns>
        private static bool AddDocumentTemplate(TenantUserSession tenantUserSession, ContextTenant context, DocumentTemplate documenttemplate, out DocumentTemplate addedDocumentTemplate)
        {

            var result = false;
            var document = GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: documenttemplate.DocumentId, documentIdType: DocumentIdType.Id, folderId: null, documentResultVersionType: DocumentResultVersionType.Minimum).FirstOrDefault();
            if (document == null) { throw (new Exception("Unable to proccess the following request")); }
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
            addedDocumentTemplate = context.DocumentTemplate.Add(documenttemplate);
            context.SaveChanges();
            context.Dispose();
            addedDocumentTemplate.Document = document;
            result = true;
            return (result);
        }
        #endregion AddDocumentTemplate

        #endregion DocumentTemplate 

        #endregion ACLImplemented

        #region DocumentSearchCriteria Management
        /// <summary>
        /// Save document search criteria.
        /// </summary>
        /// <param name="documentSearchCriteria">The Criteria to find a Document.</param>
        /// <param name="exception">The exception generated (if any).</param>
        /// <returns>True if the function succeeded. False in case of an exception.</returns>
        public static bool AddDocumentSearchCriteria(TenantUserSession tenantUserSession, DocumentSearchCriteria documentSearchCriteria, out Exception exception)
        {
            var result = false;
            exception = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    context.DocumentSearchCriteria.Add(documentSearchCriteria);
                    context.SaveChanges();
                    context.Dispose();
                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        /// <summary>
        /// Get previous saved search from Docement search criteria table
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="documentSearchCriteria"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static bool GetPreviousSearchByUserId(TenantUserSession tenantUserSession, long userId, out List<DocumentSearchCriteria> documentSearchCriteria, out Exception exception)
        {
            var result = false;
            documentSearchCriteria = null;
            exception = null;

            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    documentSearchCriteria = context.DocumentSearchCriteria.Where(x => x.UserId == userId).Select(x => x).ToList();
                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        public static bool GetPreviousSearchById(TenantUserSession tenantUserSession, long userId, long searchId, out DocumentSearchCriteria documentSearchCriteria, out Exception exception)
        {
            var result = false;
            documentSearchCriteria = null;
            exception = null;

            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    context.Configuration.ProxyCreationEnabled = false;
                    documentSearchCriteria = context.DocumentSearchCriteria.Where(x => (x.UserId == userId) && (x.Id == searchId)).Select(x => x).FirstOrDefault();
                    result = true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return (result);
        }
        #endregion DocumentSearchCriteria Management

        #region Helper Methods
        #region VerifyVersion
        /// <summary>
        /// Verify if version provided is greater than the previous version of a document and creates a new version.
        /// </summary>
        /// <param name="document">Current document containing version to be verified with newly provided version</param>
        /// <param name="VersionMajor">Verifies major version. Provide version major for manually update a version</param>
        /// <param name="VersionMinor">Verifies minor version. Provide version minor for manually update a version</param>
        /// <param name="major">Sends out major version</param>
        /// <param name="minor">Sends out minor version</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <param name="autoVersion">By default a version is auto incremented.</param>
        /// <returns>
        /// Returns true if operation was successfull else returns false if failed. 
        /// </returns>
        private static bool VerifyVersion(Document document, int VersionMajor, int VersionMinor, out int major, out int minor, out Exception exception, bool autoVersion = true)
        {
            exception = null;
            var result = false;
            major = 1;
            minor = 0;
            try
            {
                if (document != null)
                {
                    if (VersionMajor >= 0)
                    {
                        // If previous veraion major is same, new minior should be greater than previous.
                        // Otherwise new major should be greater than old version.
                        if (VersionMajor < document.VersionMajor) { throw (new Exception("Version is less than previous version")); }
                        else if (document.VersionMajor == VersionMajor)
                        {
                            major = VersionMajor;
                            if (VersionMinor < document.VersionMinor) { throw (new Exception("Version is less than previous version")); }
                            else if (document.VersionMinor == VersionMinor) { throw (new Exception("Version can not be same as previous version")); }
                            else { minor = VersionMinor; }
                            result = true;
                        }
                        else
                        {
                            major = VersionMajor;
                            minor = VersionMinor;
                            result = true;
                        }
                    }
                    else
                    {
                        if (autoVersion)
                        {
                            major = document.VersionMajor + 1;
                            minor = 0;
                            result = true;
                        }
                        else
                        {
                            throw (new Exception("Version is less than previous version"));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                result = false;
            }
            return result;
        }
        #endregion VerifyVersion
        #region GetMaxVersionDocuments
        /// <summary>
        /// Gets max version documents for each documents in the list.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session.</param>
        /// <param name="documents">List of document to be searched for max version documents.</param>
        /// <returns>Returns the max version documents.</returns>
        public static List<Document> GetMaxVersionDocuments(TenantUserSession tenantUserSession, List<Document> documents)
        {
            var filteredDocuments = new List<Document>();
            var a = documents;
            var documentVersionGroups = documents.GroupBy(d => d.DocumentOriginalId);

            foreach (var documentVersionGroup in documentVersionGroups)
            {
                Document document = null;
                var documentVersionGroupOrdered = documentVersionGroup
                    .OrderBy(d => d.VersionMajor)
                    .ThenBy(d => d.VersionMinor);
                if (documentVersionGroupOrdered.Count() == 0)
                {
                    // This should never happen.
                    throw (new InvalidOperationException("Dev bug!"));
                }
                else if (documentVersionGroupOrdered.Count() == 1)
                {
                    document = documentVersionGroupOrdered.Single();
                    if (!filteredDocuments.Any(d => d.Id == document.Id)) { filteredDocuments.Add(document); }
                }
                else if (documentVersionGroupOrdered.Count() > 1)
                {

                    if (documentVersionGroupOrdered.Last().LatestCheckedOutByUserId > 0)
                    {
                        if (documentVersionGroupOrdered.Last().LatestCheckedOutByUserId == tenantUserSession.User.Id)
                        {
                            document = documentVersionGroupOrdered.Last();
                            var doucmentUsers = documentVersionGroupOrdered.First().DocumentUsers.ToList();
                            doucmentUsers.ForEach(x => document.DocumentUsers.Add(x));
                            if (!filteredDocuments.Any(d => d.Id == document.Id)) { filteredDocuments.Add(document); }
                        }
                        else
                        {
                            document = documentVersionGroupOrdered.Last();
                            int length = documentVersionGroupOrdered.Count();
                            var secondHighestVersion = documentVersionGroupOrdered.ElementAt(length - 2);

                            var doucmentUsers = documentVersionGroupOrdered.First().DocumentUsers.ToList();
                            doucmentUsers.ForEach(x => secondHighestVersion.DocumentUsers.Add(x));
                            if (secondHighestVersion.IsPrivate == false)/* && secondHighestVersion.IsFinalized == true*/
                            {
                                if (!filteredDocuments.Any(d => d.Id == secondHighestVersion.Id)) { filteredDocuments.Add(secondHighestVersion); }
                            }
                        }
                    }
                    else
                    {
                        document = documentVersionGroupOrdered.Last();
                        var doucmentUsers = documentVersionGroupOrdered.First().DocumentUsers.ToList();
                        doucmentUsers.ForEach(x => document.DocumentUsers.Add(x));
                        if (!filteredDocuments.Any(d => d.Id == document.Id)) { filteredDocuments.Add(document); }
                    }

                }
            }

            return (filteredDocuments);
        }

        // Modified by Nandha  for Demo

        public static List<Document> GetAllMaxVersionDocuments(TenantUserSession tenantUserSession, List<Document> documents)
        {
            var filteredDocuments = new List<Document>();
            var a = documents;
            var documentVersionGroups = documents.GroupBy(d => d.DocumentOriginalId);

            foreach (var documentVersionGroup in documentVersionGroups)
            {
                Document document = null;
                var documentVersionGroupOrdered = documentVersionGroup
                    .OrderBy(d => d.VersionMajor)
                    .ThenBy(d => d.VersionMinor);
                if (documentVersionGroupOrdered.Count() == 0)
                {
                    // This should never happen.
                    throw (new InvalidOperationException("Dev bug!"));
                }
                else if (documentVersionGroupOrdered.Count() == 1)
                {
                    document = documentVersionGroupOrdered.Single();
                    if (!filteredDocuments.Any(d => d.Id == document.Id)) { filteredDocuments.Add(document); }
                }

                document = documentVersionGroupOrdered.Last();
                var doucmentUsers = documentVersionGroupOrdered.First().DocumentUsers.ToList();
                doucmentUsers.ForEach(x => document.DocumentUsers.Add(x));
                if (!filteredDocuments.Any(d => d.Id == document.Id)) { filteredDocuments.Add(document); }



            }

            return (filteredDocuments);
        }



        #endregion GetMaxVersionDocuments
        #endregion Helper Methods


    }
}


//public static List<Document> GetDocuments
//(
//	TenantUserSession tenantUserSession,
//	DbSet<Document> dbSetDocuments,
//	long? folderId,
//	DocumentResultVersionType documentResultVersionType,
//	bool includeDiscourse = false,
//	bool includeDocumentElements = false,
//	bool includeDocumentFragments = false,
//	bool includeCreatorUser = false,
//	bool includeCheckedOutUser = false,
//	bool includeFolders = false,
//	bool includeDocumentUsers = false,
//	bool includeDocumentIndexes = false,
//	bool includeDocumentTags = false,
//	bool includeDocumentTagUsers = false,
//	bool includeDocumentTemplates = false,
//	bool includeDocumentCorretiveIndexValues = false,
//	bool? isFinalized = null,
//	int? skipRows = null,
//	int? takeRows = null
//)
//{
//	if (dbSetDocuments == null)
//	{
//		using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString, proxyCreationEnabled: true, lazyLoadingEnabled: true))
//		{

//			var query = DocumentManagement.GetQueryDocuments
//			(
//				tenantUserSession: tenantUserSession,
//				context: context,
//				folderId: folderId,
//				documentId: null,
//				documentIdType: null,
//				includeDiscourse: includeDiscourse,
//				includeDocumentElements: includeDocumentElements,
//				includeDocumentFragments: includeDocumentFragments,
//				includeCreatorUser: includeCreatorUser,
//				includeCheckedOutUser: includeCheckedOutUser,
//				includeFolders: includeFolders,
//				includeDocumentUsers: includeDocumentUsers,
//				includeDocumentIndexes: includeDocumentIndexes,
//				includeDocumentTags: includeDocumentTags,
//			includeDocumentTagUsers: includeDocumentTagUsers,
//			includeDocumentTemplates: includeDocumentTemplates,
//			includeDocumentCorretiveIndexValues: includeDocumentCorretiveIndexValues,
//				isFinalized: isFinalized,
//				skipRows: skipRows,
//				takeRows: takeRows
//			);

//			var documents = query.ToList();

//			documents = DocumentManagement.GetDocumentsVersions(tenantUserSession, documents, documentResultVersionType);

//			return (documents);
//		}
//	}
//	else
//	{
//		var query = DocumentManagement.GetQueryDocuments
//			(
//				tenantUserSession: tenantUserSession,
//				dbSetDocuments: dbSetDocuments,
//				folderId: folderId,
//				documentId: null,
//				documentIdType: null,
//				includeDiscourse: includeDiscourse,
//				includeDocumentElements: includeDocumentElements,
//				includeDocumentFragments: includeDocumentFragments,
//				includeCreatorUser: includeCreatorUser,
//				includeCheckedOutUser: includeCheckedOutUser,
//				includeFolders: includeFolders,
//				includeDocumentUsers: includeDocumentUsers,
//				includeDocumentIndexes: includeDocumentIndexes,
//				includeDocumentTags: includeDocumentTags,
//			includeDocumentTagUsers: includeDocumentTagUsers,
//			includeDocumentTemplates: includeDocumentTemplates,
//			includeDocumentCorretiveIndexValues: includeDocumentCorretiveIndexValues,
//				isFinalized: isFinalized,
//				skipRows: skipRows,
//				takeRows: takeRows
//			);

//		var documents = query.ToList();

//		documents = DocumentManagement.GetDocumentsVersions(tenantUserSession, documents, documentResultVersionType);

//		return (documents);
//	}

//}