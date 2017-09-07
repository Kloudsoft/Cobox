using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public sealed class FunctionToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public FunctionToken (Builder calculationBuilder, string text, int offSetLeft = 0)
			: base(TokenType.Function, calculationBuilder, text, offSetLeft)
		{
		}

		public override string Name { get { return ("Function"); } }
		public int ParameterCount { get { return (this.FunctionParameterListToken.Tokens.Count); } }
		public override bool ContainsFunction { get { return ((this.Tokens [1] as FunctionParameterListToken).ContainsFunction); } }
		public override bool ContainsParentheses { get { return ((this.Tokens [1] as FunctionParameterListToken).ContainsParentheses); } }
		public FunctionIndentifierToken FunctionIndentifierToken { get { return ((this.Tokens.Count < 1) ? null : this.Tokens [0] as FunctionIndentifierToken); } }
		public FunctionParameterListToken FunctionParameterListToken { get { return ((this.Tokens.Count < 2) ? null : this.Tokens [1] as FunctionParameterListToken); } }

		protected override bool OnParse ()
		{
			int count = 0;
			int indexEnd = 0;
			string text = "";
			bool result = false;
			string pattern = "";
			MethodInfo method = null;
			Token tokenIdentifier = null;
			Match matchIdentifier = null;
			string matchParameterList = "";
			MatchCollection matches = null;
			List<object> attributes = null;
			Token tokenParameterList = null;
			List<ParameterInfo> parameters = null;
			CustomMethodAttribute attribute = null;

			text = this.Text;

			pattern = @"\#[A-Za-z_]+[A-Za-z0-9_]*";
			matches = Regex.Matches(text, pattern, RegexOptions.Singleline);
			if ((matches != null) && (matches.Count > 0) && (matches [0].Success) && (matches [0].Index == 0))
			{
				matchIdentifier = matches [0];

				text = text.Substring(matchIdentifier.Length);

				if (text.Trim().StartsWith("("))
				{
					indexEnd = -1;
					for (int i = 0; i < text.Length; i++)
					{
						if (text [i] == '(')
						{
							count++;
						}
						else if (text [i] == ')')
						{
							count--;

							if (count < 0)
							{
								break;
							}
							else if (count == 0)
							{
								indexEnd = i + 1;

								break;
							}
						}
					}

					while (this.ContainsDoubleParentheses(text))
					{
						indexEnd -= 2;
						text = text.Substring(1, text.Length - 2);
					}

					if (indexEnd > 0)
					{
						result = true;

						matchParameterList = text.Substring(0, indexEnd);

						tokenIdentifier = new FunctionIndentifierToken(this.Builder, matchIdentifier.Value);
						this.AddToken(tokenIdentifier);
						result &= tokenIdentifier.Parse();

						tokenParameterList = new FunctionParameterListToken(this.Builder, matchParameterList);
						this.AddToken(tokenParameterList);
						result &= tokenParameterList.Parse();

						if (tokenParameterList.Tokens.Count == 0)
						{
							return (result);
							//throw (new CalculationException(""));
						}

						if (result)
						{
							result = false;

							// TODO: Implement method overloading.
							method = Method<double>.Methods.FirstOrDefault(m => (m.Name == this.FunctionIndentifierToken.Text.Substring(1)));
							if (method == null) { method = Method<double>.Methods.FirstOrDefault(m => (m.Name.ToLower() == this.FunctionIndentifierToken.Text.Substring(1).ToLower())); }

							if (method == null)
							{
								throw (new ExecutionException("The method [" + this.FunctionIndentifierToken.Text + "] was not recognized."));
							}
							else
							{
								parameters = method.GetParameters().ToList();
								attributes = method.GetCustomAttributes(typeof(CustomMethodAttribute), true).ToList();

								if (attributes.Any())
								{
									attribute = attributes.First() as CustomMethodAttribute;

									if (attribute.HasParamArray)
									{
										if (tokenParameterList.Tokens.Count >= attribute.MinimumNumberOfArguments)
										{
											result = true;
										}
										else
										{
											throw (new ParserException("The method [" + method.Name + "] expects at least " + attribute.MinimumNumberOfArguments.ToString() + " argument(s) but only [" + tokenParameterList.Tokens.Count.ToString() + "] argument(s) were supplied."));
										}
									}
									else
									{
										if (tokenParameterList.Tokens.Count == attribute.MinimumNumberOfArguments)
										{
											result = true;
										}
										else
										{
											throw (new ParserException("The method [" + method.Name + "] expects exactly " + attribute.MinimumNumberOfArguments.ToString() + " argument(s) but [" + tokenParameterList.Tokens.Count.ToString() + "] argument(s) were supplied."));
										}
									}
								}
								else
								{
									if (tokenParameterList.Tokens.Count == parameters.Count)
									{
										result = true;
									}
									else
									{
										throw (new ParserException("The method call to [" + method.Name + "] expects " + tokenParameterList.Tokens.Count.ToString() + " paremeter(s) whereas the supplied number of parameter(s) is " + parameters.Count.ToString() + "."));
									}
								}
							}
						}
					}
					else
					{
						throw (new ParserException("An appropriate closing parentheses for the function [" + matchIdentifier.Value + "] was not found."));
					}
				}
				else
				{
					throw (new ParserException("Opening parentheses for the function [" + matchIdentifier.Value + "] was not found."));
				}
			}

			return (result);
		}

		protected override bool OnCalculate (out double value, out Exception exception, int level = 0)
		{
			bool result = false;
			MethodInfo method = null;
			List<object> attributes = null;
			List<object> parameterObjects = null;
			List<ParameterInfo> parameters = null;
			CustomMethodAttribute attribute = null;
			List<double> variableParameterObjects = null;

			this.Value = 0;
			exception = null;
			value = double.NaN;

			for (int i = 0; i < this.FunctionParameterListToken.Tokens.Count; i++)
			{
				this.FunctionParameterListToken.Tokens [i].Calculate(out value, out exception, level + 1);
			}

			// TODO: Implement method overloading.
			method = Method<double>.Methods.FirstOrDefault(m => (m.Name == this.FunctionIndentifierToken.Text.Substring(1)));
			if (method == null) { method = Method<double>.Methods.FirstOrDefault(m => (m.Name.ToLower() == this.FunctionIndentifierToken.Text.Substring(1).ToLower())); }

			if (method == null)
			{
				throw (new ExecutionException("The method [" + this.FunctionIndentifierToken.Text + "] was not recognized."));
			}
			else
			{
				parameterObjects = new List<object>();
				parameters = method.GetParameters().ToList();
				variableParameterObjects = new List<double>();
				attributes = method.GetCustomAttributes(typeof(CustomMethodAttribute), true).ToList();

				if (attributes.Any())
				{
					attribute = attributes.First() as CustomMethodAttribute;

					if (this.FunctionParameterListToken.Tokens.Count < attribute.MinimumNumberOfArguments)
					{
						throw (new ExecutionException("The method [" + method.Name + "] expects at least [" + attribute.MinimumNumberOfArguments.ToString() + "] arguments."));
					}

					if (attribute.HasFixedArguments)
					{
						for (int i = 0; i < attribute.NumberOfFixedArguments; i++)
						{
							parameterObjects.Add(this.FunctionParameterListToken.Tokens [i].Value);
						}
					}

					if (attribute.HasParamArray)
					{
						for (int i = attribute.NumberOfFixedArguments; i < this.FunctionParameterListToken.Tokens.Count; i++)
						{
							variableParameterObjects.Add(this.FunctionParameterListToken.Tokens [i].Value);
						}

						parameterObjects.Add(variableParameterObjects.ToArray());
					}
				}
				else
				{
					parameterObjects.AddRange(this.FunctionParameterListToken.Tokens.ToList().ConvertAll<object>(p => p.Value));
				}

				this.Value = value = (double) method.Invoke(null, parameterObjects.ToArray());

				result = true;
			}

			return (result);
		}

		protected override void OnGenerateRandom (Random random, int depth)
		{
			int count = 0;
			Token token = null;
			MethodInfo method = null;
			List<object> attributes = null;
			List<string> parameterValues = null;
			List<ParameterInfo> parameters = null;
			CustomMethodAttribute attribute = null;

			parameterValues = new List<string>();
			method = Method<double>.Methods [random.Next(0, Method<double>.Methods.Count)];
			//do { method = Method<double>.Methods [random.Next(0, Method<double>.Methods.Count)]; } while (method.DeclaringType != typeof(Method<double>));

			parameters = method.GetParameters().ToList();
			attributes = method.GetCustomAttributes(typeof(CustomMethodAttribute), true).ToList();

			if (attributes.Any())
			{
				attribute = attributes.First() as CustomMethodAttribute;

				if (attribute.HasFixedArguments)
				{
					for (int i = 0; i < attribute.NumberOfFixedArguments; i++)
					{
						token = new ParenthesesToken(this.Builder, "");

						token.GenerateRandomTree(random, depth - 1);

						parameterValues.Add(token.Text);
						//parameterValues.Add(token.GetText());
					}
				}

				if (attribute.HasParamArray)
				{
					count = random.Next(0, 5);

					for (int i = 0; i < count; i++)
					{
						token = new ParenthesesToken(this.Builder, "");

						token.GenerateRandomTree(random, depth - 1);

						parameterValues.Add(token.Text);
						//parameterValues.Add(token.GetText());
					}
				}
			}
			else
			{
				foreach (var parameter in parameters)
				{
					token = new ParenthesesToken(this.Builder, "");

					token.GenerateRandomTree(random, depth - 1);

					parameterValues.Add(token.Text);
					//parameterValues.Add(token.GetText());
				}
			}

			this.AddToken(new FunctionIndentifierToken(this.Builder, "#" + method.Name));
			this.AddToken(new FunctionParameterListToken(this.Builder, "(" + string.Join(", ", parameterValues) + ")"));

			//foreach (var parameter in parameterValues)
			//{
			//    var param = new FunctionParameterToken(this.Builder, "(" + parameter + ")");
			//    var paren = new ParenthesesToken(this.Builder, "(" + param.Text + ")");

			//    this.FunctionParameterListToken.AddToken(param);
			//    param.AddToken(paren);
			//    this.AddToken(param);
			//}

			// TODO: Add tokens manually instead of attempting to compose-then-parse.
			this.Text = "#" + method.Name + "(" + string.Join(", ", parameterValues) + ")";

			//while (this.ContainsDoubleParentheses(this.Text))
			//{
			//	this.Text = this.Text.Substring(1, this.Text.Length - 2);
			//}

			this.Parse();
		}

		public bool ContainsDoubleParentheses (string text)
		{
			int index = 0;
			Stack<int> stack = null;

			if (text.Trim().StartsWith("((") && text.Trim().EndsWith("))"))
			{
				if (text.Length == 4)
				{
					return (true);
				}
				else
				{
					stack = new Stack<int>();

					for (int i = 0; i < text.Length; i++)
					{
						if (text [i] == '(')
						{
							stack.Push(i);
						}
						else if (text [i] == ')')
						{
							index = stack.Pop();

							if (((index + 1) == 2) && (i == (text.Length - 2))) { return (true); }
						}
					}
				}
			}

			return (false);
		}

		public override string GetText ()
		{
			string text = "";

			text += (this.FunctionIndentifierToken == null) ? "" : this.FunctionIndentifierToken.GetText();
			text += (this.FunctionParameterListToken == null) ? "" : this.FunctionParameterListToken.GetText();

			return (text);
		}

		public static bool TryExtract (string text, int indexStart, out int indexEnd)
		{
			int count = 0;
			Match match = null;
			bool result = false;
			string pattern = "";
			MatchCollection matches = null;

			indexEnd = -1;

			// #Function([p1[,p2[,1]]])
			//	Unlimited number of parameters short-circuited only by the ')' token.
			pattern = @"\#[A-Za-z_]+[A-Za-z0-9_]*\(\S*\)";
			matches = Regex.Matches(text, pattern, RegexOptions.Singleline);
			if ((matches != null) && (matches.Count > 0) && (matches [0].Success) && (matches [0].Index == 0))
			{
				match = matches [0];

				count = 0;
				for (int i = 0; i < match.Length; i++)
				{
					if (match.Value [i] == '(')
					{
						count++;
					}
					else if (match.Value [i] == ')')
					{
						count--;

						if (count == 0)
						{
							indexEnd = i + 1;

							break;
						}
					}
				}

				result = (indexEnd >= 0);
			}

			return (result);
		}
	}
}