using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public interface INode<TDataType>
	{
		string Name { get; }
		string Prefix { get; }
		List<string> AllKeys { get; }

		bool ContainsKey (string key);
		TDataType GetValue (string key);
		bool IsCompatibleVariableType (string key);
	}
}