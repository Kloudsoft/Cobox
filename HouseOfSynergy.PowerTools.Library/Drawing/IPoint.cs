using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public interface IPoint<T>
		where T: struct
	{
		T Distance { get; }
		PointType Type { get; }
	}
}