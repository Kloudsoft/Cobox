using System;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public class EnumAttribute:
		System.Attribute
	{
		/// <summary>
		/// The numeric value of the enum member.
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// The textual name of the enum member.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Short description.
		/// </summary>
		public string DescriptionShort { get; private set; }

		/// <summary>
		/// Long description.
		/// </summary>
		public string DescriptionLong { get; private set; }

		/// <summary>
		/// Creates a new instance of EnumAttribute with named parameters.
		/// </summary>
		/// <param name="id">The numeric value of the enum member.</param>
		/// <param name="name">The textual name of the enum member.</param>
		/// <param name="descriptionShort">Short description.</param>
		/// <param name="descriptionLong">Long description.</param>
		public EnumAttribute (int id, string name, string descriptionShort = "", string descriptionLong = "")
		{
			this.Id = id;
			this.Name = name;
			this.DescriptionLong = descriptionLong ?? "";
			this.DescriptionShort = descriptionShort ?? "";
		}

		public override string ToString ()
		{
			return (this.DescriptionShort);
		}

		private static readonly System.Type AttributeType = typeof(EnumAttribute);

		public static int GetEnumId<T> (T value)
			where T: struct
		{
			System.Type type = null;
			System.Reflection.MemberInfo [] members = null;
			EnumAttribute attribute = null;

			type = typeof(T);

			if (!type.IsEnum) { throw (new ArgumentException("The generic parameter [<T>] of [" + AttributeType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod() + "<T>] must be a [System.Enum] type.", "enumeration")); }

			members = type.GetMember(value.ToString());

			if ((members != null) && (members.Length == 1))
			{
				var array = members [0].GetCustomAttributes(AttributeType, false);

				if (array.Length == 0)
				{
					throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] does not have the [" + AttributeType.Name + "] attribute applied."));
				}
				else if (array.Length == 1)
				{
					attribute = (EnumAttribute) array [0];

					return (attribute.Id);
				}
				else
				{
					throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] appears to have multiple [" + AttributeType.Name + "] attributes applied."));
				}
			}
			else
			{
				throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] does not appear to map to a single value."));
			}
		}

		public static string GetEnumName<T> (T value)
			where T: struct
		{
			System.Type type = null;
			System.Reflection.MemberInfo [] members = null;
			EnumAttribute attribute = null;

			type = typeof(T);

			if (!type.IsEnum) { throw (new ArgumentException("The generic parameter [<T>] of [" + AttributeType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod() + "<T>] must be a [System.Enum] type.", "enumeration")); }

			members = type.GetMember(value.ToString());

			if ((members != null) && (members.Length == 1))
			{
				var array = members [0].GetCustomAttributes(AttributeType, false);

				if (array.Length == 0)
				{
					throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] does not have the [" + AttributeType.Name + "] attribute applied."));
				}
				else if (array.Length == 1)
				{
					attribute = (EnumAttribute) array [0];

					return (attribute.Name);
				}
				else
				{
					throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] appears to have multiple [" + AttributeType.Name + "] attributes applied."));
				}
			}
			else
			{
				throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] does not appear to map to a single value."));
			}
		}

		public static string GetEnumDescriptionShort<T> (T value)
			where T: struct
		{
			System.Type type = null;
			System.Reflection.MemberInfo [] members = null;
			EnumAttribute attribute = null;

			type = typeof(T);

			if (!type.IsEnum) { throw (new ArgumentException("The generic parameter [<T>] of [" + AttributeType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod() + "<T>] must be a [System.Enum] type.", "enumeration")); }

			members = type.GetMember(value.ToString());

			if ((members != null) && (members.Length == 1))
			{
				var array = members [0].GetCustomAttributes(AttributeType, false);

				if (array.Length == 0)
				{
					throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] does not have the [" + AttributeType.Name + "] attribute applied."));
				}
				else if (array.Length == 1)
				{
					attribute = (EnumAttribute) array [0];

					return (attribute.DescriptionShort);
				}
				else
				{
					throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] appears to have multiple [" + AttributeType.Name + "] attributes applied."));
				}
			}
			else
			{
				throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] does not appear to map to a single value."));
			}
		}

		public static string GetEnumDescriptionLong<T> (T value)
			where T: struct
		{
			System.Type type = null;
			System.Reflection.MemberInfo [] members = null;
			EnumAttribute attribute = null;

			type = typeof(T);

			if (!type.IsEnum) { throw (new ArgumentException("The generic parameter [<T>] of [" + AttributeType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod() + "<T>] must be a [System.Enum] type.", "enumeration")); }

			members = type.GetMember(value.ToString());

			if ((members != null) && (members.Length == 1))
			{
				var array = members [0].GetCustomAttributes(AttributeType, false);

				if (array.Length == 0)
				{
					throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] does not have the [" + AttributeType.Name + "] attribute applied."));
				}
				else if (array.Length == 1)
				{
					attribute = (EnumAttribute) array [0];

					return (attribute.DescriptionLong);
				}
				else
				{
					throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] appears to have multiple [" + AttributeType.Name + "] attributes applied."));
				}
			}
			else
			{
				throw (new Exception("The enum member [" + type.Name + "." + value.ToString() + "] does not appear to map to a single value."));
			}
		}
	}
}