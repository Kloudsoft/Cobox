using HouseOfSynergy.AffinityDms.Entities.Lookup;
using HouseOfSynergy.AffinityDms.WebRole.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.CustomKendo
{
	public class TreeViewInformation
	{
		public string Name { get; set; }
		public string Template { get; set; }
		public bool LoadOnDemand { get; set; }
		public bool Expanded { get; set; }
		public KendoTreeViewEvents Events { get; set; }
		public List<FolderTreeViewModel> Data { get; set; }
	}
}