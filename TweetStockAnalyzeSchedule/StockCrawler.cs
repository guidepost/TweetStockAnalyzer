using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Domin.Stock;
using TweetStockAnalyzer.Infrastructure.Dependency;
using Microsoft.Practices.Unity;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzeSchedule
{
    public class StockCrawler : IClawler
    {
        public void Start()
        {
            using (var stockRepository = DependencyContainer.Instance.Resolve<IStockRepository>())
            {
                foreach (var stock in stockRepository.ReadAll())
                {
                    var lastDate = stock.AggregateHistory.First
                    
                }
            }
        }

        private IEnumerable<StockPrice> LoadStackPrices(Stock stock)
        {
            var stockService = DependencyContainer.Instance.Resolve<IStockService>();
            stockService.Load(stock,)
        }
    }
}
