using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public sealed class Builder:
		HouseOfSynergy.PowerTools.Library.Interfaces.IPersistXElement<HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Builder>
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public string Name { get; private set; }
		public string Text { get; private set; }
		public Tokens.Token Token { get; private set; }
		private List<Exception> _Exceptions { get; set; }
		private StringBuilder StringBuilderLog { get; set; }
		public ReadOnlyCollection<INodeDoubleNullable> Variables { get; set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		public Builder (string name) : this(name, "", null) { }

		public Builder (string name, string script) : this(name, script, null) { }

		public Builder (string name, string script, IEnumerable<INodeDoubleNullable> variables)
		{
			if (name == null) { throw (new ArgumentNullException("name")); }
			if (name.Any(c => Path.GetInvalidFileNameChars().Contains(c))) { throw (new ArgumentException("The argument name must be a valid filename (without an extension).", "name")); }

			this.Name = name;
			try { this.File.Refresh(); }
			catch { throw (new ArgumentException("The argument name must be a valid filename (without an extension).", "name")); }

			this.Text = script ?? "";
			this._Exceptions = new List<Exception>();
			this.StringBuilderLog = new StringBuilder();
			this.Token = new Tokens.GenericToken(this, this.Text);

			if (variables == null) { this.Variables = new ReadOnlyCollection<INodeDoubleNullable>(new List<INodeDoubleNullable>()); }
			else { this.Variables = new ReadOnlyCollection<INodeDoubleNullable>(variables.ToList()); }
		}

		public void Initialize ()
		{
			this.Text = "";
			this.Token.Initialize();
			this._Exceptions.Clear();
			this.StringBuilderLog.Clear();
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public bool Parsed { get { return (this.Token.Parsed); } }
		public double? Value { get { return (this.Token.Value); } }
		public bool Compiled { get { return (this.Token.Compiled); } }
		public bool Calculated { get { return (this.Token.Calculated); } }
		public string Log { get { return (this.StringBuilderLog.ToString()); } }
		public string TextSingleLine { get { return (Builder.GetMinimizedExpression(this.Text)); } }
		public ReadOnlyCollection<Exception> Exceptions { get { return (this._Exceptions.AsReadOnly()); } }

		public DirectoryInfo Directory
		{
			get
			{
				string directory = "";

				directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
				directory = Path.Combine(directory, HouseOfSynergy.PowerTools.Library.Global.Instance.ApplicationInfo.CompanyName);
				directory = Path.Combine(directory, HouseOfSynergy.PowerTools.Library.Global.Instance.ApplicationInfo.ProductName);
				directory = Path.Combine(directory, HouseOfSynergy.PowerTools.Library.Global.Instance.ApplicationInfo.ProductVersion.ToString());
				directory = Path.Combine(directory, "Expression Builder");

				if (!System.IO.Directory.Exists(directory))
				{
					System.IO.Directory.CreateDirectory(directory);
				}

				return (new DirectoryInfo(directory));
			}
		}

		public FileInfo File
		{
			get
			{
				string filename = "";

				filename = Path.Combine(this.Directory.FullName, this.Name + ".xml");

				return (new FileInfo(filename));
			}
		}

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		public void GenerateRandom ()
		{
			this.GenerateRandom(new Random((int) DateTime.Now.Ticks));
		}

		public void GenerateRandom (Random random)
		{
			//Exception exception = null;

			this.Initialize();

			this.Token = new Tokens.ParenthesesToken(this, "");
			this.Token.GenerateRandomTree(random : random, depth : random.Next(3, 6));
			this.Text = this.Token.Text;

			//this.Token.ReduceTokenMapAndCleanupRedundancies();
			//this.Token.SubstituteVariables();

			// GetText here!!!
		}

		public string GetText ()
		{
			return (this.Token.GetText());
		}

		internal void AddException (Exception exception)
		{
			this._Exceptions.Add(exception);
		}

		public bool ContainsKey (string key)
		{
			foreach (var dictionary in this.Variables)
			{
				if (dictionary.ContainsKey(key))
				{
					return (true);
				}
			}

			return (false);
		}

		#endregion Methods.

		#region Methods: Parsing.

		//====================================================================================================
		// Methods: Parsing.
		//====================================================================================================

		public bool Parse ()
		{
			return (this.Parse(this.Text));
		}

		public bool Parse (string expression)
		{
			int index = 0;
			string text = "";
			bool result = false;

			if (expression == null) { throw (new ArgumentNullException("expression")); }

			this.Initialize();

			this.Text = (expression ?? "").Trim();

			try
			{
				text = Builder.GetCleanedExpression(this.Text);

				if (!text.All(c => @"()@#+-*/,._0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Contains(c))) { throw (new ParserException(@"Expressions cannot contain characters other than letters, digits, and any one of the following: '( ) @ # + - * / , . _'.", 1)); }
				if (text.Contains(")(")) { throw (new ParserException("Expressions cannot contain adjoining parentheses tokens without an operand.", 1)); }
				if (text.Contains("++")) { throw (new ParserException("Expressions cannot contain consecutive non-negation operators.", 1)); }
				if (text.Contains("-+")) { throw (new ParserException("Expressions cannot contain a negation operator followed by a non-negation operator.", 1)); }
				if (text.Contains("(++")) { throw (new ParserException("Expressions cannot contain parentheses tokens followed by a consecutive negation operators.", 1)); }
				if (text.Contains("(+-")) { throw (new ParserException("Expressions cannot contain parentheses tokens followed by a consecutive negation operators.", 1)); }
				//if (text.Contains("(--")) { throw (new ParseException("Expressions cannot contain parentheses tokens followed by a consecutive negation operators.", 1)); }
				if (text.Contains("(-+")) { throw (new ParserException("Expressions cannot contain parentheses tokens followed by a consecutive negation operators.", 1)); }
				if (text.Contains("(*")) { throw (new ParserException("Expressions cannot contain parentheses tokens followed by a non-negation operator.", 1)); }
				if (text.Contains("(/")) { throw (new ParserException("Expressions cannot contain parentheses tokens followed by a non-negation operator.", 1)); }
				if (text.Contains("(,")) { throw (new ParserException("Function expressions cannot contain parentheses tokens followed by a comma operator.", 1)); }
				if (text.Contains(",,")) { throw (new ParserException("Function expressions cannot contain comma tokens followed by another comma operator.", 1)); }
				if (text.Contains("+)")) { throw (new ParserException("Expressions cannot contain operator tokens followed by parentheses tokens.", 1)); }
				if (text.Contains("-)")) { throw (new ParserException("Expressions cannot contain operator tokens followed by parentheses tokens.", 1)); }
				if (text.Contains("*)")) { throw (new ParserException("Expressions cannot contain operator tokens followed by parentheses tokens.", 1)); }
				if (text.Contains("/)")) { throw (new ParserException("Expressions cannot contain operator tokens followed by parentheses tokens.", 1)); }
				if (text.Contains(",)")) { throw (new ParserException("Function expressions cannot contain comma tokens followed by a parentheses token.", 1)); }
				if (Regex.IsMatch(text, @"\)[^\)\+\-\*\/,]")) { throw (new ParserException("Expressions cannot contain parentheses tokens followed or preceded by anything except operators.", 1)); }

				if (Regex.IsMatch(text, @"[^\+\-\*\/\(,]\("))
				{
					var matches = Regex.Matches(text, @"[^\+\-\*\/\(,]\(");

					foreach (Match match in matches)
					{
						index = match.Index;

						for (int i = index; i >= 0; i--)
						{
							if (text [i] == '#')
							{
								break;
							}
							else if (!char.IsLetterOrDigit(text [i]))
							{
								throw (new ParserException("Expressions cannot contain parentheses tokens followed or preceded by anything except operators.", 1));
							}
						}
					}
				}

				for (int i = 0; i < (text.Length - 1); i++)
				{
					if (i == 0)
					{
						if (Tokens.OperatorToken.IsCharacterNonNegationOperator(text [i]))
						{
							throw (new ParserException("Expressions cannot start with non-negation operators.", i + 1));
						}
					}

					//if (Parser.OperatorToken.IsCharacterOperator(text [i]))
					//{
					//    if (Parser.OperatorToken.IsCharacterOperator(text [i + 1]))
					//    {
					//        if ((i + 2) < text.Length)
					//        {
					//            if (Parser.OperatorToken.IsCharacterOperator(text [i + 2]))
					//            {
					//                throw (new ParseException("Expressions cannot contain three consecutive operators.", i + 1));
					//            }
					//        }
					//    }
					//}

					if (Tokens.OperatorToken.IsCharacterNonNegationOperator(text [i]))
					{
						if (Tokens.OperatorToken.IsCharacterNonNegationOperator(text [i + 1]))
						{
							throw (new ParserException("Expressions cannot contain consecutive non-negation operators.", i + 1));
						}
					}

					if (Tokens.OperatorToken.IsCharacterNegationOperator(text [i]))
					{
						if (Tokens.OperatorToken.IsCharacterNonNegationOperator(text [i + 1]))
						{
							throw (new ParserException("Expressions cannot contain negation operators followed by non-negation operators.", i + 1));
						}
					}
				}

				this.Token = new Tokens.ParenthesesToken(this, text);
				result = this.Token.Parse();
			}
			catch (Exception exception)
			{
				this.AddException(exception);
			}

			return (result);
		}

		#endregion Methods: Parsing.

		#region Methods: Compilation.

		//====================================================================================================
		// Methods: Compilation.
		//====================================================================================================

		public bool Compile ()
		{
			bool result = false;

			try
			{
				this.Token.DissolveGenericTokens();
				this.Token.CombineRedundantNegationOperatorsWithOperands();

				result = this.Token.Compile();
			}
			catch (Exception exception)
			{
				this.AddException(exception);
			}

			return (result);
		}

		#endregion Methods: Compilation.

		#region Methods: Execution.

		//====================================================================================================
		// Methods: Execution.
		//====================================================================================================

		public bool Calculate ()
		{
			bool result = false;

			try
			{
				result = this.Token.Calculate();
			}
			catch (Exception exception)
			{
				this.AddException(exception);
			}

			return (result);
		}

		#endregion Methods: Execution.

		#region Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.IDatabase.

		//====================================================================================================
		// Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.IDatabase.
		//====================================================================================================

		public bool Save () { Exception exception = null; return (this.Save(out exception)); }

		public bool Load () { Exception exception = null; return (this.Load(out exception)); }

		public bool Save (out Exception exception)
		{
			bool result = false;
			System.Xml.Linq.XDocument document = null;

			exception = null;

			try
			{
				document = System.Xml.Linq.XDocument.Parse(@"<InventoryInsight></InventoryInsight>");

				document.Root.Add(this.ToXElement());

				document.Save(this.File.FullName);

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public bool Load (out Exception exception)
		{
			bool result = false;
			System.Xml.Linq.XDocument document = null;

			exception = null;

			try
			{
				this.File.Refresh();
				if (!this.File.Exists)
				{
					this.Save(out exception);

					this.File.Refresh();
				}

				document = System.Xml.Linq.XDocument.Load(this.File.FullName);

				this.FromXElement(document.Root.Element(this.GetType().Name));

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		#endregion Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.IDatabase.

		#region Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.IPersistXml<T>.

		//====================================================================================================
		// Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.IPersistXml<T>.
		//====================================================================================================

		public System.Xml.Linq.XElement ToXElement ()
		{
			System.Xml.Linq.XElement element = null;

			element = new System.Xml.Linq.XElement
			(
				this.GetType().Name,
				new System.Xml.Linq.XObject []
				{
					new System.Xml.Linq.XAttribute("Name", this.Name),
					new System.Xml.Linq.XAttribute("Script", this.Text),
				}
			);

			return (element);
		}

		public HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Builder FromXElement (System.Xml.Linq.XElement element)
		{
			this.Initialize();

			this.Text = element.Attribute("Script").Value;
			this.Token = new Tokens.GenericToken(this, this.Text);

			return (this);
		}

		#endregion Interface Implementation: HouseOfSynergy.PowerTools.Library.Interfaces.IPersistXml<T>.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		public static string GetMinimizedExpression (string text)
		{
			return ((text ?? "").Trim().Replace(" ", "").Replace("	", "").Replace(System.Environment.NewLine, "").Replace(System.Environment.NewLine [0].ToString(), "").Replace(System.Environment.NewLine [1].ToString(), ""));
		}

		private static string GetCleanedExpression (string expression)
		{
			int count = 0;
			string text = "";
			bool foundOpeningParentheses = false;
			bool requiresEnclosingParentheses = false;

			text = Builder.GetMinimizedExpression(expression);

			if ((!text.Contains("(")) && (!text.Contains(")"))) { requiresEnclosingParentheses = true; }
			if ((!text.StartsWith("(")) || (!text.EndsWith(")"))) { requiresEnclosingParentheses = true; }

			for (int i = 0; i < text.Length; i++)
			{
				if (text [i] == '(')
				{
					count++;
					foundOpeningParentheses = true;
				}
				else if (text [i] == ')')
				{
					count--;
				}

				if (count < 0)
				{
					throw (new ParserException("An unexpected closing parentheses ')' was encountered."));
				}
				else if (count == 0)
				{
					if ((foundOpeningParentheses) && (i < (text.Length - 1)))
					{
						requiresEnclosingParentheses = true;
					}
				}
			}

			if (count > 0)
			{
				throw (new ParserException("An expected closing parentheses ')' is missing."));
			}

			if (requiresEnclosingParentheses)
			{
				text = "(" + text + ")";
			}

			return (text);
		}

		#endregion Static.
	}
}