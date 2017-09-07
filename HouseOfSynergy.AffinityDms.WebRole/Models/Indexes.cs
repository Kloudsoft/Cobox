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
}