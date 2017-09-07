using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static T [] GetRange<T> (this T [] array, int offset, int count)
		{
			var buffer = new T [count];
			Array.Copy(array, offset, buffer, 0, count);
			return (buffer);
		}
	}
}