using System.Net;
using System.Net.Sockets;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.PowerTools.Library.Network
{
	public static class WakeOnLan
	{
		public static void Broadcast (MacAddress address, int port)
		{
			byte [] bytes = null;
			UdpClient udpClient = null;

			bytes = CreateMagicPacket(address);

			udpClient = new UdpClient();
			udpClient.Connect(IPAddress.Broadcast, port);
			udpClient.Send(bytes, bytes.Length);
			udpClient.Close();
		}

		public static void Send (IpAddressV4 destinationIpAddress, IpAddressV4 subnetMask, MacAddress address, int port)
		{
			byte [] bytes = null;
			UdpClient udpClient = null;

			bytes = CreateMagicPacket(address);

			udpClient = new UdpClient();
			udpClient.Connect(destinationIpAddress.IpAddress, port);
			udpClient.Send(bytes, bytes.Length);
			udpClient.Close();
		}

		/// <summary>
		/// Creates a magic packet for the specified mac address.
		/// </summary>
		/// <param name="macAddress">MAC Address must be in Hex format: FF-FF-FF-FF-FF-FF</param>
		/// <returns></returns>
		private static byte [] CreateMagicPacket (MacAddress address)
		{
			int offset = 0;
			byte [] bytes = null;

			// Magic Packet of 102 bytes.
			bytes = new byte [102];

			// Reset.
			for (int i = 0; i < bytes.Length; i++)
				bytes [i] = 0;

			// Set first 6 bytes to 0xFF.
			for (int x = 0; x < 6; x++)
				bytes [x] = "0xFF".ToByteFromHex();

			// Set remaining 96 bytes to the mac address repeated 16 times.
			for (int i = 0; i < 16; i++)
			{
				offset = 6 + (i * 6);
				for (int j = 0; j < address.Parts.Length; j++)
				{
					bytes [offset + j] = address.Parts [j].ToByteFromHex();
				}
			}

			return (bytes);
		}
	}
}