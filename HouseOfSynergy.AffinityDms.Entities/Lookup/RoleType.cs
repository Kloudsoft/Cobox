using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
	public enum MasterRoleType
	{
		None,
		Custom,
		Administrator,
		Reporting,
	}

	public enum TenantRoleType
	{
		None,
		Custom,
		Administrator,
		Scanner,
		Uploader,
		Indexer,
		TemplateCreator,
		FormCreator,
		WorkflowActor,
		WorkflowCreator,
		Reporting,
      //  None1,
      //  None2,
        SiteManager,
        SitePO,
        PoftfolioManager,
        SSOAD,
//        None3,
        HQAP,
    }
}