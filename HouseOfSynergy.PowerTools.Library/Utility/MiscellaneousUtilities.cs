using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class MiscellaneousUtilities
	{
		public static T ThrowOnNull<T> (T @object, string parameterName)
			where T : class
		{
			if (@object == null)
			{
				throw (new ArgumentNullException(parameterName));
			}

			return (@object);
		}

		public static T ThrowOnNull<T> (ref T @object, string parameterName)
			where T : class
		{
			if (@object == null)
			{
				throw (new ArgumentNullException(parameterName));
			}

			return (@object);
		}

		public static T ThrowOnDefault<T> (T @object, string parameterName)
			where T : struct
		{
			if (!EqualityComparer<T>.Default.Equals(@object, default(T)))
			//if (@object == default(T))
			{
				throw (new ArgumentNullException(parameterName));
			}

			return (@object);
		}

		public static bool IsDefault<T> (this T value)
			where T : struct
		{
			return (!EqualityComparer<T>.Default.Equals(value, default(T)));
		}

		//public static bool IsDefault<T> (this T value)
		//	where T : struct, IEquatable<T>
		//{
		//	return (value.Equals(default(T)));
		//}

		public static T Min<T> (T value1, T value2)
			where T: struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
		{
			return (value1.CompareTo(value2) < 0 ? value1 : value2);
		}

		public static T Max<T> (T value1, T value2)
			where T: struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
		{
			return (value1.CompareTo(value2) > 0 ? value1 : value2);
		}

		public static T Between<T> (T min, T max, T value)
			where T: struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
		{
			if (min.CompareTo(max) > 0) { var temp = min; min = max; max = temp; }

			if (value.CompareTo(min) < 0) { return (min); }
			if (value.CompareTo(max) > 0) { return (max); }

			return (value);
		}

		public static bool IsBetween<T> (T min, T max, T value)
			where T: struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
		{
			if (min.CompareTo(max) > 0) { var temp = min; min = max; max = temp; }

			return ((value.CompareTo(min) >= 0) && (value.CompareTo(max) <= 0));
		}

		public static int GetProcessorCountPhysical ()
		{
			int count = 0;

			foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
			{
				count += (int) item ["NumberOfProcessors"];
			}

			return (count);
		}

		public static int GetProcessorCoreCountLogical ()
		{
			return (Environment.ProcessorCount);
		}

		public static int GetProcessorCoreCountPhysical ()
		{
			int count = 0;

			foreach (var item in new ManagementObjectSearcher("Select * from Win32_Processor").Get())
			{
				count += int.Parse(item ["NumberOfCores"].ToString());
			}

			return (count);
		}
	}
}