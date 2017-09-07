using System;
using System.Reflection.Emit;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static void EmitOpCode (this ILGenerator generator, OpCode code)
		{
			generator.Emit(code);
		}
	}
}