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

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public sealed class StringCaseInsensitiveComparer:
		IComparer<string>
	{
		public int Compare (string x, string y)
		{
			return (string.Compare(x, y, StringComparison.OrdinalIgnoreCase));
		}
	}
}