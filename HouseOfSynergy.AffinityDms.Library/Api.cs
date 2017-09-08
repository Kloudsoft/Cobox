using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Extensions;

namespace HouseOfSynergy.AffinityDms.Library
{
	public enum ApiTargetType
	{
		None,
		Master,
		Ocr,
		Tenant,
	}

	public enum ApiMasterType
	{
		None,
		Ping,
		GetPublicKey,
		GetTenantByDomain,
	}

	public enum ApiOcrType
	{
		None,
		Ping,
	}

	public enum ApiTenantType
	{
		None,
		Ping,
		SignIn,
		SignOut,
		GetFolders,
		GetPublicKey,
		ValidateToken,
		GetDocumentByHash,
		DocumentEntryCreate,
		DocumentEntryFinalize,
		DocumentEntryPerformOcr,
	}

	public enum EnumApiHttpRequestType
	{
		Get,
		Post,
	}

	public static class Api
	{
		public const string StringSuccess = "Success";
		public const string StringFailure = "Failure";
		public const string StringException = "Exception";

		public static string HttpRequestMethod { get { return (Api.HttpRequestType.ToString().ToUpper()); } }
		public static EnumApiHttpRequestType HttpRequestType { get { return (EnumApiHttpRequestType.Post); } }
		public static NameValueCollection GetApiHttpRequestNameValueCollection (HttpRequest httpRequest) { return (Api.HttpRequestType == EnumApiHttpRequestType.Get ? httpRequest.QueryString : httpRequest.Form); }

		public static XmlDocument CreateRequest (ApiMasterType type) { return (Api.CreateRequest(type.ToString())); }
		public static XmlDocument CreateRequest (ApiOcrType type) { return (Api.CreateRequest(type.ToString())); }
		public static XmlDocument CreateRequest (ApiTenantType type) { return (Api.CreateRequest(type.ToString())); }

		public static XmlDocument CreateRequest (string type)
		{
			XmlElement element = null;
			XmlDocument document = null;

			document = new XmlDocument();

			document.LoadXml("<HouseOfSynergy></HouseOfSynergy>");

			element = document.CreateElement("DMS");
			element.Attributes.Append(document, "Version", "1.0.0.0");
			document.DocumentElement.AppendChild(element);

			element = document.CreateElement("API");
			element.Attributes.Append(document, "Version", "1.0.0.0");
			document.DocumentElement ["DMS"].AppendChild(element);

			element = document.CreateElement("Request");
			element.Attributes.Append(document, "Type", type.ToString());
			document.DocumentElement ["DMS"] ["API"].AppendChild(element);

			element = document.CreateElement("Response");
			element.Attributes.Append(document, "Result", "");
			element.Attributes.Append(document, "Message", "");
			element.Attributes.Append(document, "Exception", "");
			element.Attributes.Append(document, "StackTrace", "");
			document.DocumentElement ["DMS"] ["API"].AppendChild(element);

			element = document.CreateElement("Data");

			document.DocumentElement ["DMS"] ["API"] ["Response"].AppendChild(element);

			return (document);
		}

		public static bool ProcessRequest (string uri, NameValueCollection values, out XmlDocument document, out Exception exception)
		{
			byte [] data = null;
			bool result = false;
			string response = "";
			NameValueCollection encrypted = null;

			document = null;
			exception = null;

			try
			{
				using (var client = new WebClient())
				{
					document = new XmlDocument();

					encrypted = new NameValueCollection();

					for (int i = 0; i < values.AllKeys.Length; i++)
					{
						// TODO: Encrypt.
						encrypted.Add(values.AllKeys [i], values [values.AllKeys [i]]);
					}

					//client.Proxy = CloudTech.ATS.Library.Global.Options.Proxy.GetWebProxyFromCurrentSettings();
					data = client.UploadValues(uri, Api.HttpRequestMethod, encrypted);
				}

				response = GlobalConstants.EncodingCryptography.GetString(data);

				// TODO: Decrypt.
				//response = CloudTech.ATS.Library.Cryptography.Decrypt(response);

				response = response.Substring(response.IndexOf("<HouseOfSynergy>"));
				response = response.Substring(0, response.IndexOf("</HouseOfSynergy>") + "</HouseOfSynergy>".Length);

				document.LoadXml(response);

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
				document = null;
			}

			return (result);
		}
	}
}