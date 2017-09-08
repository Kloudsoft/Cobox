using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
    public enum DiscussionPostAttachmentType
    {
        None,
        Template,
        Document,
        External, // Change to ExternalLocal. This doc will be uploaded by user and stored on Azure.
        Live, // Live document from DropBox, Google, OneDrive, etc.
        Form,
    }
}