using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Lookup;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Common
{
	public class UserCurrentViewModel
	{
		public long Id { get; set; }
		public string NameGiven { get; set; }
		public string NameFamily { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public AuthenticationType UserType { get; set; }
		public string Company { get; set; }
		public string Domain { get; set; }

		public string NameFull { get { return (this.NameGiven + " " + this.NameFamily); } }
	}
}