using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.Attributes
{
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class ProductVersionAttribute:
		Attribute
	{
		public Version Version { get; private set; }
		public string VersionString { get; private set; }

		public ProductVersionAttribute (string version)
		{
			this.Version = Version.Parse(version);
			this.VersionString = Version.Parse(version).ToString();
		}
	}
}