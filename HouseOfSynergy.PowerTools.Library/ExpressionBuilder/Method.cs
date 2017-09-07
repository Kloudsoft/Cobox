using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public static class Method<T>
	{
		public static ReadOnlyCollection<MethodInfo> Methods { get; private set; }

		static Method ()
		{
			HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Method<T>.Methods = HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Method<T>.GetCompatibleMethods().AsReadOnly();
		}

		private static List<MethodInfo> GetCompatibleMethods ()
		{
			Type type = null;
			bool result = false;
			List<MethodInfo> methods = null;
			List<object> attributes = null;
			List<ParameterInfo> parameters = null;
			CustomMethodAttribute attribute = null;

			methods = new List<MethodInfo>();

			type = typeof(System.Math);
			foreach (var method in type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToList())
			{
				if (method.ReturnType == typeof(T))
				{
					parameters = method.GetParameters().ToList();

					if (parameters.All(parameter => parameter.ParameterType == typeof(T)))
					{
						methods.Add(method);
					}
				}
			}

			try
			{
				type = typeof(Method<T>);
				foreach (var method in type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).ToList())
				{
					attributes = method.GetCustomAttributes(typeof(CustomMethodAttribute), true).ToList();

					if (attributes.Any())
					{
						attribute = attributes [0] as CustomMethodAttribute;

						if (method.ReturnType == typeof(T))
						{
							parameters = method.GetParameters().ToList();

							if (attribute.HasParamArray)
							{
								if (attribute.TotalNumberOfArguments == parameters.Count)
								{
									result = true;

									if (attribute.NumberOfFixedArguments > 0)
									{
										for (int i = 0; i < attribute.NumberOfFixedArguments; i++)
										{
											if (parameters [i].ParameterType != typeof(T))
											{
												result = false;

												break;
											}
										}
									}

									if (result)
									{
										if (parameters.Last().ParameterType == typeof(T []))
										{
											if (method.MethodHasParameterArray())
											{
												methods.Add(method);
											}
										}
									}
								}
								else
								{
									throw (new InvalidOperationException("The number of fixed parameters specified in the [CustomMethod] attribute [" + attribute.NumberOfFixedArguments.ToString() + "] and the number of actual parameters found [" + parameters.Count.ToString() + "] do not match for method " + method.Name + "."));
								}
							}
							else
							{
								if (attribute.NumberOfFixedArguments == parameters.Count)
								{
									if (parameters.All(parameter => parameter.ParameterType == typeof(T)))
									{
										methods.Add(method);
									}
								}
								else
								{
									throw (new InvalidOperationException("The number of fixed parameters specified in the [CustomMethod] attribute [" + attribute.NumberOfFixedArguments.ToString() + "] and the number of actual parameters found [" + parameters.Count.ToString() + "] do not match for method " + method.Name + "."));
								}
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				throw (new System.InvalidOperationException("HouseOfSynergy.PowerTools.Library.CalculationBuilder.Method<" + typeof(T).Name + ">.GetCompatibleMethods generated an error. See inner exception for details.", exception));
			}

			methods = methods.OrderBy(m => m.Name).ThenBy(m => m.GetParameters().Count()).ToList();

			return (methods);
		}

		[CustomMethod(numberOfFixedArguments : 1, hasParamArray : true)]
		public static double Average (double value, params double [] values) { return (values.ToList().Concat(new double [] { value }).Average()); }

		[CustomMethod(numberOfFixedArguments : 1)]
		public static double Factorial (double number)
		{
			long value = 1;
			long target = (long) number;

			if (number < 0)
			{
				return (double.NaN);
			}
			else if (target == 0)
			{
				value = 1;
			}
			else
			{
				try
				{
					while (target > 0)
					{
						value *= target--;
					}
				}
				catch
				{
					return (double.NaN);
				}
			}

			return (value);
		}
	}
}