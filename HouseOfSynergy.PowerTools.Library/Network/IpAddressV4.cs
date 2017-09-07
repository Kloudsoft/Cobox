using System;
using System.Linq;
using System.Net;

namespace HouseOfSynergy.PowerTools.Library.Network
{
	public class IpAddressV4: AddressBase
	{
		public IPAddress IpAddress { get; protected set; }

		protected IpAddressV4 ()
		{
			this.IpAddress = IPAddress.None;
			this.Address = IPAddress.None.ToString();
			this.Parts = this.IpAddress.ToString().Split(".".ToCharArray());
		}

		public IpAddressV4 (string address)
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
					this.Parts = this.Address.ToString().Split(".".ToCharArray());
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
			int value = 0;
			bool result = false;
			string [] parts = null;

			if (address != null)
			{
				if ((address.Length >= 7) && (address.Length <= 15))
				{
					if (address.ToList().All(c => (char.IsDigit(c) || (c == '.'))))
					{
						if (address.Contains("."))
						{
							parts = address.Split(".".ToCharArray());

							if (parts.Length == 4)
							{
								result = true;
								foreach (string part in parts)
								{
									if ((part.Length < 1) || (part.Length > 3))
									{
										result = false;

										break;
									}
									else
									{
										if (int.TryParse(part, out value))
										{
											if ((value < 0) || (value > 255))
											{
												result = false;

												break;
											}
										}
										else
										{
											result = false;

											break;
										}
									}
								}
							}
						}
					}
				}
			}

			return (result);
		}

		public override string Part1 { get { return (this.Parts [0]); } }
		public override string Part2 { get { return (this.Parts [1]); } }
		public override string Part3 { get { return (this.Parts [2]); } }
		public override string Part4 { get { return (this.Parts [3]); } }

		public static IpAddressV4 None { get { return (new IpAddressV4()); } }
	}
}