using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	public class FieldAttributeOrdinalComparer:
		System.Collections.Generic.IComparer<System.Reflection.PropertyInfo>
	{
		public int Compare (System.Reflection.PropertyInfo x, System.Reflection.PropertyInfo y)
		{
			return (x.GetFieldAttribute().Ordinal.CompareTo(y.GetFieldAttribute().Ordinal));
		}
	}
}