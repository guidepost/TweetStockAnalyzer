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
    
    public partial class CompanyProductRelation : IEntity
    {
        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Company Companies { get; set; }
        public virtual Product Products { get; set; }
    }
}
