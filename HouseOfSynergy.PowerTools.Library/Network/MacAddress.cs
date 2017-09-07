using System;

namespace HouseOfSynergy.PowerTools.Library.Network
{
	public class MacAddress: AddressBase
	{
		protected MacAddress () : base() { }

		public MacAddress (string address)
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
					if (!this.Address.Contains("-"))
					{
						this.Address
							= this.Address.Substring(0, 2)
							+ "-"
							+ this.Address.Substring(2, 2)
							+ "-"
							+ this.Address.Substring(4, 2)
							+ "-"
							+ this.Address.Substring(6, 2)
							+ "-"
							+ this.Address.Substring(8, 2)
							+ "-"
							+ this.Address.Substring(10, 2)
							;
					}
					this.Parts = this.Address.Split("-".ToCharArray());
				}
				else
				{
					throw (new ArgumentException("The provided [address] is not of legal form.", "address"));
				}
			}
		}

		public static MacAddress None { get { return (new MacAddress("00-00-00-00-00-00")); } }

		public new static bool Validate (string address)
		{
			bool result = false;

			if (address != null)
			{
				if (address.Length == 12)
				{
					result = System.Text.RegularExpressions.Regex.IsMatch("", @"^[\dA-Fa-f]{12}$");
				}
				else if (address.Length == 17)
				{
					result = System.Text.RegularExpressions.Regex.IsMatch("", @"^[\dA-Fa-f][\dA-Fa-f]-[\dA-Fa-f][\dA-Fa-f]-[\dA-Fa-f][\dA-Fa-f]-[\dA-Fa-f][\dA-Fa-f]-[\dA-Fa-f][\dA-Fa-f]-[\dA-Fa-f][\dA-Fa-f]$");
				}
			}

			return (result);
		}

		public override string Part1 { get { return (this.Parts [0]); } }
		public override string Part2 { get { return (this.Parts [1]); } }
		public override string Part3 { get { return (this.Parts [2]); } }
		public override string Part4 { get { return (this.Parts [3]); } }
		public override string Part5 { get { return (this.Parts [4]); } }
		public override string Part6 { get { return (this.Parts [5]); } }
	}
}