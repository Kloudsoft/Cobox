using System.Linq;
using System.Xml;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IPersistXmlElementTo
	{
		/// <summary>
		/// Creates an Xml representation of T.
		/// </summary>
		/// <param name="document">The XmlDocument used to create an XmlElement.</param>
		/// <returns>The newly created XmlElement.</returns>
		XmlElement ToXmlElement (XmlDocument document);
	}
}