using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public interface ILine:
		IEquatable<ILine>
	{
		float Slope { get; }
		bool IsVertical { get; }
		bool IsHorizontal { get; }
	}
}