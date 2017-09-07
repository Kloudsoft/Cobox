using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Generics
{
	public class GenericArgumentTypeException:
		ArgumentException
	{
		public Type Type { get; private set; }

		public GenericArgumentTypeException (Type type) : base() { this.Type = type; }
		public GenericArgumentTypeException (Type type, string message) : base(message) { this.Type = type; }
		public GenericArgumentTypeException (Type type, string message, Exception innerException) : base(message, innerException) { this.Type = type; }
		public GenericArgumentTypeException (Type type, string message, string paramName) : base(message, paramName) { this.Type = type; }
		public GenericArgumentTypeException (Type type, string message, string paramName, Exception innerException) : base(message, paramName, innerException) { this.Type = type; }
	}

	public class GenericArgumentTypeException<T>:
		ArgumentException
	{
		public Type Type { get; private set; }

		public GenericArgumentTypeException () : base() { this.Type = typeof(T); }
		public GenericArgumentTypeException (string message) : base(message) { this.Type = typeof(T); }
		public GenericArgumentTypeException (string message, Exception innerException) : base(message, innerException) { this.Type = typeof(T); }
		public GenericArgumentTypeException (string message, string paramName) : base(message, paramName) { this.Type = typeof(T); }
		public GenericArgumentTypeException (string message, string paramName, Exception innerException) : base(message, paramName, innerException) { this.Type = typeof(T); }
	}
}