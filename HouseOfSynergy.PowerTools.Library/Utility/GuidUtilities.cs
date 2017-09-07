using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class GuidUtilities
	{
		public enum EnumGuidFormat
		{
			/// <summary>
			/// [N]: 32 digits: 00000000000000000000000000000000
			/// </summary>
			N,

			/// <summary>
			/// [D]: 32 digits separated by hyphens: 00000000-0000-0000-0000-000000000000.
			/// </summary>
			D,

			/// <summary>
			/// [B]: 32 digits separated by hyphens, enclosed in braces: {00000000-0000-0000-0000-000000000000}.
			/// </summary>
			B,

			/// <summary>
			/// [P]: 32 digits separated by hyphens, enclosed in parentheses: (00000000-0000-0000-0000-000000000000).
			/// </summary>
			P,

			/// <summary>
			/// [X]: Four hexadecimal values enclosed in braces, where the fourth value is a subset of eight hexadecimal values that is also enclosed in braces: {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}.
			/// </summary>
			X,

			/// <summary>
			/// [Database [B]]: 32 digits separated by hyphens, enclosed in braces: {00000000-0000-0000-0000-000000000000}.
			/// </summary>
			Database = B,

			/// <summary>
			/// [FileSystem [D]]: 32 digits separated by hyphens: 00000000-0000-0000-0000-000000000000.
			/// </summary>
			FileSystem = D,
		}
	}
}