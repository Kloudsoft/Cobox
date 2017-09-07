using System.Linq;
using System.Xml.Linq;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IPersistXElementTo
	{
		/// <summary>
		/// Creates an Xml representation of T.
		/// </summary>
		/// <returns>The newly created XElement.</returns>
		XElement ToXElement ();
	}
}