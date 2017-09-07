using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Reflection
{
	public sealed class PropertyOrderPair:
		System.IComparable
	{
		public int Order { get; private set; }
		public string Name { get; private set; }

		public PropertyOrderPair (string name, int order) { this.Order = order; this.Name = name; }

		public int CompareTo (object obj)
		{
			// Sort the pair objects by ordering by order value. Equal values get the same rank.
			int otherOrder = ((PropertyOrderPair) obj).Order;

			if (otherOrder == this.Order)
			{
				// If order not specified, sort by name.
				string otherName = ((PropertyOrderPair) obj).Name;

				return (string.Compare(this.Name, otherName));
			}
			else if (otherOrder > this.Order)
			{
				return -1;
			}

			return 1;
		}
	}
}