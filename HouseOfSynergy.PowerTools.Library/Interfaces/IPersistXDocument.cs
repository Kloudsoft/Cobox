using System.Linq;
using System.Xml.Linq;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IPersistXDocument<T>:
		IPersistXElement<T>,
		IInitializable
	{
		/// <summary>
		/// Creates an Xml representation of T.
		/// </summary>
		/// <returns>The newly created XDocument.</returns>
		XDocument ToXDocument ();

		/// <summary>
		/// Initializes T and copies values from the provided XDocument.
		/// </summary>
		/// <param name="document">The XDocument to populate values from.</param>
		/// <returns>Retuens a self reference for chanied syntax.</returns>
		T FromXDocument (XDocument document);
	}
}