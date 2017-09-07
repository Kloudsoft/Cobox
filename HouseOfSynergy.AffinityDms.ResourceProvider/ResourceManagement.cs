using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.ResourceProvider.Classes;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.ResourceProvider
{
	public static partial class ResourceManagement
	{
		public static bool IsLoaded { get; private set; }
		public static ReadOnlyDictionary<string, ReadOnlyDictionary<string, string>> Dictionary { get; private set; }

		static ResourceManagement ()
		{
			ResourceManagement.IsLoaded = false;
			ResourceManagement.Dictionary = new ReadOnlyDictionary<string, ReadOnlyDictionary<string, string>>(new Dictionary<string, ReadOnlyDictionary<string, string>>());
		}

		public static bool Load (out Exception exception)
		{
			var filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources.xml");

			return (ResourceManagement.Load(filename, out exception));
		}
		public static bool Load (string filename, out Exception exception)
		{
			var result = false;
			var dictionaryValues = new Dictionary<string, string>();
			var dictionaryCultures = new Dictionary<string, ReadOnlyDictionary<string, string>>();

			exception = null;

			ResourceManagement.IsLoaded = false;

			try
			{
				var resourceContainer = new ResourceContainer();

				if (resourceContainer.Load(filename, out exception))
				{
					foreach (var culture in resourceContainer.Cultures)
					{
						dictionaryValues = new Dictionary<string, string>();

						foreach (var translation in resourceContainer.Translations)
						{
							dictionaryValues.Add(translation.Name, resourceContainer.CultureTranslations.Single(ct => ((ct.Culture.Id == culture.Id) && (ct.Translation.Id == translation.Id))).Value);
						}

						dictionaryCultures.Add(culture.Name, dictionaryValues.ToReadOnlyDictionary());
					}

					ResourceManagement.IsLoaded = true;
					ResourceManagement.Dictionary = dictionaryCultures.ToReadOnlyDictionary();

					result = true;
				}
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}
	}
}