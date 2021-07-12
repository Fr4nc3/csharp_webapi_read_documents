using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSearch.Models
{
    /// <summary>
    /// Blob Document class
    /// </summary>
    public class BlobDocument
    {
        /// <summary>
        /// these fieldsa re the fiedlsa available form blob object when we upload to azure container
        /// </summary>
        public string content { get; set; }
        public string metadata_storage_content_type { get; set; }
        public DateTime metadata_storage_last_modified { get; set; }
        public string metadata_storage_name { get; set; }
        public string metadata_storage_path { get; set; }
        public string metadata_content_type { get; set; }
        public string metadata_language { get; set; }
    }
}
