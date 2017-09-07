using System.Linq;
using System.Xml;

namespace HouseOfSynergy.PowerTools.Library.Interfaces
{
	public interface IPersistXmlDocumentTo:
		IPersistXmlElementTo
	{
		/// <summary>
		/// Creates an Xml representation of T.
		/// </summary>
		/// <returns>The newly created XmlDocument.</returns>
		XmlDocument ToXmlDocument ();
	}
}