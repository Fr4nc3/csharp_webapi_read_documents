using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSearch.Models
{
    /// <summary>
    ///  to have together al the variables
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        /// <value>The search results.</value>
        public List<BlobSearchResult> SearchResults { get; set; }
        /// <summary>
        /// Pagination variables
        /// </summary>
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public bool ShowPrevious { get; set; }
        public bool ShowNext { get; set; }
        public string PreviousUrl { get; set; }
        public string NextUrl { get; set; }
        public int FromItem { get; set; }
        public int ToItem { get; set; }
    }
}
