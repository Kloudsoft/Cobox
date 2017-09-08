using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.BusinessLayer.Lookup;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public static class FolderManagement
    {
        internal static IQueryable<Folder> GetQueryFolders
        (
            TenantUserSession tenantUserSession,
            ContextTenant context,
            //long? documentId,
            long? folderId,
            FolderIdType? folderIdType,
            //bool includeParentFolder = false,
            //bool includeDocument = false,
            bool includeDepartment = false,
            bool includeFolderUsers = false,
            bool includeCreatorUser = false
        //bool includeChildFolders = false
        )
        {

            if ((!folderId.HasValue) && (folderIdType== FolderIdType.Id)) { throw (new ArgumentException("The arguments [folderId] should can not be null when folderIdType is [Id] ", "documentId, documentIdType")); }
            var userId = tenantUserSession.User.Id;
            var query = context.Folders.AsQueryable();
            if (folderIdType.HasValue)
            {
                switch (folderIdType)
                {
                    case FolderIdType.Id: { if (folderId.HasValue) { query = query.Where(f => (f.Id == folderId.Value)); } break; }
                    case FolderIdType.ParentId: { if (folderId.HasValue) { query = query.Where(f => (f.ParentId == folderId.Value)); } break; }
                    default: { throw (new NotImplementedException()); }
                }
            }

            if (includeCreatorUser) { query = query.Include(f => f.UserCreatedBy); }
            if (includeFolderUsers) { query = query.Include(f => f.FolderUsers); }
            if (includeDepartment) { query = query.Include(x => x.Department).Include(x => x.Department.Users); }

            query = query
                .Where
                (
                    f =>
                    (
                        (
                            (f.UserCreatedById == userId)
                        // TODO: Remove & test for production.
                        //|| (f.FolderUsers.Any(uf => uf.Id == userId))
                        )
                        ||
                        (
                            (f.UserCreatedById != userId)
                            && (f.FolderUsers.Any(uf => uf.UserId == userId))
                        )
                    )
                );

            return (query);
            //if (!folderIdType.HasValue|| ((!folderId.HasValue)&&(!folderIdType.HasValue))) { throw (new ArgumentException("The arguments [folderIdType] should not be null or both have values.", "folderId, folderIdType")); }

            //if (folderId.HasValue) { query = query.Where(d => (d.FolderId.Value == folderId.Value)); }
            //if (documentId.HasValue) { query = query.Where(d => (d.Documents == folderId.Value)); }
            //if (includeParentFolder)
            //{
            //	query = query
            //			.Include(d => d.Parent)
            //			.Include(d => d.Parent.FolderUsers)
            //			.Include(d => d.Parent.Documents);
            //}


            //if (includeDocument)
            //{
            //	query = query
            //			.Include(x => x.Documents)
            //			.Include(x => x.Documents.Select(y => y.DocumentUsers))
            //			.Include(x => x.Documents.Select(y => y.User))
            //			.Include(x => x.Documents.Select(y => y.CheckedOutByUser))
            //			.Include(x => x.Documents.Select(y => y.Folder));
            //}

            //if (includeChildFolders)
            //{
            //	query = query
            //			.Include(x => x.Folders)
            //			.Include(x => x.Folders.Select(y => y.UserCreatedBy))
            //			.Include(x => x.Folders.Select(y => y.Documents))
            //			.Include(x => x.Folders.Select(y => y.Folders))
            //			.Include(x => x.Folders.Select(y => y.FolderUsers));
            //}

        }


        public static bool ValidateUserFolderRightsHirarchy(long folderId, List<Folder> allFolders, long userId)
        {
            bool result = false;
            var currentFolder = allFolders.Where(x => x.Id == folderId).FirstOrDefault();
            if (currentFolder == null) { return result; }
            if (currentFolder.FolderUsers.Any(x => ((x.UserId == userId) && (x.IsActive == true))))
            {
                result = true;
            }
            if (currentFolder.ParentId == null) { return true; }
            else { return FolderManagement.ValidateUserFolderRightsHirarchy((long)currentFolder.ParentId, allFolders, userId) && result; }
        }

        //Assign (TenantUserSession tus, User user, Folder folder)
        //{
        //	folder.FolderUsers.Add(user);
        //	var parent = folder;
        //	while (parent.Parent != null)
        //	{
        //		parent.FolderUsers.Add(user);
        //	}
        //}


        //New Function
        internal static List<Folder> GetFolders
        (
            TenantUserSession tenantUserSession,
            ContextTenant context,
            long? folderId,
            FolderIdType? folderIdType,
            long? documentId,
            FolderResultType folderResultType,
            bool includeParentFolder = false,
            //bool includeDocument = false,
            bool includeDepartment = false,
            bool includeFolderUsers = false,
            bool includeCreatorUser = false
        //bool includeChildFolders = false
        )
        {
            if (context == null)
            {
                using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString, proxyCreationEnabled: false, lazyLoadingEnabled: false))
                {
                    var query = FolderManagement.GetQueryFolders
                    (
                        tenantUserSession: tenantUserSession,
                        context: context,
                        folderId: folderId,
                        //documentId: documentId,
                        folderIdType: folderIdType,
                        //	includeParentFolder: includeParentFolder,
                        //	includeDocument: includeDocument,
                        includeDepartment: includeDepartment,
                        includeFolderUsers: includeFolderUsers,
                        includeCreatorUser: includeCreatorUser
                    //	includeChildFolders: includeChildFolders
                    );

                    var folders = query.ToList();

                    folders = FolderManagement.GetFolderResults(tenantUserSession: tenantUserSession, folders: folders,  folderResultType: folderResultType, folderId: folderId);
                    return (folders);
                }
            }
            else
            {
                var query = FolderManagement.GetQueryFolders
                (
                        tenantUserSession: tenantUserSession,
                        context: context,
                        folderId: folderId,
                        //documentId: documentId,
                        folderIdType: folderIdType,
                        //	includeParentFolder: includeParentFolder,
                        //includeDocument: includeDocument,
                        includeDepartment: includeDepartment,
                        includeFolderUsers: includeFolderUsers,
                        includeCreatorUser: includeCreatorUser
                //includeChildFolders: includeChildFolders
                );

                var folders = query.ToList();

                folders = FolderManagement.GetFolderResults(tenantUserSession: tenantUserSession, folders: folders, folderResultType: folderResultType, folderId: folderId);
                return (folders);
            }
        }


        public static List<Folder> GetFolders
        (
            TenantUserSession tenantUserSession,
            long? folderId,
            FolderIdType? folderIdType,
            long? documentId,
            FolderResultType folderResultType,
            //	bool includeParentFolder = false,
            //bool includeDocument = false,
            bool includeDepartment = false,
            bool includeFolderUsers = false,
            bool includeCreatorUser = false
        //	bool includeChildFolders = false
        )
        {
            var folders = FolderManagement.GetFolders
                (
                        tenantUserSession: tenantUserSession,
                        context: null,
                        folderId: folderId,
                        documentId: documentId,
                        folderIdType: folderIdType,
                        folderResultType: folderResultType,
                        //	includeParentFolder: includeParentFolder,
                        //includeDocument: includeDocument,
                        includeDepartment: includeDepartment,
                        includeFolderUsers: includeFolderUsers,
                        includeCreatorUser: includeCreatorUser
                //	includeChildFolders: includeChildFolders
                );

            //var folders = query.ToList();

            //folders = FolderManagement.GetFolderResults(tenantUserSession, folders, folderResultType);
            return (folders);

        }

        /// <summary>
        /// Sort and returns the sorted values.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="folders">List of folders to be sorted</param>
        /// <param name="folderResultType">Type of folder sorting</param>
        /// <param name="folderId">Sorts the list of folders with respect to the folder id. Null can be passed for default sorting with respect to it's folder type.</param>
        /// <returns>Returns the sorted folder list</returns>
        //private static List<Folder> GetFolderResults(TenantUserSession tenantUserSession, List<Folder> folders, FolderResultType folderResultType, long? folderId = null)
        //{
        //    // TODO: Implement in-memory logic.
        //    switch (folderResultType)
        //    {
        //        case FolderResultType.Root:
        //        {
        //            folders = folders
        //                        .Where(f => f.ParentId == null)
        //                        .OrderBy(f => f.Id)
        //                        .ThenBy(f => f.Name)
        //                        .ToList();
        //            break;
        //        }
        //        case FolderResultType.Parents:
        //        {
        //            if (folderId == null)
        //            {
        //                var parentIds = folders.Select(f => f.ParentId).Distinct();
        //                folders = folders
        //                            .Where(f => parentIds.Contains(f.Id))
        //                            .OrderBy(f => f.Id)
        //                            .ThenBy(f => f.Name)
        //                            .ToList();
        //            }
        //            else
        //            {
        //                var parentId = folders.Where(f => f.Id == folderId).FirstOrDefault();
        //                if (parentId != null)
        //                {
        //                    folders = folders
        //                            .Where(f => f.Id == parentId.ParentId)
        //                            .OrderBy(f => f.Id)
        //                            .ThenBy(f => f.Name)
        //                            .ToList();
        //                }
        //            }

        //            break;
        //        }
        //        case FolderResultType.Childrens:
        //        {
        //            if (folderId == null)
        //            {
        //                var id = folders.Where(x => x.ParentId == null).Select(x => x.Id).FirstOrDefault();
        //                folders = folders
        //                        .Where(f => f.ParentId == id)
        //                        .OrderByDescending(d => d.Id)
        //                        .OrderBy(f => f.Id)
        //                        .ThenBy(f => f.Name)
        //                        .ToList();
        //            }
        //            else
        //            {
        //                folders = folders
        //                    .Where(f => f.ParentId == folderId)
        //                    .OrderByDescending(d => d.Id)
        //                    .OrderBy(f => f.Id)
        //                    .ThenBy(f => f.Name)
        //                    .ToList();
        //            }
        //            break;
        //        }
        //        case FolderResultType.Exact:
        //        {
        //            folders = folders
        //                        .Where(f => f.Id == folderId)
        //                        .OrderBy(f => f.Id)
        //                        .ThenBy(f => f.Name)
        //                        .ToList();
        //            break;
        //        }
        //        default:
        //        {
        //            if (folderId == null)
        //            {
        //                folders = folders
        //                        .OrderBy(f => f.Id)
        //                        .ThenBy(f => f.Name)
        //                        .ToList();
        //            }
        //            else
        //            {
        //                folders = folders
        //                        .Where(f => f.Id == folderId)
        //                        .OrderBy(f => f.Id)
        //                        .ThenBy(f => f.Name)
        //                        .ToList();
        //            }
        //            break;
        //        }
        //    }

        //    return (folders);
        //}
        private static List<Folder> GetFolderResults(TenantUserSession tenantUserSession, List<Folder> folders, FolderResultType folderResultType, long? folderId = null)
        {
            // TODO: Implement in-memory logic.
            switch (folderResultType)
            {
                case FolderResultType.Root:
                {
                    folders = folders
                                .Where(f => f.ParentId == null)
                                .OrderBy(f => f.Id)
                                .ThenBy(f => f.Name)
                                .ToList();
                    break;
                }
                case FolderResultType.Parents:
                {
                    folders = folders
                                .Where(f => f.Id == f.ParentId)
                                .OrderBy(f => f.Id)
                                .ThenBy(f => f.Name)
                                .ToList();

                    //if (folderId == null)
                    //{
                    //    var parentIds = folders.Select(f => f.ParentId).Distinct();
                    //    folders = folders
                    //                .Where(f => parentIds.Contains(f.Id))
                    //                .OrderBy(f => f.Id)
                    //                .ThenBy(f => f.Name)
                    //                .ToList();
                    //}
                    //else
                    //{
                    //    var parentId = folders.Where(f => f.Id == folderId).FirstOrDefault();
                    //    if (parentId != null)
                    //    {
                    //        folders = folders
                    //                .Where(f => f.Id == parentId.ParentId)
                    //                .OrderBy(f => f.Id)
                    //                .ThenBy(f => f.Name)
                    //                .ToList();
                    //    }
                    //}

                    break;
                }
                //case FolderResultType.Childrens:
                //{
                //    if (!folderId.HasValue) { throw (new Exception("Unable to find the following folder")); }
                //    folders = folders
                //                .Where(f => f.ParentId == folderId)
                //                .OrderBy(f => f.Id)
                //                .ThenBy(f => f.Name)
                //                .ToList();

                    //    //if (folderId == null)
                    //    //{
                    //    //    var id = folders.Where(x => x.ParentId == null).Select(x => x.Id).FirstOrDefault();
                    //    //    folders = folders
                    //    //            .Where(f => f.ParentId == id)
                    //    //            .OrderByDescending(d => d.Id)
                    //    //            .OrderBy(f => f.Id)
                    //    //            .ThenBy(f => f.Name)
                    //    //            .ToList();
                    //    //}
                    //    //else
                    //    //{
                    //    //    folders = folders
                    //    //        .Where(f => f.ParentId == folderId)
                    //    //        .OrderByDescending(d => d.Id)
                    //    //        .OrderBy(f => f.Id)
                    //    //        .ThenBy(f => f.Name)
                    //    //        .ToList();
                    //    //}
                    //    break;
                    //}
                case FolderResultType.Exact:
                {
                    folders = folders
                                .Where(f => f.Id == folderId)
                                .OrderBy(f => f.Id)
                                .ThenBy(f => f.Name)
                                .ToList();
                    break;
                }
                default:
                {

                    folders = folders
                                .OrderBy(f => f.Id)
                                .ThenBy(f => f.Name)
                                .ToList();
                    //if (folderId == null)
                    //{
                    //    folders = folders
                    //            .OrderBy(f => f.Id)
                    //            .ThenBy(f => f.Name)
                    //            .ToList();
                    //}
                    //else
                    //{
                    //    folders = folders
                    //            .Where(f => f.Id == folderId)
                    //            .OrderBy(f => f.Id)
                    //            .ThenBy(f => f.Name)
                    //            .ToList();
                    //}
                    break;
                }
            }

            return (folders);
        }

        #region ACLImplemented

        #region Folder Management

        #region AddFolder
        /// <summary>
        /// Request to add a new folder.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="folder">Information of the folder to be created</param>
        /// <param name="newFolderCreated">Sends out the folder created with the folder id</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        public static bool AddFolder(TenantUserSession tenantUserSession, Folder folder, out Folder newFolderCreated, out Exception exception)
        {
            exception = null;
            newFolderCreated = null;
            bool result = false;
            try
            {
                if (!(result = AddFolder(tenantUserSession: tenantUserSession, context: null, folder: folder, newFolderCreated: out newFolderCreated, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForAddingFolder(tenantUserSession, newFolderCreated));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Request to add a new folder. 
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="folder">Information of the folder to be created</param>
        /// <param name="newFolderCreated">Sends out the folder created with the folder id</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed.</returns>
        internal static bool AddFolder(TenantUserSession tenantUserSession, ContextTenant context, Folder folder, out Folder newFolderCreated, out Exception exception)
        {
            exception = null;
            newFolderCreated = null;
            bool result = false;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = AddFolder(tenantUserSession: tenantUserSession, context: context, folder: folder, newFolderCreated: out newFolderCreated);
                    }
                }
                else
                {
                    result = AddFolder(tenantUserSession: tenantUserSession, context: context, folder: folder, newFolderCreated: out newFolderCreated);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Request to add a new folder. Notice! this should be called only through its internal function.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="folder">Information of the folder to be created</param>
        /// <param name="newFolderCreated">Sends out the folder created with the folder id</param>
        /// <returns>
        /// Returns true if operation was successfull else returns false if failed. 
        /// Exception is to be handled in it's calling function. 
        /// </returns>
        private static bool AddFolder(TenantUserSession tenantUserSession, ContextTenant context, Folder folder, out Folder newFolderCreated)
        {
            newFolderCreated = null;
            bool result = false;
            using (var contextTrans = context.Database.BeginTransaction())
            {
                var folderName = (folder.Name ?? string.Empty).Trim();
                folder.Name = folderName;
                if (string.IsNullOrEmpty(folderName)) { throw (new Exception("Please provide a valid folder name")); }
                if (folder.ParentId <= 1) { throw (new Exception("No folder selected")); }
                var parentFolder = GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: folder.ParentId, folderIdType: FolderIdType.Id, documentId: null, folderResultType: FolderResultType.Exact).FirstOrDefault();
                if (parentFolder != null)
                {
                    var FoldersWithSameName = context.Folders.Where(x => ((x.ParentId == folder.ParentId) && (x.Name == folderName))).ToList(); //GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: parentFolder.Id, folderIdType: FolderIdType.ParentId, documentId: null, folderResultType: FolderResultType.All);
                    if (FoldersWithSameName.Count() > 0) { throw (new Exception("A folder with same name already exists, Please enter a unique folder name")); }
                    folder.UserCreatedById = tenantUserSession.User.Id;
                    newFolderCreated = context.Folders.Add(folder);
                    context.SaveChanges();
                    var userFolder = new UserFolder();
                    userFolder.IsActive = true;
                    userFolder.UserId = tenantUserSession.User.Id;
                    userFolder.FolderId = folder.Id;
                    context.UserFolders.Add(userFolder);
                    context.SaveChanges();
                    contextTrans.Commit();
                    result = true;
                }
                else { throw (new Exception("Unable to find the following folder.")); }
            }
            return result;
        }
        #endregion AddFolder
        #region GetDocumentFolderWiseData
        /// <summary>
        /// Gets all document and folder information with respect to a folder selected.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="parentFolderId">Id of the selected folder returns its documents and sub folders. Null can be passed to get root folder information eturns its documents and sub folders.</param>
        /// <param name="parentFolder">Returns the information for the requested folder</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        public static bool GetDocumentFolderWiseData(TenantUserSession tenantUserSession, long? parentFolderId, out Folder parentFolder, out Exception exception)
        {
            exception = null;
            parentFolder = null;
            bool result = false;
            try
            {
                if (!(result = GetDocumentFolderWiseData(tenantUserSession: tenantUserSession, context: null, parentFolderId: parentFolderId, parentFolder: out parentFolder, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingFolder(tenantUserSession, parentFolder));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Gets all document and folder information with respect to a folder selected.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="parentFolderId">Id of the selected folder returns its documents and sub folders. Null can be passed to get root folder information eturns its documents and sub folders.</param>
        /// <param name="parentFolder">Sends out the information for the requested folder</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        internal static bool GetDocumentFolderWiseData(TenantUserSession tenantUserSession, ContextTenant context, long? parentFolderId,  out Folder parentFolder, out Exception exception)
        {
            exception = null;
            parentFolder = null;
            bool result = false;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString, proxyCreationEnabled: false, lazyLoadingEnabled: false))
                    {
                        result = GetDocumentFolderWiseData(tenantUserSession: tenantUserSession, context: context, parentFolderId: parentFolderId,  parentFolder: out parentFolder);
                    }
                }
                else
                {
                    result = GetDocumentFolderWiseData(tenantUserSession: tenantUserSession, context: context, parentFolderId: parentFolderId, parentFolder: out parentFolder);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Gets all document and folder information with respect to a folder selected. Notice! this should be called only through its internal function.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="parentFolderId">Id of the selected folder returns its documents and sub folders. Null can be passed to get root folder information eturns its documents and sub folders.</param>
        /// <param name="parentFolder">Returns the information for the requested folder</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>
        /// Returns true if operation was successfull else returns false if failed. 
        /// Exception is to be handled in it's calling function. 
        /// </returns>
        private static bool GetDocumentFolderWiseData(TenantUserSession tenantUserSession, ContextTenant context, long? parentFolderId,  out Folder parentFolder)
        {
            parentFolder = null;
            bool result = false;
            if (parentFolderId == null)
            {
                //parentFolder = context.Folders
                //						.Include(x => x.Documents)
                //						.Include(x => x.Documents.Select(y => y.CheckedOutByUser))
                //						.Include(x => x.Documents.Select(y => y.Folder))
                //						.Include(x => x.Documents.Select(y => y.User))
                //						.Include(x => x.Folders)
                //						.Include(x => x.Folders.Select(y => y.UserCreatedBy))
                //						.Where(x => x.ParentId == parentFolderId).SingleOrDefault();\
                parentFolder = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: parentFolderId, folderIdType: FolderIdType.ParentId, documentId: null, folderResultType: FolderResultType.All).FirstOrDefault();
                //parentFolder = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: parentFolderId, folderIdType: FolderIdType.ParentId, documentId: null, folderResultType: FolderResultType.All).FirstOrDefault();
                
                //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=ENABLE THIS IF YOU WANT DOCUMENTS TO BE SHOWN FOR ROOT FOLDER
                ////////////if (parentFolder != null)
                ////////////{
                ////////////    //Faraz Get Approval
                ////////////    var documents = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: null, documentIdType: null, folderId: parentFolder.Id, documentResultVersionType: DocumentResultVersionType.All, includeCreatorUser: true, includeCheckedOutUser: true).ToList();
                ////////////    parentFolder.Documents.Clear();
                ////////////    foreach (var document in documents) { parentFolder.Documents.Add(document); }//each document had folder included. thats weired. if this causes an exception then update it.
                ////////////                                                                                 //var childFolders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: parentFolder.Id, folderIdType: FolderIdType.ParentId, documentId: null, folderResultType: FolderResultType.All, includeCreatorUser: true).ToList();
                ////////////                                                                                 //foreach (var childFolder in childFolders) { parentFolder.Folders.Add(childFolder); }
                ////////////}
            }
            else
            {
                //parentFolder = context.Folders
                //					  .Include(x => x.Documents)
                //					  .Include(x => x.Documents.Select(y => y.CheckedOutByUser))
                //					  .Include(x => x.Documents.Select(y => y.Folder))
                //					  .Include(x => x.Documents.Select(y => y.User))
                //					  .Include(x => x.Folders)
                //					  .Include(x => x.Folders.Select(y => y.UserCreatedBy))
                //					  .Include(x => x.Folders.Select(y => y.Folders))
                //					  .Where(x => x.Id == parentFolderId).SingleOrDefault();
                parentFolder = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: parentFolderId, folderIdType: FolderIdType.Id, documentId: null, folderResultType: FolderResultType.Exact).FirstOrDefault();
                if (parentFolder != null)
                {
                    //Faraz Get Approval
                    //var documents = new List<Document>();
                    parentFolder.Documents.Clear();
                    var documents = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: null, documentIdType: null, folderId: parentFolder.Id, documentResultVersionType: DocumentResultVersionType.All, includeCreatorUser: true, includeCheckedOutUser: true,includeDocumentUsers:true).ToList();
                    parentFolder.Documents = documents.Distinct().ToList();
                    parentFolder.Folders.Clear();

                    //foreach (var document in documents) { parentFolder.Documents.Add(document); }//each document had folder included. thats weired. if this causes an exception then update it.

                    //parentFolder.GetType().GetMember("_Documents").SetValue(parentFolder, parentFolder.Documents.Distinct().ToList());
                    var childFolders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: parentFolder.Id, folderIdType: FolderIdType.ParentId, documentId: null, folderResultType: FolderResultType.All, includeCreatorUser: true).ToList();
                    foreach (var childFolder in childFolders)
                    {
                        var foldersOfChildFolder = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: childFolder.Id, folderIdType: FolderIdType.ParentId, documentId: null, folderResultType: FolderResultType.All, includeCreatorUser: true).ToList();
                        foreach (var childSubFolder in foldersOfChildFolder)
                        {
                            childFolder.Folders.Add(childSubFolder);
                        }
                        parentFolder.Folders.Add(childFolder);
                    }
                }
            }
            if (parentFolder != null)
            {
                context.Entry(parentFolder).State = EntityState.Unchanged;

                if (parentFolder.Documents.Count > 0)
                {
                    var documentsList = parentFolder.Documents.ToList();
                    parentFolder.Documents.Clear();

                    var ids = documentsList.ConvertAll(d => d.Id);
                    var names = documentsList.ConvertAll(d => d.Name);


                    documentsList = DocumentManagement.GetMaxVersionDocuments(tenantUserSession, documentsList);


                    foreach (var item in documentsList)
                    {
                        parentFolder.Documents.Add(item);
                    }
                }
            }

            result = true;
            return result;
        }
        #endregion GetDocumentFolderWiseData
        #region GetDocumentbyFolderId
        /// <summary>
        /// Gets all the documents within a folder.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="parentFolderId">Id of the selected folder returns its documents.</param>
        /// <param name="parentFolder">Sends out the information for the requested folder</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        public static bool GetDocumentbyFolderId(TenantUserSession tenantUserSession, long parentFolderId, out Folder parentFolder, out Exception exception)
        {
            exception = null;
            parentFolder = null;
            bool result = false;
            try
            {
                if (!(result = GetDocumentbyFolderId(tenantUserSession: tenantUserSession, context: null, parentFolderId: parentFolderId, parentFolder: out parentFolder, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingDocumentsUnderFolder(tenantUserSession, parentFolder));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Gets all the documents within a folder.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="parentFolderId">Id of the selected folder returns its documents.</param>
        /// <param name="parentFolder">Sends out the information for the requested folder</param>
        /// <param name="exception">Sends out exception if occured</param>
        /// <returns>Returns true if operation was successfull else returns false if failed</returns>
        internal static bool GetDocumentbyFolderId(TenantUserSession tenantUserSession, ContextTenant context, long parentFolderId, out Folder parentFolder, out Exception exception)
        {
            exception = null;
            parentFolder = null;
            bool result = false;
            try
            {
                result = GetDocumentbyFolderId(tenantUserSession: tenantUserSession, context: context, parentFolderId: parentFolderId, parentFolder: out parentFolder);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        /// <summary>
        /// Gets all the documents within a folder. Notice! this should be called only through its internal function.
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="context">Context Tenant if created. Context is created if null.</param>
        /// <param name="parentFolderId">Id of the selected folder returns its documents.</param>
        /// <param name="parentFolder">Sends out the information for the requested folder</param>
        /// <returns>
        /// Returns true if operation was successfull else returns false if failed. 
        /// Exception is to be handled in it's calling function. 
        /// </returns>
        private static bool GetDocumentbyFolderId(TenantUserSession tenantUserSession, ContextTenant context, long parentFolderId, out Folder parentFolder)
        {
            parentFolder = null;
            bool result = false;
            if (parentFolderId <= 0) { throw (new Exception("Unable to find the following folder")); }
            //parentFolder = context.Folders
            //						.Include(x => x.Documents)
            //						.Include(x => x.Documents.Select(y => y.CheckedOutByUser))
            //						.Include(x => x.Documents.Select(y => y.Folder))
            //						.Include(x => x.Documents.Select(y => y.User))
            //						.Where(x => x.Id == parentFolderId).SingleOrDefault();
            parentFolder = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: parentFolderId, folderIdType: FolderIdType.Id, documentId: null, folderResultType: FolderResultType.Exact).FirstOrDefault();
            if (parentFolder != null)
            {
                var documents = DocumentManagement.GetDocuments(tenantUserSession: tenantUserSession, context: context, documentId: null, documentIdType: null, folderId: parentFolder.Id, documentResultVersionType: DocumentResultVersionType.All, includeCreatorUser: true, includeCheckedOutUser: true).ToList();
                foreach (var document in documents) { parentFolder.Documents.Add(document); }//each document had folder included. thats weired. if this causes an exception then update it.
                if (parentFolder.Documents != null)
                {
                    if (parentFolder.Documents.Count > 0)
                    {
                        var documentsList = parentFolder.Documents.ToList();
                        parentFolder.Documents.Clear();
                        documentsList = DocumentManagement.GetMaxVersionDocuments(tenantUserSession, documentsList);
                        foreach (var item in documentsList)
                        {
                            parentFolder.Documents.Add(item);
                        }
                    }
                }
            }
            result = true;
            return result;
        }


        #endregion GetDocumentbyFolderId
        #region RenameFolderByFolderId
        public static bool RenameFolderByFolderId(TenantUserSession tenantUserSession, long folderId, string folderName, out Exception exception)
        {
            bool result = false;
            Folder folderAfterRename = null;
            string oldFolderName;
            try
            {
                if (!(result = RenameFolderByFolderId(tenantUserSession: tenantUserSession, context: null, folderId: folderId, folderName: folderName, folderAfterRename: out folderAfterRename, oldFolderName:out oldFolderName, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRenamingFolder(tenantUserSession, folderAfterRename, oldFolderName));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool RenameFolderByFolderId(TenantUserSession tenantUserSession, ContextTenant context, long folderId, string folderName, out Folder folderAfterRename, out string oldFolderName, out Exception exception)
        {
            var result = false;
            exception = null;
            folderAfterRename = null;
            oldFolderName = "";
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = RenameFolderByFolderId(tenantUserSession: tenantUserSession, context: context, folderId: folderId, folderName: folderName, folderAfterRename: out folderAfterRename, oldFolderName:out oldFolderName);
                    }
                }
                else
                {
                    result = RenameFolderByFolderId(tenantUserSession: tenantUserSession, context: context, folderId: folderId, folderName: folderName, folderAfterRename: out folderAfterRename, oldFolderName: out oldFolderName);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool RenameFolderByFolderId(TenantUserSession tenantUserSession, ContextTenant context, long folderId, string folderName,out Folder folderAfterRename,out string oldFolderName)
        {
            bool result = false;
            var folder = FolderManagement.GetFolders(tenantUserSession, context, folderId, FolderIdType.Id, null, FolderResultType.Exact).FirstOrDefault();
            oldFolderName = folder.Name;
            List<Folder> FoldersWithSameName = context.Folders.Where(x => ((x.ParentId == folder.ParentId) && (x.Name == folderName))).ToList();
            folderName = (folderName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(folderName)) { throw (new Exception("Please provide a valid folder name")); }
            if (folder != null)
            {
                if (folder.Name == folderName) { throw (new Exception("Folder name can not be same. Please enter a unique folder name")); }
                if (FoldersWithSameName.Count() > 0) { throw (new Exception("A folder with same name already exists, Please enter a unique folder name")); }
                folder.Name = folderName;
                context.SaveChanges();
                result = true;
            }
            else
            {
                throw (new Exception("Unable to find the following folder"));
            }
            folderAfterRename = folder;
            return result;
        }
        #endregion RenameFolderByFolderId
        #region GetRootFolderChildrens

        public static bool GetRootFolderChildrens(TenantUserSession tenantUserSession, out List<Folder>folders, out Exception exception)
        {
            bool result = false;
            folders = new List<Folder>();
            try
            {
                if (!(result = GetRootFolderChildrens(tenantUserSession: tenantUserSession, context: null, folders: out folders, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingFoldersUnderRootfolder(tenantUserSession,folders.FirstOrDefault()));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool GetRootFolderChildrens(TenantUserSession tenantUserSession, ContextTenant context, out List<Folder> folders, out Exception exception)
        {
            var result = false;
            exception = null;
            folders = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetRootFolderChildrens(tenantUserSession: tenantUserSession, context: context, folders: out folders);
                    }
                }
                else
                {
                    result = GetRootFolderChildrens(tenantUserSession: tenantUserSession, context: context, folders: out folders);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool GetRootFolderChildrens(TenantUserSession tenantUserSession, ContextTenant context,out List<Folder> folders)
        {
            bool result = false;
            var folder = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: null, folderIdType: null, documentId: null, folderResultType: FolderResultType.All);
            if (folder != null)
            {
                if (tenantUserSession.User.AuthenticationType == AuthenticationType.External)
                {
                    folder = folder.Where(f => (!((f.ParentId == 1) && ((f.Name == "Enterprise") || (f.Name == "Private"))))).ToList();
                }
                var rootfolder = folder.Where(x => x.ParentId == null).FirstOrDefault();
                folders = folder.Where(x => x.ParentId == rootfolder.Id).ToList();
                result = true;
            }
            else
            {
                throw (new Exception("Unable to find the following folder"));
            }
            return result;
        }
        #endregion GetRootFolderChildrens
        #region GetFolderById

        public static bool GetFolderById(TenantUserSession tenantUserSession, long folderId, out Folder folder, out Exception exception)
        {
            bool result = false;
            folder = new Folder();
            try
            {
                if (!(result = GetFolderById(tenantUserSession: tenantUserSession, context: null, folderId: folderId, folder: out folder, exception: out exception))) { if (exception != null) { throw (exception); } }
                AuditTrailManagement.Add(new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString), AuditTrailManagement.CreateForRetrievingFolder(tenantUserSession, folder));
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        internal static bool GetFolderById(TenantUserSession tenantUserSession, ContextTenant context, long folderId, out Folder folder, out Exception exception)
        {
            var result = false;
            exception = null;
            folder = null;
            try
            {
                if (context == null)
                {
                    using (context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        result = GetFolderById(tenantUserSession: tenantUserSession, context: context, folderId: folderId, folder: out folder);
                    }
                }
                else
                {
                    result = GetFolderById(tenantUserSession: tenantUserSession, context: context, folderId: folderId, folder: out folder);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        private static bool GetFolderById(TenantUserSession tenantUserSession, ContextTenant context, long folderId, out Folder folder)
        {
            bool result = false;
            folder = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, context: context, folderId: folderId, folderIdType:  FolderIdType.Id, documentId: null, folderResultType: FolderResultType.Exact,includeFolderUsers:true,includeCreatorUser:true).FirstOrDefault();
            if (folder == null)
            {
                throw (new Exception("Unable to find the following folder"));
            }
            return result;
        }
        #endregion RenameFolderByFolderId
        #endregion Folder Management

        #endregion ACLImplemented

    }
}