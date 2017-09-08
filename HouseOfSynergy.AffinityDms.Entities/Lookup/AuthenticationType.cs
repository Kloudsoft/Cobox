using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
	public enum AuthenticationType
	{
		None,
		Local,
		ActiveDirectory,
		Both,
        External,
	}
}