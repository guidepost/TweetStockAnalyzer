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
    
    public partial class StockPrice
    {
        public long StockPriceId { get; set; }
        public int StockId { get; set; }
        public System.DateTime Date { get; set; }
        public long Dealings { get; set; }
        public double ClosingPrice { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Stock Stock { get; set; }
    }
}
