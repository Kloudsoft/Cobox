using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class CustomMethodAttribute:
		Attribute
	{
		public enum EnumArgumentsType { None, Fixed, Variable, Both }

		public bool HasParamArray { get; protected set; }
		public int NumberOfFixedArguments { get; protected set; }

		public CustomMethodAttribute ()
			: this(0)
		{
		}

		public CustomMethodAttribute (int numberOfFixedArguments)
		{
			if (numberOfFixedArguments < 0)
			{
				throw (new ArgumentException("The argument [numberOfFixedArguments] cannot be a negative number."));
			}

			this.HasParamArray = false;
			this.NumberOfFixedArguments = numberOfFixedArguments;
		}

		public CustomMethodAttribute (bool hasParamArray)
			: this(0)
		{
			this.HasParamArray = hasParamArray;
		}

		public CustomMethodAttribute (int numberOfFixedArguments, bool hasParamArray)
			: this(numberOfFixedArguments)
		{
			this.HasParamArray = hasParamArray;
		}

		public bool HasFixedArguments { get { return (this.NumberOfFixedArguments > 0); } }
		public int MinimumNumberOfArguments { get { return (this.NumberOfFixedArguments); } }
		public int TotalNumberOfArguments { get { return (this.HasParamArray ? (this.NumberOfFixedArguments + 1) : (this.NumberOfFixedArguments)); } }
	}
}