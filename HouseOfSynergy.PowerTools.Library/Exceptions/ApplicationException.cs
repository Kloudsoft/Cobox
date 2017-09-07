using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;

namespace HouseOfSynergy.PowerTools.Library.Exceptions
{
	public class ApplicationException:
		Exception
	{
		public ApplicationException () : base() { }
		public ApplicationException (string message) : base(message) { }
		[SecuritySafeCritical]
		protected ApplicationException (SerializationInfo info, StreamingContext context) : base(info, context) { }
		public ApplicationException (string message, Exception innerException) : base(message, innerException) { }
	}
}