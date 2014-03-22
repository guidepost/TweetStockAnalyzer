using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase.Repository
{
    public interface IAggregateHistoryRepository : IRepository<AggregateHistory>
    {
        AggregateHistory Create(Stock stock, DateTime startDate, DateTime endDate);
    }
    [AutoRegist(typeof(IAggregateHistoryRepository))]
    public class AggregateHistoryRepository : RepositoryBase<AggregateHistory>, IAggregateHistoryRepository
    {
        protected override DbSet<AggregateHistory> DbSet
        {
            get { return Entities.AggregateHistory; }
        }

        public override void Update(AggregateHistory value)
        {
            var entity = Read(value.AggregateHistoryId);
            entity.StartDate = value.StartDate;
            entity.EndDate = value.EndDate;
            entity.IsDeleted = value.IsDeleted;
            base.Update(entity);
        }

        public override AggregateHistory Read(Expression<Func<AggregateHistory, object>> include, params object[] id)
        {
            return ReadAll().Include(include).FirstOrDefault(p=>p.AggregateHistoryId == (int)id[0]);
        }

        public AggregateHistory Create(Stock stock, DateTime startDate, DateTime endDate)
        {
            var entity = new AggregateHistory();
            entity.StockId = stock.StockId;
            entity.StartDate = startDate;
            entity.EndDate = endDate;
            return base.Create(entity);
        }
    }
}
