using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSearch.Models
{
    public class SearchParam
    {
        /// <summary>
        /// The search string variable private
        /// </summary>
        private string _searchString;
        private string _filter;
        private string _sortField;
        private string _sortDirection;
        private int _currentPage;

        /// <summary>
        /// Gets or sets the search string.
        /// </summary>
        /// <value>The search string.</value>
        public string SearchString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_searchString))
                {
                    return String.Empty;
                }
                return _searchString;
            }
            set
            {
                _searchString = value;
            }
        }
        /// <summary>
        ///  current page default is on the index =1 
        /// </summary>
        public int CurrentPage
        {
            get
            {

                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }
        /// <summary>
        /// Filter field deagult empty
        /// </summary>
        public string Filter
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_filter))
                {
                    return String.Empty;
                }
                return _filter;
            }
            set
            {
                _filter = value;
            }
        }
        /// <summary>
        /// sortField dafualt content type
        /// </summary>
        public string SortField
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_sortField))
                {
                    return "DocumentName";
                }
                return _sortField;
            }
            set
            {
                _sortField = value;
            }
        }
        /// <summary>
        /// sort direction default asc
        /// </summary>
        public string SortDirection
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_sortDirection))
                {
                    return "asc";
                }
                return _sortDirection;
            }
            set
            {
                _sortDirection = value;
            }
        }
        /// <summary>
        ///  PageSize for pagination
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// containerUrl to build download
        /// </summary>
        public string ContainerUrl { get; set; }
    }
}
