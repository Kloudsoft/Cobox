using System;

namespace HouseOfSynergy.PowerTools.Library.Network
{
	public abstract class AddressBase
	{
		public string Address { get; protected set; }
		public string [] Parts { get; protected set; }

		protected AddressBase ()
		{
			this.Address = "";
			this.Parts = null;
		}

		protected AddressBase (string address)
			: this()
		{
			if (address == null)
			{
				throw (new ArgumentNullException("The provided [address] cannot be NULL.", "address"));
			}
			else
			{
				this.Address = address;
			}
		}

		public override string ToString () { return (this.Address); }

		public static bool Validate (string address)
		{
			throw (new NotImplementedException("Derived classes must provide their own implementations."));
		}

		public virtual string Part1 { get { throw (new System.NotSupportedException()); } }
		public virtual string Part2 { get { throw (new System.NotSupportedException()); } }
		public virtual string Part3 { get { throw (new System.NotSupportedException()); } }
		public virtual string Part4 { get { throw (new System.NotSupportedException()); } }
		public virtual string Part5 { get { throw (new System.NotSupportedException()); } }
		public virtual string Part6 { get { throw (new System.NotSupportedException()); } }
		public virtual string Part7 { get { throw (new System.NotSupportedException()); } }
		public virtual string Part8 { get { throw (new System.NotSupportedException()); } }
	}
}