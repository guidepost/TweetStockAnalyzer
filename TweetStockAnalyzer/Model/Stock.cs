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
    
    public partial class Stock : IEntity
    {
        public Stock()
        {
            this.AggregateHistories = new HashSet<AggregateHistory>();
            this.StockPrices = new HashSet<StockPrice>();
        }
    
        public int StockId { get; set; }
        public string StockCode { get; set; }
        public int CompanyId { get; set; }
        public int BussinessCategoryId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    
        protected virtual ICollection<AggregateHistory> AggregateHistories { get; set; }
        public virtual BussinessCategory BussinessCategory { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<StockPrice> StockPrices { get; set; }
    }
}
