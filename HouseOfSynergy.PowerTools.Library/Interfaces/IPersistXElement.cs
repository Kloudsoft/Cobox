using System.Linq;
using System.Xml.Linq;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IPersistXElement:
		IPersistXElementTo,
		IInitializable
	{
		/// <summary>
		/// Initializes T and copies values from the provided XElement.
		/// </summary>
		/// <param name="element">The XElement to populate values from.</param>
		void FromXElement (XElement element);
	}

	public interface IPersistXElement<T>:
		IPersistXElementTo,
		IInitializable
	{
		/// <summary>
		/// Initializes T and copies values from the provided XElement.
		/// </summary>
		/// <param name="element">The XElement to populate values from.</param>
		/// <returns>Retuens a self reference for chanied syntax.</returns>
		T FromXElement (XElement element);
	}
}