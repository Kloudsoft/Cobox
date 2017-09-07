using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Common
{
	public static class JsonNetFormatter
	{
		private static JsonSerializerSettings _JsonSerializerSettings = null;
		private static ReadOnlyCollection<JsonMediaTypeFormatter> _JsonMediaTypeFormatters = null;

		static JsonNetFormatter ()
		{
			JsonNetFormatter._JsonSerializerSettings = new JsonSerializerSettings();

			JsonNetFormatter._JsonSerializerSettings.Formatting = Formatting.Indented;
			JsonNetFormatter._JsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			JsonNetFormatter._JsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

			var formatter = new JsonMediaTypeFormatter();
			var formatters = new List<JsonMediaTypeFormatter>();

			formatter.SerializerSettings = new JsonSerializerSettings();
			formatter.SerializerSettings.Formatting = Formatting.Indented;
			formatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			formatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

			formatters.Add(formatter);
			JsonNetFormatter._JsonMediaTypeFormatters = formatters.AsReadOnly();
		}

		public static JsonSerializerSettings JsonSerializerSettings { get { return (JsonNetFormatter._JsonSerializerSettings); } }
		public static ReadOnlyCollection<JsonMediaTypeFormatter> JsonMediaTypeFormatters { get { return (JsonNetFormatter._JsonMediaTypeFormatters); } }
	}
}