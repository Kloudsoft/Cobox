using System;
using System.Linq;
using System.Xml;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static byte [] GetRange (this byte [] value, int index, int count)
		{
			byte [] bytes = new byte [count];

			Array.Copy(value, index, bytes, 0, count);

			return (bytes);
		}

		public static XmlElement ToXmlElement (this byte [] bytes, XmlDocument document)
		{
			XmlElement element = null;

			element = document.CreateElement("Bytes");

			bytes.ToList().ForEach
			(
				b =>
				{
					XmlElement be = null;

					be = document.CreateElement("Byte");
					be.Attributes.Append(document, "Value", b.ToString());

					element.AppendChild(be);
				}
			);

			return (element);
		}

		public static byte [] FromXmlElement (this byte [] bytes, XmlElement element)
		{
			byte [] temp = null;

			temp = new byte [element.ChildNodes.Count];

			for (int i = 0; i < element.ChildNodes.Count; i++)
			{
				temp [i] = byte.Parse(element.ChildNodes [i].Attributes ["Value"].Value);
			}

			return (temp);
		}
	}
}