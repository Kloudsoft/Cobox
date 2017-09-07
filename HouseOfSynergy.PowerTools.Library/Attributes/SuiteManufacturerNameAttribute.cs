using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class ProductManufacturerNameAttribute: Attribute
	{
		public string Company { get; private set; }

		public ProductManufacturerNameAttribute (string company)
		{
			this.Company = company;
		}
	}
}