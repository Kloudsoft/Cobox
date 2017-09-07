using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public sealed class NegationOperatorToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public NegationOperatorToken (Builder calculationBuilder, string text, int offSetLeft = 0) : base(TokenType.NegationOperator, calculationBuilder, text, offSetLeft) { }

		public new bool Parsed { get { return (this.IsOperatorValid); } }
		public override string Name { get { return ("Negation Operator"); } }
		public bool IsOperatorValid { get { return ((this.OperatorType == ExpressionBuilder.OperatorType.Addition) || (this.OperatorType == ExpressionBuilder.OperatorType.Subtraction)); } }

		public OperatorType OperatorType
		{
			get
			{
				if (this.Text.Trim().Length == 1)
				{
					switch (this.Text.Trim())
					{
						case "+": { return (ExpressionBuilder.OperatorType.Addition); }
						case "-": { return (ExpressionBuilder.OperatorType.Subtraction); }
						default: { return (ExpressionBuilder.OperatorType.Unknown); }
					}
				}
				else
				{
					return (ExpressionBuilder.OperatorType.Unknown);
				}
			}
		}

		protected override bool OnParse ()
		{
			bool result = false;

			switch (this.OperatorType)
			{
				case OperatorType.Addition:
				case OperatorType.Subtraction: { result = true; break; }
				default: { throw (new ParserException("The token [" + this.Text + "] is not a valid negation operator.")); }
			}

			return (result);
		}

		protected override bool OnCalculate (out double value, out Exception exception, int level = 0)
		{
			bool result = false;

			exception = null;
			value = double.NaN;

			if (this.Tokens.Count == 0) { throw (new ExecutionException("Negation operator [" + this.Text + "] does not contain any targets.")); }
			else if (this.Tokens.Count == 1) { }
			else { throw (new ExecutionException("Negation operator [" + this.Text + "] contains too many targets [" + this.Tokens.Count.ToString() + "]." + Environment.NewLine + string.Join(System.Environment.NewLine, this.Tokens.ToList().ConvertAll(t => t.Text)))); }

			result = this.Tokens [0].Calculate(out value, out exception);

			if (this.OperatorType == ExpressionBuilder.OperatorType.Addition) { this.Value = value; }
			else if (this.OperatorType == ExpressionBuilder.OperatorType.Subtraction) { this.Value = value = -value; }
			else { throw (new ParserException("The token [" + this.Text + "] is not a valid negation operator.")); }

			return (result);
		}

		protected override void OnGenerateRandom (Random random, int maxDepth)
		{
			string operators = "-";
			this.Text = operators [random.Next(0, operators.Length)].ToString();
		}

		public override string GetText ()
		{
			return (this.Text.Trim() + ((this.Tokens.Count == 1) ? this.Tokens [0].GetText() : "(InvalidNegationOperatorChildren(" + this.Tokens.Count.ToString() + "))"));
		}

		public bool CanSwallow { get { return (((this.Tokens.Count == 0) || ((this.Tokens [0].Type == TokenType.NegationOperator) && ((this.Tokens [0] as NegationOperatorToken).CanSwallow)))); } }

		public void SwallowToken (Token token)
		{
			if (this.Tokens.Count == 0)
			{
				this.AddToken(token);
			}
			else if (this.Tokens.Count == 1)
			{
				if (this.Tokens [0].Type == TokenType.NegationOperator)
				{
					(this.Tokens [0] as NegationOperatorToken).SwallowToken(token);
				}
				else
				{
					throw (new ParserException("A negation operator cannot swallow a token of type [" + token.Type.ToString() + "] when it already contains a token of type [" + this.Tokens [0].Type.ToString() + "]."));
				}
			}
			else
			{
				throw (new ParserException("Negation operator contains more than one target."));
			}
		}

		public static bool TryExtract (string text, int indexStart, out int indexEnd)
		{
			return ((indexEnd = (Token.IsCharacterNegationOperator(text [0]) ? 1 : -1)) == 1);
		}
	}
}