using System.Linq;
using System.Xml;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IPersistXmlElement:
		IPersistXmlElementTo,
		IInitializable
	{
		/// <summary>
		/// Initializes T and copies values from the provided XmlElement.
		/// </summary>
		/// <param name="element">The XmlElement to populate values from.</param>
		void FromXmlElement (XmlElement element);
	}

	public interface IPersistXmlElement<T>:
		IPersistXmlElementTo,
		IInitializable
	{
		/// <summary>
		/// Initializes T and copies values from the provided XmlElement.
		/// </summary>
		/// <param name="element">The XmlElement to populate values from.</param>
		/// <returns>Retuens a self reference for chanied syntax.</returns>
		T FromXmlElement (XmlElement element);
	}
}