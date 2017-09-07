using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Extensions;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.PowerTools.Library.Exceptions
{
	public class ExceptionContainer:
		IPersistXmlElement<ExceptionContainer>
	{
		public IDictionary Data { get; private set; }
		public string HelpLink { get; private set; }
		public ExceptionContainer InnerException { get; private set; }
		public string Message { get; private set; }
		public string Source { get; private set; }
		public string StackTrace { get; private set; }
		public string TargetSite { get; private set; }
		public string Raw { get; private set; }

		public ExceptionContainer ()
		{
			this.Initialize();
		}

		public ExceptionContainer (Exception exception)
		{
			this.Initialize(exception);
		}

		public virtual void Initialize ()
		{
			this.Data = new Dictionary<string, string>();
			this.HelpLink = "";
			this.InnerException = null;
			this.Message = "";
			this.Source = "";
			this.StackTrace = "";
			this.TargetSite = "";
			this.Raw = "";
		}

		public virtual void Initialize (Exception exception)
		{
			if (exception == null) { throw (new ArgumentNullException("exception")); }

			this.Data = exception.Data ?? new Dictionary<string, string>();
			this.HelpLink = exception.HelpLink ?? "";
			this.InnerException = exception.InnerException == null ? null : new ExceptionContainer(exception.InnerException);
			this.Message = exception.Message ?? "";
			this.Source = exception.Source ?? "";
			this.StackTrace = exception.StackTrace ?? "";
			this.TargetSite = exception.TargetSite.Name ?? "";
			this.Raw = exception.ToString();
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			XmlElement pair = null;
			XmlElement element = null;

			element = document.CreateElement("Exception");

			element.Attributes.Append(document, "HelpLink", this.HelpLink);
			element.Attributes.Append(document, "Message", this.Message);
			element.Attributes.Append(document, "Source", this.Source);
			element.Attributes.Append(document, "StackTrace", this.StackTrace);
			element.Attributes.Append(document, "TargetSite", this.TargetSite);
			element.Attributes.Append(document, "Raw", this.ToString());

			element.AppendChild(document.CreateElement("Data"));
			element.AppendChild(document.CreateElement("InnerException"));

			foreach (var key in this.Data.Keys)
			{
				pair = document.CreateElement("KeyValuePair");

				pair.Attributes.Append(document, "Key", key.ToString());
				pair.Attributes.Append(document, "Value", (this.Data [key] == null ? "" : this.Data [key]).ToString());

				element ["Data"].AppendChild(pair);
			}

			if (this.InnerException != null)
			{
				element ["InnerException"].AppendChild(this.InnerException.ToXmlElement(document));
			}

			return (element);
		}

		public ExceptionContainer FromXmlElement (XmlElement element)
		{
			this.Initialize();

			return (this);
		}
	}
}