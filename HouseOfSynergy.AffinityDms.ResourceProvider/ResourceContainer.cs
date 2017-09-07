using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.ResourceProvider.Classes
{
	public class ResourceContainer:
		IPersistXmlElement<ResourceContainer>,
		IPersistXmlDocument<ResourceContainer>,
		ICopyable<ResourceContainer>
	{
		/// <summary>
		/// TODO: Decide between [Encoding.Unicode] and [Encoding.UTF8].
		/// </summary>
		public static readonly Encoding EncodingDefault = Encoding.UTF8;

		private List<Culture> _Cultures = null;
		private List<Translation> _Translations = null;
		private List<CultureTranslation> _CultureTranslations = null;

		public ReadOnlyCollection<Culture> Cultures { get; private set; }
		public ReadOnlyCollection<Translation> Translations { get; private set; }
		public ReadOnlyCollection<CultureTranslation> CultureTranslations { get; private set; }

		public ResourceContainer ()
		{
			this._Cultures = new List<Culture>();
			this._Translations = new List<Translation>();
			this._CultureTranslations = new List<CultureTranslation>();

			this.Cultures = new ReadOnlyCollection<Culture>(this._Cultures);
			this.Translations = new ReadOnlyCollection<Translation>(this._Translations);
			this.CultureTranslations = new ReadOnlyCollection<CultureTranslation>(this._CultureTranslations);
		}

		//public ReadOnlyCollection<Culture> Cultures { get { return (this._Cultures.AsReadOnly()); } }
		//public ReadOnlyCollection<Translation> Translations { get { return (this._Translations.AsReadOnly()); } }
		//public ReadOnlyCollection<CultureTranslation> CultureTranslations { get { return (this._CultureTranslations.AsReadOnly()); } }

		public void Initialize ()
		{
			this._Cultures.Clear();
			this._Translations.Clear();
			this._CultureTranslations.Clear();
		}

		public void Add (params Culture [] cultures)
		{
			this.Add(cultures.ToList());
		}

		public void Add (IEnumerable<Culture> cultures)
		{
			if (cultures == null) { throw (new ArgumentNullException("cultures")); }
			if (cultures.Any(c => c == null)) { throw (new ArgumentException("The argument [cultures] contains at least one null element.", "cultures")); }

			foreach (var culture in cultures)
			{
				this.Add(culture);
			}
		}

		public void Add (Culture culture)
		{
			if (culture == null) { throw (new ArgumentNullException("culture")); }

			var message = "";
			if (!culture.Validate(out message)) { throw (new ArgumentException(message, "translation")); }
			if (this._Cultures.Any(c => string.Compare(c.Name, culture.Name, StringComparison.Ordinal) == 0)) { throw (new ArgumentException("A culture with the same name already exists.", "culture")); }

			culture.Id = this._Cultures.Any() ? (this._Cultures.Max(c => c.Id) + 1) : 1;

			this._Cultures.Add(culture);

			foreach (var translation in this._Translations)
			{
				var cultureTranslation = new CultureTranslation()
				{
					Id = this._CultureTranslations.Any() ? (this._CultureTranslations.Max(ct => ct.Id)) : 1,
					Value = "",
					Comment = "",
					ValueReference = "",
					Culture = culture,
					CultureId = culture.Id,
					Translation = translation,
					TranslationId = translation.Id,
				};

				this._CultureTranslations.Add(cultureTranslation);
			}
		}

		public void Add (params Translation [] translations)
		{
			this.Add(translations.ToList());
		}

		public void Add (IEnumerable<Translation> translations)
		{
			if (translations == null) { throw (new ArgumentNullException("translations")); }
			if (translations.Any(t => t == null)) { throw (new ArgumentException("The argument [translations] contains at least one null element.", "translations")); }

			foreach (var translation in translations)
			{
				this.Add(translation);
			}
		}

		public void Add (Translation translation)
		{
			if (translation == null) { throw (new ArgumentNullException("translation")); }

			var message = "";
			if (!translation.Validate(out message)) { throw (new ArgumentException(message, "translation")); }
			if (this._Translations.Any(t => string.Compare(t.Name, translation.Name, StringComparison.OrdinalIgnoreCase) == 0)) { throw (new ArgumentException("A translation with the same name already exists.", "translation")); }

			translation.Id = this._Translations.Any() ? (this._Translations.Max(t => t.Id) + 1) : 1;

			this._Translations.Add(translation);

			foreach (var culture in this._Cultures)
			{
				var cultureTranslation = new CultureTranslation()
				{
					Id = this._CultureTranslations.Any() ? (this._CultureTranslations.Max(ct => ct.Id) + 1) : 1,
					Value = "",
					Comment = "",
					ValueReference = "",
					Culture = culture,
					CultureId = culture.Id,
					Translation = translation,
					TranslationId = translation.Id,
				};

				this._CultureTranslations.Add(cultureTranslation);
			}
		}

		private void Add (params CultureTranslation [] cultureTranslations)
		{
			this.Add(cultureTranslations.ToList());
		}

		private void Add (IEnumerable<CultureTranslation> cultureTranslations)
		{
			if (cultureTranslations == null) { throw (new ArgumentNullException("cultureTranslations")); }
			if (cultureTranslations.Any(ct => ct == null)) { throw (new ArgumentException("The argument [cultureTranslations] contains at least one null element.", "cultureTranslations")); }

			foreach (var cultureTranslation in cultureTranslations)
			{
				this.Add(cultureTranslation);
			}
		}

		private void Add (CultureTranslation cultureTranslation)
		{
			if (cultureTranslation == null) { throw (new ArgumentNullException("cultureTranslation")); }

			if (cultureTranslation.Culture == null) { throw (new ArgumentException("The argument [cultureTranslation] has a null culture reference.", "cultureTranslation")); }
			if (cultureTranslation.Translation == null) { throw (new ArgumentException("The argument [cultureTranslation] has a null translation reference.", "cultureTranslation")); }

			if (cultureTranslation.Culture.Id != cultureTranslation.CultureId) { throw (new ArgumentException("The argument [cultureTranslation] has a mismatch between its culture reference and culture identifier.", "cultureTranslation")); }
			if (cultureTranslation.Translation.Id != cultureTranslation.TranslationId) { throw (new ArgumentException("The argument [cultureTranslation] has a mismatch between its translation reference and translation identifier.", "cultureTranslation")); }

			if (cultureTranslation.Culture.Id <= 0) { throw (new ArgumentException("The argument [cultureTranslation] has a an invalid culture identifier.", "cultureTranslation")); }
			if (cultureTranslation.Translation.Id <= 0) { throw (new ArgumentException("The argument [cultureTranslation] has a an invalid translation identifier.", "cultureTranslation")); }

			if (this._Cultures.SingleOrDefault(c => c.Id == cultureTranslation.Culture.Id) == null) { throw (new ArgumentException("The argument [cultureTranslation] has a a broken culture identifier.", "cultureTranslation")); }
			if (this._Translations.SingleOrDefault(t => t.Id == cultureTranslation.Translation.Id) == null) { throw (new ArgumentException("The argument [cultureTranslation] has a a broken translation identifier.", "cultureTranslation")); }

			cultureTranslation.Id = this._CultureTranslations.Any() ? (this._CultureTranslations.Max(ct => ct.Id) + 1) : 1;

			this._CultureTranslations.Add(cultureTranslation);
		}

		public bool Remove (Culture culture)
		{
			var result = this._Cultures.Remove(culture);

			if (result)
			{
				this._CultureTranslations.ToList().RemoveAll(ct => ct.Culture.Id == culture.Id);
			}

			return (result);
		}

		public bool Remove (Translation translation)
		{
			var result = this._Translations.Remove(translation);

			if (result)
			{
				this._CultureTranslations.ToList().RemoveAll(ct => ct.Translation.Id == translation.Id);
			}

			return (result);
		}

		public bool Save (string filename)
		{
			Exception exception = null;
			return (this.Save(filename, out exception));
		}

		public bool Save (string filename, out Exception exception)
		{
			var message = "";
			var result = false;

			exception = null;

			try
			{
				if (!this.Validate(out message)) { throw (new Exception(message)); }

				var document = this.ToXmlDocument();

				var settings = new XmlWriterSettings();
				settings.ConformanceLevel = ConformanceLevel.Auto;
				settings.Encoding = ResourceContainer.EncodingDefault;
				settings.Indent = true;
				settings.IndentChars = "	";
				settings.NamespaceHandling = NamespaceHandling.Default;
				settings.NewLineChars = Environment.NewLine;
				settings.NewLineHandling = NewLineHandling.None;
				settings.NewLineOnAttributes = false;
				settings.OmitXmlDeclaration = false;
				settings.WriteEndDocumentOnClose = false;

				using (var writer = XmlTextWriter.Create(filename, settings))
				{
					document.Save(writer);
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public bool Load (string filename)
		{
			Exception exception = null;
			return (this.Load(filename, out exception));
		}

		public bool Load (string filename, out Exception exception)
		{
			var result = false;

			exception = null;

			try
			{
				this.Initialize();

				var document = new XmlDocument();

				document.Load(filename);
				this.FromXmlDocument(document);

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public bool Validate (out string message)
		{
			var result = true;
			var found = false;

			message = "";

			if (((!this._Cultures.Any()) || (!this._Translations.Any())) && (this._CultureTranslations.Any()))
			{
				result = false;
				message += Environment.NewLine + " - Invalid Culture Translations found.";
			}

			if (this._Cultures.GroupBy(c => c.Id).Any(g => g.Count() > 1))
			{
				result = false;
				message += Environment.NewLine + " - Duplicate Culture Id found.";
			}

			if (this._Cultures.GroupBy(c => c.Name.ToLower()).Any(g => g.Count() > 1))
			{
				result = false;
				message += Environment.NewLine + " - Duplicate Culture Name found.";
			}

			if (this._Translations.GroupBy(c => c.Id).Any(g => g.Count() > 1))
			{
				result = false;
				message += Environment.NewLine + " - Duplicate Translation Id found.";
			}

			if (this._Translations.GroupBy(c => c.Name.ToLower()).Any(g => g.Count() > 1))
			{
				result = false;
				message += Environment.NewLine + " - Duplicate Translation Name found.";
			}

			if (this._CultureTranslations.GroupBy(ct => ct.Id).Any(g => g.Count() > 1))
			{
				result = false;
				message += Environment.NewLine + " - Duplicate Culture Translation Id found.";
			}

			for (int i = 0; i < this._CultureTranslations.Count; i++)
			{
				for (int j = 0; j < this._CultureTranslations.Count; j++)
				{
					var x = this._CultureTranslations [i];
					var y = this._CultureTranslations [j];

					if ((x != y) && (x.Id != y.Id))
					{
						if ((x.CultureId == y.CultureId) && (x.TranslationId == y.TranslationId))
						{
							found = true;
							result = false;
							message += Environment.NewLine + " - Duplicate Culture Translation combination found.";
						}
					}

					if (found) { break; }
				}

				if (found) { break; }
			}

			foreach (var cultureTranslation in this._CultureTranslations)
			{
				if (this._Cultures.All(c => (c.Id != cultureTranslation.CultureId)))
				{
					result = false;
					message += Environment.NewLine + " - Invalid Culture Translation found.";
				}

				if (this._Translations.All(t => (t.Id != cultureTranslation.TranslationId)))
				{
					result = false;
					message += Environment.NewLine + " - Invalid Culture Translation found.";
				}
			}

			if (!result) { message = "The following inconsistencies were found:" + Environment.NewLine + message; }

			return (result);
		}

		public ResourceContainer Clone ()
		{
			return (new ResourceContainer().CopyFrom(this));
		}

		public ResourceContainer CopyTo (ResourceContainer destination)
		{
			return (destination.CopyFrom(this));
		}

		public ResourceContainer CopyFrom (ResourceContainer source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			var elementResources = document.CreateElement("Resources");

			var elementCultures = document.CreateElement("Cultures");
			var elementTranslations = document.CreateElement("Translations");
			var elementCultureTranslations = document.CreateElement("CultureTranslations");

			elementResources.AppendChild(elementCultures);
			elementResources.AppendChild(elementTranslations);
			elementResources.AppendChild(elementCultureTranslations);

			foreach (var culture in this._Cultures)
			{
				elementCultures.AppendChild(culture.ToXmlElement(document));
			}

			foreach (var translation in this._Translations)
			{
				elementTranslations.AppendChild(translation.ToXmlElement(document));
			}

			// Empty CultureTranslation.ValueReference.
			var cultureTranslations = this._CultureTranslations.ConvertAll(ct => { var cultureTranslation = ct.Clone(); cultureTranslation.ValueReference = ""; return (cultureTranslation); });

			foreach (var cultureTranslation in cultureTranslations)
			{
				elementCultureTranslations.AppendChild(cultureTranslation.ToXmlElement(document));
			}

			return (elementResources);
		}

		public ResourceContainer FromXmlElement (XmlElement element)
		{
			if (element.Name != "Resources") { throw (new Exception("The provided Xml Element is invalid.")); }
			if (element ["Cultures"] == null) { throw (new Exception("The provided Xml Element is invalid.")); }
			if (element ["Translations"] == null) { throw (new Exception("The provided Xml Element is invalid.")); }
			if (element ["CultureTranslations"] == null) { throw (new Exception("The provided Xml Element is invalid.")); }

			this.Initialize();

			for (int i = 0; i < element ["Cultures"].ChildNodes.Count; i++)
			{
				this._Cultures.Add(new Culture().FromXmlElement(element ["Cultures"].ChildNodes [i] as XmlElement));
			}

			for (int i = 0; i < element ["Translations"].ChildNodes.Count; i++)
			{
				this._Translations.Add(new Translation().FromXmlElement(element ["Translations"].ChildNodes [i] as XmlElement));
			}

			for (int i = 0; i < element ["CultureTranslations"].ChildNodes.Count; i++)
			{
				this._CultureTranslations.Add(new CultureTranslation().FromXmlElement(element ["CultureTranslations"].ChildNodes [i] as XmlElement));
			}

			foreach (var cultureTranslation in this._CultureTranslations)
			{
				cultureTranslation.Culture = this._Cultures.SingleOrDefault(c => c.Id == cultureTranslation.CultureId);
				cultureTranslation.Translation = this._Translations.SingleOrDefault(t => t.Id == cultureTranslation.TranslationId);
			}

			return (this);
		}

		public XmlDocument ToXmlDocument ()
		{
			var document = new XmlDocument();

			document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><HouseOfSynergy><AffinityDms></AffinityDms></HouseOfSynergy>");

			document ["HouseOfSynergy"] ["AffinityDms"].AppendChild(this.ToXmlElement(document));

			return (document);
		}

		public ResourceContainer FromXmlDocument (XmlDocument document)
		{
			if (document.DocumentElement.Name != "HouseOfSynergy") { throw (new Exception("The provided Xml Document is invalid.")); }
			if (document.DocumentElement ["AffinityDms"] == null) { throw (new Exception("The provided Xml Document is invalid.")); }
			if (document.DocumentElement ["AffinityDms"] ["Resources"] == null) { throw (new Exception("The provided Xml Document is invalid.")); }

			this.Initialize();

			this.FromXmlElement(document.DocumentElement ["AffinityDms"] ["Resources"]);

			return (this);
		}
	}
}