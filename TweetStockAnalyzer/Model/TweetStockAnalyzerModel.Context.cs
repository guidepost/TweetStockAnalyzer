﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TweetStockAnalyzerEntities1 : DbContext
    {
        public TweetStockAnalyzerEntities1()
            : base("name=TweetStockAnalyzerEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AggregateHistory> AggregateHistory { get; set; }
        public virtual DbSet<BussinessCategory> BussinessCategory { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyProductRelation> CompanyProductRelation { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SearchResult> SearchResult { get; set; }
        public virtual DbSet<SearchWord> SearchWord { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<StockPrice> StockPrice { get; set; }
    }
}
