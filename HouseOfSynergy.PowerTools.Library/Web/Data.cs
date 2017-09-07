using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.PowerTools.Library.Web
{
	public class Data:
		ICustomData
	{
		public List<XmlElement> Elements { get; private set; }

		public Data ()
		{
			this.Elements = new List<XmlElement>();
		}

		public virtual void Initialize ()
		{
			this.Elements.Clear();
		}

		public virtual XmlElement ToXmlElement (XmlDocument document)
		{
			var element = document.CreateElement("Data");

			foreach (var item in this.Elements)
			{
				var e = document.CreateElement("Element");

				e.AppendChild(item);

				element.AppendChild(e);
			}

			return (element);
		}

		public virtual void FromXmlElement (XmlElement element)
		{
			this.Initialize();

			if (element.Name != "Data") { throw (new ArgumentException("The argument [element] does not conform to [ICustomData].", "element")); }

			for (int i = 0; i < element.ChildNodes.Count; i++)
			{
				if (element.ChildNodes [i].Name != "Element") { throw (new ArgumentException("The argument [element] does not conform to [ICustomData].", "element")); }
				if (element.ChildNodes [i].ChildNodes.Count != 1) { throw (new ArgumentException("The argument [element] does not conform to [ICustomData].", "element")); }

				this.Elements.Add((XmlElement) element.ChildNodes [i].ChildNodes [0]);
			}
		}
	}
}