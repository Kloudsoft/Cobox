using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using HouseOfSynergy.AffinityDms.BusinessLayer;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Common;
using HouseOfSynergy.AffinityDms.Library;
using HouseOfSynergy.AffinityDms.WebRole.Classes;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HouseOfSynergy.AffinityDms.WebRole.Pages
{
	public partial class MasterApiWebForms:
		System.Web.UI.Page
	{
		protected void Page_Load (object sender, EventArgs e)
		{
			var type = ApiMasterType.None;
			var document = Api.CreateRequest(type);
			var source = Api.GetApiHttpRequestNameValueCollection(this.Request);

			if (!string.IsNullOrWhiteSpace(source ["Type"]))
			{
				type = (ApiMasterType) Enum.Parse(typeof(ApiMasterType), source ["Type"]);

				try
				{
					if (Enum.IsDefined(typeof(ApiMasterType), type))
					{
						document = Api.CreateRequest(type);

						switch (type)
						{
							case ApiMasterType.Ping: { this.ProcessPing(source, document); break; }
							case ApiMasterType.GetPublicKey: { this.ProcessGetPublicKey(source, document); break; }
							case ApiMasterType.GetTenantByDomain: { this.ProcessGetTenantByDomain(source, document); break; }
							default: { this.ProcessBadRequest(source, document); break; }
						}
					}
					else
					{
						this.ProcessBadRequest(source, document);
					}
				}
				catch (Exception exception)
				{
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringFailure;
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Exception"].Value = exception.Message;

					if (AffinityConfiguration.IsConfigurationDebug)
					{
						document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["StackTrace"].Value = exception.StackTrace;
					}
				}
				finally
				{
					// TODO: Encrypt.
					this.Response.Clear();
					this.Response.Write(document.OuterXml);
				}
			}
		}

		private void ProcessBadRequest (NameValueCollection source, XmlDocument document)
		{
			document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringFailure;
			document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Exception"].Value = "Bad request type.";
		}

		private void ProcessPing (NameValueCollection source, XmlDocument document)
		{
			document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
		}

		private void ProcessGetPublicKey (NameValueCollection source, XmlDocument document)
		{
			var keyClient = source ["Key"];

			if (string.IsNullOrWhiteSpace(keyClient)) { throw (new Exception("Request parameters missing.")); }

			Exception exception = null;
			var output = new byte [] { };
			var filename = Path.Combine(this.MapPath("~/App_Data/Security"), "RsaKeyPublic.txt");
			var keyServer = File.ReadAllText(filename);

			// Validate.
			Rsa.KeyFromString(keyServer);
			Rsa.KeyFromBase64String(keyClient);

			if (!Rsa.Encrypt(keyServer, Encoding.ASCII, Rsa.KeyFromBase64String(keyClient), out output, out exception)) { throw (exception); }

			var element = document.CreateElement("Key");

			element.Attributes.Append(document, "Value", Convert.ToBase64String(output));

			document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Data"].AppendChild(element);
			document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
		}

		private void ProcessGetTenantByDomain (NameValueCollection source, XmlDocument document)
		{
			Tenant tenant = null;
			Exception exception = null;
			var domain = source ["Domain"];

			if (string.IsNullOrWhiteSpace(domain)) { throw (new Exception("Request parameters missing.")); }
			if (domain.Length == 0) { throw (new Exception("Request parameters invalid.")); }

			if (MasterTenantManagement.GetTenantByDomain(domain, out tenant, out exception))
			{
				document.DocumentElement ["DMS"] ["API"] ["Response"] ["Data"].AppendChild(tenant.ToXmlElement(document));
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringSuccess;
			}
			else
			{
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Result"].Value = Api.StringFailure;
				document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["Exception"].Value = exception.Message;

				if (AffinityConfiguration.IsConfigurationDebug)
				{
					document.DocumentElement ["DMS"] ["API"] ["Response"].Attributes ["StackTrace"].Value = exception.StackTrace;
				}
			}
		}
	}
}