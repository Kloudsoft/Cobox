using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public class ParenthesesToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public bool AllowEmptyParentheses { get; private set; }

		public ParenthesesToken (Builder calculationBuilder, string text, int offSetLeft = 0) : base(TokenType.Parentheses, calculationBuilder, text, offSetLeft) { }

		public ParenthesesToken (Builder calculationBuilder, string text, bool allowEmptyParentheses, int offSetLeft = 0) : base(TokenType.Parentheses, calculationBuilder, text, offSetLeft) { this.AllowEmptyParentheses = allowEmptyParentheses; }

		public ParenthesesToken (Builder calculationBuilder, string text, bool allowEmptyParentheses, bool skipParseStage, bool skipCompileStage = false, int offSetLeft = 0) : base(TokenType.Parentheses, calculationBuilder, text, offSetLeft) { this.AllowEmptyParentheses = allowEmptyParentheses; this.SkipParseStage = skipParseStage; this.SkipCompileStage = skipCompileStage; }

		public override string Name { get { return ("Parentheses"); } }

		protected override bool OnParse ()
		{
			string text = "";
			Token token = null;
			bool result = false;

			if (this.Text == null) { throw (new ParserException("Token cannot be null.")); }
			if (!this.Text.StartsWith("(")) { throw (new ParserException("Token be in the form '(Expression)'.", this.OffsetLeft)); }
			if (!this.Text.EndsWith(")")) { throw (new ParserException("Token be in the form '(Expression)'.", this.OffsetLeft)); }
			if (this.Text.Length < 3) { throw (new ParserException("Token must be at least three characters long e,g, (1).", this.OffsetLeft)); }

			text = this.Text;
			text = this.Text.Substring(1, text.Length - 2).Trim();

			if (text.Trim().Length == 0)
			{
				if (!this.AllowEmptyParentheses) { if (text.Length == 0) { throw (new ParserException("Empty parentheses token detected.", this.OffsetLeft)); } }
			}
			else
			{
				token = new GenericToken(this.Builder, text, 0);
				this.AddToken(token);
				result = token.Parse();
			}

			return (result);
		}

		protected override bool OnCompile (out Exception exception)
		{
			bool result = false;

			exception = null;

			result = true;
			foreach (var token in this.Tokens)
			{
				result &= token.Compile();
			}

			return (result);
		}

		protected override bool OnCalculate (out double value, out Exception exception, int level = 0)
		{
			int index = 0;
			Token token = null;
			bool result = false;
			Token parentheses = null;

			exception = null;
			value = double.NaN;

			result = true;
			this.Value = double.NaN;

			for (int i = 0; i < this.Tokens.Count; i++)
			{
				switch (this.Tokens [i].Type)
				{
					case TokenType.Constant:
					case TokenType.Function:
					case TokenType.NegationOperator:
					case TokenType.Variable:
					{
						result &= this.Tokens [i].Calculate(out value, out exception);
						if (!result) { throw (exception); }

						break;
					}
					case TokenType.Parentheses:
					{
						if (this.Tokens [i].Tokens.All(t => ((t.IsOperator) || (t.Type == TokenType.Constant))))
						{
							result &= this.Tokens [i].Calculate(out value, out exception);
							if (!result) { throw (exception); }
						}

						break;
					}
				}
			}

			if (!result)
			{
			}
			else
			{
				result = false;
				if (this.Tokens.Count == 0)
				{
					result = true;
					this.Value = value = 0;
				}
				else if (this.Tokens.Count == 1)
				{
					if (this.Tokens [0].IsOperand)
					{
						result = this.Tokens [0].Calculate(out value, out exception);
						if (result) { this.Value = value; }
						else { throw (exception); }
					}
					else
					{
						throw (new ExecutionException("A parentheses with one tokens does not contain a valid operand."));
					}
				}
				else if (this.Tokens.Count == 2)
				{
					var operand1 = this.Tokens [0];
					var operand2 = this.Tokens [1];

					if (operand1.Type == TokenType.NegationOperator)
					{
						if (operand2.Type == TokenType.NegationOperator)
						{
							value = 0;
							value += operand1.Value;
							value += operand2.Value;
							this.Value = value;

							result = true;
						}
						else
						{
							throw (new ExecutionException("A parentheses with two tokens was encountered."));
						}
					}
					else
					{
						if (operand2.Type == TokenType.NegationOperator)
						{
							value = 0;
							value += operand1.Value;
							value += operand2.Value;
							this.Value = value;

							result = true;
						}
						else
						{
							throw (new ExecutionException("A parentheses with two tokens was encountered."));
						}
					}
				}
				else if (this.Tokens.Count == 3)
				{
					var operand1 = this.Tokens [0];
					var op = this.Tokens [1];
					var operand2 = this.Tokens [2];

					if ((operand1.IsOperand) && (op.IsOperator) && (operand2.IsOperand))
					{
						if (operand1.Type == TokenType.Parentheses) { operand1.Calculate(out value, out exception); }
						if (operand2.Type == TokenType.Parentheses) { operand2.Calculate(out value, out exception); }

						switch ((op as OperatorToken).OperatorType)
						{
							case OperatorType.Addition: { this.Value = value = operand1.Value + operand2.Value; result = true; break; }
							case OperatorType.Subtraction: { this.Value = value = operand1.Value - operand2.Value; result = true; break; }
							case OperatorType.Multiplication: { this.Value = value = operand1.Value * operand2.Value; result = true; break; }
							case OperatorType.Division: { if (operand2.Value != 0) { this.Value = value = operand1.Value / operand2.Value; result = true; break; } else { throw (new DivideByZeroException()); } }
							default: { throw (new ExecutionException("The operator [op] is an invalid type: [" + (op as OperatorToken).OperatorType.ToString() + "].")); }
						}
					}
					else if ((operand1.IsOperand) && (op.IsOperand) && (operand2.IsOperand))
					{
						if ((operand1.Type == TokenType.NegationOperator) && (op.Type == TokenType.NegationOperator) && (operand2.Type == TokenType.NegationOperator))
						{
							value = 0;
							value += operand1.Value;
							value += op.Value;
							value += operand2.Value;
							this.Value = value;
							result = true;
						}
						else
						{
							throw (new ExecutionException("A parentheses with three tokens does not contain a valid operand-operator-operand or operand-operand-operand sequence."));
						}
					}
					else
					{
						throw (new ExecutionException("A parentheses with three tokens does not contain a valid operand-operator-operand or operand-operand-operand sequence."));
					}
				}
				else
				{
					while ((token = this.Tokens.FirstOrDefault(t => ((t.IsOperator) && ((t as OperatorToken).OperatorType == OperatorType.Division)))) != null)
					{
						index = this.Tokens.IndexOf(token);

						var operand1 = this.Tokens [index - 1];
						var op = this.Tokens [index];
						var operand2 = this.Tokens [index + 1];

						parentheses = new ParenthesesToken(this.Builder, "(" + operand1.GetText() + op.Text + operand2.GetText() + ")", true, true, true);

						this.RemoveTokenRange(index - 1, 3);

						this.InsertToken(index - 1, parentheses);
						parentheses.AddToken(operand1);
						parentheses.AddToken(op);
						parentheses.AddToken(operand2);

						parentheses.Calculate(out value, out exception);
						this.Text = string.Join("", this.Tokens.ToList().ConvertAll<string>(t => t.Text));
					}

					while ((token = this.Tokens.FirstOrDefault(t => ((t.IsOperator) && ((t as OperatorToken).OperatorType == OperatorType.Multiplication)))) != null)
					{
						index = this.Tokens.IndexOf(token);

						var operand1 = this.Tokens [index - 1];
						var op = this.Tokens [index];
						var operand2 = this.Tokens [index + 1];

						parentheses = new ParenthesesToken(this.Builder, "(" + operand1.GetText() + op.Text + operand2.GetText() + ")", true, true, true);

						this.RemoveTokenRange(index - 1, 3);

						this.InsertToken(index - 1, parentheses);
						parentheses.AddToken(operand1);
						parentheses.AddToken(op);
						parentheses.AddToken(operand2);

						parentheses.Calculate(out value, out exception);
						this.Text = string.Join("", this.Tokens.ToList().ConvertAll<string>(t => t.Text));
					}

					if (this.Tokens.Count == 0)
					{
						throw (new ExecutionException("No tokens remained after consoldating division and multiplication."));
					}
					else if (this.Tokens.Count == 1)
					{
						result = this.Calculate(out value, out exception);
						if (result) { this.Value = value; }
						else { throw (exception); }
					}
					else if (this.Tokens.Count == 2)
					{
						result = this.Calculate(out value, out exception);
						if (result) { this.Value = value; }
						else { throw (exception); }
					}
					else if (this.Tokens.Count == 3)
					{
						result = this.Calculate(out value, out exception);
						if (result) { this.Value = value; }
						else { throw (exception); }
					}
					else
					{
						while (this.Tokens.Count > 1)
						{
							var operand1 = this.Tokens [0];
							var op = this.Tokens [1];
							var operand2 = this.Tokens [2];

							parentheses = new ParenthesesToken(this.Builder, "(" + operand1.GetText() + op.Text + operand2.GetText() + ")", true, true, true);

							this.RemoveTokenRange(0, 3);

							this.InsertToken(0, parentheses);
							parentheses.AddToken(operand1);
							parentheses.AddToken(op);
							parentheses.AddToken(operand2);

							parentheses.Calculate(out value, out exception);
							this.Text = string.Join("", this.Tokens.ToList().ConvertAll<string>(t => t.GetText()));
						}

						result = this.Calculate(out value, out exception);
						if (result) { this.Value = value; }
						else { throw (exception); }
					}
				}
			}

			result
				&= (!double.IsNaN(this.Value))
				&& (!double.IsNegativeInfinity(this.Value))
				&& (!double.IsInfinity(this.Value))
				&& (!double.IsPositiveInfinity(this.Value))
				;

			if ((!result) && (exception == null))
			{
				throw (new ExecutionException("Calculation failed but did not report an error."));
			}

			return (result);
		}

		protected override void OnGenerateRandom (Random random, int depth)
		{
			int count = 0;
			Token token = null;
			Token @operator = null;
			List<TokenType> types = null;

			count = random.Next(3, 6);
			types = new List<TokenType>();

			types.Add(TokenType.Constant);
			types.Add(TokenType.Function);
			types.Add(TokenType.Parentheses);
			types.Add(TokenType.Variable);

			while (this.Tokens.Count < count)
			{
				if (depth <= 0)
				{
					types.Clear();
					types.Add(TokenType.Constant);
					types.Add(TokenType.Variable);

					switch (types [random.Next(0, types.Count)])
					{
						case TokenType.Constant: { token = new ConstantToken(this.Builder, ""); break; }
						case TokenType.Variable: { token = new VariableToken(this.Builder, ""); break; }
						default: { throw (new ParserException("Internal error.")); }
					}
				}
				else
				{
					switch (types [random.Next(0, types.Count)])
					{
						case TokenType.Constant: { token = new ConstantToken(this.Builder, ""); break; }
						case TokenType.Function: { token = new FunctionToken(this.Builder, ""); break; }
						case TokenType.Parentheses: { token = new ParenthesesToken(this.Builder, ""); break; }
						case TokenType.Variable: { token = new VariableToken(this.Builder, ""); break; }
						default: { throw (new ParserException("Internal error.")); }
					}
				}

				if ((this.Tokens.Count > 0) && (types.Contains(this.Tokens.Last().Type)))
				{
					@operator = new OperatorToken(this.Builder, "");
					@operator.GenerateRandomTree(random, depth);
					this.AddToken(@operator);
					this.Text += @operator.Text;
				}

				token.GenerateRandomTree(random, depth - 1);
				this.AddToken(token);
				this.Text += token.GetText();

				//if (random.Next(0, 2) == 0)
				//{
				//    int index = 0;
				//    index = this.Tokens.Count - 1;

				//    token = this.Tokens [index];
				//    this.Tokens.RemoveAt(index);

				//    @operator = new NegationOperatorToken(this.Builder, "");
				//    @operator.GenerateRandomTree(random, depth - 1);
				//    @operator.AddToken(token);

				//    //@operator.Text = @operator.GetText();

				//    this.Tokens.Insert(index, @operator);
				//}

				if (depth <= 0)
				{
					break;
				}
			}

			this.Text = "(" + this.Text + ")";
		}

		public override string GetText ()
		{
			return ("(" + string.Join(" ", this.Tokens.ToList().ConvertAll<string>(p => p.GetText())) + ")");
		}

		public static bool TryExtract (string text, int indexStart, out int indexEnd)
		{
			int count = 0;
			bool result = false;

			indexEnd = -1;

			if (text.Substring(indexStart).StartsWith("("))
			{
				count = 0;
				for (int i = indexStart; i < text.Length; i++)
				{
					if (text [i] == '(')
					{
						count++;
					}
					else if (text [i] == ')')
					{
						count--;

						if (count == 0)
						{
							result = false;
							indexEnd = i + 1;

							break;
						}
					}
				}

				result = (count == 0);
			}

			return (result);
		}
	}
}