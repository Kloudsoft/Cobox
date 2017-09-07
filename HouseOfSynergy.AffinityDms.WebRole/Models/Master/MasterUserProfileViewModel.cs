using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Master
{
	public class MasterUserProfileViewModel
	{
		public long Id { get; set; } // Read-only, no display.

		[Required]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthNameGiven)]
        [Display(Name="First Name")]
		public string NameGiven { get; set; } // First name.

		[Required]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthNameFamily)]
        [Display(Name = "Last Name")]
		public string NameFamily { get; set; } // Last name.

        [Required]
        [DataType(DataType.EmailAddress)]
        [MinLength(3)]
        [StringLength(EntityConstants.LengthEmail)]
        [Display(Name = "Email")]
        public string Email { get; set; } // TODO: Implement an activation-based email change.

        [DataType(DataType.Text)]
        [Display(Name = "Active Directory Id")]
        public string ActiveDirectoryId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[MinLength(EntityConstants.LengthUserNameMinimum)]
		[StringLength(EntityConstants.LengthUserNameMaximum)]
        [Display(Name = "UserName")]
		public string UserName { get; set; } // Read-only display.

		[Required]
		[DataType(DataType.Password)]
		[MinLength(EntityConstants.LengthPasswordMinimum)]
		[StringLength(EntityConstants.LengthPasswordMaximum)]
        [Display(Name = "Password")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[MinLength(EntityConstants.LengthPasswordMinimum)]
		[StringLength(EntityConstants.LengthPasswordMaximum)]
        [Compare("Password",ErrorMessage="Password not matched")]
        [Display(Name = "Confirm password")]
		public string PasswordConfirmation { get; set; }

		[Required(AllowEmptyStrings = true)]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthAddress)]
        [Display(Name = "Address 1")]
		public string Address1 { get; set; }

		[Required(AllowEmptyStrings = true)]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthAddress)]
        [Display(Name = "Address 2")]
		public string Address2 { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthCity)]
        [Display(Name = "City")]
		public string City { get; set; }

		[Required]
		[DataType(DataType.PostalCode)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthZipOrPostCode)]
        [Display(Name = " ZipCode/PostCode")]
		public string ZipOrPostCode { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthCountry)]
        [Display(Name = "Country")]
		public string Country { get; set; }

		[Required(AllowEmptyStrings = true)]
		[DataType(DataType.PhoneNumber)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthCountry)]
        [Display(Name = "Phone(Work)")]
		public string PhoneWork { get; set; }

		[Required(AllowEmptyStrings = true)]
		[DataType(DataType.PhoneNumber)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthCountry)]
        [Display(Name = "Phone(Mobile)")]
		public string PhoneMobile { get; set; }
        [Display(Name = "Authentication Type")]
		public AuthenticationType AuthenticationType { get; set; } // Read-only display.

        //public string Domain { get; set; } // Read-only display.
        //public string CompanyName { get; set; } // Read-only display.
        [Display(Name = "Full Name")]
		public string NameFull { get { return (this.NameGiven + " " + this.NameFamily); } } // Read-only, no display.

        public string ActiveDirectory_AuthenticationEmail { get; set; }
        public string ActiveDirectory_AuthenticationPhone { get; set; }
        public string ActiveDirectory_AuthenticationPhoneAlternate { get; set; }
        public string ActiveDirectory_Department { get; set; }
        public string ActiveDirectory_JobTitle { get; set; }
        [Display(Name = "AD Manager Id")]
        public Guid ActiveDirectory_ManagerId { get; set; }
        public string ActiveDirectory_NameDisplay { get; set; }
        [Display(Name = "AD Object Id")]
        public Guid ActiveDirectory_ObjectId { get; set; }
        public string ActiveDirectory_RoleDisplayName { get; set; }
        public string ActiveDirectory_UsageLocation { get; set; }
    }
}