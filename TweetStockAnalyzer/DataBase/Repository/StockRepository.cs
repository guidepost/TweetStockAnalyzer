using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public override Stock Read(Expression<Func<Stock, object>> include, params object[] id)
        {
            return ReadAll().Include(include).FirstOrDefault(p => p.StockId == (int) id[0]);
        }

        public Stock Create(Company company, BussinessCategory category, string stockCode)
        {
            var entity = new Stock();
            entity.StockCode = stockCode;
            entity.CompanyId = company.CompanyId;
            entity.BussinessCategoryId = category.BussinessCategoryId;
            return base.Create(entity);
        }


    }
}
