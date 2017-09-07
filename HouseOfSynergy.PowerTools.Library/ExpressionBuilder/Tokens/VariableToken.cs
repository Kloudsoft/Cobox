using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public class VariableToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public VariableToken (Builder calculationBuilder, string text, int offSetLeft = 0) : base(TokenType.Variable, calculationBuilder, text, offSetLeft) { }

		public string ListName { get; private set; }
		public string VariableName { get; private set; }
		public override string Name { get { return ("Variable"); } }

		protected override bool OnParse ()
		{
			string key = "";
			bool result = false;

			this.ListName = "";
			this.VariableName = "";

			if ((this.Text != null) && (this.Text.Length >= 2) && (this.Text [0] == '@'))
			{
				if ((char.IsLetter(this.Text [1])) || (this.Text [1] == '_'))
				{
					foreach (var list in this.Builder.Variables)
					{
						if (this.Text.StartsWith("@" + list.Prefix + "."))
						{
							key = this.Text.Substring(("@" + list.Prefix + ".").Length);

							this.VariableName = key;
							this.ListName = list.Prefix;

							break;
						}
					}

					if (key.Length > 0)
					{
						if (this.Builder.ContainsKey(key))
						{
							result = true;
						}
						else
						{
							throw (new ParserException("The variable [" + this.Text + "] does not exist."));
						}
					}
				}
				else
				{
					throw (new ParserException("Illegal variable name."));
				}
			}
			else
			{
				throw (new ParserException("Illegal variable name."));
			}

			return (result);
		}

		protected override bool OnCalculate (out double value, out Exception exception, int level = 0)
		{
			double? temp = null;

			value = double.NaN;
			exception = new NotImplementedException();

			var list = this.Builder.Variables.SingleOrDefault(l => l.Prefix == this.ListName);

			temp = list.GetValue(this.VariableName);

			if (temp.HasValue)
			{
				this.Value = value = temp.Value;
			}
			else
			{
				this.Value = value = double.NaN;
				throw (new ParserException("The variable [" + this.Text + "] returned a null value."));
			}

			return (true);
		}

		protected override void OnGenerateRandom (Random random, int maxDepth)
		{
			string variable = "";
			INodeDoubleNullable list = null;

			do
			{
				list = this.Builder.Variables [random.Next(0, this.Builder.Variables.Count)];
				variable = list.AllKeys [random.Next(0, list.AllKeys.Count)];
			}
			while ((!list.IsCompatibleVariableType(variable)) || (variable.Contains("Single")) || (variable.Contains("Double")));

			this.Text = "@" + list.Prefix + "." + variable;
			this.Value = list.GetValue(variable).HasValue ? list.GetValue(variable).Value : double.NaN;
		}

		public override string GetText ()
		{
			return (this.Text);
		}

		public static bool TryExtract (string text, int startIndex, out Match match)
		{
			bool result = false;
			string pattern = "";
			MatchCollection matches = null;

			match = null;

			pattern = @"\@[A-Za-z0-9_]+\.[A-Za-z_]+[A-Za-z0-9_]*";
			matches = Regex.Matches(text, pattern, RegexOptions.Singleline);

			if ((matches != null) && (matches.Count > 0) && (matches [0].Success) && (matches [0].Index == startIndex))
			{
				result = true;
				match = matches [0];
			}

			return (result);
		}
	}
}