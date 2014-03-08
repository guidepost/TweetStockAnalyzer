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
    
    public partial class SearchResult
    {
        public int SearchResultId { get; set; }
        public int ProductId { get; set; }
        public int SearchWordId { get; set; }
        public long TweetCount { get; set; }
        public System.DateTime SearchDate { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual SearchWord SearchWord { get; set; }
    }
}
