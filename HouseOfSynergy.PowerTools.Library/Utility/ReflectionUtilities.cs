using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class ReflectionUtilities
	{
		public static T Copy<T> (T source, T destination)
			where T: class, new()
		{
			var type = typeof(T);
			var list = new List<PropertyInfo>();
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			foreach (var property in properties)
			{
				if
				(
					(property.PropertyType.IsEnum)
					|| (property.PropertyType.IsPrimitive)
					|| (property.PropertyType == typeof(string))
					|| (property.PropertyType == typeof(DateTime))
					|| (property.PropertyType == typeof(Guid))
				)
				{
					property.SetValue(destination, property.GetValue(source));
				}
				else
				{
					list.Add(property);
				}
			}

			return (destination);
		}

		public static XmlElement ToXmlElement<T> (XmlDocument document, T instance)
			where T: class, IPersistXmlElement<T>, new()
		{
			var type = typeof(T);
			var list = new List<PropertyInfo>();
			var element = document.CreateElement(type.Name);
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			foreach (var property in properties)
			{
				try
				{
					if ((property.PropertyType.IsGenericType) && (property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) && (property.PropertyType.GetGenericArguments().Any(t => t.IsValueType && t.IsPrimitive)))
					{
						// It's a nullable primitive.
						var value = property.GetValue(instance);
						element.Attributes.Append(document, property.Name, value == null ? "" : value.ToString());
					}
					else if
					(
						(property.PropertyType.IsEnum)
						|| (property.PropertyType.IsPrimitive)
						|| (property.PropertyType == typeof(string))
					)
					{
						if (property.PropertyType == typeof(int))
						{
							var value = property.GetValue(instance);
							element.Attributes.Append(document, property.Name, value == null ? "" : value.ToString().PadLeft(10, '0'));
						}
						else if (property.PropertyType == typeof(long))
						{
							var value = property.GetValue(instance);
							element.Attributes.Append(document, property.Name, value == null ? "" : value.ToString().PadLeft(19, '0'));
						}
						else
						{
							var value = property.GetValue(instance);
							element.Attributes.Append(document, property.Name, value == null ? "" : value.ToString());
						}
					}
					else if (property.PropertyType == typeof(string))
					{
						var value = property.GetValue(instance);
						element.Attributes.Append(document, property.Name, value == null ? "" : value.ToString());
					}
					else if (property.PropertyType == typeof(DateTime))
					{
						var value = property.GetValue(instance);
						element.Attributes.Append(document, property.Name, ((DateTime) value).Ticks.ToString());
					}
					else if (property.PropertyType == typeof(Guid))
					{
						var value = property.GetValue(instance);
						element.Attributes.Append(document, property.Name, ((Guid) value).ToString());
					}
					else
					{
						list.Add(property);
					}
				}
				catch
				{
					Debugger.Break();
				}
			}

			return (element);
		}

		public static T FromXmlElement<T> (T instance, XmlElement element)
			where T: class, IPersistXmlElement<T>, new()
		{
			var type = typeof(T);
			var list = new List<PropertyInfo>();
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			instance.Initialize();

			foreach (var property in properties)
			{
				var attribute = element.Attributes.OfType<XmlAttribute>().SingleOrDefault(a => a.Name == property.Name);

				if (attribute != null)
				{
					if ((property.PropertyType.IsGenericType) && (property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) && (property.PropertyType.GetGenericArguments().Any(t => t.IsValueType && t.IsPrimitive)))
					{
						// It's a nullable primitive.
						if (attribute.Value == "")
						{
							property.SetValue(instance, null);
						}
						else
						{
							var argument = property.PropertyType.GetGenericArguments() [0];

							if (argument == typeof(Boolean)) { property.SetValue(instance, Boolean.Parse(attribute.Value)); }
							else if (argument == typeof(Byte)) { property.SetValue(instance, Byte.Parse(attribute.Value)); }
							else if (argument == typeof(SByte)) { property.SetValue(instance, SByte.Parse(attribute.Value)); }
							else if (argument == typeof(Int16)) { property.SetValue(instance, Int16.Parse(attribute.Value)); }
							else if (argument == typeof(UInt16)) { property.SetValue(instance, UInt16.Parse(attribute.Value)); }
							else if (argument == typeof(Int32)) { property.SetValue(instance, Int32.Parse(attribute.Value)); }
							else if (argument == typeof(UInt32)) { property.SetValue(instance, UInt32.Parse(attribute.Value)); }
							else if (argument == typeof(Int64)) { property.SetValue(instance, Int64.Parse(attribute.Value)); }
							else if (argument == typeof(UInt64)) { property.SetValue(instance, UInt64.Parse(attribute.Value)); }
							else if (argument == typeof(IntPtr)) { property.SetValue(instance, new IntPtr(long.Parse(attribute.Value))); }
							else if (argument == typeof(UIntPtr)) { property.SetValue(instance, new UIntPtr(ulong.Parse(attribute.Value))); }
							else if (argument == typeof(Char)) { property.SetValue(instance, Char.Parse(attribute.Value)); }
							else if (argument == typeof(Double)) { property.SetValue(instance, Double.Parse(attribute.Value)); }
							else if (argument == typeof(Single)) { property.SetValue(instance, Single.Parse(attribute.Value)); }
						}
					}
					else if (property.PropertyType.IsEnum)
					{
						property.SetValue(instance, Enum.Parse(property.PropertyType, attribute.Value));
					}
					if (property.PropertyType == typeof(string))
					{
						property.SetValue(instance, attribute.Value);
					}
					else if (property.PropertyType.IsPrimitive)
					{
						if (property.PropertyType == typeof(Boolean)) { property.SetValue(instance, Boolean.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(Byte)) { property.SetValue(instance, Byte.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(SByte)) { property.SetValue(instance, SByte.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(Int16)) { property.SetValue(instance, Int16.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(UInt16)) { property.SetValue(instance, UInt16.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(Int32)) { property.SetValue(instance, Int32.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(UInt32)) { property.SetValue(instance, UInt32.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(Int64)) { property.SetValue(instance, Int64.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(UInt64)) { property.SetValue(instance, UInt64.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(IntPtr)) { property.SetValue(instance, new IntPtr(long.Parse(attribute.Value))); }
						else if (property.PropertyType == typeof(UIntPtr)) { property.SetValue(instance, new UIntPtr(ulong.Parse(attribute.Value))); }
						else if (property.PropertyType == typeof(Char)) { property.SetValue(instance, Char.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(Double)) { property.SetValue(instance, Double.Parse(attribute.Value)); }
						else if (property.PropertyType == typeof(Single)) { property.SetValue(instance, Single.Parse(attribute.Value)); }
					}
					else if (property.PropertyType == typeof(DateTime))
					{
						property.SetValue(instance, new DateTime(long.Parse(attribute.Value)));
					}
					else if (property.PropertyType == typeof(Guid))
					{
						property.SetValue(instance, Guid.Parse(attribute.Value));
					}
					else
					{
						list.Add(property);
					}
				}
			}

			return (instance);
		}

		public static bool InheritsFrom (Type baseType, Type inheritorType)
		{
			bool result = false;

			try
			{
				if (inheritorType != null)
				{
					if (inheritorType != typeof(object))
					{
						if (baseType.IsInterface)
						{
							if (ReflectionUtilities.InheritsFrom(baseType, inheritorType.BaseType))
							{
								result = true;
							}
							else
							{
								var interfaces = inheritorType.GetInterfaces().ToList();

								if (interfaces.Contains(baseType))
								{
									result = true;
								}
								else
								{
									result = interfaces.Any(i => ReflectionUtilities.InheritsFrom(baseType, i));
								}
							}
						}
						else
						{
						}
					}
				}
			}
			catch// (Exception e)
			{
				Debugger.Break();
			}

			return (result);
		}
	}
}