using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.Entities.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Master
{
    public class MasterUserViewModel
    {
		public long Id { get; set; }
		public string NameGiven { get; set; }
		public string NameFamily { get; set; }
		public string Email { get; set; }

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

		public string SignInTokenValue { get; set; }
		public DateTime SignInTokenDateTimeCreated { get; set; }
		public DateTime SignInTokenDateTimeExpires { get; set; }

		public bool RoleCustom { get; set; }
		public bool RoleAdministrator { get; set; }
		//public bool RoleNone { get; set; }
		public bool RoleReporting { get; set; }

		public List<MasterRole> Roles { get; set; }

        public string ActiveDirectory_AuthenticationEmail { get; set; }
        public string ActiveDirectory_AuthenticationPhone { get; set; }
        public string ActiveDirectory_AuthenticationPhoneAlternate { get; set; }
        public string ActiveDirectory_Department { get; set; }
        public string ActiveDirectory_JobTitle { get; set; }
        public Guid ActiveDirectory_ManagerId { get; set; }
        public string ActiveDirectory_NameDisplay { get; set; }
        public Guid ActiveDirectory_ObjectId { get; set; }
        public string ActiveDirectory_RoleDisplayName { get; set; }
        public string ActiveDirectory_UsageLocation { get; set; }


        public MasterUserViewModel ()
		{
			this.Roles = new List<MasterRole>();
		}

		public MasterUser ToEntity (MasterUser user)
		{
            user.Id = this.Id;
			user.NameGiven = this.NameGiven;
			user.NameFamily = this.NameFamily;
			user.Email = this.Email;
			user.UserName = this.UserName;
			user.PasswordHash = this.Password;
			user.AuthenticationType = this.AuthenticationType;

			if (this.RoleAdministrator) { user.Roles.Add(new MasterRole() { RoleType = MasterRoleType.Administrator, }); }
			if (this.RoleCustom) { user.Roles.Add(new MasterRole() { RoleType = MasterRoleType.Custom, }); }
			//if (this.RoleNone) { user.Roles.Add(new MasterRole() { RoleType = MasterRoleType.None, }); }
			if (this.RoleReporting) { user.Roles.Add(new MasterRole() { RoleType = MasterRoleType.Reporting, }); }
			

			return (user);
		}

		public MasterUser ToEntity ()
		{
			return (this.ToEntity(new MasterUser()));
		}

		public MasterUserViewModel FromEntity (MasterUser user)
		{
            this.Id = user.Id;
			this.NameGiven = user.NameGiven;
			this.NameFamily = user.NameFamily;
			this.Email = user.Email;
			this.UserName = user.UserName;
			this.Password = user.PasswordHash;
			this.AuthenticationType = user.AuthenticationType;

            this.RoleAdministrator = user.UserRoles.Any(role => role.Role.RoleType == MasterRoleType.Administrator);//Roles.Any(role => role.RoleType == );
            this.RoleCustom = user.UserRoles.Any(role => role.Role.RoleType == MasterRoleType.Custom);//user.Roles.Any(role => role.RoleType == MasterRoleType.Custom);
            //this.RoleNone = user.UserRoles.Any(role => role.Role.RoleType == MasterRoleType.None); //user.Roles.Any(role => role.RoleType == MasterRoleType.None);
            this.RoleReporting = user.UserRoles.Any(role => role.Role.RoleType == MasterRoleType.Reporting); //user.Roles.Any(role => role.RoleType == MasterRoleType.Reporting);
			return (this);
		}
    }
}