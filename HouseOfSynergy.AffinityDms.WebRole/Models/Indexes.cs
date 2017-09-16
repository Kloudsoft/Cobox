using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace HouseOfSynergy.AffinityDms.WebRole.Models
{
    public class Indexes
    {
        public FacetResults Facets { get; set; }
        public IList<SearchResult> Results { get; set; }
        public int? Count { get; set; }
    }

    public class IndexesLookup
    {
        public Document Result { get; set; }
    }

    public class AzureSearchResults       // Added by Nandha 
    {
        public string account_no { get; set; }
        public string delivery_order { get; set; }
        public string vendorname { get; set; }
        public string ocr { get; set; }
        public string link { get; set; }

    }

}