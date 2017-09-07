using System;
using System.Drawing;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public interface ILineSegment:
		IEquatable<ILineSegment>,
		ILine
	{
		int X1 { get; }
		int Y1 { get; }
		int X2 { get; }
		int Y2 { get; }

		Point Point1 { get; }
		Point Point2 { get; }

		int Width { get; }
		int Height { get; }

		int AbsoluteWidth { get; }
		int AbsoluteHeight { get; }

		float Length { get; }

		bool IsPoint { get; }
	}
}