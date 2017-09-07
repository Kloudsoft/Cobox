using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
	public class UserProfileViewModel
	{
		public long Id { get; set; } // Read-only, no display.

		[Required]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthNameGiven)]
		public string NameGiven { get; set; } // First name.

		[Required]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthNameFamily)]
		public string NameFamily { get; set; } // Last name.

		[Required]
		[DataType(DataType.EmailAddress)]
		[MinLength(3)]
		[StringLength(EntityConstants.LengthEmail)]
		public string Email { get; set; } // TODO: Implement an activation-based email change.

		[Required]
		[DataType(DataType.Text)]
		[MinLength(EntityConstants.LengthUserNameMinimum)]
		[StringLength(EntityConstants.LengthUserNameMaximum)]
		public string UserName { get; set; } // Read-only display.

        [DataType(DataType.Password)]
		[MinLength(EntityConstants.LengthPasswordMinimum)]
		[StringLength(EntityConstants.LengthPasswordMaximum)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[MinLength(EntityConstants.LengthPasswordMinimum)]
		[StringLength(EntityConstants.LengthPasswordMaximum)]
		public string PasswordConfirmation { get; set; }

		[Required(AllowEmptyStrings = true)]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthAddress)]
		public string Address1 { get; set; }

		[Required(AllowEmptyStrings = true)]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthAddress)]
		public string Address2 { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthCity)]
		public string City { get; set; }

		[Required]
		[DataType(DataType.PostalCode)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthZipOrPostCode)]
		public string ZipOrPostCode { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthCountry)]
		public string Country { get; set; }

		[Required(AllowEmptyStrings = true)]
		[DataType(DataType.PhoneNumber)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthCountry)]
		public string PhoneWork { get; set; }

		[Required(AllowEmptyStrings = true)]
		[DataType(DataType.PhoneNumber)]
		[MinLength(1)]
		[StringLength(EntityConstants.LengthCountry)]
		public string PhoneMobile { get; set; }

		//public AuthenticationType AuthenticationType { get; set; } // Read-only display.

		//public string Domain { get; set; } // Read-only display.
		//public string CompanyName { get; set; } // Read-only display.
        public long? DepartmentId { get; set; }
		public string NameFull { get { return (this.NameGiven + " " + this.NameFamily); } } // Read-only, no display.

        //public string ActiveDirectory_AuthenticationEmail { get; set; }
        //public string ActiveDirectory_AuthenticationPhone { get; set; }
        //public string ActiveDirectory_AuthenticationPhoneAlternate { get; set; }
        //public string ActiveDirectory_Department { get; set; }
        //public string ActiveDirectory_JobTitle { get; set; }
        public Guid ActiveDirectory_ManagerId { get; set; }
        //public string ActiveDirectory_NameDisplay { get; set; }
        public Guid ActiveDirectory_ObjectId { get; set; }
        //public string ActiveDirectory_RoleDisplayName { get; set; }
        //public string ActiveDirectory_UsageLocation { get; set; }
    }
}