using HouseOfSynergy.AffinityDms.DataLayer.Contexts;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using System.Security.Cryptography;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Tenants
{
    public class DiscourseManagement
    {
        /// <summary>
        /// Gets All Discussions
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="discourses">List of available discussions and its related table data</param>
        /// <param name="exception">Exception if occured</param>
        /// <returns>Return true if successfull</returns>
        public static bool GetAllDiscourses(TenantUserSession tenantUserSession, out List<Discourse> discourses, out Exception exception)
        {
            discourses = null;
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    // context.Discussions.AsNoTracking().Include(d => d.DiscussionPosts.Select(dp => dp.DiscussionPostVersions.Select(dpv => dpv.DiscussionPostVersionAttachments.Select(dpva => dpva.Template )))).Include(x=>x.DiscussionUsers.Select(du=>du.User)).ToList();
                    //discussions = context.Discussions.Where(x => x.Id > 0).Include(x=>x.DiscussionPosts).Include(x => x.DiscussionUsers).ToList();

                    //.Include(x => x.PostVersionAttachments.OrderByDescending(xId).Take(5).OrderBy(y => y.Id))
                    //.Include(x => x.Posts.Select(dp => dp.Versions))
                    //.Include(x => x.Posts.Select(dp => dp.Versions.Select(dpv => dpv.Attachments)))
                    //.Include(x => x.Posts.Select(dp => dp.Versions.Select(dpv => dpv.Attachments.Select(dpva => dpva.Template))))
                    //.Include(x => x.Posts.Select(dp => dp.Versions.Select(dpv => dpv.Attachments.Select(dpva => dpva.Document))))
                    //.Include(x => x.Users.Select(du => du.User))\


                    /*
                    discourses = context.Discussions
                        .Include(x => x.Posts)
                        .Include(x => x.Posts.Select(dp => dp.User))
                        .Include(x => x.Users)
                        .Include(x => x.Posts.Select(dp => dp.Versions))
                        .Include(x => x.PostVersionAttachments)
                        .Include(x => x.PostVersionAttachments.Select(pva => pva.Template))
                        .Include(x => x.PostVersionAttachments.Select(pva => pva.Document))
                        .ToList();
                        */

                    if (tenantUserSession.User.AuthenticationType == AuthenticationType.External)
                    {
                        discourses = context.Discussions
                                        .Include(x => x.Posts)
                                        .Include(x => x.Posts.Select(dp => dp.User))
                                        .Include(x => x.Posts.Select(dp => dp.Versions))
                                        .Include(x => x.Posts.Select(dp => dp.Versions.Select(va => va.Attachments)))
                                        .Include(x => x.Posts.Select(dp => dp.Versions.Select(va => va.Attachments.Select(d => d.Document))))
                                        .Include(x => x.Posts.Select(dp => dp.Versions.Select(va => va.Attachments.Select(d => d.Template))))
                                        .Include(x => x.Users)
                                        .Include(x => x.Users.Select(y => y.User))
                                        .Where(x => x.Users.Any(y => (y.UserId == tenantUserSession.User.Id) && (y.IsActive)))
                                        .ToList();
                    }
                    else
                    {
                        discourses = context.Discussions
                                       .Include(x => x.Posts)
                                       .Include(x => x.Posts.Select(dp => dp.User))
                                       .Include(x => x.Posts.Select(dp => dp.Versions))
                                       .Include(x => x.Posts.Select(dp => dp.Versions.Select(va => va.Attachments)))
                                       .Include(x => x.Posts.Select(dp => dp.Versions.Select(va => va.Attachments.Select(d => d.Document))))
                                       .Include(x => x.Posts.Select(dp => dp.Versions.Select(va => va.Attachments.Select(d => d.Template))))
                                       .Include(x => x.Users)
                                       .Include(x => x.Users.Select(y => y.User))
                                       .ToList();
                    }

                    var discourseObj = discourses;
                    discourses = new List<Discourse>();
                    var attachmentsList = new List<DiscoursePostVersionAttachment>();
                    foreach (var discourse in discourseObj)
                    {
                        discourse.PostVersionAttachments.Clear();
                        var totalAttachmentsCount = 0;
                        foreach (var posts in discourse.Posts.OrderByDescending(x => x.Id).ToList())
                        {
                            if (totalAttachmentsCount < 5)
                            {
                                if (posts.Versions.Last().Attachments != null)
                                {
                                    if (posts.Versions.Last().Attachments.Count > 0)
                                    {
                                        foreach (var attachment in posts.Versions.Last().Attachments.ToList())
                                        {
                                            if (totalAttachmentsCount < 5)
                                            {
                                                discourse.PostVersionAttachments.Add(attachment);
                                                totalAttachmentsCount++;
                                            }
                                            else
                                            {

                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        discourses.Add(discourse);
                    }

                    /* foreach (var discourse in discourseObj)
                     {
                         var attachments = discourse.PostVersionAttachments.OrderByDescending(x => x.Id).Take(5).OrderBy(x => x.Id).ToList();
                         discourse.PostVersionAttachments.Clear();
                         foreach (var attachment in attachments)
                         {
                             discourse.PostVersionAttachments.Add(attachment);
                         }
                         discourses.Add(discourse);
                     }
                     */
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool AddUserWithoutRemovingDiscourseUsers(TenantUserSession tenantUserSession, long discourseId, User user, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (user != null)
                {
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        using (var contextTrans = context.Database.BeginTransaction())
                        {
                            var discourseUsers = context.DiscussionUsers
                                                        .Include(x => x.User)
                                                        .Where(x => x.DiscourseId == discourseId)
                                                        .ToList();
                            bool isUserAdded = false;
                            foreach (var discourseUser in discourseUsers)
                            {
                                if (discourseUser.UserId == user.Id)
                                {
                                    discourseUser.IsActive = true;
                                    isUserAdded = true;
                                    context.SaveChanges();
                                }
                            }
                            if (!isUserAdded)
                            {
                                var discourseUser = new DiscourseUser() { DiscourseId = discourseId, UserId = user.Id, IsActive = true };
                                context.DiscussionUsers.Add(discourseUser);
                                context.SaveChanges();
                                isUserAdded = true;
                            }
                            contextTrans.Commit();
                            result = true;
                        }
                    }
                }
                else
                {
                    throw new Exception("No Users Found");
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool AddRemoveDiscourseUsers(TenantUserSession tenantUserSession, long discourseId, List<User> users, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (users.Count > 0)
                {
                    using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                    {
                        using (var contextTrans = context.Database.BeginTransaction())
                        {
                            var discourseUsers = context.DiscussionUsers
                                                        .Include(x => x.User)
                                                        .Where(x => x.DiscourseId == discourseId)
                                                        .ToList();
                            if (discourseUsers.Count > 0)
                            {
                                foreach (var discourseuser in discourseUsers)
                                {
                                    discourseuser.IsActive = false;
                                }
                            }
                            foreach (var user in users)
                            {
                                bool isUserAdded = false;
                                foreach (var discourseUser in discourseUsers)
                                {
                                    if (discourseUser.UserId == user.Id)
                                    {
                                        discourseUser.IsActive = true;
                                        isUserAdded = true;
                                        context.SaveChanges();
                                    }
                                }
                                if (!isUserAdded)
                                {
                                    var discourseUser = new DiscourseUser() { DiscourseId = discourseId, UserId = user.Id, IsActive = true };
                                    context.DiscussionUsers.Add(discourseUser);
                                    context.SaveChanges();
                                    isUserAdded = true;
                                }
                            }
                            contextTrans.Commit();
                            result = true;
                        }
                    }
                }
                else
                {
                    throw new Exception("No Users Found");
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool RemoveAllDiscourseUsers(TenantUserSession tenantUserSession, long discourseId, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        var discourseUsers = context.DiscussionUsers
                                                    .Include(x => x.User)
                                                    .Where(x => x.DiscourseId == discourseId)
                                                    .ToList();
                        if (discourseUsers.Count > 0)
                        {
                            foreach (var discourseuser in discourseUsers)
                            {
                                discourseuser.IsActive = false;

                                context.Entry(discourseuser).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        contextTrans.Commit();
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool GetDiscourseUsersByDiscourseId(TenantUserSession tenantUserSession, long discourseId, out List<DiscourseUser> discourseUsers, out Exception exception)
        {
            exception = null;
            bool result = false;
            discourseUsers = new List<DiscourseUser>();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    if (discourseId > 0)
                    {
                        discourseUsers = context.DiscussionUsers
                                                .Include(x => x.User)
                                                 .Include(x => x.Discourse)
                                                 .Include(x => x.Discourse.Posts)
                                                .Where(x => (x.DiscourseId == discourseId) && (x.IsActive == true))
                                                .ToList();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool GetDiscourseById(TenantUserSession tenantUserSession, long id, out Discourse discourse, out Exception exception)
        {
            bool result = false;
            exception = null;
            discourse = null;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    discourse = context.Discussions
                                        .Include(x => x.Posts)
                                        .Include(x => x.Posts.Select(dp => dp.User))
                                        .Include(x => x.Users)
                                        .Include(x => x.Users.Select(du => du.User))
                                        .Include(x => x.Posts.Select(dp => dp.Versions))
                                        .Include(x => x.Posts.Select(dp => dp.Versions.Select(dpv => dpv.Attachments)))
                                        .Include(x => x.Posts.Select(dp => dp.Versions.Select(dpv => dpv.Attachments.Select(dpva => dpva.Template))))
                                        .Include(x => x.Posts.Select(dp => dp.Versions.Select(dpv => dpv.Attachments.Select(dpva => dpva.Document))))
                                        .Include(x => x.PostVersionAttachments)
                                        //.Include(x => x.PostVersionAttachments.Select(pva => pva.Template))
                                        //.Include(x => x.PostVersionAttachments.Select(pva => pva.Document))
                                        .Where(x => x.Id == id).SingleOrDefault();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool AddPost(TenantUserSession tenantUserSession, string title, string comment, List<long> documentIds, List<long> templateIds, List<string> external, List<string> live, out Discourse discourse, out List<FileUploadStatus> fileEntries, out Exception exception)
        {
            exception = null;
            bool result = false;
            discourse = new Discourse();
            FileInfo fileinfo = null;
            fileEntries = new List<FileUploadStatus>();
            try
            {
                if (documentIds == null) { documentIds = new List<long>(); }
                if (templateIds == null) { templateIds = new List<long>(); }
                if (external == null) { external = new List<string>(); }
                if (live == null) { live = new List<string>(); }

                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {

                        discourse.Topic = title;
                        discourse.Description = comment;
                        discourse = context.Discussions.Add(discourse);
                        context.SaveChanges();
                        var datetime = DateTime.UtcNow;
                        var discoursePost = new DiscoursePost();
                        discoursePost.UserId = tenantUserSession.User.Id;
                        discoursePost.DiscourseId = discourse.Id;
                        discoursePost = context.DiscussionPosts.Add(discoursePost);
                        context.SaveChanges();
                        var discoursePostVersion = new DiscoursePostVersion();
                        discoursePostVersion.Comments = comment;
                        discoursePostVersion.DateTime = datetime;
                        discoursePostVersion.PostId = discoursePost.Id;
                        discoursePostVersion.DiscourseId = discourse.Id;
                        discoursePostVersion = context.DiscussionPostVersions.Add(discoursePostVersion);
                        context.SaveChanges();

                        if (documentIds.Count > 0)
                        {
                            foreach (var documentId in documentIds)
                            {
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discourse.Id;
                                discoursePostVersionAttachment.DocumentId = documentId;
                                discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Document;
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();
                            }
                        }

                        if (templateIds.Count > 0)
                        {
                            foreach (var templateId in templateIds)
                            {
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discourse.Id;
                                discoursePostVersionAttachment.TemplateId = templateId;
                                var template = context.Templates.Where(x => x.Id == templateId).SingleOrDefault();
                                if (template == null) { throw (new Exception("Unable to Find The Template")); }
                                if (template.TemplateType == Entities.Lookup.TemplateType.Template)
                                {
                                    discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Template;
                                }
                                else if (template.TemplateType == Entities.Lookup.TemplateType.Form)
                                {
                                    discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Form;
                                }
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();
                            }
                        }
                        #region External Documents
                        
                        if (external.Count > 0)
                        {
                            fileEntries = external.ToList().ConvertAll<FileUploadStatus>(f => new FileUploadStatus() { File = f, FileName = null, StatusMessage = null, Finalized = false, });
                            foreach (var externalfile in fileEntries)
                            {
                                fileinfo = new FileInfo(externalfile.File.ToString());
                                var fileNameSpited = Path.GetFileName(fileinfo.FullName).Split(new string[1] { "._." }, StringSplitOptions.None)[0];
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discourse.Id;
                                discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.External;
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                discoursePostVersionAttachment.FileNameClient = fileNameSpited;
                                discoursePostVersionAttachment.Url = fileNameSpited;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();

                                var id = discoursePostVersionAttachment.Id;
                                discoursePostVersionAttachment.FileNameServer = id.ToString() + "." + GlobalConstants.FileExtensionCloud;
                                context.SaveChanges();

                                try
                                {
                                    externalfile.FileName = fileNameSpited;
                                    using (var algorithm = HashAlgorithm.Create(GlobalConstants.AlgorithmHashName))
                                    {
                                        using (var stream = fileinfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                                        {
                                            Document document = null;
                                            var hash = Convert.ToBase64String(algorithm.ComputeHash(stream));

                                            if (DocumentManagement.GetDocumentByHash(tenantUserSession, hash, out document, out exception))
                                            {
                                                if (document == null)
                                                {
                                                    var documentIdStr = fileinfo.Name.Substring(fileinfo.Name.IndexOf("._.") + 3);
                                                    long documentId = 0;
                                                    if (!(long.TryParse(documentIdStr, out documentId))) { throw (new Exception("Invalid Document.")); }
                                                    result = DocumentManagement.GetDocumentById(tenantUserSession, documentId, out document, out exception);
                                                    if (exception != null) { throw (exception); }
                                                    document = DocumentManagement.UpdateDocumentEntry(tenantUserSession, documentId, null, hash, stream.Length);
                                                    using (var azureHelper = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant))
                                                    {
                                                        using (var cancellationTokenSource = new CancellationTokenSource())
                                                        {
                                                            if (azureHelper.DiscourseDocumentUpload(tenantUserSession, discoursePostVersionAttachment, stream, cancellationTokenSource.Token, out exception))
                                                            {
                                                                externalfile.Finalized = true;
                                                                externalfile.StatusMessage = "Document uploaded and finalized successfully";
                                                                fileinfo = null;
                                                            }
                                                            else
                                                            {
                                                                // Upload to azure failed. Notify user.
                                                                throw new Exception("Document failed to upload");
                                                            }
                                                            if (azureHelper.DocumentUpload(tenantUserSession, document, stream, cancellationTokenSource.Token, out exception))
                                                            {
                                                                if (DocumentManagement.DocumentEntryFinalize(tenantUserSession, document.Id, out document, out exception))
                                                                {
                                                                    // User can no longer cancel this upload.
                                                                    externalfile.Finalized = true;
                                                                    externalfile.StatusMessage = "Document uploaded and finalized successfully";
                                                                    document = null;
                                                                    fileinfo = null;
                                                                }
                                                                else
                                                                {
                                                                    // Notify user of failed upload/finalization.
                                                                    throw new Exception("Document failed to finalized");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // Upload to azure failed. Notify user.
                                                                throw new Exception("Document failed to upload");
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception($"Document {document.Name} already Exists");

                                                }
                                            }

                                        }
                                    }
                                }
                                catch (Exception ee)
                                {
                                    externalfile.Finalized = false;
                                    externalfile.StatusMessage = ee.Message;
                                    context.Entry(discoursePostVersionAttachment).State = EntityState.Deleted;
                                    context.SaveChanges();
                                    throw ee;
                                }

                            }
                        }

                        #endregion
                        if (live.Count > 0)
                        {
                            foreach (var url in live)
                            {
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discourse.Id;
                                discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Live;
                                discoursePostVersionAttachment.Url = url;
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();
                            }
                        }
                        contextTrans.Commit();

                        result = true;

                    }
                }
                //if (discoursePost.Id > 0)
                //{
                //    GetDiscoursePostById(tenantUserSession, discoursePost.Id, out discoursePost, out exception);
                //    if (exception != null) { throw exception; }
                //}
                //else
                //{
                //    throw (new Exception("Unable to find the following post"));
                //}
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool AddPostVersion(TenantUserSession tenantUserSession, long postId, string comment, List<long> documentIds, List<long> templateIds, List<string> external, List<long> existingExternal, List<string> live, out DiscoursePost discoursePost, out List<FileUploadStatus> fileEntries, out Exception exception)
        {
            exception = null;
            bool result = false;
            discoursePost = new DiscoursePost();
            DiscoursePost discoursePostNew = new DiscoursePost();
            FileInfo fileinfo = null;
            fileEntries = new List<FileUploadStatus>();
            try
            {
                if (postId <= 0) { throw (new Exception("Unable to find the Following Post")); }
                if (documentIds == null) { documentIds = new List<long>(); }
                if (templateIds == null) { templateIds = new List<long>(); }
                if (external == null) { external = new List<string>(); }
                if (existingExternal == null) { existingExternal = new List<long>(); }
                if (live == null) { live = new List<string>(); }

                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        var datetime = DateTime.UtcNow;
                        discoursePostNew = context.DiscussionPosts.Where(x => x.Id == postId)
                                                                    .Include(dpv => dpv.Versions)
                                                                    .Include(dpv => dpv.Versions.Select(dpva => dpva.Attachments))
                                                                    .SingleOrDefault();
                        List<DiscoursePostVersionAttachment> existingattachments = new List<DiscoursePostVersionAttachment>();
                        if (discoursePostNew != null)
                        {
                            if (discoursePostNew.Versions.Last() != null)
                            {
                                if (discoursePostNew.Versions.Last().Attachments != null)
                                {
                                    existingattachments = discoursePostNew.Versions.Last().Attachments.ToList();
                                }
                            }
                        }
                        if (discoursePostNew == null) { throw (new Exception("Unable to find the following post")); }
                        var discoursePostVersion = new DiscoursePostVersion();
                        discoursePostVersion.Comments = comment;
                        discoursePostVersion.DateTime = datetime;
                        discoursePostVersion.PostId = discoursePostNew.Id;
                        discoursePostVersion.DiscourseId = discoursePostNew.DiscourseId;
                        discoursePostVersion = context.DiscussionPostVersions.Add(discoursePostVersion);
                        context.SaveChanges();

                        if (documentIds.Count > 0)
                        {
                            foreach (var documentId in documentIds)
                            {
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discoursePostNew.DiscourseId;
                                discoursePostVersionAttachment.DocumentId = documentId;
                                discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Document;
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();
                            }
                        }
                        if (templateIds.Count > 0)
                        {
                            foreach (var templateId in templateIds)
                            {
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discoursePostNew.DiscourseId;
                                discoursePostVersionAttachment.TemplateId = templateId;
                                var template = context.Templates.Where(x => x.Id == templateId).SingleOrDefault();
                                if (template == null) { throw (new Exception("Unable to Find The Template")); }
                                if (template.TemplateType == Entities.Lookup.TemplateType.Template)
                                {
                                    discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Template;
                                }
                                else if (template.TemplateType == Entities.Lookup.TemplateType.Form)
                                {
                                    discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Form;
                                }
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();
                            }
                        }

                        #region External Documents                                
                        //fileinfo = new FileInfo(externalfile.File.ToString());
                        //var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                        //discoursePostVersionAttachment.DiscourseId = discoursePostNew.DiscourseId;
                        //discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.External;
                        //discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                        //discoursePostVersionAttachment.FileNameClient = Path.GetFileName(fileinfo.FullName);
                        //discoursePostVersionAttachment.Url = fileinfo.Name;
                        //context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                        //context.SaveChanges();

                        //var id = discoursePostVersionAttachment.Id;
                        //discoursePostVersionAttachment.FileNameServer = id.ToString() + "." + GlobalConstants.FileExtensionCloud;
                        //context.SaveChanges();
                        if (external.Count > 0)
                        {
                            fileEntries = external.ToList().ConvertAll<FileUploadStatus>(f => new FileUploadStatus() { File = f, FileName = null, StatusMessage = null, Finalized = false, });
                            foreach (var externalfile in fileEntries)
                            {
                                fileinfo = new FileInfo(externalfile.File.ToString());
                                var fileNameSpited = Path.GetFileName(fileinfo.FullName).Split(new string[1] { "._." }, StringSplitOptions.None)[0];
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discoursePostNew.DiscourseId;
                                discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.External;
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                discoursePostVersionAttachment.FileNameClient = fileNameSpited;
                                discoursePostVersionAttachment.Url = fileNameSpited;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();

                                var id = discoursePostVersionAttachment.Id;
                                discoursePostVersionAttachment.FileNameServer = id.ToString() + "." + GlobalConstants.FileExtensionCloud;
                                context.SaveChanges();

                                try
                                {
                                    externalfile.FileName = fileNameSpited;
                                    using (var algorithm = HashAlgorithm.Create(GlobalConstants.AlgorithmHashName))
                                    {
                                        using (var stream = fileinfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                                        {
                                            Document document = null;
                                            var hash = Convert.ToBase64String(algorithm.ComputeHash(stream));

                                            if (DocumentManagement.GetDocumentByHash(tenantUserSession, hash, out document, out exception))
                                            {
                                                if (document == null)
                                                {
                                                    var documentIdStr = fileinfo.Name.Substring(fileinfo.Name.IndexOf("._.") + 3);
                                                    long documentId = 0;
                                                    if (!(long.TryParse(documentIdStr, out documentId))) { throw (new Exception("Invalid Document.")); }
                                                    result = DocumentManagement.GetDocumentById(tenantUserSession, documentId, out document, out exception);
                                                    if (exception != null) { throw (exception); }
                                                    document = DocumentManagement.UpdateDocumentEntry(tenantUserSession, documentId, null, hash, stream.Length);
                                                    using (var azureHelper = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant))
                                                    {
                                                        using (var cancellationTokenSource = new CancellationTokenSource())
                                                        {
                                                            if (azureHelper.DiscourseDocumentUpload(tenantUserSession, discoursePostVersionAttachment, stream, cancellationTokenSource.Token, out exception))
                                                            {
                                                                externalfile.Finalized = true;
                                                                externalfile.StatusMessage = "Document uploaded and finalized successfully";
                                                                fileinfo = null;
                                                            }
                                                            else
                                                            {
                                                                // Upload to azure failed. Notify user.
                                                                throw new Exception("Document failed to upload");
                                                            }
                                                            if (azureHelper.DocumentUpload(tenantUserSession, document, stream, cancellationTokenSource.Token, out exception))
                                                            {
                                                                if (DocumentManagement.DocumentEntryFinalize(tenantUserSession, document.Id, out document, out exception))
                                                                {
                                                                    // User can no longer cancel this upload.
                                                                    externalfile.Finalized = true;
                                                                    externalfile.StatusMessage = "Document uploaded and finalized successfully";
                                                                    document = null;
                                                                    fileinfo = null;
                                                                }
                                                                else
                                                                {
                                                                    // Notify user of failed upload/finalization.
                                                                    throw new Exception("Document failed to finalized");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // Upload to azure failed. Notify user.
                                                                throw new Exception("Document failed to upload");
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception($"Document {document.Name} already Exists");

                                                }
                                            }

                                        }
                                    }
                                }
                                catch (Exception ee)
                                {
                                    externalfile.Finalized = false;
                                    externalfile.StatusMessage = ee.Message;
                                    context.Entry(discoursePostVersionAttachment).State = EntityState.Deleted;
                                    context.SaveChanges();
                                    throw ee;
                                }

                            }
                        }
                        if (existingExternal.Count > 0)
                        {

                            foreach (var existingExternalId in existingExternal)
                            {
                                foreach (var attachment in existingattachments)
                                {
                                    if (attachment.AttachmentType == Entities.Lookup.DiscussionPostAttachmentType.External)
                                    {
                                        if (attachment.Id == existingExternalId)
                                        {


                                            var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                            discoursePostVersionAttachment.DiscourseId = discoursePostNew.DiscourseId;
                                            discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.External;
                                            discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                            discoursePostVersionAttachment.FileNameClient = attachment.FileNameClient;
                                            discoursePostVersionAttachment.Url = attachment.FileNameClient;
                                            context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                            context.SaveChanges();

                                            var id = discoursePostVersionAttachment.Id;
                                            discoursePostVersionAttachment.FileNameServer = id.ToString() + "." + GlobalConstants.FileExtensionCloud;
                                            context.SaveChanges();



                                            using (var azureHelper = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant))
                                            {
                                                using (var cancellationTokenSource = new CancellationTokenSource())
                                                {
                                                    if (azureHelper.DiscourseDocumentCopy(tenantUserSession, id, attachment.Id, cancellationTokenSource.Token, out exception))
                                                    {
                                                        fileinfo = null;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        #endregion

                        if (live.Count > 0)
                        {
                            foreach (var url in live)
                            {
                                if (url.Count() > 0)
                                {
                                    var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                    discoursePostVersionAttachment.DiscourseId = discoursePostNew.DiscourseId;
                                    discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Live;
                                    discoursePostVersionAttachment.Url = url;
                                    discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                    context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                    context.SaveChanges();
                                }

                            }
                        }
                        contextTrans.Commit();
                        result = true;
                    }
                }
                if (discoursePostNew.Id > 0)
                {
                    GetDiscoursePostById(tenantUserSession, discoursePostNew.Id, out discoursePost, out exception);
                    if (exception != null) { throw exception; }
                }
                else
                {
                    throw (new Exception("Unable to find the following post"));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }

        public static bool GetDiscoursePostVersionByPostId(TenantUserSession tenantUserSession, long id, out List<DiscoursePostVersion> discoursePostVersions, out Exception exception)
        {
            bool result = false;
            exception = null;
            discoursePostVersions = new List<DiscoursePostVersion>();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    discoursePostVersions = context.DiscussionPostVersions
                                                    .Where(x => x.PostId == id)
                                                    .Include(x => x.Attachments)
                                                    .Include(x => x.Attachments.Select(y => y.Template))
                                                    .Include(x => x.Attachments.Select(y => y.Document))
                                                    .ToList();
                    foreach (var discoursePostVersion in discoursePostVersions)
                    {
                        foreach (var attachment in discoursePostVersion.Attachments.ToList())
                        {
                            if (attachment.Template != null)
                            {
                                attachment.Template.TemplateImage = null;
                            }
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }


        public static bool GetDiscoursePostsByLastPostId(TenantUserSession tenantUserSession, long id, out List<DiscoursePost> discoursePosts, out Exception exception)
        {
            bool result = false;
            exception = null;
            discoursePosts = new List<DiscoursePost>();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    discoursePosts = context.DiscussionPosts
                                                    .Include(x => x.User)
                                                    .Include(x => x.Versions)
                                                    .Include(x => x.Versions.Select(y => y.Attachments))
                                                    .Include(x => x.Versions.Select(y => y.Attachments.Select(z => z.Template)))
                                                    .Include(x => x.Versions.Select(y => y.Attachments.Select(z => z.Document)))
                                                    .Where(x => x.Id > id && x.UserId != tenantUserSession.User.Id)
                                                    .ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }


        public static bool GetDiscourseMaxPostVersionByPostId(TenantUserSession tenantUserSession, long id, out DiscoursePostVersion discoursePostVersion, out Exception exception)
        {
            bool result = false;
            exception = null;
            discoursePostVersion = new DiscoursePostVersion();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    discoursePostVersion = context.DiscussionPostVersions
                                                    .Include(x => x.Post)
                                                    .Include(x => x.Post.Versions)
                                                    .Include(x => x.Post.Discourse)
                                                    .Include(x => x.Post.User)
                                                    .Include(x => x.Attachments)
                                                    .Include(x => x.Attachments.Select(y => y.Template))
                                                    .Include(x => x.Attachments.Select(y => y.Document))
                                                    .Where(x => x.PostId == id)
                                                    .FirstOrDefault();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool GetDiscoursePostVersionById(TenantUserSession tenantUserSession, long id, out DiscoursePostVersion discoursePostVersion, out Exception exception)
        {
            bool result = false;
            exception = null;
            discoursePostVersion = new DiscoursePostVersion();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    discoursePostVersion = context.DiscussionPostVersions
                                                    .Include(x => x.Post)
                                                    .Include(x => x.Post.User)
                                                    .Include(x => x.Post.Discourse)
                                                    .Include(x => x.Post.Versions)
                                                    .Include(x => x.Attachments)
                                                    .Include(x => x.Attachments.Select(y => y.Document))
                                                    .Include(x => x.Attachments.Select(y => y.Template))
                                                    .Include(x => x.Discourse)
                                                    .Where(x => x.Id == id).FirstOrDefault();
                    if (discoursePostVersion == null) { throw (new Exception("unable to find the following post")); }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }







        public static bool GetDiscourseMaxPostVersionAttachmentByDiscourseId(TenantUserSession tenantUserSession, long discourseId, out List<DiscoursePostVersionAttachment> attachments, out Exception exception)
        {
            bool result = false;
            exception = null;
            attachments = new List<DiscoursePostVersionAttachment>();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    var discoursePosts = context.DiscussionPosts
                                         .Include(x => x.Versions)
                                         .Include(x => x.Versions.Select(y => y.Attachments))
                                         .Include(x => x.Versions.Select(y => y.Attachments.Select(z => z.Document)))
                                         .Include(x => x.Versions.Select(y => y.Attachments.Select(z => z.Template)))
                                         .Where(x => x.DiscourseId == discourseId)
                                         .OrderBy(x => x.Id)
                                         .ToList();
                    foreach (var posts in discoursePosts)
                    {
                        var attachmentsList = posts.Versions.Last().Attachments;
                        foreach (var attachment in attachmentsList)
                        {
                            attachments.Add(attachment);
                        }
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }





        public static bool GetDiscoursePostVersionAttachmentById(TenantUserSession tenantUserSession, long id, out DiscoursePostVersionAttachment discoursePostVersionAttachment, out Exception exception)
        {
            bool result = false;
            exception = null;
            discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    discoursePostVersionAttachment = context.DiscussionPostAttachements
                                                            .Include(x => x.PostVersion)
                                                            .Include(x => x.PostVersion.Post)
                                                            .Include(x => x.PostVersion.Post.User)
                                                            .Where(x => x.Id == id).SingleOrDefault();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool AddPostComment(TenantUserSession tenantUserSession, long discourseId, string comment, List<long> documentIds, List<long> templateIds, List<string> external, List<string> live, out DiscoursePost discoursePost, out List<FileUploadStatus> fileEntries, out Exception exception)
        {
            exception = null;
            bool result = false;
            discoursePost = new DiscoursePost();
            DiscoursePost discoursePostNew = new DiscoursePost();
            FileInfo fileinfo = null;
            fileEntries = new List<FileUploadStatus>();
            try
            {
                if (discourseId <= 0) { throw (new Exception("Unable to find the Following Discourse")); }
                if (documentIds == null) { documentIds = new List<long>(); }
                if (templateIds == null) { templateIds = new List<long>(); }
                if (external == null) { external = new List<string>(); }

                if (live == null) { live = new List<string>(); }

                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    using (var contextTrans = context.Database.BeginTransaction())
                    {
                        var discourse = context.Discussions.Include(x => x.Posts).Include(x => x.Users).Where(x => x.Id == discourseId).FirstOrDefault();
                        if (discourse == null) { throw (new Exception("Unable to find the following chat.")); }
                        if (discourse.Posts.First().UserId != tenantUserSession.User.Id)
                        {
                            if (!(discourse.Users.Any(x => (x.UserId == tenantUserSession.User.Id) && (x.IsActive)))) { throw (new Exception("You do not have permission to comment on the following chat.")); }
                        }
                        var datetime = DateTime.UtcNow;
                        discoursePost.UserId = tenantUserSession.User.Id;
                        discoursePost.DiscourseId = discourseId;
                        discoursePostNew = context.DiscussionPosts.Add(discoursePost);
                        context.SaveChanges();
                        var discoursePostVersion = new DiscoursePostVersion();
                        discoursePostVersion.Comments = comment;
                        discoursePostVersion.DateTime = datetime;
                        discoursePostVersion.PostId = discoursePostNew.Id;
                        discoursePostVersion.DiscourseId = discourseId;
                        discoursePostVersion = context.DiscussionPostVersions.Add(discoursePostVersion);
                        context.SaveChanges();

                        if (documentIds.Count > 0)
                        {
                            foreach (var documentId in documentIds)
                            {
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discourseId;
                                discoursePostVersionAttachment.DocumentId = documentId;
                                discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Document;
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();
                            }
                        }

                        if (templateIds.Count > 0)
                        {
                            foreach (var templateId in templateIds)
                            {
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discourseId;
                                discoursePostVersionAttachment.TemplateId = templateId;
                                var template = context.Templates.Where(x => x.Id == templateId).SingleOrDefault();
                                if (template == null) { throw (new Exception("Unable to Find The Template")); }
                                if (template.TemplateType == Entities.Lookup.TemplateType.Template)
                                {
                                    discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Template;
                                }
                                else if (template.TemplateType == Entities.Lookup.TemplateType.Form)
                                {
                                    discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Form;
                                }
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();
                            }
                        }
                        #region External Documents
                        if (external.Count > 0)
                        {
                            fileEntries = external.ToList().ConvertAll<FileUploadStatus>(f => new FileUploadStatus() { File = f, FileName = null, StatusMessage = null, Finalized = false, });
                            foreach (var externalfile in fileEntries)
                            {
                                fileinfo = new FileInfo(externalfile.File.ToString());
                                var fileNameSpited = Path.GetFileName(fileinfo.FullName).Split(new string[1] { "._." }, StringSplitOptions.None)[0];
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discourseId;
                                discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.External;
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                discoursePostVersionAttachment.FileNameClient = fileNameSpited;
                                discoursePostVersionAttachment.Url = fileNameSpited;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();

                                var id = discoursePostVersionAttachment.Id;
                                discoursePostVersionAttachment.FileNameServer = id.ToString() + "." + GlobalConstants.FileExtensionCloud;
                                context.SaveChanges();

                                try
                                {
                                    externalfile.FileName = fileNameSpited;
                                    using (var algorithm = HashAlgorithm.Create(GlobalConstants.AlgorithmHashName))
                                    {
                                        using (var stream = fileinfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                                        {
                                            Document document = null;
                                            var hash = Convert.ToBase64String(algorithm.ComputeHash(stream));

                                            if (DocumentManagement.GetDocumentByHash(tenantUserSession, hash, out document, out exception))
                                            {
                                                if (document == null)
                                                {
                                                    var documentIdStr = fileinfo.Name.Substring(fileinfo.Name.IndexOf("._.") + 3);
                                                    long documentId = 0;
                                                    if (!(long.TryParse(documentIdStr, out documentId))) { throw (new Exception("Invalid Document.")); }
                                                    result = DocumentManagement.GetDocumentById(tenantUserSession, documentId, out document, out exception);
                                                    if (exception != null) { throw (exception); }
                                                    document = DocumentManagement.UpdateDocumentEntry(tenantUserSession, documentId, null, hash, stream.Length);
                                                    using (var azureHelper = new AzureCloudStorageAccountHelper(tenantUserSession.Tenant))
                                                    {
                                                        using (var cancellationTokenSource = new CancellationTokenSource())
                                                        {
                                                            if (azureHelper.DiscourseDocumentUpload(tenantUserSession, discoursePostVersionAttachment, stream, cancellationTokenSource.Token, out exception))
                                                            {
                                                                externalfile.Finalized = true;
                                                                externalfile.StatusMessage = "Document uploaded and finalized successfully";
                                                                fileinfo = null;
                                                            }
                                                            else
                                                            {
                                                                // Upload to azure failed. Notify user.
                                                                throw new Exception("Document failed to upload");
                                                            }
                                                            if (azureHelper.DocumentUpload(tenantUserSession, document, stream, cancellationTokenSource.Token, out exception))
                                                            {
                                                                if (DocumentManagement.DocumentEntryFinalize(tenantUserSession, document.Id, out document, out exception))
                                                                {
                                                                    // User can no longer cancel this upload.
                                                                    externalfile.Finalized = true;
                                                                    externalfile.StatusMessage = "Document uploaded and finalized successfully";
                                                                    document = null;
                                                                    fileinfo = null;
                                                                }
                                                                else
                                                                {
                                                                    // Notify user of failed upload/finalization.
                                                                    throw new Exception("Document failed to finalized");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // Upload to azure failed. Notify user.
                                                                throw new Exception("Document failed to upload");
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception($"Document {document.Name} already Exists");

                                                }
                                            }
                                            
                                        }
                                    }
                                }
                                catch (Exception ee)
                                {
                                    externalfile.Finalized = false;
                                    externalfile.StatusMessage = ee.Message;
                                    context.Entry(discoursePostVersionAttachment).State = EntityState.Deleted;
                                    context.SaveChanges();
                                    throw ee;
                                }

                            }
                        }

                        #endregion

                        if (live.Count > 0)
                        {
                            foreach (var url in live)
                            {
                                var discoursePostVersionAttachment = new DiscoursePostVersionAttachment();
                                discoursePostVersionAttachment.DiscourseId = discourseId;
                                discoursePostVersionAttachment.AttachmentType = Entities.Lookup.DiscussionPostAttachmentType.Live;
                                discoursePostVersionAttachment.Url = url;
                                discoursePostVersionAttachment.PostVersionId = discoursePostVersion.Id;
                                context.DiscussionPostAttachements.Add(discoursePostVersionAttachment);
                                context.SaveChanges();
                            }
                        }
                        contextTrans.Commit();

                        result = true;

                    }
                }
                if (discoursePostNew.Id > 0)
                {
                    GetDiscoursePostById(tenantUserSession, discoursePostNew.Id, out discoursePost, out exception);
                    if (exception != null) { throw exception; }
                }
                else
                {
                    throw (new Exception("Unable to find the following post"));
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool GetDiscoursePostById(TenantUserSession tenantUserSession, long discoursePostId, out DiscoursePost discoursePost, out Exception exception)
        {
            bool result = false;
            exception = null;
            discoursePost = new DiscoursePost();
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    discoursePost = context.DiscussionPosts
                                           .Include(x => x.Discourse)
                                           .Include(x => x.User)
                                           .Include(x => x.Versions)
                                           .Include(x => x.Versions.Select(y => y.Attachments))
                                           .Include(x => x.Versions.Select(y => y.Attachments.Select(z => z.Document)))
                                           .Include(x => x.Versions.Select(y => y.Attachments.Select(z => z.Template)))
                                           .Where(x => x.Id == discoursePostId).SingleOrDefault();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        //public static bool AddPost(long discourseId, long id, out long discoursePost, out Exception exception)
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// Gets All Discussions
        /// </summary>
        /// <param name="tenantUserSession">Current Tenants User Session</param>
        /// <param name="discourseid">Discussion Id</param>
        /// <param name="discourses">List of available discussion posts by discussion id. Includes users and discussion post versions</param>
        /// <param name="exception">Exception if occured</param>
        /// <returns>Return true if successfull</returns>
        public static bool GetDiscoursePostsByDiscourseId(TenantUserSession tenantUserSession, long discourseid, out List<DiscoursePost> discourses, out Exception exception)
        {
            discourses = null;
            exception = null;
            bool result = false;
            try
            {
                using (var context = new ContextTenant(tenantUserSession.Tenant.DatabaseConnectionString))
                {
                    discourses = context.DiscussionPosts
                                        .Include(x => x.User)
                                        .Include(x => x.Versions)
                                        .Where(x => x.Id > discourseid)
                                        .ToList();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            return result;
        }

    }
}
