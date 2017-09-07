using System;
using System.Collections.Generic;
using System.Reflection;
using HouseOfSynergy.PowerTools.Library.Attributes;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static List<PropertyInfo> GetPropertiesWithFieldAttribute (this Type type)
		{
			PropertyInfo [] properties = null;
			List<PropertyInfo> filtered = null;
			FieldAttribute fieldAttribute = null;

			properties = type.GetProperties();
			filtered = new List<PropertyInfo>();

			foreach (PropertyInfo property in properties)
			{
				fieldAttribute = property.GetFieldAttribute();

				if (fieldAttribute != null)
				{
					fieldAttribute.PropertyName = property.Name;

					filtered.Add(property);
				}
			}

			return (filtered);
		}
	}
}