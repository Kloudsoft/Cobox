using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.ResourceProvider.Classes
{
	public class CultureTranslation:
		IPersistXmlElement<CultureTranslation>,
		ICopyable<CultureTranslation>
	{
		public long Id { get; set; }

		public long CultureId { get; set; }
		public Culture Culture { get; set; }

		public long TranslationId { get; set; }
		public Translation Translation { get; set; }

		public string Value { get; set; }
		public string Comment { get; set; }
		public string ValueReference { get; set; }

		public CultureTranslation ()
		{
		}

		public void Initialize ()
		{
		}

		public CultureTranslation Clone ()
		{
			return (new CultureTranslation().CopyFrom(this));
		}

		public CultureTranslation CopyTo (CultureTranslation destination)
		{
			return (destination.CopyFrom(this));
		}

		public CultureTranslation CopyFrom (CultureTranslation source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public CultureTranslation FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}