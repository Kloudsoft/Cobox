using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static byte [] ToBytes (this global::System.IO.Stream stream)
		{
			return (stream.ToBytes(0, (int) stream.Length));
		}

		public static byte [] ToBytes (this global::System.IO.Stream stream, int offset, int count)
		{
			byte [] buffer = null;

			buffer = new byte [count];

			stream.Position = 0;
			stream.Read(buffer, offset, count);

			return (buffer);
		}
	}
}