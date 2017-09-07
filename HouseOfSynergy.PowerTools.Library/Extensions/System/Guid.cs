using System;
using System.Linq;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		/// <summary>
		/// Returns a string representation of the value of this Guid instance, according to the provided format specifier.
		/// </summary>
		/// <param name="format">A single format specifier that indicates how to format the value of this Guid.</param>
		/// <returns>TThe value of this Guid, represented as a series of lowercase hexadecimal digits in the specified format.</returns>
		public static string ToString (this Guid guid, GuidUtilities.EnumGuidFormat format)
		{
			switch (format)
			{
				case GuidUtilities.EnumGuidFormat.Database: { return (guid.ToString("B")); }
				case GuidUtilities.EnumGuidFormat.FileSystem: { return (guid.ToString("D")); }
				default: { return (guid.ToString(format.ToString())); }
			}
		}
	}
}