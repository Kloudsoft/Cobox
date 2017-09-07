using HouseOfSynergy.AffinityDms.Entities.Tenants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.WebRole.Models.Tenants
{
    public class DocumentFolderTreeViewModel
    {
        public Folder Parentfolder { get; set; }
        public List<Folder> ChildFolders { get; set; }
        public List<Document> ParentFolderDocuments { get; set; }
    }
}