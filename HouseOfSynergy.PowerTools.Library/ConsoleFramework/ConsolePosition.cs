using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public struct ConsolePosition
	{
		private int _X;
		private int _Y;

		public ConsolePosition (int left, int top)
		{
			this._X = left;
			this._Y = top;
		}

		public int X { get { return (this._X); } }
		public int Y { get { return (this._Y); } }
		public int Left { get { return (this._X); } }
		public int Top { get { return (this._Y); } }
	}
}