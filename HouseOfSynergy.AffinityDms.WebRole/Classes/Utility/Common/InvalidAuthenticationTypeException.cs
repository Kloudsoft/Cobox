using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Common
{
	public abstract class WebRoleException:
		AffinityException
	{
		protected WebRoleException (string message) : base(message) { }
		protected WebRoleException (string message, Exception innerException) : base(message, innerException) { }
	}

	public abstract class AuthenticationException:
		WebRoleException
	{
		protected AuthenticationException (string message) : base(message) { }
		protected AuthenticationException (string message, Exception innerException) : base(message, innerException) { }
	}

	public class InvalidAuthenticationTypeException:
		AuthenticationException
	{
		public InvalidAuthenticationTypeException () : base("The authentication type is not valid.") { }
		public InvalidAuthenticationTypeException (string message) : base(message) { }
		public InvalidAuthenticationTypeException (string message, Exception innerException) : base(message, innerException) { }
	}
}