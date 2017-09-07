using System;
using System.Linq;
using System.Runtime.Serialization;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class ReadOnlyListItemEditException:
		ReadOnlyListException
	{
		public ReadOnlyListItemEditException () : base() { }
		public ReadOnlyListItemEditException (string message) : base(message) { }
		public ReadOnlyListItemEditException (string message, Exception innerException) : base(message, innerException) { }
		public ReadOnlyListItemEditException (SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}