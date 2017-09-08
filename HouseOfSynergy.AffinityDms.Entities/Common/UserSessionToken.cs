using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Common
{
	public class UserSessionToken
	{
		public string UserAgent { get; set; }
		public string IPAddressString { get; set; }
		public string Username { get; set; }
		public long DateTimeTicks { get; set; }
		public string ExpirationString { get; set; }
		public DateTime Expiration { get; set; }

		public UserSessionToken ()
		{
		}

		public DateTime DateTime { get { return (new DateTime(this.DateTimeTicks)); } }
		public IPAddress IPAddress { get { return (IPAddress.Parse(this.IPAddressString)); } }
	}
}