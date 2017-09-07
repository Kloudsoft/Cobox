using System.Linq;
using System.Xml;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IPersistXmlDocument:
		IPersistXmlDocumentTo,
		IPersistXmlElement,
		IInitializable
	{
		/// <summary>
		/// Initializes T and copies values from the provided XmlDocument.
		/// </summary>
		/// <param name="document">The XmlDocument to populate values from.</param>
		void FromXmlDocument (XmlDocument document);
	}

	public interface IPersistXmlDocument<T>:
		IPersistXmlDocumentTo,
		IPersistXmlElement<T>,
		IInitializable
	{
		/// <summary>
		/// Initializes T and copies values from the provided XmlDocument.
		/// </summary>
		/// <param name="document">The XmlDocument to populate values from.</param>
		/// <returns>Retuens a self reference for chanied syntax.</returns>
		T FromXmlDocument (XmlDocument document);
	}
}