using System;
using HouseOfSynergy.PowerTools.Library.Attributes;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static FieldAttribute GetFieldAttribute (this global::System.Reflection.PropertyInfo propertyInfo)
		{
			object [] attributes = null;

			if (propertyInfo == null)
			{
				throw (new ArgumentNullException("propertyInfo", "Parameter [propertyInfo] cannot be null."));
			}
			else
			{
				attributes = propertyInfo.GetCustomAttributes(typeof(FieldAttribute), true);

				if ((attributes == null) || (attributes.Length == 0))
				{
					return (null);
				}
				else
				{
					return (attributes [0] as FieldAttribute);
				}
			}
		}
	}
}