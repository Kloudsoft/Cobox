using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public interface IPoint3D<T>:
		IPoint<T>,
		IPoint2D<T>
		where T: struct
	{
		T Z { get; }
	}
}