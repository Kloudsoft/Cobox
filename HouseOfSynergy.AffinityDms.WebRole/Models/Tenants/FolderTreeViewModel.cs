using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
    public class FolderTreeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }
        public long? ParentId { get; set; }
		public string ParentIdStr { get; set; }
        public long UserCreatedById { get; set; }
        public List<FolderTreeViewModel> Childrens { get; set; }
		public static bool ConvertListToHeirarchy(List<FolderTreeViewModel> folders,long? folderId, out List<FolderTreeViewModel> hierarchy, out Exception exception)
		{
			var result = false;
			Action<FolderTreeViewModel> setChildren = null;
			hierarchy = null;
			exception = null;
			try
			{
				setChildren = parent =>
				{
					if (parent.Childrens == null)
					{
						parent.Childrens = new List<FolderTreeViewModel>();
					}
					var list
						= folders
						.Where(childItem => ((!childItem.ParentId.HasValue) || (childItem.ParentId == parent.Id)))
						.ToList();

					foreach (var item in list)
					{
						//item.Parent = parent;
						parent.Childrens.Add(item);
					}

					// Recursively call the setChildren method for each child.
					parent.Childrens
						.ToList()
						.ForEach(setChildren);
				};

				// Initialize the hierarchical list to root level items.
				List<FolderTreeViewModel> hierarchicalItems
					= folders
					.Where(rootItem => ((!rootItem.ParentId.HasValue) || (rootItem.ParentId == folderId)))
					.ToList();

				// Call the SetChildren method to set the children on each root level item.
				hierarchicalItems.ForEach(setChildren);

				result = true;
				hierarchy = hierarchicalItems;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}
	}
}