using DocumentSearch.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSearch.Services
{
    /// <summary>
    /// Service for the search
    /// </summary>
    public class SearchServices
    {
        /// <summary>
        ///  Create the oagination 
        /// </summary>
        /// <returns>the page list</returns>
        public static List<PageItem> BuildPagination(SearchResult searchResult, SearchParam searchParam)
        {
            List<PageItem> pages = new List<PageItem>();
            foreach (int value in Enumerable.Range(1, searchResult.TotalPages))
            {
                pages.Add(new PageItem()
                {
                    number = value,
                    isCurrent = value == searchParam.CurrentPage,
                    url = BuildUrlVariables(value, searchParam)
                });
            }
            return pages;
        }
        /// <summary>
        /// build the url
        /// </summary>
        /// <param name="number"></param>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public static string BuildUrlVariables(int number, SearchParam searchParam)
        {
            string url = $"/?currentPage={number}&";
            //string searchString, string filter, string sortField, string sortDirection, int currentPage = 1
            if (!string.IsNullOrEmpty(searchParam.SearchString))
            {
                url += $"searchString={searchParam.SearchString}&";
            }
            if (!string.IsNullOrEmpty(searchParam.Filter))
            {
                url += $"filter={searchParam.Filter}&";
            }
            if (!string.IsNullOrEmpty(searchParam.SortField))
            {
                url += $"sortField={searchParam.SortField}&";
            }
            if (!string.IsNullOrEmpty(searchParam.SortDirection))
            {
                url += $"sortDirection={searchParam.SortDirection}&";
            }

            return url;
        }
        /// <summary>
        /// to not whow the rial values of the filter
        /// </summary>
        /// <param name="sortField"></param>
        /// <returns></returns>
        public static string SortField(string sortField)
        {

            switch (sortField)
            {
                case "DocumentType":
                    return "metadata_storage_content_type";
                case "DocumentName":
                    return "metadata_storage_name";
                case "LastModifiedDate":
                    return "metadata_storage_last_modified";
                default:
                    return "metadata_storage_name"; // default sort by 
            }
        }
        /// <summary>
        /// Executes the search.
        /// </summary>
        /// <param name="searchParam"></param>
        /// <param name="SearchClientInstance">The client.</param>
        /// <returns>Updates the SearchResults upon return.</returns>
        /// <returns></returns>
        public static async Task<SearchResult> ExecuteSearch(SearchParam searchParam, SearchIndexClient SearchClientInstance)
        {
            // Sort by document type ascending
            SearchParameters searchParameters = new SearchParameters()
            {
                OrderBy = new List<string>()
                {
                    $"{SortField(searchParam.SortField)} {searchParam.SortDirection}"
                },
                HighlightFields = new List<string>() { "content" },
                HighlightPreTag = "<mark>",
                HighlightPostTag = "</mark>"

            };

            if (!string.IsNullOrEmpty(searchParam.Filter))
            {
                searchParameters.Filter = $"metadata_storage_content_type eq '{searchParam.Filter.Trim()}'";
            }

            searchParameters.IncludeTotalResultCount = true; // get totals
            searchParameters.Top = searchParam.PageSize; // limit the results
            if (searchParam.CurrentPage != 1)
            {
                // pagination next results
                searchParameters.Skip = (searchParam.CurrentPage - 1) * searchParam.PageSize;
            }
            DocumentSearchResult<BlobDocument> result = await SearchClientInstance.Documents.SearchAsync<BlobDocument>(searchParam.SearchString, searchParameters);

            List<BlobSearchResult> SearchResults = new List<BlobSearchResult>();
            // Pagination usage
            int TotalPages = (int)Math.Ceiling(decimal.Divide((int)result.Count, searchParam.PageSize));
            int TotalResults = (int)result.Count;
            foreach (var item in result.Results)
            {
                BlobSearchResult blobResult = new BlobSearchResult()
                {
                    DocumentName = item.Document.metadata_storage_name,
                    DocumentType = item.Document.metadata_storage_content_type,
                    LastModifiedDate = item.Document.metadata_storage_last_modified.ToLocalTime().ToString("yyyy/MM/dd"),
                    DownloadUrl = $"{searchParam.ContainerUrl}{item.Document.metadata_storage_name}",
                    DocumenContent = item.Document.content
                };
               
                if (item.Highlights != null && item.Highlights.Count>0)
                {
                    blobResult.HighlightedContent = string.Join("<br>", new List<string>(item.Highlights["content"]).ToArray());
                }
                SearchResults.Add(blobResult);
            }
            // build the result obkect with the variables we need
            SearchResult searchResult = new SearchResult()
            {
                SearchResults = SearchResults,
                TotalResults = TotalResults,
                TotalPages = TotalPages,
                ShowPrevious = searchParam.CurrentPage > 1,
                ShowNext = searchParam.CurrentPage < TotalPages,
                PreviousUrl = BuildUrlVariables(searchParam.CurrentPage - 1, searchParam),
                NextUrl = BuildUrlVariables(searchParam.CurrentPage + 1, searchParam),
                FromItem = searchParam.CurrentPage > 1 ? (searchParam.CurrentPage - 1) * searchParam.PageSize + 1 : 1,
                ToItem = searchParam.CurrentPage * searchParam.PageSize
            };

            if (searchResult.ToItem > TotalResults)// fix egde case
            {
                searchResult.ToItem = TotalResults;
            }
            return searchResult;
        }

        /// <summary>
        /// get the contenty list for filter
        /// </summary>
        /// <param name="SearchClientInstance"></param>
        /// <returns>SelectList</returns>
        public static async Task<SelectList> ExecuteSearchForContents(SearchParam searchParam, SearchIndexClient SearchClientInstance)
        {
            // Sort by document type ascending
            SearchParameters searchParameters = new SearchParameters()
            {
                OrderBy = new List<string>()
                {
                    $"{SortField(searchParam.SortField)} {searchParam.SortDirection}"
                },

            };
            searchParameters.IncludeTotalResultCount = true; // get totals
            searchParameters.Top = 70; // forcing my elements // by default is 50
            // this call is only to create the content type select dowpdown.
            DocumentSearchResult<BlobDocument> result = await SearchClientInstance.Documents.SearchAsync<BlobDocument>("", searchParameters);
            var contentType = result.Results.Select(x => x.Document.metadata_storage_content_type).Distinct().ToList();
            contentType.Insert(0, "Select Filter");
            SelectList ContentTypeList = new SelectList(contentType);
            return ContentTypeList;
        }
    }
}
