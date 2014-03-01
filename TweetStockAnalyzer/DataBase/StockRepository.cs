using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
{
    public class StockRepository : RepositoryBase<Stock>
    {
        protected override DbSet<Stock> DbSet
        {
            get { return Entities.Stock; }
        }

        public override void Update(Stock value)
        {
            throw new NotImplementedException();
        }
    }
}
