using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public class OperatorToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public OperatorToken (Builder calculationBuilder, string text, int offSetLeft = 0) : base(TokenType.Operator, calculationBuilder, text, offSetLeft) { }

		public override string Name { get { return ("Operator"); } }
		public new bool Parsed { get { return (this.IsOperatorValid); } }
		public bool IsNegationOperator { get { return ((this.IsOperatorValid) && (Token.IsCharacterNegationOperator(this.Text [0]))); } }
		public bool IsNonNegationOperator { get { return ((this.IsOperatorValid) && (Token.IsCharacterNonNegationOperator(this.Text [0]))); } }
		public bool IsOperatorValid { get { return ((this.OperatorType != ExpressionBuilder.OperatorType.None) && (this.OperatorType != ExpressionBuilder.OperatorType.Unknown)); } }

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
						case "*": { return (ExpressionBuilder.OperatorType.Multiplication); }
						case "/": { return (ExpressionBuilder.OperatorType.Division); }
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
			if (this.IsOperatorValid) { return (true); }
			else { throw (new ParserException("The token [" + this.Text + "] is not a valid operator.")); }
		}

		protected override void OnGenerateRandom (Random random, int maxDepth)
		{
			string operators = "+-*/";
			this.Text = operators [random.Next(0, operators.Length)].ToString();
		}

		public override string GetText ()
		{
			return ((this.IsOperatorValid) ? this.Text.Trim() : "(InvalidOperator(" + this.Text.Trim() + "))");
		}

		public static bool TryExtract (string text, int indexStart, out int indexEnd)
		{
			return ((indexEnd = (Token.IsCharacterOperator(text [0]) ? 1 : -1)) == 1);
		}
	}
}