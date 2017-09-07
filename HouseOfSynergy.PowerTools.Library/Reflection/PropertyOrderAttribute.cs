using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Reflection
{
	[System.AttributeUsage(System.AttributeTargets.Property)]
	public class PropertyOrderAttribute:
		System.Attribute
	{
		public int Order { get; private set; }

		public PropertyOrderAttribute (int order) { this.Order = order; }
	}
}