using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public sealed class GenericToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public GenericToken (Builder calculationBuilder, string text, int offSetLeft = 0) : base(TokenType.Generic, calculationBuilder, text, offSetLeft) { }

		public override string Name { get { return ("Generic Expression"); } }

		protected override bool OnParse ()
		{
			int indexEnd = 0;
			string text = "";
			Token token = null;
			bool result = false;
			System.Text.RegularExpressions.Match match = null;

			result = true;

			text = this.Text;

			try
			{
				if (text.Trim().Length == 0)
				{
					result = true;
				}
				else
				{
					if (OperatorToken.IsCharacterOperator(text [text.Length - 1]))
					{
						//throw (new ParseException("Trailing operator found [" + text [text.Length - 1] + "] without any following operands."));
					}

					while (text.Length > 0)
					{
						while (OperatorToken.TryExtract(text, 0, out indexEnd))
						{
							if (this.Tokens.Count == 0)
							{
								if (OperatorToken.IsCharacterNegationOperator(text [0])) { token = new NegationOperatorToken(this.Builder, text.Substring(0, indexEnd - 0), 0); }
								else { token = new OperatorToken(this.Builder, text.Substring(0, indexEnd - 0), 0); }
							}
							else
							{
								if (OperatorToken.IsCharacterNegationOperator(text [0]))
								{
									if (this.Tokens.Last().IsOperator) { token = new NegationOperatorToken(this.Builder, text.Substring(0, indexEnd - 0), 0); }
									else if (this.Tokens.Last().Type == TokenType.NegationOperator) { token = new NegationOperatorToken(this.Builder, text.Substring(0, indexEnd - 0), 0); }
									else { token = new OperatorToken(this.Builder, text.Substring(0, indexEnd - 0), 0); }
								}
								else if (OperatorToken.IsCharacterNonNegationOperator(text [0]))
								{
									if (this.Tokens.Last().Type == TokenType.Operator) { throw (new ParserException("Consecutive operators encountered.")); }
									else { token = new OperatorToken(this.Builder, text.Substring(0, indexEnd - 0), 0); }
								}
								else
								{
									throw (new NotImplementedException("An operator was encountered that is niether negation nor non-negation."));
								}
							}

							result &= token.Parse();
							result &= token.Compile();
							if (result) { this.AddToken(token); text = text.Substring(indexEnd); }

							//if ((this.Tokens.Count > 0) && (this.Tokens.Last().Type == TokenType.NegationOperator) && ((this.Tokens.Last() as NegationOperatorToken).CanSwallow) && ((token.IsOperator) || (token.Type == TokenType.NegationOperator)))
							//{
							//    result &= token.Parse();
							//    result &= token.Compile();
							//    if (result) { (this.Tokens.Last() as NegationOperatorToken).SwallowToken(token); text = text.Substring(indexEnd); }
							//}
							//else
							//{
							//    result &= token.Parse();
							//    result &= token.Compile();
							//    if (result) { this.AddToken(token); text = text.Substring(indexEnd); }
							//}
						}

						if (ParenthesesToken.TryExtract(text, 0, out indexEnd))
						{
							token = new ParenthesesToken(this.Builder, text.Substring(0, indexEnd - 0), 0);
						}
						else if (ConstantToken.TryExtract(text, 0, out indexEnd))
						{
							token = new ConstantToken(this.Builder, text.Substring(0, indexEnd - 0), 0);
						}
						else if (VariableToken.TryExtract(text, 0, out match))
						{
							indexEnd = match.Index + match.Length;
							token = new VariableToken(this.Builder, match.Value, match.Index);
						}
						else if (FunctionToken.TryExtract(text, 0, out indexEnd))
						{
							token = new FunctionToken(this.Builder, text.Substring(0, indexEnd - 0), 0);
						}
						else
						{
							result = false;
							throw (new ParserException("The token was not recognized staring at [" + text + "].", this.OffsetLeft));
						}

						result &= token.Parse();
						result &= token.Compile();
						if (result) { this.AddToken(token); text = text.Substring(indexEnd); }

						////if ((this.Tokens.Count > 0) && (this.Tokens.Last().Type == TokenType.NegationOperator))
						//if ((this.Tokens.Count > 0) && (this.Tokens.Last().Type == TokenType.NegationOperator) && ((this.Tokens.Last() as NegationOperatorToken).CanSwallow))// && ((token.IsOperator) || (token.Type == TokenType.NegationOperator)))
						//{
						//    result &= token.Parse();
						//    result &= token.Compile();
						//    if (result) { (this.Tokens.Last() as NegationOperatorToken).SwallowToken(token); text = text.Substring(indexEnd); }
						//}
						//else
						//{
						//    result &= token.Parse();
						//    result &= token.Compile();
						//    if (result) { this.AddToken(token); text = text.Substring(indexEnd); }
						//}

						if (result)
						{
							// Detect consecutive operands without operator.
							//if (this.Tokens.Count > 1)
							//{
							//    if (this.Tokens [this.Tokens.Count - 2].IsOperand)
							//    {
							//        throw (new ParseException("Consecutive operands found [" + this.Tokens [this.Tokens.Count - 2].Text + ", " + this.Tokens [this.Tokens.Count - 1].Text + "] without any operators in between."));
							//    }
							//}
						}

						if ((!result) && (text.Length > 0))
						{
							throw (new ParserException("Invalid token encountered. Reason ambiguous.", this.OffsetLeft));
						}
					}
				}
			}
			catch (Exception exception)
			{
				//throw;
				this.AddException(exception);
			}

			return (result);
		}

		protected override bool OnCalculate (out double value, out Exception exception, int level = 0)
		{
			throw (new ExecutionException(";lsajfa;sdkjsd;klfj"));
			//bool result = false;

			//exception = null;
			//value = double.NaN;

			//if (this.Tokens.Count == 1)
			//{
			//    result = this.Tokens [0].Calculate(out value, out exception, level + 1);
			//}
			//else
			//{
			//    throw (new CalculationException("A generic token contains an unexpected number of tokens [" + this.Tokens.Count.ToString() + "]."));
			//}

			//return (result);
		}

		public override string GetText ()
		{
			return (this.Text);
		}
	}
}