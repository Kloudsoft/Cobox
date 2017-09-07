using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public abstract class Token
	{
		#region Members.

		//====================================================================================================
		// Members.
		//====================================================================================================

		public Token Parent { get; private set; }

		public bool Parsed { get; private set; }
		public bool Compiled { get; private set; }
		public bool Calculated { get; private set; }

		protected bool SkipParseStage { get; set; }
		protected bool SkipCompileStage { get; set; }
		protected bool SkipCalculateStage { get; set; }

		private List<Token> _Tokens { get; set; }
		private List<Exception> _Exceptions { get; set; }
		public ReadOnlyCollection<Token> Tokens { get; private set; }
		public ReadOnlyCollection<Exception> Exceptions { get; private set; }

		public string Text { get; protected set; }
		public double Value { get; protected set; }
		public int OffsetLeft { get; private set; }
		public TokenType Type { get; private set; }
		protected Builder Builder { get; private set; }

		#endregion Members.

		#region Constructors & Initializers.

		//====================================================================================================
		// Constructors & Initializers.
		//====================================================================================================

		protected Token (TokenType type, Builder calculationBuilder, string text, int offSetLeft)
		{
			this.Type = type;
			this.OffsetLeft += offSetLeft;
			this.Text = (text ?? "").Trim();
			this._Tokens = new List<Token>();
			this.Builder = calculationBuilder;
			this.Tokens = this._Tokens.AsReadOnly();
			this._Exceptions = new List<Exception>();
			this.Exceptions = this._Exceptions.AsReadOnly();

			this.Initialize(false);
		}

		public virtual void Initialize (bool clearExpression = true)
		{
			this.Value = double.NaN;

			this.Parsed = false;
			this.Compiled = false;
			this.Calculated = false;

			this.SkipParseStage = false;
			this.SkipCompileStage = false;
			this.SkipCalculateStage = false;

			this._Tokens.Clear();
			this._Exceptions.Clear();

			if (clearExpression) { this.Text = ""; }
		}

		#endregion Constructors & Initializers.

		#region Properties.

		//====================================================================================================
		// Properties.
		//====================================================================================================

		public bool IsRoot { get { return (this.Parent == null); } }
		public bool IsLeaf { get { return (this._Tokens.Count == 0); } }
		public bool HasChildren { get { return (this._Tokens.Count > 0); } }
		public virtual string Name { get { return (this.GetType().Name); } }

		public bool IsOperator { get { return (this.Type == TokenType.Operator); } }
		public bool IsOperand { get { return ((this.Type == TokenType.Constant) || (this.Type == TokenType.Function) || (this.Type == TokenType.NegationOperator) || (this.Type == TokenType.Parentheses) || (this.Type == TokenType.Variable)); } }

		public virtual bool ContainsParentheses { get { return (this._Tokens.Any(token => token.Type == TokenType.Parentheses)); } }
		public virtual bool ContainsFunction { get { return (this._Tokens.Any(token => ((token is FunctionToken) || token.ContainsFunction))); } }

		public bool IsInFunction { get { return (((this.Type == TokenType.Function) || ((this.Parent != null) && (this.Parent.IsInFunction)))); } }
		public bool IsInFunctionParameter { get { return (((this.Type == TokenType.FunctionParameter) || ((this.Parent != null) && (this.Parent.IsInFunctionParameter)))); } }

		#endregion Properties.

		#region Methods.

		//====================================================================================================
		// Methods.
		//====================================================================================================

		protected Token GetTokenAt (int index) { return (this._Tokens [index]); }

		public void AddToken (Token token) { this.InsertToken(this._Tokens.Count, token); }

		protected void AddTokens (List<Token> tokens) { foreach (var token in tokens) { this.AddToken(token); } }

		protected void InsertToken (int index, Token token) { token.Parent = this; this._Tokens.Insert(index, token); }

		protected void InsertTokens (int index, List<Token> tokens) { var list = tokens; list.Reverse(); foreach (var token in list) { this.InsertToken(index, token); } }

		protected void RemoveToken (Token token) { token.Parent = null; this._Tokens.Remove(token); }

		protected void RemoveTokenAt (int index) { this._Tokens [index].Parent = null; this._Tokens.RemoveAt(index); }

		protected void RemoveTokenRange (int index, int count) { this._Tokens.RemoveRange(index, count); }

		protected void AddException (Exception exception)
		{
			if (exception == null) { throw (new ArgumentNullException("exception")); }

			if (exception != null)
			{
				this._Exceptions.Add(exception);

				if (this.Builder != null)
				{
					this.Builder.AddException(exception);
				}
			}
		}

		#endregion Methods.

		#region Methods: Parsing.

		//====================================================================================================
		// Methods: Parsing.
		//====================================================================================================

		public bool Parse ()
		{
			this.Initialize(false);

			try { this.Parsed = this.OnParse(); }
			catch (Exception exception) { this.Parsed = false; this.AddException(exception); }

			return (this.Parsed);
		}

		protected abstract bool OnParse ();

		#endregion Methods: Parsing.

		#region Methods: Compilation.

		//====================================================================================================
		// Methods: Compilation.
		//====================================================================================================

		public bool Compile ()
		{
			Exception exception = null;

			return (this.Compile(out exception));
		}

		public bool Compile (out Exception exception)
		{
			exception = null;

			try
			{
				if ((this.Parsed) || (this.SkipParseStage))
				{
					this.DissolveGenericTokens();
					this.CombineRedundantNegationOperatorsWithOperands();

					this.Compiled = this.OnCompile(out exception);

					if (!this.Compiled)
					{
						this.AddException(exception);
					}
				}
				else
				{
					throw (new ParserException("Compilation cannot proceed unless the builder has successfully parsed an expression."));
				}
			}
			catch (Exception e)
			{
				this.AddException(e);
				this.Compiled = false;
			}

			return (this.Compiled);
		}

		protected virtual bool OnCompile (out Exception exception)
		{
			exception = null;

			this.Compiled = true;
			foreach (var token in this.Tokens)
			{
				this.Compiled &= token.Compile();
			}

			return (true);
		}

		public void DissolveGenericTokens ()
		{
			List<GenericToken> tokens = null;

			tokens = this._Tokens.OfType<GenericToken>().ToList();

			foreach (var token in tokens)
			{
				this._Tokens.AddRange(token.Tokens);
			}

			this._Tokens.RemoveAll(t => tokens.Contains(t));

			this._Tokens.ForEach(t => t.DissolveGenericTokens());
		}

		public void CombineRedundantNegationOperatorsWithOperands ()
		{
			int index = 0;
			Token token = null;
			Token negation = null;

			//return;

			while ((index = this.FindNegationOperatorsThatCanSwallow()) >= 0)
			{
				negation = this.Tokens [index];
				token = this.Tokens [index + 1];

				this.RemoveTokenAt(index + 1);
				(negation as NegationOperatorToken).SwallowToken(token);
			}
		}

		#endregion Methods: Compilation.

		#region Methods: Calculation.

		//====================================================================================================
		// Methods: Calculation.
		//====================================================================================================

		public bool Calculate ()
		{
			double value = 0;
			Exception exception = null;

			return (this.Calculate(out value, out exception));
		}

		public bool Calculate (out double value, out Exception exception, int level = 0)
		{
			exception = null;
			value = double.NaN;

			try
			{
				if (((this.Parsed) || (this.SkipParseStage)) && ((this.Compiled) || (this.SkipCompileStage)))
				{
					this.Calculated = this.OnCalculate(out value, out exception);

					if (this.Calculated)
					{
						this.Calculated
							&= (!double.IsNaN(this.Value))
							&& (!double.IsNegativeInfinity(this.Value))
							&& (!double.IsInfinity(this.Value))
							&& (!double.IsPositiveInfinity(this.Value))
							;

						if (!this.Calculated)
						{
							throw (new ParserException("The calculation resulted in a non-numeric value: " + this.Value.ToString() + "."));
						}
					}
					else
					{
						this.AddException(exception);
					}
				}
				else
				{
					throw (new ParserException("Calculation cannot proceed unless the builder has successfully parsed and compiled an expression."));
				}
			}
			catch (Exception e)
			{
				exception = e;
				this.Calculated = false;
				this.AddException(exception);
			}

			return (this.Calculated);
		}

		protected virtual bool OnCalculate (out double value, out Exception exception, int level = 0)
		{
			throw (new NotImplementedException("Node Value: " + this.Text + "." + Environment.NewLine + this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name + " is not implemented."));
		}

		#endregion Methods: Calculation.

		#region Methods: Helper.

		//====================================================================================================
		// Methods: Helper.
		//====================================================================================================

		internal void GenerateRandomTree (Random random, int depth)
		{
			this.OnGenerateRandom(random, depth);
		}

		protected virtual void OnGenerateRandom (Random random, int depth)
		{
			throw (new NotImplementedException());
		}

		private int FindNegationOperatorsThatCanSwallow ()
		{
			int index = 0;

			index = -1;

			for (int i = (this.Tokens.Count - 1); i >= 0; i--)
			{
				if (this.Tokens [i].Type == TokenType.NegationOperator)
				{
					if ((this.Tokens [i] as NegationOperatorToken).CanSwallow)
					{
						index = i;

						break;
					}
				}
			}

			return (index);
		}

		public static bool ContainsInvalidCharacters (string text)
		{
			bool result = true;

			foreach (var character in text)
			{
				result
					&= (char.IsDigit(character))
					|| (char.IsLetter(character))
					|| (character == '_')
					|| (character == '@')
					|| (character == '#')
					|| ("+-*/".Any(c => c == character))
					|| ("()".Any(c => c == character))
					;

				if (!result)
				{
					break;
				}
			}

			return (false);
		}

		public virtual string GetText ()
		{
			return (this.Text);
			throw (new NotImplementedException());
			//string text = "";
			//List<Token> tokens = null;

			//tokens = this.Tokens.ToList();

			//foreach (var token in tokens)
			//{
			//    if (token.Type == TokenType.Parentheses)
			//    {
			//        text += "(";
			//        text += token.GetText();
			//        text += ")";
			//    }
			//    else if (token.Type == TokenType.Variable)
			//    {
			//        text += token.Value.ToString();
			//    }
			//    else if (token.Type == TokenType.Constant)
			//    {
			//        text += token.Value.ToString();
			//    }
			//    else
			//    {
			//        text += token.Text;
			//    }
			//}

			//return (text);
		}

		public int ContainsCalculationPairs ()
		{
			int index = -1;
			List<Token> tokens = null;

			tokens = this._Tokens.ToList();

			for (int i = 0; i < tokens.Count; i++)
			{
				if (i < (tokens.Count - 2))
				{
					if ((tokens [i].Type == TokenType.Constant) || (tokens [i].Type == TokenType.Variable) || (tokens [i].Type == TokenType.Function) || (tokens [i].Type == TokenType.Parentheses))
					{
						if (tokens [i + 1] is OperatorToken)
						{
							if ((tokens [i + 2].Type == TokenType.Constant) || (tokens [i + 2].Type == TokenType.Variable) || (tokens [i + 2].Type == TokenType.Function) || (tokens [i + 2].Type == TokenType.Parentheses))
							{
								index = i;
							}
						}
					}
				}
			}

			return (index);
		}

		public int ContainsParenthesesToken ()
		{
			int index = -1;

			for (int i = 0; i < this._Tokens.Count; i++)
			{
				if (this._Tokens [i] is ParenthesesToken)
				{
					index = i;

					break;
				}
			}

			return (index);
		}

		/// <summary>
		/// Checks to see if any positive/negation operators exist as separated tokens from their respective literals.
		/// </summary>
		/// <returns></returns>
		public int ContainsConsecutiveOperators ()
		{
			int index = -1;
			List<Token> tokens = null;

			tokens = this.Tokens.ToList();

			for (int i = 0; i < tokens.Count; i++)
			{
				if (tokens [i] is OperatorToken)
				{
					if (i < (tokens.Count - 2))
					{
						if (tokens [i + 1] is OperatorToken)
						{
							if ((tokens [i + 2] is ConstantToken) || (tokens [i + 2] is VariableToken))
							{
								index = i + 1;

								break;
							}
						}
					}
				}
			}

			return (index);
		}

		public List<Exception> GetAllExceptions ()
		{
			List<Exception> exceptions = null;

			exceptions = this.Exceptions.ToList();

			foreach (var t in this.Tokens)
			{
				exceptions.AddRange(t.GetAllExceptions());
			}

			return (exceptions);
		}

		public static bool IsCharacterOperator (char character) { return ("+-*/".Any(c => c == character)); }

		public static bool IsCharacterExponentSignOperator (char character) { return ("+-".Any(c => c == character)); }

		public static bool IsCharacterNonExponentSignOperator (char character) { return ("*/".Any(c => c == character)); }

		public static bool IsCharacterNegationOperator (char character) { return ("+-".Any(c => c == character)); }

		public static bool IsCharacterNonNegationOperator (char character) { return ("+*/".Any(c => c == character)); }

		public override string ToString () { return (this.Text); }

		public Token Clone ()
		{
			Token token = null;

			switch (this.Type)
			{
				case TokenType.Constant: { token = new ConstantToken(this.Builder, this.Text, this.OffsetLeft); break; }
				case TokenType.Function: { token = new FunctionToken(this.Builder, this.Text, this.OffsetLeft); break; }
				case TokenType.Generic: { token = new GenericToken(this.Builder, this.Text, this.OffsetLeft); break; }
				case TokenType.Operator: { token = new OperatorToken(this.Builder, this.Text, this.OffsetLeft); break; }
				case TokenType.NegationOperator: { token = new NegationOperatorToken(this.Builder, this.Text, this.OffsetLeft); break; }
				case TokenType.Parentheses: { token = new ParenthesesToken(this.Builder, this.Text, this.OffsetLeft); break; }
				case TokenType.Variable: { token = new VariableToken(this.Builder, this.Text, this.OffsetLeft); break; }
				default: { throw (new NotImplementedException()); }
			}

			return (token);
		}

		#endregion Methods: Helper.

		#region Static.

		//====================================================================================================
		// Static.
		//====================================================================================================

		#endregion Static.
	}
}