using System;
using System.Net;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.PowerTools.Library.Network
{
	public class IpAddressV6: IpAddressV4
	{
		protected IpAddressV6 ()
		{
			this.Address = IPAddress.None.ToString();
			this.IpAddress = IPAddress.None;
			this.Parts = this.IpAddress.ToString().Split(".".ToCharArray());
		}

		public IpAddressV6 (string address)
		{
			if (address == null)
			{
				throw (new ArgumentNullException("The provided [address] cannot be NULL.", "address"));
			}
			else
			{
				if (Validate(address))
				{
					this.Address = address;
					this.Parts = this.Address.ToString().Split(":".ToCharArray());
					this.IpAddress = IPAddress.Parse(this.Address);
				}
				else
				{
					throw (new ArgumentException("The provided [address] is not of legal form.", "address"));
				}
			}
		}

		public new static bool Validate (string address)
		{
			bool result = false;
			string [] parts = null;

			if (address != null)
			{
				if ((address.Length >= 15) && (address.Length <= 39))
				{
					if (address.Contains(":"))
					{
						parts = address.Split(":".ToCharArray());

						if (parts.Length == 8)
						{
							result = true;
							foreach (string part in parts)
							{
								if (((part.Length < 1) || (part.Length > 4)) || (!part.IsHex()))
								{
									result = false;

									break;
								}
							}
						}
					}
				}
			}

			return (result);
		}

		public new static IpAddressV6 None { get { return (new IpAddressV6()); } }

		public override string Part1 { get { return (this.Parts [0]); } }
		public override string Part2 { get { return (this.Parts [1]); } }
		public override string Part3 { get { return (this.Parts [2]); } }
		public override string Part4 { get { return (this.Parts [3]); } }
		public override string Part5 { get { return (this.Parts [4]); } }
		public override string Part6 { get { return (this.Parts [5]); } }
		public override string Part7 { get { return (this.Parts [6]); } }
		public override string Part8 { get { return (this.Parts [7]); } }
	}
}