using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Library
{
	/// <summary>
	/// Services can only except values between 128 and 256 inclusive.
	/// Values less than 128 are system-reserved.
	/// </summary>
	public enum ServiceCommand
    {
        None = 0,
        RequestTerminate = 128 + 0,
    }
}