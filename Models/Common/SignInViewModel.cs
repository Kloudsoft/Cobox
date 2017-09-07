using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Common
{
	public class SignInViewModel
	{
		[Required]
		[MinLength(EntityConstants.LengthDomainNameMinimum)]
		[StringLength(EntityConstants.LengthDomainNameMaximum)]
		public string Domain { get; set; }

		[Required]
		[MinLength(EntityConstants.LengthUserNameMinimum)]
		[StringLength(EntityConstants.LengthUserNameMaximum)]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[MinLength(EntityConstants.LengthPasswordMinimum)]
		[StringLength(EntityConstants.LengthPasswordMaximum)]
		public string Password { get; set; }

		//[Required]
		//[DataType(DataType.Password)]
		//[MinLength(EntityConstants.LengthPasswordHashMinimum)]
		//[StringLength(EntityConstants.LengthPasswordHashMaximum)]
		//public string PasswordHash { get; set; }

		//public bool RememberMe { get; set; }

		public SignInViewModel ()
		{
		}

		public SignInViewModel (string domain, string username, string password)
		{
			this.Domain = domain;
			this.UserName = username;
			this.Password = password;
			//this.PasswordHash = password;
		}

		//public SignInViewModel (string domain, string username, string password, bool rememberMe)
		//{
		//	this.Domain = domain;
		//	this.UserName = username;
		//	this.Password = password;
		//	this.PasswordHash = password;
		//	this.RememberMe = rememberMe;
		//}
	}
}