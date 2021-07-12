using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSearch.Settings
{
    /// <summary>
    /// Defines the search settings
    /// </summary>
    public class SearchSettings
    {
        /// <summary>
        /// The key used to query the index
        /// </summary>
        /// <value>The query key.</value>
        public string QueryKey { get; set; }

        /// <summary>
        /// The index name
        /// </summary>
        /// <value>The index name.</value>
        public string IndexName { get; set; }

        /// <summary>
        /// The search service name
        /// </summary>
        /// <value>The name of the service.</value>
        public string ServiceName { get; set; }
        /// <summary>
        /// url for blob container 
        /// </summary>
        /// <value>the url for the blob container</value>
        public string ContainerUrl { get; set; }
        /// <summary>
        ///  limit result per page
        /// </summary>
        /// <value>limit number</value>
        public int PageSize { get; set; }
    }
}
