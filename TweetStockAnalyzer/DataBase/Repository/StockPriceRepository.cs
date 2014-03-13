using System;
using System.Data.Entity;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase.Repository
{
    public interface IStockPriceRepository : IRepository<StockPrice>
    {
        StockPrice Create(Stock stock, DateTime date, long dealings, double closingPrice);
    }

    [AutoRegist(typeof(IStockPriceRepository))]
    public class StockPriceRepository : RepositoryBase<StockPrice> , IStockPriceRepository
    {
        protected override DbSet<StockPrice> DbSet
        {
            get { return Entities.StockPrice; }
        }

        public override void Update(StockPrice value)
        {
            var entity = Read(value.StockPriceId);
            entity.Date = value.Date;
            entity.IsDeleted = value.IsDeleted;
            base.Update(entity);
        }

        public StockPrice Create(Stock stock, DateTime date, long dealings, double closingPrice)
        {
            var entity = new StockPrice();
            entity.Date = date;
            entity.Dealings = dealings;
            entity.ClosingPrice = closingPrice;
            entity.StockId = stock.StockId;
            DbSet.Add(entity);
            return entity;
        }
    }
}
