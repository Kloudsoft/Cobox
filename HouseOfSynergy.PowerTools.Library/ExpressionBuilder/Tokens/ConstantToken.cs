using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public class ConstantToken:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens.Token
	{
		public ConstantToken (Builder calculationBuilder, string text, int offSetLeft = 0) : base(TokenType.Constant, calculationBuilder, text, offSetLeft) { }

		public ConstantToken (Builder calculationBuilder, string text, bool forceParseSuccess) : base(TokenType.Constant, calculationBuilder, text, 0) { this.SkipParseStage = true; }

		public override string Name { get { return ("Constant"); } }

		protected override bool OnParse ()
		{
			double.Parse(this.Text);

			return (true);
		}

		protected override bool OnCalculate (out double value, out Exception exception, int level = 0)
		{
			exception = null;
			value = double.NaN;

			this.Value = value = double.Parse(this.Text);

			return (true);
		}

		public override string GetText ()
		{
			return (this.Text);
		}

		protected override void OnGenerateRandom (Random random, int maxDepth)
		{
			string text = "";
			double fraction = 0;

			do { fraction = System.Math.Round(random.NextDouble(), 2); } while (fraction.ToString().Length <= 3);
			do { text = random.Next(-10, +10).ToString(); } while (text == "0");

			if (random.Next(0, 2) == 0) { text += "." + fraction.ToString().Substring(2); }
			if (random.Next(0, 3) == 0) { text += "E" + random.Next(-5, +5).ToString(); }
			if (random.Next(0, 2) == 0) { text = text.Replace("E", "e"); }

			if ((text.Contains("E")) || (text.Contains("e")))
			{
				if ((!text.Contains("+")) && (!text.Contains("-")))
				{
					text = text.Replace("E", "E+").Replace("e", "e+");
				}
			}

			this.Text = text;
			this.Value = double.Parse(text);
		}

		public static bool TryExtract (string textToParse, int indexStart, out int indexEnd)
		{
			string temp = "";
			string text = "";
			Match match = null;
			bool result = false;
			string pattern = "";
			List<string> patterns = null;
			MatchCollection matches = null;

			indexEnd = -1;

			patterns = new List<string>();
			patterns.Add(@"^\d+\.\d+");
			patterns.Add(@"^\.\d+");
			patterns.Add(@"^\d+");

			temp = textToParse;
			for (int i = 0; i < patterns.Count; i++)
			{
				pattern = patterns [i];
				matches = System.Text.RegularExpressions.Regex.Matches(temp, pattern, System.Text.RegularExpressions.RegexOptions.Singleline);

				if ((matches != null) && (matches.Count > 0) && (matches [0].Success) && (matches [0].Index == indexStart))
				{
					try
					{
						match = matches [0];
						indexEnd = match.Length;
						temp = temp.Substring(indexEnd);
						text = textToParse.Substring(0, indexEnd);

						double.Parse(match.Value);

						result = true;

						break;
					}
					catch (OverflowException) { throw; }
					catch { }
				}
			}

			if (result)
			{
				patterns = new List<string>();
				patterns.Add(@"^[Ee][+\-]?\d+");

				for (int i = 0; i < patterns.Count; i++)
				{
					pattern = patterns [i];
					matches = System.Text.RegularExpressions.Regex.Matches(temp, pattern, System.Text.RegularExpressions.RegexOptions.Singleline);

					if ((matches != null) && (matches.Count > 0) && (matches [0].Success) && (matches [0].Index == indexStart))
					{
						try
						{
							match = matches [0];
							indexEnd += match.Length;
							text = textToParse.Substring(0, indexEnd);

							double.Parse(text);

							result = true;

							break;
						}
						catch (OverflowException) { throw; }
						catch { }
					}
				}
			}

			return (result);
		}
	}
}