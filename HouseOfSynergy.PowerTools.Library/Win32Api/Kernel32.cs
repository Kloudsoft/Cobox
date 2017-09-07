using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Win32Api
{
	public static class Kernel32
	{
		[DllImport("kernel32.dll")]
		public static extern uint GetLastError ();

		[ResourceExposure(ResourceScope.None)]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, BestFitMapping = true)]
		public static extern uint FormatMessage (int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr va_list_arguments);

		[DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
		public static extern void CopyMemory (IntPtr dest, IntPtr src, uint count);

		/// <summary>
		/// Moves a block of memory from one location to another. The source and destination blocks may overlap.
		/// This function is defined as the RtlMoveMemory function. Its implementation is provided inline. For more information, see WinBase.h and Winnt.h.
		/// Native Declaration: void MoveMemory(_In_  PVOID Destination, _In_  const VOID *Source, _In_  SIZE_T Length);
		/// </summary>
		/// <param name="destination">A pointer to the starting address of the move destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to be moved.</param>
		/// <param name="length">The size of the block of memory to move, in bytes.</param>
		[DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
		private static extern void RtlMoveMemory ([In, Out] byte [] destination, byte [] source, uint length);
	}
}