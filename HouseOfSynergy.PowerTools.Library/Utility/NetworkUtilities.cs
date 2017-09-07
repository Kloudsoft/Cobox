using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class NetworkUtilities
	{
		public const long NO_ERROR = 0L;
		public const long ERROR_CAN_NOT_COMPLETE = 1003L;
		public const long ERROR_INVALID_PARAMETER = 87L;
		public const long ERROR_NOT_SUPPORTED = 50L;
		public const long ERROR_INVALID_DATA = 13L;
		public const long ERROR_NOT_FOUND = 1168L;

		/// <summary>
		/// The GetBestInterface function retrieves the index of the interface that has the best route to the specified IPv4 address.
		/// Native Decleration: DWORD GetBestInterface(_In_ IPAddr dwDestAddr, _Out_ PDWORD pdwBestIfIndex);.
		/// <remarks>
		/// The GetBestInterface function only works with IPv4 addresses. For use with IPv6 addresses, the GetBestInterfaceEx must be used.
		/// For information about the IPAddr data type, see Windows Data Types. To convert an IP address between dotted decimal notation and IPAddr format, use the inet_addr and inet_ntoa functions.
		/// On Windows Vista and later, the pdwBestIfIndex parameter is treated internally by IP Helper as a pointer to a NET_IFINDEX datatype.
		/// </remarks>
		/// </summary>
		/// <param name="dwDestAddr">The destination IPv4 address for which to retrieve the interface that has the best route, in the form of an IPAddr structure.</param>
		/// <param name="pdwBestIfIndex">A pointer to a DWORD variable that receives the index of the interface that has the best route to the IPv4 address specified by dwDestAddr.</param>
		/// <returns>
		/// NO_ERROR: The function succeeded.
		/// ERROR_CAN_NOT_COMPLETE: The operation could not be completed.
		/// ERROR_INVALID_PARAMETER: An invalid parameter was passed to the function. This error is returned if a NULL pointer is passed in the pdwBestIfIndex parameter or if the pdwBestIfIndex points to memory that cannot be written.
		/// ERROR_NOT_SUPPORTED: The request is not supported. This error is returned if no IPv4 stack is on the local computer.
		/// Other: Use FormatMessage to obtain the message string for the returned error.
		/// </returns>
		[DllImport("iphlpapi.dll", EntryPoint = "GetBestInterface", SetLastError = true)]
		private static extern int Win21Api_Native_GetBestInterface (UInt32 dwDestAddr, out UInt32 pdwBestIfIndex);

		/// <summary>
		/// The GetIfEntry function retrieves information for the specified interface on the local computer.
		/// Native Decleration: DWORD GetIfEntry(_Inout_ PMIB_IFROW pIfRow);.
		/// <remarks>
		/// The GetIfEntry function retrieves information for an interface on a local computer.
		/// The dwIndex member in the MIB_IFROW structure pointed to by the pIfRow parameter must be initialized to a valid network interface index retrieved by a previous call to the GetIfTable, GetIfTable2, or GetIfTable2Ex function.
		/// The GetIfEntry function will fail if the dwIndex member of the MIB_IFROW pointed to by the pIfRow parameter does not match an existing interface index on the local computer.
		/// </remarks>
		/// </summary>
		/// <param name="pIfRow">A pointer to a MIB_IFROW structure that, on successful return, receives information for an interface on the local computer. On input, set the dwIndex member of MIB_IFROW to the index of the interface for which to retrieve information. The value for the dwIndex must be retrieved by a previous call to the GetIfTable, GetIfTable2, or GetIfTable2Ex function.</param>
		/// <returns>
		/// NO_ERROR: The function succeeded.
		/// ERROR_CAN_NOT_COMPLETE: The request could not be completed. This is an internal error.
		/// ERROR_INVALID_DATA: The data is invalid. This error is returned if the network interface index specified by the dwIndex member of the MIB_IFROW structure pointed to by the pIfRow parameter is not a valid interface index on the local computer.
		/// ERROR_INVALID_PARAMETER: An invalid parameter was passed to the function. This error is returned if a NULL pointer is passed in the pIfRow parameter.
		/// ERROR_NOT_FOUND: The specified interface could not be found. This error is returned if the network interface index specified by the dwIndex member of the MIB_IFROW structure pointed to by the pIfRow parameter could not be found.
		/// ERROR_NOT_SUPPORTED: The request is not supported. This error is returned if IPv4 is not configured on the local computer.
		/// Other: Use FormatMessage to obtain the message string for the returned error.
		/// </returns>
		[DllImport("iphlpapi.dll", EntryPoint = "GetBestInterface", SetLastError = true)]
		private static extern int Win21Api_Native_GetIfEntry (ref uint pIfRow);

		public static bool GetBestNetworkInterface (IPAddress ipAddressDestination, out uint bestInterfaceIndex, out Exception exception)
		{
			var result = NetworkUtilities.Win21Api_Native_GetBestInterface(BitConverter.ToUInt32(ipAddressDestination.GetAddressBytes(), 0), out bestInterfaceIndex);

			return (NetworkUtilities.GetResultWithException(result, out exception));
		}

		/// <summary>
		/// TODO: Get the PMIB_IFROW incorporated.
		/// </summary>
		public static bool GetNetworkInterfaceEntry (ref uint networkInterfaceIndex, out Exception exception)
		{
			var result = NetworkUtilities.Win21Api_Native_GetIfEntry(ref networkInterfaceIndex);

			return (NetworkUtilities.GetResultWithException(result, out exception));
		}

		private static bool GetResultWithException (int result, out Exception exception)
		{
			if (result == NetworkUtilities.NO_ERROR) { exception = null; return (true); } else { exception = new Win32Exception(result); return (false); }
		}

		public static List<string> GetNetworkDirectoryEntryNames ()
		{
			var root = new DirectoryEntry("WinNT:");
			var entries = NetworkUtilities.GetNetworkDirectoryEntries();
			var names = entries.ConvertAll<string>(entry => entry.Name);

			entries.ForEach(entry => entry.Dispose());

			return (names);
		}

		public static List<DirectoryEntry> GetNetworkDirectoryEntries ()
		{
			var root = new DirectoryEntry("WinNT:");
			var entries = new List<DirectoryEntry>();

			foreach (DirectoryEntry availableDomains in root.Children)
			{
				foreach (DirectoryEntry entry in availableDomains.Children)
				{
					if (string.Compare(entry.SchemaClassName, "Computer", true, CultureInfo.InvariantCulture) == 0)
					{
						entries.Add(entry);
					}
				}
			}

			return (entries);
		}
	}
}