using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder.Tokens
{
	public enum TokenType
	{
		Constant,
		Function,
		FunctionIdentifier,
		FunctionParameter,
		FunctionParameterList,
		Generic,
		NegationOperator,
		Operator,
		Parentheses,
		Variable,
	}
}