using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
    /// <summary>
    /// Status for maintaining document index status
    /// </summary>
    public enum DocumentCorrectiveIndexValueStatus
    {
        /// <summary>
        /// Default 
        /// </summary>
        None, //0
        /// <summary>
        /// When a user performs indexing and saves it.
        /// </summary>
        Updated, //1
        /// <summary>
        /// When user performs indexing and approves the document to be processed for verification.
        /// </summary>
        Submitted, //2
    }
}
