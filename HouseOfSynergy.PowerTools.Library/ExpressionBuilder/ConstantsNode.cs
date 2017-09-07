using System.Collections.Generic;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ExpressionBuilder
{
	public sealed class ConstantsNode:
		HouseOfSynergy.PowerTools.Library.ExpressionBuilder.INodeDoubleNullable
	{
		private Dictionary<string, double> Dictionary { get; set; }

		public ConstantsNode ()
		{
			this.Dictionary = new Dictionary<string, double>();

			this.Dictionary.Add("Pi", System.Math.PI);
			this.Dictionary.Add("EulersNumberE", System.Math.E);
			this.Dictionary.Add("SquareRootOf2", System.Math.Sqrt(2D));
			this.Dictionary.Add("GoldenRatio", (System.Math.Sqrt(5) + 1D) / 2D);
			this.Dictionary.Add("SingleEpsilon", float.Epsilon);
			this.Dictionary.Add("SingleMinimum", float.MinValue);
			this.Dictionary.Add("SingleMaximum", float.MaxValue);
			this.Dictionary.Add("DoubleEpsilon", double.Epsilon);
			this.Dictionary.Add("DoubleMinimum", double.MinValue);
			this.Dictionary.Add("DoubleMaximum", double.MaxValue);
		}

		public string Name { get { return ("Constants"); } }
		public string Prefix { get { return ("Constants"); } }
		public List<string> AllKeys { get { return (this.Dictionary.Keys.ToList()); } }

		public double? GetValue (string key) { return (this.Dictionary [key]); }

		public bool ContainsKey (string key) { return (this.Dictionary.ContainsKey(key)); }

		public bool IsCompatibleVariableType (string key) { return (this.ContainsKey(key)); }
	}
}