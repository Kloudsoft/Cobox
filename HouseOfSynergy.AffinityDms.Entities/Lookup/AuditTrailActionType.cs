using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
	public enum AuditTrailActionType
	{
		None,
		View,
		Add,
		Edit,
		Delete,
        Rename,
        Move,
        Search,
        Confirm,

        CancelCheckOut,
        CheckIn,
        CheckOut,
        AddIndex,
        UpdateVersion,
        Retrieve,
        RetrieveVersions,
        RetrieveAll,
        RetrieveAllRelated,
        RetrieveAccessRights,
        UpdateAccessRights,
        EnableManualClassification,
        FinalizeDocument,
        MarkPrivate,
        MarkPublic,
        UpdateIndex,
        AddFragments,
        AddCorrectiveIndexing,
        UpdateCorrectiveIndexing,
        RetrieveCorrectiveIndexing,


    }
}