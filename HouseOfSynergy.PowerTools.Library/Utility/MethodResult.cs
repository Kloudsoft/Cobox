using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public class MethodResult<TEnum>
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		public bool IsSuccess { get; }
		public bool IsFailure => !this.IsSuccess;
		public string ErrorMessage { get; }
		public TEnum? ErrorType { get; }

		protected internal MethodResult (bool isSuccess, TEnum errorType, string errorMessage)
		{
			EnumUtilities.ThrowOnNonEnum<TEnum>();

			this.IsSuccess = isSuccess;
			this.ErrorMessage = errorMessage;
		}

		public static MethodResult<TEnum> Ok ()
		{
			return (new MethodResult<TEnum>(isSuccess: true, errorType: default(TEnum), errorMessage: ""));
		}

		public static MethodResult<TEnum, TValueType> Ok<TValueType> (TValueType value)
		{
			return (new MethodResult<TEnum, TValueType>(value: value, isSuccess: true, errorType: default(TEnum), errorMessage: ""));
		}

		public static MethodResult<TEnum> Fail (TEnum errorType, string errorMessage) { return (new MethodResult<TEnum>(isSuccess: false, errorType: errorType, errorMessage: errorMessage)); }
		public static MethodResult<TEnum, TValuetype> Fail<TValuetype> (TValuetype value, TEnum errorType, string message) { return (new MethodResult<TEnum, TValuetype>(value: default(TValuetype), isSuccess: false, errorType: errorType, errorMessage: message)); }
	}

	public class MethodResult<TEnum, TValue>:
		MethodResult<TEnum>
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		private readonly TValue _Value = default(TValue);

		protected internal MethodResult (TValue value, bool isSuccess, TEnum errorType, string errorMessage)
			: base(isSuccess, errorType, errorMessage)
		{
			this._Value = value;
		}

		public TValue Value
		{
			get
			{
				if (!this.IsSuccess) { throw (new InvalidOperationException()); }

				return (this._Value);
			}
		}
	}
}