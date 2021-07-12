using DocumentSearch.Models;
using DocumentSearch.Services;
using DocumentSearch.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSearch.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The search settings
        /// </summary>
        private SearchSettings _searchSettings;

        /// <summary>
        /// To use the filter 
        /// </summary>
        public SelectList ContentTypeList { get; set; }
        /// <summary>
        ///  direction dropdown
        /// </summary>
        public SelectList DirectionList { get; set; }
        public SelectList SortFieldList { get; set; }
        /// <summary>
        /// Class to keep all the url params together
        /// </summary>
        public SearchParam SearchParams { get; set; }
        /// <summary>
        ///  full result variables from the search engine
        /// </summary>
        public SearchResult Results { get; set; }
        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        /// <value>The search results.</value>
        public List<BlobSearchResult> SearchResults { get; set; }
        /// <summary>
        /// The search index client instance
        /// </summary>
        private SearchIndexClient _searchClientInstance;
        public List<PageItem> PageItems { get; set; }
        /// <summary>
        /// Gets the search client instance.
        /// </summary>
        /// <value>The search client instance.</value>
        public SearchIndexClient SearchClientInstance
        {
            get
            {
                if (_searchClientInstance == null)
                {
                    _searchClientInstance = new SearchIndexClient(_searchSettings.ServiceName,
                                                                  _searchSettings.IndexName,
                                                                  new SearchCredentials(_searchSettings.QueryKey));
                }

                return _searchClientInstance;
            }
        }
        /// <summary>
        /// logger
        /// </summary>
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="searchSettings">The search settings.</param>
        public IndexModel(ILogger<IndexModel> logger, IOptions<SearchSettings> searchSettings)
        {
            _logger = logger;
            _searchSettings = searchSettings.Value;
        }

        /// <summary>
        /// on get as an asynchronous operation.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="filter"></param>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <param name="currentPage"></param>
        /// <returns>Task.</returns>
        public async Task OnGetAsync(string searchString, string filter, string sortField, string sortDirection, int currentPage = 1)
        {
            // create the Search paramter object
            SearchParams = new SearchParam()
            {
                SearchString = searchString,
                CurrentPage = currentPage,
                Filter = filter,
                SortField = sortField,
                SortDirection = sortDirection,
                PageSize = _searchSettings.PageSize,
                ContainerUrl = _searchSettings.ContainerUrl
            };
            // direction dropdown
            var dic = new List<string>() { "asc", "desc" };
            DirectionList = new SelectList(dic);
            if (!string.IsNullOrEmpty(SearchParams.SortDirection))
            {
                var selected = DirectionList.Where(x => x.Text == SearchParams.SortDirection).First();
                if (selected!=null)
                {
                    selected.Selected = true;
                } 
            }
            // sort field default setting
            var sor = new List<string>() { "DocumentType", "DocumentName", "LastModifiedDate"};
            SortFieldList = new SelectList(sor);
            if (!string.IsNullOrEmpty(SearchParams.SortDirection))
            {
                var selectedField = SortFieldList.Where(x => x.Text == SearchParams.SortField).First();
                if (selectedField != null)
                {
                    selectedField.Selected = true;
                }
            }


            /// set the main variables
            ContentTypeList = await SearchServices.ExecuteSearchForContents(SearchParams, SearchClientInstance);
            Results = await SearchServices.ExecuteSearch(SearchParams, SearchClientInstance);

            PageItems = SearchServices.BuildPagination(Results, SearchParams);
           
            if (!string.IsNullOrEmpty(SearchParams.Filter))
            {
                var selectedFilter = ContentTypeList.Where(x => x.Text == SearchParams.Filter).First();
               
                if (selectedFilter != null)
                {
                    selectedFilter.Selected = true;
                }
            }
            SearchResults = Results.SearchResults;

        }

    }
}
