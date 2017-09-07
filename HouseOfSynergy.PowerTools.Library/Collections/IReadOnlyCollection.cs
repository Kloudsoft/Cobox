using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Collections
{
	public interface IReadOnlyCollection
	{
		bool AllowListEdit { get; }
		bool AllowItemEdit { get; }
	}
}