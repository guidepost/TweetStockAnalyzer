using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
{
    public class StockRepository : RepositoryBase<Stock>
    {
        public override Stock Create()
        {
            var stock = new Stock();
            Entities.Stock.Add(stock);
            Entities.SaveChanges();
            return stock;
        }

        public override Stock Read(int id)
        {
            return Entities.Stock.Find(id);
        }

        public override void Update(Stock valud)
        {
            throw new NotImplementedException();
        }

        public override void Delte(int id)
        {
            var stock = Entities.Stock.Find(id);
            Entities.Stock.Remove(stock);
            Entities.SaveChanges();
        }
    }
}
