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
    
    public partial class Product
    {
        public Product()
        {
            this.CompanyProductRelation = new HashSet<CompanyProductRelation>();
            this.SearchResult = new HashSet<SearchResult>();
            this.SearchWord = new HashSet<SearchWord>();
        }
    
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public System.DateTime ServiceStartDate { get; set; }
        public Nullable<System.DateTime> ServiceEndDate { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
    
        public virtual ICollection<CompanyProductRelation> CompanyProductRelation { get; set; }
        public virtual ICollection<SearchResult> SearchResult { get; set; }
        public virtual ICollection<SearchWord> SearchWord { get; set; }
    }
}