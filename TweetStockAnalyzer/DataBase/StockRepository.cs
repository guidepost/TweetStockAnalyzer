using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
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
            Entities.SaveChanges();
        }

        public Stock Create(Company company, BussinessCategory category, string stockCode)
        {
            var entity = new Stock();
            entity.StockCode = stockCode;
            entity.Company = company;
            entity.BussinessCategory = category;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
    }
}
