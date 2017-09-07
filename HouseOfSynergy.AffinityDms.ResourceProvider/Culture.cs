using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.ResourceProvider.Classes
{
	public class Culture:
		IPersistXmlElement<Culture>,
		ICopyable<Culture>
	{
		public const int LocaleIdMinimum = 1;
		public const int NameLengthMinimum = 1;
		public const int LocaleIdMaximum = int.MaxValue;

		public long Id { get; set; }

		public int LocaleId { get; set; }
		public string Name { get; set; }
		public string NameNative { get; set; }
		public string NameDisplay { get; set; }
		public string NameEnglish { get; set; }
		public string NameIsoTwoLetter { get; set; }
		public string NameIsoThreeLetter { get; set; }
		public string NameWindowsThreeLetter { get; set; }

		public Culture ()
		{
		}

		public void Initialize ()
		{
		}

		public bool Validate (out string message)
		{
			var result = true;

			message = "";

			if (this.LocaleId < Culture.LocaleIdMinimum) { result = false; message += Environment.NewLine + " - Culture locale identifier must be greater than or equal to " + Culture.LocaleIdMinimum.ToString() + "."; }
			if (this.LocaleId > Culture.LocaleIdMaximum) { result = false; message += Environment.NewLine + " - Culture locale identifier must be less than or equal to " + Culture.LocaleIdMaximum.ToString() + "."; }

			if (string.IsNullOrWhiteSpace(this.Name)) { result = false; message += Environment.NewLine + " - Culture name cannot be empty."; }
			if (string.IsNullOrWhiteSpace(this.NameNative)) { result = false; message += Environment.NewLine + " - Culture native name cannot be empty."; }
			if (string.IsNullOrWhiteSpace(this.NameDisplay)) { result = false; message += Environment.NewLine + " - Culture display name cannot be empty."; }
			if (string.IsNullOrWhiteSpace(this.NameEnglish)) { result = false; message += Environment.NewLine + " - Culture english name cannot be empty."; }
			if (string.IsNullOrWhiteSpace(this.NameIsoTwoLetter)) { result = false; message += Environment.NewLine + " - Culture Iso two-letter name cannot be empty."; }
			if (string.IsNullOrWhiteSpace(this.NameIsoThreeLetter)) { result = false; message += Environment.NewLine + " - Culture Iso three-letter name cannot be empty."; }
			if (string.IsNullOrWhiteSpace(this.NameWindowsThreeLetter)) { result = false; message += Environment.NewLine + " - Culture Windows three-letter name cannot be empty."; }

			if ((this.Name ?? "").Trim().Length < Culture.NameLengthMinimum) { result = false; message += Environment.NewLine + " - Culture name must be at least " + Culture.NameLengthMinimum.ToString() + " characters long."; }

			if (!result) { message = "Culture has the following inconsistencies:" + Environment.NewLine + message; }

			return (result);
		}

		public override string ToString ()
		{
			if ((this.Name == "en") || (this.Name.StartsWith("en-")))
			{
				return (this.NameEnglish + "  - [" + this.Name + "]");
			}
			else
			{
				return (this.NameEnglish + "  - " + this.NameNative);
			}
		}

		public Culture Clone ()
		{
			return (new Culture().CopyFrom(this));
		}

		public Culture CopyTo (Culture destination)
		{
			return (destination.CopyFrom(this));
		}

		public Culture CopyFrom (Culture source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var element = ReflectionUtilities.ToXmlElement(document, this);

			return (element);
		}

		public Culture FromXmlElement (XmlElement element)
		{
			ReflectionUtilities.FromXmlElement(this, element);

			return (this);
		}

		public static Culture FromCultureInfo (CultureInfo cultureInfo)
		{
			var culture = new Culture();

			culture.Name = cultureInfo.Name;
			culture.LocaleId = cultureInfo.LCID;
			culture.NameNative = cultureInfo.NativeName;
			culture.NameDisplay = cultureInfo.DisplayName;
			culture.NameEnglish = cultureInfo.EnglishName;
			culture.NameIsoTwoLetter = cultureInfo.TwoLetterISOLanguageName;
			culture.NameIsoThreeLetter = cultureInfo.ThreeLetterISOLanguageName;
			culture.NameWindowsThreeLetter = cultureInfo.ThreeLetterWindowsLanguageName;

			return (culture);
		}

		public static ReadOnlyCollection<Culture> GetDefaultCultures ()
		{
			var cultureInfos = CultureInfo
				.GetCultures(CultureTypes.AllCultures)
				.Where
				(
					c =>
					(
						(c.Name == "en")
						|| (c.Name == "en-US")
						|| (c.Name == "en-GB")
						|| (c.Name == "zh-Hans")
						|| (c.Name == "ur")
						|| (c.Name == "ur-PK")
					)
				)
				.OrderBy(c => (c.Name != "en"))
				.ThenBy(c => (c.Name != "en-US"))
				.ThenBy(c => (c.Name != "en-GB"))
				.ThenBy(c => (c.Name != "zh-Hans"))
				.ThenBy(c => (c.Name != "ur"))
				.ThenBy(c => (c.Name != "ur-PK"))
				.ToList();

			var index = 0;
			var cultures = cultureInfos.ConvertAll(ci => Culture.FromCultureInfo(ci));

			cultures.ForEach(c => c.Id = ++index);

			return (cultures.AsReadOnly());
		}
	}
}