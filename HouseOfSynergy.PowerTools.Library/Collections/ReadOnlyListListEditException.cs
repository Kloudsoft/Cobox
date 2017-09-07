using System;
using System.Linq;
using System.Runtime.Serialization;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public sealed class ReadOnlyListListEditException:
		ReadOnlyListException
	{
		public ReadOnlyListListEditException () : base() { }
		public ReadOnlyListListEditException (string message) : base(message) { }
		public ReadOnlyListListEditException (string message, Exception innerException) : base(message, innerException) { }
		public ReadOnlyListListEditException (SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}