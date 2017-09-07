using System.Collections.ObjectModel;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Drawing
{
	public interface IPointND<T>:
		IPoint<T>
		where T: struct
	{
		int VerticeCount { get; }
		ReadOnlyCollection<T> Vertices { get; }
	}
}