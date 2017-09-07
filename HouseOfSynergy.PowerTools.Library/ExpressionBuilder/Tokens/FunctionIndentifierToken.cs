using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public sealed class FunctionIndentifierToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public FunctionIndentifierToken (Builder calculationBuilder, string text, int offSetLeft = 0)
			: base(TokenType.FunctionIdentifier, calculationBuilder, text, offSetLeft)
		{
		}

		public override string Name { get { return ("Function Name"); } }

		protected override bool OnParse ()
		{
			string text = "";
			Match match = null;
			bool result = false;
			string pattern = "";
			MatchCollection matches = null;

			text = this.Text;

			pattern = @"^\#[A-Za-z_]+[A-Za-z0-9_]*$";
			matches = Regex.Matches(text, pattern, RegexOptions.Singleline);
			if ((matches != null) && (matches.Count > 0) && (matches [0].Success) && (matches [0].Index == 0))
			{
				match = matches [0];

				result = true;
			}

			return (result);
		}

		public override string GetText ()
		{
			return (this.Text);
		}
	}
}