using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class ProductManufacturerUrlAttribute: Attribute
	{
		public Uri Uri { get; private set; }
		public string Url { get; private set; }

		public ProductManufacturerUrlAttribute (string uri)
		{
			this.Uri = new Uri(uri);
			this.Url = new Uri(uri).AbsoluteUri;
		}
	}
}