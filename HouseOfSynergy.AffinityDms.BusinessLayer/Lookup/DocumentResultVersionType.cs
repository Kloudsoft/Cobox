using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.BusinessLayer.Lookup
{
	public enum DocumentResultVersionType
	{
		All,
		Minimum,
		Maximum,
        Parent,
        Exact,
	}
}