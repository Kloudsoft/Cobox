using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public sealed class FunctionParameterListToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public FunctionParameterListToken (Builder calculationBuilder, string text, int offSetLeft = 0)
			: base(TokenType.FunctionParameterList, calculationBuilder, text, offSetLeft)
		{
		}

		public override string Name { get { return ("Function Parameter List"); } }
		public override bool ContainsFunction { get { return (this.Tokens.Any(t => t.Type == TokenType.Function)); } }
		public override bool ContainsParentheses { get { return (this.Tokens.Any(t => t.Type == TokenType.Parentheses)); } }

		protected override bool OnParse ()
		{
			int count = 0;
			int indexEnd = 0;
			int indexStart = 0;
			Token token = null;
			bool result = false;
			string pattern = "";
			List<string> parameters = null;
			MatchCollection matches = null;

			pattern = @"\(\S*\)";
			matches = Regex.Matches(this.Text, pattern, RegexOptions.Singleline);
			if ((matches != null) && (matches.Count > 0) && (matches [0].Success) && (matches [0].Index == 0))
			{
				this.Text = this.Text.Substring(1, this.Text.Length - 2);

				if (this.Text.Length == 0)
				{
					// No parameters.

					result = true;
				}
				else
				{
					if (!this.Text.Contains(","))
					{
						// Single parameter.

						token = new FunctionParameterToken(this.Builder, this.Text, 0);
						this.AddToken(token);
						result = token.Parse();
					}
					else
					{
						// Multiple parameters.

						if (this.Text.Contains(",,"))
						{
							throw (new ParserException("Function parameter list contains empty paremeter delimiters."));
						}
						else
						{
							// Break comma delimited parameters such that nested parenthesis or functions do not interfere.

							parameters = new List<string>();

							indexStart = indexEnd = 0;
							for (int i = 0; i < this.Text.Length; i++)
							{
								if (this.Text [i] == '(')
								{
									count++;
								}
								else if (this.Text [i] == ')')
								{
									count--;
								}

								if ((this.Text [i] == ',') && (count == 0))
								{
									indexEnd = i;

									parameters.Add(this.Text.Substring(indexStart, indexEnd - indexStart));

									indexStart = i + 1;
									indexEnd = indexStart;
								}

								if ((this.Text [i] == ')') && (count == -1))
								{
									indexEnd = i;

									parameters.Add(this.Text.Substring(indexStart, indexEnd - indexStart));

									indexStart = i + 1;
									indexEnd = indexStart;
								}
							}

							if (count == 0)
							{
								if (indexStart < this.Text.Length)
								{
									parameters.Add(this.Text.Substring(indexStart, this.Text.Length - indexStart));
								}
							}

							result = true;
							foreach (var parameter in parameters)
							{
								token = new FunctionParameterToken(this.Builder, parameter);
								this.AddToken(token);
								result &= token.Parse();
							}
						}
					}
				}
			}

			return (result);
		}

		public override string GetText ()
		{
			return ("(" + string.Join(", ", this.Tokens.ToList().ConvertAll<string>(p => p.GetText())) + ")");
		}

		public static bool TryExtract (string text, int indexStart, out Match match)
		{
			bool result = false;
			string pattern = "";
			MatchCollection matches = null;

			match = null;

			// #Function([p1[,p2[,1]]])
			//	Unlimited number of parameters short-circuited only by the ')' token.
			pattern = @"\#[A-Za-z_]+[A-Za-z0-9_]*\(\S*\)";
			matches = Regex.Matches(text, pattern, RegexOptions.Singleline);

			if ((matches != null) && (matches.Count > 0) && (matches [0].Success) && (matches [0].Index == 0))
			{
				match = matches [0];

				if (match.Value.Contains(",,"))
				{
					throw (new ParserException("Function parameter list contains empty paremeter delimiters."));
				}
				else
				{
					result = true;
				}
			}

			return (result);
		}
	}
}