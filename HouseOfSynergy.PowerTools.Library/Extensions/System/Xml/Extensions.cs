using System.Xml;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static XmlAttribute Append (this XmlAttributeCollection xmlAttributeCollection, XmlDocument xmlDocument, string name, string value)
		{
			XmlAttribute xmlAttribute = null;

			xmlAttribute = xmlDocument.CreateAttribute(name);
			xmlAttribute.Value = value;

			xmlAttributeCollection.Append(xmlAttribute);

			return (xmlAttribute);
		}
	}
}