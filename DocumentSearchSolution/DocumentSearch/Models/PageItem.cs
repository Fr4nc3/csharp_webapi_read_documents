using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSearch.Models
{
    /// <summary>
    /// PageItem for pagination
    /// </summary>
    public class PageItem
    {
        public int number { get; set; }
        public string url { get; set; }
        public bool isCurrent { get; set; } = false;
    }
}
