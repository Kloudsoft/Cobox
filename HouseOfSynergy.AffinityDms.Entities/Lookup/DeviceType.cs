using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
	/// <summary>
	/// Determines the type of user client device.
	/// </summary>
	public enum DeviceType
	{
		None,
		Desktop,
		Mobile,
		Other,
		Unknown,
	}
}