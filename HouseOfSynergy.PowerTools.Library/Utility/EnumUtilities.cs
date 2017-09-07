using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class EnumUtilities
	{
		public enum ResultType
		{
			ReturnNull,
			ThrowException,
		}

		public enum EnumType
		{
			Nullable,
			NonNullable,
			NoneRequiredAtZero,
		}

		public static Type GetEnum (Type type, ResultType resultType)
		{
			if (type.IsEnum)
			{
				return (type);
			}
			else
			{
				var genericTypeDefinition = type.GetGenericTypeDefinition();

				if (genericTypeDefinition == typeof(Nullable<>))
				{
					var genericArgument = type.GetGenericArguments() [0];

					if (genericArgument.IsEnum)
					{
						return (genericArgument);
					}
					else
					{
						if (resultType == ResultType.ReturnNull) { return (null); }
						else if (resultType == ResultType.ThrowException) { throw (new ArgumentException("The parameter [type] must be a nullable or non-nullable enumeration.", "type")); }
						else { throw (new NotImplementedException()); }
					}
				}
				else
				{
					if (resultType == ResultType.ReturnNull) { return (null); }
					else if (resultType == ResultType.ThrowException) { throw (new ArgumentException("The parameter [type] must be a nullable or non-nullable enumeration.", "type")); }
					else { throw (new NotImplementedException()); }
				}
			}
		}

		public static bool IsEnum (Type type, bool allowNullableEnums) { return ((type.IsEnum) || ((allowNullableEnums) && (Nullable.GetUnderlyingType(type)?.IsEnum ?? false))); }
		public static bool IsEnumNonNullable (Type type) { return (type.IsEnum); }

		public static bool IsEnumNullable (Type type) { return ((!type.IsEnum) && (Nullable.GetUnderlyingType(type)?.IsEnum ?? false)); }

		#region Generic Wrapper Methods.

		public static bool IsEnumNullable<TEnum> () { return (EnumUtilities.IsEnumNullable(typeof(TEnum))); }
		public static bool IsEnumNonNullable<TEnum> () { return (EnumUtilities.IsEnumNonNullable(typeof(TEnum))); }
		public static bool IsEnum<TEnum> (bool allowNullableEnums) where TEnum : struct, IComparable, IFormattable, IConvertible { return (EnumUtilities.IsEnum(typeof(TEnum), allowNullableEnums)); }
		public static Type GetEnum<TEnum> (ResultType resultType) where TEnum : struct, IComparable, IFormattable, IConvertible { return (EnumUtilities.GetEnum(typeof(TEnum), resultType)); }

		#endregion Generic Wrapper Methods.

		public static List<TEnum> GetValues<TEnum> (bool allowNullableEnums = true)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			EnumUtilities.ThrowOnNonEnum<TEnum>(allowNullableEnums);

			var list = Enum.GetValues(typeof(TEnum))
				.OfType<TEnum>()
				.ToList()
				.ConvertAll<TEnum>(v => ((TEnum) v));

			return (list);
		}

		public static bool IsCompliantWithNoFlagsAndNoneAtZero<TEnum> (bool allowNullableEnums = true)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			EnumUtilities.ThrowOnEnumWithFlags<TEnum>(allowNullableEnums);

			var values = EnumUtilities.GetValues<TEnum>();

			if (values.Any(v => v.ToString() == "None"))
			{
				var type = Enum.GetUnderlyingType(typeof(TEnum));
				var value = values.First(v => v.ToString() == "None");
				var number = Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(TEnum)));

				if (type == typeof(byte)) { return (((byte) number) == ((byte) 0)); }
				else if (type == typeof(sbyte)) { return (((sbyte) number) == ((sbyte) 0)); }
				else if (type == typeof(short)) { return (((short) number) == ((short) 0)); }
				else if (type == typeof(ushort)) { return (((ushort) number) == ((ushort) 0)); }
				else if (type == typeof(int)) { return (((int) number) == ((int) 0)); }
				else if (type == typeof(uint)) { return (((uint) number) == ((uint) 0)); }
				else if (type == typeof(long)) { return (((long) number) == ((long) 0)); }
				else if (type == typeof(ulong)) { return (((ulong) number) == ((ulong) 0)); }

				if (number == ((object) 0))
				{
					return (true);
				}
			}

			return (false);
		}

		public static bool IsValueDefinedAndComposite<TEnum> (TEnum value, bool allowNullableEnums)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			EnumUtilities.ThrowOnEnumWithoutFlags<TEnum>(allowNullableEnums);

			var values = EnumUtilities.GetValues<TEnum>();

			throw (new NotImplementedException());

			//var result = false;
			//var result = values.Count(v => (value | v) == value) > 1;
			// How to determine whether the argument [value] is composite.

			//return (result);
		}

		public static bool IsValueDefinedAndNonComposite<TEnum> (TEnum value, bool allowNullableEnums)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			EnumUtilities.ThrowOnNonEnum<TEnum>(allowNullableEnums);

			return (Enum.IsDefined(typeof(TEnum), value));
		}

		public static bool IsValueDefined<TEnum> (TEnum value, bool allowNullableEnums)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			EnumUtilities.ThrowOnNonEnum<TEnum>(allowNullableEnums);

			return (EnumUtilities.IsValueDefinedAndNonComposite(value, allowNullableEnums) || EnumUtilities.IsValueDefinedAndComposite(value, allowNullableEnums));
		}

		public static void ThrowOnNonEnum (Type type, bool allowNullableEnums)
		{
			if (EnumUtilities.IsEnum(type, allowNullableEnums))
			{
				throw (new ArgumentException("The argument [type] must be an enumeration.", "type"));
			}
		}

		public static void ThrowOnEnumWithFlags (Type type, bool allowNullableEnums)
		{
			EnumUtilities.ThrowOnNonEnum(type, allowNullableEnums);

			var attributes = type.GetCustomAttributes(typeof(FlagsAttribute), false);

			if (attributes.Length > 0)
			{
				throw (new ArgumentException("The generic argument [type] must be an enumeration without the [FlagsAttribute] applied.", "type"));
			}
		}

		public static void ThrowOnEnumWithoutFlags (Type type, bool allowNullableEnums = true)
		{
			EnumUtilities.ThrowOnNonEnum(type, allowNullableEnums);

			var attributes = type.GetCustomAttributes(typeof(FlagsAttribute), false);

			if (attributes.Length == 0)
			{
				throw (new ArgumentException("The generic argument [type] must be an enumeration with the [FlagsAttribute] applied.", "type"));
			}
		}

		public static void ThrowOnNonEnum<TEnum> (bool allowNullableEnums = true)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			if (!EnumUtilities.IsEnum(typeof(TEnum), allowNullableEnums))
			{
				throw (new ArgumentException("The generic argument [<T>] must be an enumeration.", "T: " + typeof(TEnum).FullName));
			}
		}

		public static void ThrowOnEnumWithFlags<TEnum> (bool allowNullableEnums = true)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			EnumUtilities.ThrowOnNonEnum<TEnum>(allowNullableEnums);

			var attributes = typeof(TEnum).GetCustomAttributes(typeof(FlagsAttribute), false);

			if (attributes.Length > 0)
			{
				throw (new ArgumentException("The generic argument [<T>] must be an enumeration without the [FlagsAttribute] applied.", "T: " + typeof(TEnum).FullName));
			}
		}

		public static void ThrowOnEnumWithoutFlags<TEnum> (bool allowNullableEnums)
			where TEnum: struct, IComparable, IFormattable, IConvertible
		{
			EnumUtilities.ThrowOnNonEnum<TEnum>(allowNullableEnums);

			var attributes = typeof(TEnum).GetCustomAttributes(typeof(FlagsAttribute), false);

			if (attributes.Length == 0)
			{
				throw (new ArgumentException("The generic argument [<T>] must be an enumeration with the [FlagsAttribute] applied.", "T: " + typeof(TEnum).FullName));
			}
		}
	}
}