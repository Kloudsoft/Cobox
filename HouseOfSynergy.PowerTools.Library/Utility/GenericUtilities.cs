using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using HouseOfSynergy.PowerTools.Library.Generics;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class GenericUtilities
	{
		static GenericUtilities ()
		{
		}
	}

	public static class GenericUtilities<T>
	{
		public static readonly int SizeOfT = -1;
		public static readonly Type TypeOfT = null;

		public static readonly ReadOnlyCollection<Type> TypesPrimitiveIntegral = null;
		public static readonly ReadOnlyCollection<Type> TypesPrimitiveFloatingPoint = null;
		public static readonly ReadOnlyCollection<Type> TypesPrimitiveIntegralAndFloatingPoint = null;

		static GenericUtilities ()
		{
			var typeIntegral = new List<Type>();
			var typeFloatingPoint = new List<Type>();
			var typeIntegralAndFloatingPoint = new List<Type>();

			typeIntegral.Add(typeof(byte));
			typeIntegral.Add(typeof(sbyte));
			typeIntegral.Add(typeof(short));
			typeIntegral.Add(typeof(ushort));
			typeIntegral.Add(typeof(int));
			typeIntegral.Add(typeof(uint));
			typeIntegral.Add(typeof(long));
			typeIntegral.Add(typeof(ulong));

			typeFloatingPoint.Add(typeof(float));
			typeFloatingPoint.Add(typeof(double));
			typeFloatingPoint.Add(typeof(decimal));

			typeIntegralAndFloatingPoint.AddRange(typeIntegral.Concat(typeFloatingPoint));

			GenericUtilities<T>.TypesPrimitiveIntegral = new ReadOnlyCollection<Type>(typeIntegral);
			GenericUtilities<T>.TypesPrimitiveFloatingPoint = new ReadOnlyCollection<Type>(typeFloatingPoint);
			GenericUtilities<T>.TypesPrimitiveIntegralAndFloatingPoint = new ReadOnlyCollection<Type>(typeIntegralAndFloatingPoint);

			GenericUtilities<T>.ThrowOnIncompatibleGenericType();

			GenericUtilities<T>.TypeOfT = typeof(T);

			if (GenericUtilities<T>.TypeOfT == typeof(byte)) { GenericUtilities<T>.SizeOfT = sizeof(byte); }
			else if (GenericUtilities<T>.TypeOfT == typeof(sbyte)) { GenericUtilities<T>.SizeOfT = sizeof(sbyte); }
			else if (GenericUtilities<T>.TypeOfT == typeof(short)) { GenericUtilities<T>.SizeOfT = sizeof(short); }
			else if (GenericUtilities<T>.TypeOfT == typeof(ushort)) { GenericUtilities<T>.SizeOfT = sizeof(ushort); }
			else if (GenericUtilities<T>.TypeOfT == typeof(int)) { GenericUtilities<T>.SizeOfT = sizeof(int); }
			else if (GenericUtilities<T>.TypeOfT == typeof(uint)) { GenericUtilities<T>.SizeOfT = sizeof(uint); }
			else if (GenericUtilities<T>.TypeOfT == typeof(long)) { GenericUtilities<T>.SizeOfT = sizeof(long); }
			else if (GenericUtilities<T>.TypeOfT == typeof(ulong)) { GenericUtilities<T>.SizeOfT = sizeof(ulong); }
			else if (GenericUtilities<T>.TypeOfT == typeof(float)) { GenericUtilities<T>.SizeOfT = sizeof(float); }
			else if (GenericUtilities<T>.TypeOfT == typeof(double)) { GenericUtilities<T>.SizeOfT = sizeof(double); }
			else if (GenericUtilities<T>.TypeOfT == typeof(decimal)) { GenericUtilities<T>.SizeOfT = sizeof(decimal); }
		}

		public static void ThrowOnIncompatibleGenericType ()
		{
			if (!GenericUtilities<T>.TypesPrimitiveIntegralAndFloatingPoint.Contains(typeof(T)))
			{
				throw (new GenericArgumentTypeException(GenericUtilities<T>.TypeOfT));
			}
		}
	}
}