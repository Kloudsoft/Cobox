using System;
using System.Linq;
using System.Runtime.Serialization;

namespace HouseOfSynergy.PowerTools.Library.Media
{
	[Serializable()]
	public class MediaInfoFormatException:
		Exception
	{
		public MediaInfoFormatException () : base() { }
		public MediaInfoFormatException (string message) : base(message) { }
		public MediaInfoFormatException (string message, Exception inner) : base(message, inner) { }
		public MediaInfoFormatException (SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}