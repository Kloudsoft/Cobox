using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Generics
{
	public class GenericArgumentTypesException:
		ArgumentException
	{
		private List<Type> _Types { get; set; }
		public ReadOnlyCollection<Type> Types { get; private set; }

		private GenericArgumentTypesException (string message, string paramName, Exception innerException) : base(message, paramName, innerException) { this._Types = new List<Type>(); this.Types = new ReadOnlyCollection<Type>(this._Types); }

		public GenericArgumentTypesException (IEnumerable<Type> types) : this(types, "", "", null) { }
		public GenericArgumentTypesException (IEnumerable<Type> types, string message) : this(types, message, "", null) { }
		public GenericArgumentTypesException (IEnumerable<Type> types, string message, Exception innerException) : this(types, message, "", innerException) { }
		public GenericArgumentTypesException (IEnumerable<Type> types, string message, string paramName) : this(types, message, paramName, null) { }

		public GenericArgumentTypesException (IEnumerable<Type> types, string message, string paramName, Exception innerException)
			: this(message, paramName, innerException)
		{
			this._Types = new List<Type>();
			this.Types = new ReadOnlyCollection<Type>(this._Types);

			this._Types.AddRange(types);
		}
	}
}