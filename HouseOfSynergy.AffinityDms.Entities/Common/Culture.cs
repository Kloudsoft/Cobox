using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Common
{
	public class Culture:
		IEntity<Culture>
	{
		public virtual long Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int LocaleId { get; set; }
		public virtual string NameNative { get; set; }
		public virtual string NameDisplay { get; set; }
		public virtual string NameEnglish  { get; set; }
		public virtual string NameIsoTwoLetter { get; set; }
		public virtual string NameIsoThreeLetter { get; set; }
		public virtual string NameWindowsThreeLetter { get; set; }

		public Culture()
		{
		}

		public CultureInfo ToCultureInfo ()
		{
			var cultureInfo = CultureInfo
				.GetCultures(CultureTypes.AllCultures)
				.SingleOrDefault(c => c.Name == this.Name);

			return (cultureInfo);
		}

		public Culture FromCultureInfo (CultureInfo cultureInfo)
		{
			this.Name = cultureInfo.Name;
			this.LocaleId = cultureInfo.LCID;
			this.NameNative = cultureInfo.NativeName;
			this.NameDisplay = cultureInfo.DisplayName;
			this.NameEnglish = cultureInfo.EnglishName;
			this.NameIsoTwoLetter = cultureInfo.TwoLetterISOLanguageName;
			this.NameIsoThreeLetter = cultureInfo.ThreeLetterISOLanguageName;
			this.NameWindowsThreeLetter = cultureInfo.ThreeLetterWindowsLanguageName;

			return (this);
		}

		public void Initialize()
		{
		}

		public Culture Clone()
		{
			return (new Culture().CopyFrom(this));
		}

		public Culture CopyTo(Culture destination)
		{
			return (destination.CopyFrom(this));
		}

		public Culture CopyFrom(Culture source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement(XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Culture FromXmlElement(XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}
	}
}