//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TweetStockAnalyzer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SearchWord
    {
        public SearchWord()
        {
            this.SearchResult = new HashSet<SearchResult>();
        }
    
        public int SearchWordId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string Word { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual ICollection<SearchResult> SearchResult { get; set; }
    }
}
