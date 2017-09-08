using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
    /// <summary>
    /// Used to determine which type of settings are required.
    /// </summary>
    public enum SettingsType
    {
        None,
        UserManagement,
        AuditTrail,
        General,
        All,
    }
}
