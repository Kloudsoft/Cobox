namespace HouseOfSynergy.PowerTools.Library.Network
{
	public class Computer:
		HouseOfSynergy.PowerTools.Library.Network.NetworkDevice,
		HouseOfSynergy.PowerTools.Library.Interfaces.ICloneable<HouseOfSynergy.PowerTools.Library.Network.Computer>
	{
		public string NetBios { get; set; }
		public IpAddressV4 IpAddressV4 { get; set; }
		public IpAddressV6 IpAddressV6 { get; set; }
		public SubnetMask SubnetMask { get; set; }
		public MacAddress MacAddress { get; set; }

		public Computer ()
			: base()
		{
		}

		public void Initialize ()
		{
		}

		public override string ToString ()
		{
			return (this.Name);
		}

		//HouseOfSynergy.PowerTools.Library.Interfaces.ICloneable<HouseOfSynergy.PowerTools.Library.Net.Computer>.
		public HouseOfSynergy.PowerTools.Library.Network.Computer Clone ()
		{
			return (new HouseOfSynergy.PowerTools.Library.Network.Computer().CopyFrom(this));
		}

		public HouseOfSynergy.PowerTools.Library.Network.Computer CopyFrom (HouseOfSynergy.PowerTools.Library.Network.Computer source)
		{
			this.IpAddressV4 = new IpAddressV4(source.IpAddressV4.Address);
			this.IpAddressV6 = new IpAddressV6(source.IpAddressV6.Address);
			this.SubnetMask = new SubnetMask(source.SubnetMask.Address);
			this.MacAddress = new MacAddress(source.MacAddress.Address);
			this.NetBios = source.NetBios;

			return (this);
		}

		public HouseOfSynergy.PowerTools.Library.Network.Computer CopyTo (HouseOfSynergy.PowerTools.Library.Network.Computer destination)
		{
			return (destination.CopyFrom(this));
		}
	}
}