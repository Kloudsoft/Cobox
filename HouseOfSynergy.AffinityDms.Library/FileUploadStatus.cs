using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfSynergy.AffinityDms.Library
{
    public class FileUploadStatus
    {
        public string FileName { get; set; }
        public string File { get; set; }
        public bool Finalized { get; set; }
        public string StatusMessage { get; set; }
    }
}