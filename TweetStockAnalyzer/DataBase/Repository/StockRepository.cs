﻿using System.Data.Entity;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase.Repository
{
    public interface IStockRepository : IRepository<Stock>
    {
        Stock Create(Company company, BussinessCategory category, string stockCode);
    }

    [AutoRegist(typeof(IStockRepository))]
    public class StockRepository : RepositoryBase<Stock> , IStockRepository
    {
        protected override DbSet<Stock> DbSet
        {
            get { return Entities.Stock; }
        }

        public override void Update(Stock value)
        {
            var entity = Read(value.StockId);
            entity.StockCode = value.StockCode;
            entity.IsDeleted = value.IsDeleted;
            base.Update(entity);
        }

        public Stock Create(Company company, BussinessCategory category, string stockCode)
        {
            var entity = new Stock();
            entity.StockCode = stockCode;
            entity.CompanyId = company.CompanyId;
            entity.BussinessCategoryId = category.BussinessCategoryId;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
    }
}
