using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Collections;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.PowerTools.Library.Web
{
	public abstract class Request
	{
		public const string TextSuccess = "True";
		public const string TextFailure = "False";

		protected static readonly Encoding EncodingDefault = Encoding.UTF8;
	}

	public class Request<TEnum>:
		Request,
		IPersistXmlDocument<Request<TEnum>>
		where TEnum: struct, IComparable, IFormattable, IConvertible
	{
		public TEnum Type { get; private set; }
		public ICustomData CustomData { get; private set; }
		public Response<Data> Response { get; private set; }
		public Dictionary<string, string> CustomDictionary { get; private set; }

		private Request ()
		{
			EnumUtilities.ThrowOnEnumWithFlags<TEnum>();

			if (!EnumUtilities.IsCompliantWithNoFlagsAndNoneAtZero<TEnum>()) { throw (new ArgumentException("The generic argument [<TEnum>] must be an enumeration without the [Flags] attribute and the first element as [None = 0].", "TEnum")); }

			this.Type = default(TEnum);
			this.CustomData = new Data();
			this.Response = new Response<Data>();
			this.CustomDictionary = new Dictionary<string, string>();

			this.Initialize();
		}

		public Request (TEnum type) : this() { this.Type = type; }
		public Request (TEnum type, ICustomData customData) : this(type) { this.CustomData = customData; }
		public Request (TEnum type, Dictionary<string, string> customDictionary) : this(type) { this.CustomDictionary = customDictionary; }
		public Request (TEnum type, Dictionary<string, string> customDictionary, ICustomData customData) : this(type) { this.CustomData = customData; this.CustomDictionary = customDictionary; }

		public void Initialize ()
		{
			this.Type = default(TEnum);
			this.Response.Initialize();
			this.CustomData.Initialize();
			this.CustomDictionary.Clear();
		}

		public bool Process (Uri uri, HttpRequestMethod httpRequestMethod, SymmetricAlgorithm algorithm, out Exception exception)
		{
			var result = false;

			exception = null;

			try
			{
				var proxy = uri;
				var data = new byte [] { };
				var encoding = Request<TEnum>.EncodingDefault;
				var dictionary = new Dictionary<string, string>();

				foreach (var pair in this.CustomDictionary)
				{
					//dictionary.Add(pair.Key, pair.Value);
					dictionary.Add(ConversionUtilities.Encode(pair.Key, encoding), ConversionUtilities.Encode(pair.Value, encoding));
				}

				using (var client = new WebClient())
				{
					client.Proxy = WebRequest.GetSystemWebProxy();

					if (client.Proxy != null)
					{
						if (client.Proxy.IsBypassed(uri)) { proxy = uri; }
						else { proxy = client.Proxy.GetProxy(uri); }
					}

					data = client.UploadValues(proxy, httpRequestMethod.ToString().ToUpper(), dictionary.ToNameValueCollection());
				}

				var document = new XmlDocument();
				var xml = encoding.GetString(data);
				document.LoadXml(xml);
				this.FromXmlDocument(document);

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			XmlElement element = null;

			element = document.CreateElement("Request");
			element.AppendChild(document.CreateElement("Request"));
			element ["Request"].Attributes.Append(document, "Type", this.Type.ToString());

			element ["Request"].AppendChild(document.CreateElement("CustomData"));
			element ["Request"] ["CustomData"].AppendChild(this.CustomData.ToXmlElement(document));

			try
			{
				element ["Request"].AppendChild(document.CreateElement("CustomDictionary"));
				foreach (var pair in this.CustomDictionary)
				{
					var e = document.CreateElement("KeyValuePair");
					e.Attributes.Append(document, pair.Key, pair.Value);
					element ["Request"] ["CustomDictionary"].AppendChild(e);
				}
			}
			catch (Exception eee)
			{
				Console.Write(eee);
			}

			element.AppendChild(this.Response.ToXmlElement(document));

			return (element);
		}

		public Request<TEnum> FromXmlElement (XmlElement element)
		{
			this.Initialize();

			return (this);
		}

		public XmlDocument ToXmlDocument ()
		{
			XmlElement element = null;
			XmlDocument document = null;

			document = new XmlDocument();

			element = document.CreateElement("HouseOfSynergy");
			element.AppendChild(document.CreateElement("PowerTools"));
			element ["PowerTools"].AppendChild(document.CreateElement("Library"));
			element ["PowerTools"] ["Library"].AppendChild(document.CreateElement("Web"));
			element ["PowerTools"] ["Library"] ["Web"].AppendChild(document.CreateElement("Request"));
			element ["PowerTools"] ["Library"] ["Web"] ["Request"].AppendChild(this.ToXmlElement(document));

			document.AppendChild(element);

			return (document);
		}

		public XmlDocument ToXmlDocument (XmlDocument document)
		{
			XmlElement element = null;

			element = document.CreateElement("HouseOfSynergy");
			element.AppendChild(document.CreateElement("PowerTools"));
			element ["PowerTools"].AppendChild(document.CreateElement("Library"));
			element ["PowerTools"] ["Library"].AppendChild(document.CreateElement("Web"));
			element ["PowerTools"] ["Library"] ["Web"].AppendChild(document.CreateElement("Request"));
			element ["PowerTools"] ["Library"] ["Web"] ["Request"].AppendChild(this.ToXmlElement(document));

			document.AppendChild(element);

			return (document);
		}

		public Request<TEnum> FromXmlDocument (XmlDocument document)
		{
			this.Initialize();

			var elementData = document ["HouseOfSynergy"] ["PowerTools"] ["Library"] ["Web"] ["Request"] ["Request"] ["Response"] ["Data"] ["Data"];

			this.Response.Data.FromXmlElement(elementData);

			//this.CustomData = new Data().from

			return (this);
		}

		public static Request<TEnum> FromType (TEnum type)
		{
			EnumUtilities.ThrowOnEnumWithFlags<TEnum>();

			return (new Request<TEnum>(type));
		}

		public static Request<TEnum> FromType (TEnum type, Dictionary<string, string> customDictionary)
		{
			EnumUtilities.ThrowOnEnumWithFlags<TEnum>();

			return (new Request<TEnum>(type, customDictionary));
		}

		//public static Request<TEnum> FromType (TEnum type, ReadOnlyDictionary<string, string> customDictionary)
		//{
		//	EnumUtilities.ThrowOnEnumWithFlags<TEnum>();

		//	return (new Request<TEnum>(type, customDictionary));
		//}

		public static Request<TEnum> FromType (TEnum type, ICustomData customData)
		{
			EnumUtilities.ThrowOnEnumWithFlags<TEnum>();

			return (new Request<TEnum>(type, customData));
		}

		public static Request<TEnum> FromType (TEnum type, Dictionary<string, string> customDictionary, ICustomData customData)
		{
			EnumUtilities.ThrowOnEnumWithFlags<TEnum>();

			return (new Request<TEnum>(type, customDictionary, customData));
		}

		//public static Request<TEnum> FromType (TEnum type, ReadOnlyDictionary<string, string> customDictionary, ICustomData customData)
		//{
		//	EnumUtilities.ThrowOnEnumWithFlags<TEnum>();

		//	return (new Request<TEnum>(type, customDictionary, customData));
		//}
	}
}