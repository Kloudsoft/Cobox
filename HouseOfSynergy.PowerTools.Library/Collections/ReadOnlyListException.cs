using System;
using System.Linq;
using System.Runtime.Serialization;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public abstract class ReadOnlyListException:
		Exception
	{
		protected ReadOnlyListException () : base() { }
		protected ReadOnlyListException (string message) : base(message) { }
		protected ReadOnlyListException (string message, Exception innerException) : base(message, innerException) { }
		protected ReadOnlyListException (SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}