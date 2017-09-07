using HouseOfSynergy.AffinityDms.BusinessLayer.Lookup;
using HouseOfSynergy.AffinityDms.BusinessLayer.Tenants;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Tenants
{
    public class GetFolderTreeViewData
    {


        public static List<FolderTreeViewModel> GetFolderTreeviewHieraricalList(TenantUserSession tenantUserSession, long? folderId, FolderIdType? folderIdType, FolderResultType folderResultType,out Exception exception, bool skipSharedFolder = false, bool skipPrivateFolder = false)
        {
            exception = null;
            List<FolderTreeViewModel> FolderTreeVMList;
            List<Folder> folders = FolderManagement.GetFolders(tenantUserSession: tenantUserSession, folderId: null, folderIdType: null, documentId: null, folderResultType: folderResultType).ToList();
            if (skipSharedFolder)
            {
                folders = folders.Where(f => (!((f.ParentId == 1) && (f.Name == "Shared")))).ToList();
            }
            if (skipPrivateFolder)
            {
                folders = folders.Where(f => (!((f.ParentId == 1) && (f.Name == "Private")))).ToList();
            }

            FolderTreeVMList = folders.Select(x => new FolderTreeViewModel()
            {
                Id = x.Id,
                HasChildren = x.HasChildren,
                Name = x.Name,
                UserCreatedById = x.UserCreatedById,
                ParentId = x.ParentId,// ((x.ParentId != null) ? (x.ParentId) : (0)),
                ParentIdStr = ((x.ParentId != null) ? (x.ParentId.ToString()) : ("0"))
            }).ToList();
            //List<FolderTreeViewModel> treeviewHieraricalList = null;
            //FolderTreeViewModel.ConvertListToHeirarchy(FolderTreeVMList, folderId, out treeviewHieraricalList, out exception);
            //if (treeviewHieraricalList.Count <= 0)
            //{
            //	treeviewHieraricalList = FolderTreeVMList;
            //}
            return FolderTreeVMList;
        }








        //public static List<FolderTreeViewModel> GetFolderList(TenantUserSession tenantUserSession, long? id,out List<Folder> folders, out Exception exception)
        //{
        //    Folder parentFolder = null;
        //    List<FolderTreeViewModel> FolderTreeVMList;
        //    if (!DocumentManagement.GetDocumentFolderWiseData(tenantUserSession, id, out parentFolder, out folders, out exception)) { if (exception != null) { throw exception; } }
        //    if (parentFolder != null)
        //    {
        //        var ftvm = new FolderTreeViewModel()
        //        {
        //            Id = parentFolder.Id,
        //            HasChildren = (parentFolder.Folders.Count > 0) ? (true) : (false),
        //            Name = parentFolder.Name,
        //            ParentId = parentFolder.ParentId
        //        };
        //        FolderTreeVMList = new List<FolderTreeViewModel>();
        //        FolderTreeVMList.Add(ftvm);
        //    }
        //    else
        //    {
        //        FolderTreeVMList = folders.Select(x => new FolderTreeViewModel()
        //        {
        //            Id = x.Id,
        //            HasChildren = x.HasChildren,
        //            Name = x.Name,
        //            ParentId = x.ParentId
        //        }).ToList();
        //    }
        //    return FolderTreeVMList;
        //}
        public static List<FolderTreeViewModel> GetFolderList(TenantUserSession tenantUserSession, long? id,  out Folder folders, out Exception exception)
        {
            Folder parentFolder = null;
            List<FolderTreeViewModel> FolderTreeVMList;
            if (!FolderManagement.GetDocumentFolderWiseData(tenantUserSession, id, out parentFolder, out exception)) { if (exception != null) { throw exception; } }
            if (parentFolder != null)
            {
                if (parentFolder.ParentId == null && id == null)
                {
                    var ftvm = new FolderTreeViewModel()
                    {
                        Id = parentFolder.Id,
                        HasChildren = parentFolder.HasChildren,
                        Name = parentFolder.Name,
                        ParentId = parentFolder.ParentId
                    };
                    FolderTreeVMList = new List<FolderTreeViewModel>();
                    FolderTreeVMList.Add(ftvm);

                }
                else
                {
                    FolderTreeVMList = parentFolder.Folders.Select(x => new FolderTreeViewModel()
                    {
                        Id = x.Id,
                        HasChildren = x.HasChildren,
                        Name = x.Name,
                        ParentId = x.ParentId
                    }).ToList();
                }
                folders = parentFolder;
            }
            else
            {
                FolderTreeVMList = new List<FolderTreeViewModel>();
                folders = new Folder();
            }
            return FolderTreeVMList;
        }
        public static Folder CleanParentFolder(Folder folder)
        {
            Folder parent = new Folder();
            if (folder != null)
            {
                parent = new Folder()
                {
                    Id = folder.Id,
                    Name = folder.Name
                };
            }
            return parent;
        }
        public static List<Folder> CleanListofFolders(List<Folder> folderlist)
        {

            if (folderlist == null) { folderlist = new List<Folder>(); }
            List<Folder> folders = new List<Folder>();
            foreach (var childfolder in folderlist)
            {
                User user = new Entities.Tenants.User()
                {
                    Id = childfolder.UserCreatedBy.Id,
                    NameFamily = childfolder.UserCreatedBy.NameFamily,
                    NameGiven = childfolder.UserCreatedBy.NameGiven,
                };
                Folder folder = new Folder()
                {
                    Id = childfolder.Id,
                    Name = childfolder.Name,
                    DateTimeCreated = childfolder.DateTimeCreated,
                    DateTimeModified = childfolder.DateTimeModified,
                    UserCreatedBy = user,
                };
                folders.Add(folder);
            }
            return folders;
        }
        public static List<Document> CleanDocumentList(List<Document> documentlist, TenantUserSession tenantUserSession)
        {
            List<Document> Documents = new List<Document>();
            if (documentlist == null) { documentlist = new List<Document>(); }
            foreach (var parentFolderDocument in documentlist)
            {

                Template template = null;
                if (parentFolderDocument.Template != null)
                {
                    template = new Template()
                    {
                        Id = parentFolderDocument.Template.Id,
                        Title = parentFolderDocument.Template.Title,
                    };
                }
                Folder folder = null;
                if (parentFolderDocument.Folder != null)
                {
                    folder = new Folder()
                    {
                        Id = parentFolderDocument.Folder.Id,
                        Name = parentFolderDocument.Folder.Name
                    };
                }
                User documentCheckedOutUser = new User()
                {
                    Id = parentFolderDocument.CheckedOutByUser.Id,
                    NameFamily = parentFolderDocument.CheckedOutByUser.NameFamily,
                    NameGiven = parentFolderDocument.CheckedOutByUser.NameGiven

                };


                User documentOwner = new User()
                {
                    Id = parentFolderDocument.User.Id,
                    NameFamily = parentFolderDocument.User.NameFamily,
                    NameGiven = parentFolderDocument.User.NameGiven

                };
                
                Document document = new Document()
                {
                    Id = parentFolderDocument.Id,
                    Name = parentFolderDocument.Name,
                    TemplateId = parentFolderDocument.TemplateId,
                    Template = template,
                    FolderId = parentFolderDocument.FolderId,
                    Folder = folder,
                    DateTime = parentFolderDocument.DateTime,
                    DocumentType = parentFolderDocument.DocumentType,
                    VersionMajor = parentFolderDocument.VersionMajor,
                    VersionMinor = parentFolderDocument.VersionMinor,
                    VersionCount = parentFolderDocument.VersionCount,
                    IsFinalized = parentFolderDocument.IsFinalized,
                    IsCancelled = parentFolderDocument.IsCancelled,
                    IsDigital = parentFolderDocument.IsDigital,
                    IsInTransit = parentFolderDocument.IsInTransit,
                    IsPrivate = parentFolderDocument.IsPrivate,
                    LatestCheckedOutByUserId = parentFolderDocument.LatestCheckedOutByUserId,
                    CheckedOutByUser = documentCheckedOutUser,
                    CheckedOutByUserId = documentCheckedOutUser.Id,
                    CheckedOutDateTime = parentFolderDocument.CheckedOutDateTime,
                    User = documentOwner,
                    UserId = documentOwner.Id,
                    Confidence = parentFolderDocument.Confidence,
                    AssignedByUserId = parentFolderDocument.AssignedByUserId,
                    AssignedToUserId = parentFolderDocument.AssignedToUserId,
                    AssignmentState = parentFolderDocument.AssignmentState,
                    AssignedDate = parentFolderDocument.AssignedDate,
                    State = parentFolderDocument.State,
                };
                foreach (var userDoc in parentFolderDocument.DocumentUsers)
                {
                    if (userDoc.User != null)
                    {
                        User user = new User()
                        {
                            Id = parentFolderDocument.User.Id,
                            NameFamily = parentFolderDocument.User.NameFamily,
                            NameGiven = parentFolderDocument.User.NameGiven
                        };
                        userDoc.User = user;
                        document.DocumentUsers.Add(userDoc);
                    }
                }
                Documents.Add(document);
            }
            return Documents;
        }
        public static DocumentFolderTreeViewModel CleanDocumentTreeViewModalData(DocumentFolderTreeViewModel documentFolderTreeViewModel, TenantUserSession tenantUserSession)
        {
            var parentfolder = documentFolderTreeViewModel.Parentfolder;
            documentFolderTreeViewModel.Parentfolder = null;
            documentFolderTreeViewModel.Parentfolder = CleanParentFolder(parentfolder);

            var ChildFolders = documentFolderTreeViewModel.ChildFolders;
            documentFolderTreeViewModel.ChildFolders = new List<Folder>();
            documentFolderTreeViewModel.ChildFolders = CleanListofFolders(ChildFolders);

            var ParentFolderDocuments = documentFolderTreeViewModel.ParentFolderDocuments;
            documentFolderTreeViewModel.ParentFolderDocuments = new List<Document>();
            documentFolderTreeViewModel.ParentFolderDocuments = CleanDocumentList(ParentFolderDocuments, tenantUserSession);
            return documentFolderTreeViewModel;
        }
    }
}