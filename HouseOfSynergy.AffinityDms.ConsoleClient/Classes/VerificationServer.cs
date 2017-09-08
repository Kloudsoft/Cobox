using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Utility;
using HouseOfSynergy.PowerTools.Library.Web;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Classes
{
    public sealed class VerificationServer:
        HttpServer
    {
        public VerificationServer(Uri uri) : this(new Uri[] { uri }) { }
        public VerificationServer(IEnumerable<Uri> uris) : this(uris, HttpServer.EncodingDefault) { }
        public VerificationServer(Uri uri, Encoding encoding) : this(new Uri[] { uri }, encoding) { }
        public VerificationServer(IEnumerable<Uri> uris, Encoding encoding) : base(uris, encoding) { }

        protected override void OnContextCreated(HttpListenerContext context, CancellationToken cancellationToken)
        {
            base.OnContextCreated(context, cancellationToken);
        }

        protected override byte[] OnDataReceived(byte[] buffer, Encoding encoding, CancellationToken cancellationToken)
        {
            // Verify fingetprint data.
            var data = Encoding.UTF8.GetString(buffer);
			var nameValuePairs = data.Split('&');
			var dictionary = new Dictionary<string, string>();

			foreach (var nameValuePair in nameValuePairs)
			{
				var pair = nameValuePair.Split('=');
				var name = ConversionUtilities.Decode(HttpUtility.UrlDecode(pair [0]), encoding);
				var value = ConversionUtilities.Decode(HttpUtility.UrlDecode(pair [1]), encoding);

				dictionary.Add(name, value);
			}

			var type = (VerificationCommand) Enum.Parse(typeof(VerificationCommand), dictionary ["Type"], false);
			var request = Request<VerificationCommand>.FromType(type);

			switch (request.Type)
			{
				case VerificationCommand.Enroll:
				{
					long userId = 0;
					var document = new XmlDocument();

					// Call SDK to get fingerprint image.
					var image = encoding.GetBytes(dictionary ["FingetprintImage"]);

					// Process [image], and create DB entry, and get newly created user details.
					userId = 1;

					var elementUser = document.CreateElement("User");
					elementUser.Attributes.Append(document, "Id", userId.ToString());
					request.Response.Data.Elements.Add(elementUser);

					return (encoding.GetBytes(request.ToXmlDocument(document).OuterXml));
				}
				case VerificationCommand.Verify:
				{
					long userId = 0;
					var document = new XmlDocument();

					// Call SDK to get fingerprint image.
					//var image = encoding.GetBytes(dictionary ["FingetprintImage"]);

					// Process [image] and get corresponding UserId.
					userId = 1;

					var elementUser = document.CreateElement("User");
					elementUser.Attributes.Append(document, "Id", userId.ToString());
					request.Response.Data.Elements.Add(elementUser);
					request.Response.Result = true;

					return (encoding.GetBytes(request.ToXmlDocument(document).OuterXml));
				}
			}

            return (new byte [] { });
        }

        protected override byte[] OnTimeout(byte[] buffer, Encoding encoding, CancellationToken cancellationToken)
        {
            return (base.OnTimeout(buffer, encoding, cancellationToken));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
