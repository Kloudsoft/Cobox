using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public struct ConsoleDimension
	{
		private int _Width;
		private int _Height;

		public ConsoleDimension (int width, int height)
		{
			this._Width = width;
			this._Height = height;
		}

		public int Width { get { return (this._Width); } }
		public int Height { get { return (this._Height); } }
	}
}