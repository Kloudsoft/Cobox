using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class ProductGuidAttribute:
		Attribute
	{
		public Guid Guid { get; private set; }
		public string GuidString { get; private set; }

		public ProductGuidAttribute (string guid)
		{
			this.Guid = Guid.Parse(guid);
			this.GuidString = Guid.Parse(guid).ToString("D");
		}
	}
}