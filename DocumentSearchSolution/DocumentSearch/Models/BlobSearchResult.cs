using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSearch.Models
{ 
   /// <summary>
    /// Results from the document
    /// </summary>
    public class BlobSearchResult
    {
        [Display(Name = "Document Name")]
        public string DocumentName { get; set; }
        public string DocumenContent { get; set; }
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; }
        [Display(Name = "Last Modified Date")]
        public string LastModifiedDate { get; set; }
        public string DownloadUrl { get; set; }
        public string HighlightedContent { get; set; }
    }
}
