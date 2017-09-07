using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public interface IPoint2D<T>:
		IPoint<T>
		where T: struct
	{
		T X { get; }
		T Y { get; }
	}
}