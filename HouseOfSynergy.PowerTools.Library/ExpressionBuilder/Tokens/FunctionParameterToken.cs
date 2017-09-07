using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public sealed class FunctionParameterToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public FunctionParameterToken (Builder calculationBuilder, string text, int offSetLeft = 0)
			: base(TokenType.FunctionParameter, calculationBuilder, "(" + text + ")", offSetLeft)
		{
		}

		public override string Name { get { return ("Function Parameter"); } }

		protected override bool OnParse ()
		{
			Token token = null;
			bool result = false;

			if (!this.Text.StartsWith("(")) { throw (new ParserException("Function parameter does not resolve to a parentheses.")); }
			if (!this.Text.EndsWith(")")) { throw (new ParserException("Function parameter does not resolve to a parentheses.")); }

			token = new ParenthesesToken(this.Builder, this.Text);
			this.AddToken(token);
			result = token.Parse();

			return (result);
		}

		protected override bool OnCalculate (out double value, out Exception exception, int level = 0)
		{
			bool result = false;

			exception = null;
			value = double.NaN;

			if (this.Tokens.Count == 1)
			{
				result = this.Tokens [0].Calculate(out value, out exception);

				this.Value = value;
			}
			else
			{
				throw (new ParserException("The parameter does not evaluate to a parentheses."));
			}

			return (result);
		}

		public override string GetText ()
		{
			return ((this.Tokens.Count == 0) ? "" : this.Tokens [0].GetText());
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
				result = true;
				match = matches [0];
			}

			return (result);
		}
	}
}