using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.PowerTools.Library.Exceptions
{
	public class ExceptionContainerCollection:
		List<ExceptionContainer>,
		IPersistXmlElement<ExceptionContainerCollection>
	{
		public virtual void Initialize ()
		{
			this.Clear();
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			XmlElement element = null;

			element = document.CreateElement("Exceptions");

			foreach (var exception in this)
			{
				element.AppendChild(exception.ToXmlElement(document));
			}

			return (element);
		}

		public ExceptionContainerCollection FromXmlElement (XmlElement element)
		{
			this.Initialize();

			foreach (XmlNode e in element.ChildNodes)
			{
				this.Add(new ExceptionContainer().FromXmlElement(e as XmlElement));
			}

			return (this);
		}
	}
}