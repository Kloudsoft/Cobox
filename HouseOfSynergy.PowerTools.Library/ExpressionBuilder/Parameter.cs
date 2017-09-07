using System;
using System.Linq;
using System.Reflection;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public class Parameter<T>
	{
		public string Name { get; private set; }
		public ParameterInfo ParameterInfo { get; private set; }
		public Type Type { get; private set; }
		public T Value { get; private set; }

		public Parameter (ParameterInfo parameter)
		{
			this.Name = parameter.Name;
			this.ParameterInfo = parameter;
			this.Type = parameter.ParameterType;
			this.Value = default(T);
		}
	}
}