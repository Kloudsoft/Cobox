using System;
using System.Drawing;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public interface ILineSegmentF:
		IEquatable<ILineSegmentF>,
		ILine
	{
		float X1 { get; }
		float Y1 { get; }
		float X2 { get; }
		float Y2 { get; }

		PointF Point1 { get; }
		PointF Point2 { get; }

		float Width { get; }
		float Height { get; }

		float AbsoluteWidth { get; }
		float AbsoluteHeight { get; }

		float Length { get; }

		bool IsPoint { get; }
	}
}