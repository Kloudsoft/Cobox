using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Exceptions;
using HouseOfSynergy.PowerTools.Library.Interfaces;

namespace HouseOfSynergy.PowerTools.Library.Web
{
	public abstract class Response
	{
	}

	public sealed class Response<TData>:
		IPersistXmlElement<Response<TData>>
		where TData: ICustomData, new()
	{
		public TData Data { get; private set; }
		public bool Result { get; set; }
		public string Message { get; private set; }
		public ExceptionContainerCollection Exceptions { get; private set; }

		public Response ()
		{
			this.Exceptions = new ExceptionContainerCollection();

			this.Initialize();
		}

		public Response (bool result)
			: this()
		{
			this.Result = result;
		}

		public Response (TData data, bool result)
			: this(result)
		{
			if (data == null) { throw (new ArgumentNullException("data")); }

			this.Data = data;
		}

		public Response (TData data, bool result, string message)
			: this(data, result)
		{
			if (message == null) { throw (new ArgumentNullException("message")); }

			this.Message = message;
		}

		public Response (TData data, bool result, string message, Exception exception)
			: this(data, result, message)
		{
			this.Exceptions.Add(new ExceptionContainer(exception));
		}

		public Response (TData data, bool result, string message, List<Exception> exceptions)
			: this(data, result, message)
		{
			this.Exceptions.AddRange(exceptions.ConvertAll<HouseOfSynergy.PowerTools.Library.Exceptions.ExceptionContainer>(e => new HouseOfSynergy.PowerTools.Library.Exceptions.ExceptionContainer(e)));
		}

		public Response (TData data, bool result, string message, IEnumerable<Exception> exceptions)
			: this(data, result, message)
		{
			this.Exceptions.AddRange(exceptions.ToList().ConvertAll<ExceptionContainer>(e => new ExceptionContainer(e)));
		}

		public void Initialize ()
		{
			this.Message = "";
			this.Result = false;
			this.Data = new TData();
			this.Exceptions.Clear();
		}

		public XmlElement ToXmlElement (XmlDocument document)
		{
			XmlElement element = null;

			element = document.CreateElement("Response");
			element.Attributes.Append(document.CreateAttribute("Result"));
			element.Attributes.Append(document.CreateAttribute("Message"));
			element.AppendChild(document.CreateElement("Exceptions"));
			element.AppendChild(document.CreateElement("Data"));

			element.Attributes ["Result"].Value = this.Result.ToString();
			element.Attributes ["Message"].Value = this.Message.ToString();
			element ["Exceptions"].AppendChild(this.Exceptions.ToXmlElement(document));

			if (this.Data != null)
			{
				element ["Data"].AppendChild(this.Data.ToXmlElement(document));
			}

			return (element);
		}

		public Response<TData> FromXmlElement (XmlElement element)
		{
			this.Initialize();

			return (this);
		}
	}
}