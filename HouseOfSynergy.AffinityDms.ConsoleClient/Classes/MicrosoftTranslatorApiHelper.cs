using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HouseOfSynergy.AffinityDms.Library;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Classes
{
	public static class MicrosoftTranslatorApiHelper
	{
		public static bool GetAccessToken (out string header, out Exception exception)
		{
			var result = false;

			header = "";
			exception = null;

			try
			{
				var url
					= "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";

				var parameters
					= "client_id="
					+ HttpUtility.UrlEncode("dde1e30b-3146-4f44-b6f1-db9f1128dbd4")
					+ "&"
					+ "client_secret="
					+ HttpUtility.UrlEncode("sEZTPAg/BZkjf1aJYU95OsmgN08Vng6SB4jXoEdZlWM=")
					+ "&"
					+ "scope="
					+ "http://api.microsofttranslator.com"
					+ "&"
					+ "grant_type="
					+ "client_credentials";

				var request = WebRequest.Create(url);
				var bytes = Encoding.ASCII.GetBytes(parameters);

				request.ContentType = "application/x-www-form-urlencoded";
				request.Method = "POST";
				request.ContentLength = bytes.Length;

				using (var stream = request.GetRequestStream())
				{
					stream.Write(bytes, 0, bytes.Length);
				}

				var response = request.GetResponse();

				var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(AdmAccessToken));

				// Get deserialized object from JSON stream.
				var token = (AdmAccessToken) serializer.ReadObject(response.GetResponseStream());

				header = "Bearer " + token.access_token;

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool GetTranslation (string header, string textSource, CultureInfo cultureInfoSource, CultureInfo cultureInfoTarget, out string textTranslated, out Exception exception)
		{
			var result = false;

			exception = null;
			textTranslated = "";

			try
			{
				var uri
					= "http://api.microsofttranslator.com/v2/Http.svc/Translate"
					+ "?text="
					+ System.Web.HttpUtility.UrlEncode(textSource)
					+ "&from="
					+ cultureInfoSource.Name
					+ "&to="
					+ cultureInfoTarget.Name;

				var request = WebRequest.Create(uri);

				request.Headers.Add("Authorization", header);

				using (var response = request.GetResponse())
				{
					using (var stream = response.GetResponseStream())
					{
						//System.Text.Encoding.GetEncoding("utf-8");
						using (var reader = new System.IO.StreamReader(stream, GlobalConstants.EncodingCryptography))
						{
							var document = new System.Xml.XmlDocument();

							document.LoadXml(reader.ReadToEnd());

							textTranslated = document.InnerText;
						}
					}
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}
	}
}