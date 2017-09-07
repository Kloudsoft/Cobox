using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Tenants;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
	public class UserViewModel
	{
		public long Id { get; set; }
		public string NameGiven { get; set; }
		public string NameFamily { get; set; }
		public string Email { get; set; }
        public string Domain { get; set; }
        public string Key { get; set; }
        //Used for User Rights CheckBoxes etc
        public bool HasRights { get; set; }
        public string DepartmentName { get; set; }
        [Required]
		[MinLength(EntityConstants.LengthUserNameMinimum)]
		[StringLength(EntityConstants.LengthUserNameMaximum)]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[MinLength(EntityConstants.LengthPasswordMinimum)]
		[StringLength(EntityConstants.LengthPasswordMaximum)]
		public string Password { get; set; }

		public AuthenticationType AuthenticationType { get; set; }
        public Guid? InviteGuid { get; set; }

        public string SignInTokenValue { get; set; }
		public DateTime SignInTokenDateTimeCreated { get; set; }
		public DateTime SignInTokenDateTimeExpires { get; set; }

		public bool RoleCustom { get; set; }
		public bool RoleAdministrator { get; set; }
		public bool RoleScanner { get; set; }
		public bool RoleUploader { get; set; }
		public bool RoleIndexer { get; set; }
		public bool RoleTemplateCreator { get; set; }
		public bool RoleFormCreator { get; set; }
		public bool RoleWorkflowActor { get; set; }
		public bool RoleWorkflowCreator { get; set; }
		public bool RoleReporting { get; set; }

        public bool RoleSiteManager { get; set; }
        public bool RoleSitePO { get; set; }
        public bool RolePoftfolioManager { get; set; }

        public bool RoleSSOAD { get; set; }
        public bool RoleHQAP { get; set; }

        public bool IsActive { get; set; }
        public List<Role> Roles { get; set; }

		public UserViewModel ()
		{
			this.Roles = new List<Role>();
		}


        public User ToEntity()
        {
            User user = new User();
            user.Id = this.Id;
            user.NameGiven = this.NameGiven;
            user.NameFamily = this.NameFamily;
            user.Email = this.Email;
            user.UserName = this.UserName;
            user.PasswordHash = this.Password;
            user.AuthenticationType = this.AuthenticationType;
            user.IsActive = this.IsActive;

            if (this.RoleAdministrator) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.Administrator), UserId = user.Id }); }
            if (this.RoleCustom) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.Custom), UserId = user.Id }); }
            if (this.RoleScanner) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.Scanner), UserId = user.Id }); }
            if (this.RoleUploader) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.Uploader), UserId = user.Id }); }
            if (this.RoleIndexer) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.Indexer), UserId = user.Id }); }
            if (this.RoleTemplateCreator) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.TemplateCreator), UserId = user.Id }); }
            if (this.RoleFormCreator) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.FormCreator), UserId = user.Id }); }
            if (this.RoleWorkflowActor) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.WorkflowActor), UserId = user.Id }); }
            if (this.RoleWorkflowCreator) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.WorkflowCreator), UserId = user.Id }); }
            if (this.RoleReporting) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.Reporting), UserId = user.Id }); }

            if (this.RoleSiteManager) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.SiteManager), UserId = user.Id }); }
            if (this.RoleSitePO) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.SitePO), UserId = user.Id }); }
            if (this.RolePoftfolioManager) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.PoftfolioManager), UserId = user.Id }); }

            if (this.RoleSSOAD) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.SSOAD), UserId = user.Id }); }
            if (this.RoleHQAP) { user.UserRoles.Add(new UserRole() { RoleId = ((int)TenantRoleType.HQAP), UserId = user.Id }); }

            return (user);
        }
        public User ToEntity(UserViewModel userViewModel)
        {
            User user = new User();
            user.Id = userViewModel.Id;
            user.NameGiven = userViewModel.NameGiven;
            user.NameFamily = userViewModel.NameFamily;
            user.Email = userViewModel.Email;
            user.UserName = userViewModel.UserName;
            user.PasswordHash = userViewModel.Password;
            user.AuthenticationType = userViewModel.AuthenticationType;
            user.IsActive = userViewModel.IsActive;
            if (userViewModel.RoleAdministrator) { user.Roles.Add(new Role() { RoleType = TenantRoleType.Administrator, }); }
            if (userViewModel.RoleCustom) { user.Roles.Add(new Role() { RoleType = TenantRoleType.Custom, }); }
            if (userViewModel.RoleScanner) { user.Roles.Add(new Role() { RoleType = TenantRoleType.Scanner, }); }
            if (userViewModel.RoleUploader) { user.Roles.Add(new Role() { RoleType = TenantRoleType.Uploader, }); }
            if (userViewModel.RoleIndexer) { user.Roles.Add(new Role() { RoleType = TenantRoleType.Indexer, }); }
            if (userViewModel.RoleTemplateCreator) { user.Roles.Add(new Role() { RoleType = TenantRoleType.TemplateCreator, }); }
            if (userViewModel.RoleFormCreator) { user.Roles.Add(new Role() { RoleType = TenantRoleType.FormCreator, }); }
            if (userViewModel.RoleWorkflowActor) { user.Roles.Add(new Role() { RoleType = TenantRoleType.WorkflowActor, }); }
            if (userViewModel.RoleWorkflowCreator) { user.Roles.Add(new Role() { RoleType = TenantRoleType.WorkflowCreator, }); }
            if (userViewModel.RoleReporting) { user.Roles.Add(new Role() { RoleType = TenantRoleType.Reporting, }); }

            if (userViewModel.RoleSiteManager) { user.Roles.Add(new Role() { RoleType = TenantRoleType.SiteManager, }); }
            if (userViewModel.RoleSitePO) { user.Roles.Add(new Role() { RoleType = TenantRoleType.SitePO, }); }
            if (userViewModel.RolePoftfolioManager) { user.Roles.Add(new Role() { RoleType = TenantRoleType.PoftfolioManager, }); }

            if (userViewModel.RoleSSOAD) { user.Roles.Add(new Role() { RoleType = TenantRoleType.SSOAD, }); }
            if (userViewModel.RoleHQAP) { user.Roles.Add(new Role() { RoleType = TenantRoleType.HQAP, }); }
            

            return (user);
        }

        public UserViewModel FromEntity (User user)
		{
			this.Id = user.Id;
			this.NameGiven = user.NameGiven;
			this.NameFamily = user.NameFamily;
			this.Email = user.Email;
			this.UserName = user.UserName;
			this.Password = user.PasswordHash;
			this.AuthenticationType = user.AuthenticationType;
			this.DepartmentName = user.Department?.Name;
            this.IsActive = user.IsActive;
            this.InviteGuid = user.InviteGuid;
			if (user.UserRoles == null)
			{
				throw (new Exception("Dev Bug: [user.UserRoles] is null."));
			}
			else
			{
				if (user.UserRoles.Any(ur => ur.Role == null))
				{
					throw (new Exception("Dev Bug: The calling query has not included the [User.UserRoles.Role] property."));
				}
				else
				{
					this.RoleAdministrator = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Administrator);
					this.RoleCustom = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Custom);
					this.RoleScanner = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Scanner);
					this.RoleUploader = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Uploader);
					this.RoleIndexer = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Indexer);
					this.RoleTemplateCreator = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.TemplateCreator);
					this.RoleFormCreator = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.FormCreator);
					this.RoleWorkflowActor = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.WorkflowActor);
					this.RoleWorkflowCreator = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.WorkflowCreator);
					this.RoleReporting = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Reporting);

                    this.RoleSiteManager = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.SiteManager);
                    this.RoleSitePO = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.SitePO);
                    this.RolePoftfolioManager = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.PoftfolioManager);

                    this.RoleSSOAD = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.SSOAD);
                    this.RoleHQAP = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.HQAP);
                    
                }
			}

			return (this);
		}

        public UserViewModel UserRights (User user, string type, long id)
		{
			this.Id = user.Id;
			this.NameGiven = user.NameGiven;
			this.NameFamily = user.NameFamily;
			this.Email = user.Email;
			this.UserName = user.UserName;
			this.Password = user.PasswordHash;
			this.AuthenticationType = user.AuthenticationType;
			this.DepartmentName = user.Department?.Name;

			if (user.UserRoles == null)
			{
				throw (new Exception("Dev Bug: [user.UserRoles] is null."));
			}
			else
			{
				if (user.UserRoles.Any(ur => ur.Role == null))
				{
					throw (new Exception("Dev Bug: The calling query has not included the [User.UserRoles.Role] property."));
				}
				else
				{
					this.RoleAdministrator = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Administrator);
					this.RoleCustom = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Custom);
					this.RoleScanner = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Scanner);
					this.RoleUploader = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Uploader);
					this.RoleIndexer = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Indexer);
					this.RoleTemplateCreator = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.TemplateCreator);
					this.RoleFormCreator = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.FormCreator);
					this.RoleWorkflowActor = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.WorkflowActor);
					this.RoleWorkflowCreator = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.WorkflowCreator);
					this.RoleReporting = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.Reporting);

                    this.RoleSiteManager = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.SiteManager);
                    this.RoleSitePO = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.SitePO);
                    this.RolePoftfolioManager = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.PoftfolioManager);

                    this.RoleSSOAD = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.SSOAD);
                    this.RoleHQAP = user.UserRoles.Any(role => role.Role.RoleType == TenantRoleType.HQAP);
                }
			}

			switch (type.ToLower())
			{
				case "folder":
				{
					if (user.UserFolders.Count > 0)
					{
						if (user.UserFolders.Where(x => ((x.FolderId == id) && (x.IsActive == true))).ToList().Count > 0)
						{
							this.HasRights = true;
						}
					}
					break;
				}
				case "document":
				{
					if (user.UserDocuments.Count > 0)
					{
						if (user.UserDocuments.Where(x => (x.DocumentId == id) && (x.IsActive == true)).ToList().Count > 0)
						{
							this.HasRights = true;
						}
					}
					break;
				}
				case "form":
				case "template":
				{
					if (user.UserTemplates.Count > 0)
					{
						if (user.UserTemplates.Where(x => (x.TemplateId == id) && (x.IsActive == true)).ToList().Count > 0)
						{
							this.HasRights = true;
						}
					}
					break;
				}
				default:
				{
					break;
				}
			}

			return (this);
		}
    }
}