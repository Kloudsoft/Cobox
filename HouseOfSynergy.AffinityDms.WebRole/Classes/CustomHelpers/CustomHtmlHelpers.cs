using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.CustomHelpers
{
	public class CustomHtmlHelpers
	{

		public static string GetAttachmentsString(DiscoursePostVersionAttachment attachment, long? attachmentsMaxWidth = 48)
		{
			string s = "";
			var templateImageSrc = "../../Images/FileIcons/Template.png";
			var formImageSrc = "../../Images/FileIcons/Form.png";
			var documentImageSrc = "../../Images/FileIcons/Documents.png";
			var externalFilesImageSrc = "../../Images/FileIcons/Attachment.png";
			var urlImageSrc = "../../Images/FileIcons/Link.png";

			if (attachment != null)
			{
				if (attachment.AttachmentType == DiscussionPostAttachmentType.Template)
				{
					if (attachment.Template != null)
					{
						s += "<a onclick='javascript: window.open(\"/TenantTemplateRenderView/MaxRenderView/" + attachment.TemplateId + "\")' data-id='" + attachment.TemplateId + "' data-type='" + Convert.ToInt16(DiscussionPostAttachmentType.Template) + "' data-name='" + attachment.Template.Title + "'><img src='" + templateImageSrc + "' title='" + attachment.Template.Title + "' style='max-width:" + attachmentsMaxWidth + "px'></a>";
					}
				}
				else if (attachment.AttachmentType == DiscussionPostAttachmentType.Form)
				{
					if (attachment.Template != null)
					{
						s += "<a href='javascript: window.open(\"/TenantFormProtectedView/MaxProtectedView/" + attachment.TemplateId + "\")' data-id='" + attachment.TemplateId + "' data-type='" + Convert.ToInt16(DiscussionPostAttachmentType.Form) + "' data-name='" + attachment.Template.Title + "'><img src='" + formImageSrc + "' title='" + attachment.Template.Title + "' style='max-width:" + attachmentsMaxWidth + "px'></a>";
					}
				}
				else if (attachment.AttachmentType == DiscussionPostAttachmentType.Document)
				{
					if (attachment.Document != null)
					{
						s += "<a href='javascript: window.open(\"/TenantDocumentIndex/DocumentIndexMaxReadOnlyView/" + attachment.DocumentId + "\")' data-id='" + attachment.DocumentId + "' data-type='" + Convert.ToInt16(DiscussionPostAttachmentType.Document) + "' data-name='" + attachment.Document.Name + "'><img src='" + documentImageSrc + "' title='" + attachment.Document.Name + "' style='max-width:" + attachmentsMaxWidth + "px'></a>";
					}
				}
				else if (attachment.AttachmentType == DiscussionPostAttachmentType.External)
				{
					if (!(string.IsNullOrWhiteSpace(attachment.Url) || string.IsNullOrEmpty(attachment.Url)))
					{
						s += "<a href='javascript: window.open(\"/TenantDocumentViewer/Index/" + attachment.Id + "?type=External\")' data-id='" + attachment.Id + "' data-type='" + Convert.ToInt16(DiscussionPostAttachmentType.External) + "' data-name='" + attachment.Url + "' data-url='" + attachment.Url + "'><img src='" + externalFilesImageSrc + "' title='" + attachment.FileNameClient + "' style='max-width:" + attachmentsMaxWidth + "px'></a>";
					}
				}
				else if (attachment.AttachmentType == DiscussionPostAttachmentType.Live)
				{
					if (!(string.IsNullOrWhiteSpace(attachment.Url) || string.IsNullOrEmpty(attachment.Url)))
					{
						string javascripturl = "javascript: window.open(/^(http|https|ftp):/.test(\"" + attachment.Url + "\") ? \"" + attachment.Url + "\" : \"http://" + attachment.Url + "\")";
						s += "<a href='" + javascripturl + "'  data-id='" + attachment.Id + "' data-type='" + Convert.ToInt16(DiscussionPostAttachmentType.Live) + "' data-name='" + attachment.Url + "' data-url='" + attachment.Url + "'><img src='" + urlImageSrc + "' title='" + attachment.Url + "' style='max-width:" + attachmentsMaxWidth + "px'></a>";
					}
				}
			}

			return (s);//(MvcHtmlString.Create(s));
		}
		public static string GetMonthName(int month)
		{
			var strMonth = "January";
			if (month > 0)
			{
				switch (month)
				{
					case 1:
					strMonth = "January";
					break;
					case 2:
					strMonth = "February";
					break;
					case 3:
					strMonth = "March";
					break;
					case 4:
					strMonth = "April";
					break;
					case 5:
					strMonth = "May";
					break;
					case 6:
					strMonth = "June";
					break;
					case 7:
					strMonth = "July";
					break;
					case 8:
					strMonth = "August";
					break;
					case 9:
					strMonth = "September";
					break;
					case 10:
					strMonth = "October";
					break;
					case 11:
					strMonth = "November";
					break;
					case 12:
					strMonth = "December";
					break;
					default:
					strMonth = "January";
					break;
				}

			}
			return strMonth;
		}
		public static string GetMonthNameShort(int month)
		{
			var strMonth = GetMonthName(month);
			if (strMonth.Length > 4)
			{
				strMonth = strMonth.Substring(0, 3);
			}
			return strMonth;
		}
		public static string GetDiscourseUsersCommaSeperated(Discourse discourse)
		{
			string usersStr = "";
			if (discourse.Users != null)
			{
				if (discourse.Users.Count > 0)
				{
					usersStr = String.Join(", ", discourse.Users.Select(u => u.User.NameFull.ToString()).ToArray());
				}
				else
				{
					usersStr = "No user are invited";
				}
			}
			else
			{
				usersStr = "No user are invited";
			}
			return usersStr;
		}
        public static string GetDocumentUsersCommaSeperated(Document document)
        {
            string usersStr = "";
            if (document.DocumentUsers != null)
            {
                if (document.DocumentUsers.Count > 0)
                {
                    usersStr = String.Join(", ", document.DocumentUsers.Select(u => ((string.IsNullOrEmpty(u.User.NameFull.Trim()))?(u.User.Email.ToString()):(u.User.NameFull.ToString()))).ToArray());
                }
                else
                {
                    usersStr = "No user are invited";
                }
            }
            else
            {
                usersStr = "No user are invited";
            }
            return usersStr;
        }
        public static string GetFolderBreadcrumb(List<Folder> folders,string folderPageUrl, long? currentFolderId,string firstNodeName, string firstNodeUrl, string lastNodeName = "", string lastNodeUrl = "", string lastNodeIdentity = "", string lastNodeIdentityUrl = "", bool isSharedFolder = false,bool isPrivateFolder = false)
        {
            var breadcrumb = $"<li><a href='{firstNodeUrl}'>{firstNodeName}</a></li>";
            if (currentFolderId.HasValue)
            {
                if (currentFolderId.Value > 0)
                {
                    var currentFolder = folders.Where(x => x.Id == currentFolderId).FirstOrDefault();
                    List<Folder> BreadcrumbsFollowedFolders = new List<Folder>();
                    if (currentFolder == null)
                    {
                        isSharedFolder = true;
                    }
                    else
                    {
                        var parentFolderId = currentFolder.ParentId;
                        if (parentFolderId == 1)
                        {
                            BreadcrumbsFollowedFolders.Add(currentFolder);
                        }
                        else
                        {
                            parentFolderId = currentFolder.Id;
                            while (parentFolderId != null && parentFolderId != 1)
                            {
                                var folder = folders.Where(x => x.Id == parentFolderId).FirstOrDefault();
                                BreadcrumbsFollowedFolders.Add(folder);
                                parentFolderId = folder.ParentId;
                            }
                        }
                    }
                    if (isSharedFolder)
                    {
                        var sharedFolder = folders.Where(x => ((x.Name == "Shared") && (x.ParentId == 1))).FirstOrDefault();
                        breadcrumb += $"<li><a href='{folderPageUrl}?parentFolderId={sharedFolder.Id}'>{sharedFolder.Name}</a></li>";
                    }
                    else if (isSharedFolder)
                    {
                        var privateFolder = folders.Where(x => ((x.Name == "Private") && (x.ParentId == 1))).FirstOrDefault();
                        breadcrumb += $"<li><a href='{folderPageUrl}?parentFolderId={privateFolder.Id}'>{privateFolder.Name}</a></li>";
                    }
                    else
                    {
                        BreadcrumbsFollowedFolders.Reverse();
                        Folder parentFolder = null;
                        foreach (var folder in BreadcrumbsFollowedFolders)
                        {
                            if (folder.Id == BreadcrumbsFollowedFolders.First().Id)
                            {
                                parentFolder = folder;
                                breadcrumb += $"<li><a href='{folderPageUrl}?parentFolderId={parentFolder.Id}'>{parentFolder.Name}</a></li>";
                            }
                            else
                            {
                                breadcrumb += $"<li><a href='{folderPageUrl}?parentFolderId={parentFolder.Id}&subfolderId={folder.Id}'>{folder.Name}</a></li>";

                            }
                        }
                    }
                }
            }
           
            if (!((string.IsNullOrEmpty(lastNodeName?.Trim())) && string.IsNullOrEmpty(lastNodeUrl?.Trim())))
            {
                breadcrumb += $"<li><a href='{lastNodeUrl}'>{lastNodeName}</a></li>";
            }
            if (!((string.IsNullOrEmpty(lastNodeIdentity?.Trim())) && string.IsNullOrEmpty(lastNodeIdentityUrl?.Trim())))
            {
                breadcrumb += $"<li><a href='{lastNodeIdentityUrl}'>{lastNodeIdentity}</a></li>";
            }
            return breadcrumb;
        }

    }
}