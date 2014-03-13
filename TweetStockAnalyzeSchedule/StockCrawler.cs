using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;
using TweetStockAnalyzer.Domain.Stock;
using TweetStockAnalyzer.Infrastructure.Dependency;
using Microsoft.Practices.Unity;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzeSchedule
{
    public class StockCrawler : IClawler
    {
        public void Start()
        {
            using (var stockPriceRepository = DependencyContainer.Instance.Resolve<IStockPriceRepository>())
            using (var aggregateHistoryRepository = DependencyContainer.Instance.Resolve<IAggregateHistoryRepository>())
            {
                foreach (var aggregateHistory in aggregateHistoryRepository.ReadAll().OrderByDescending(p=>p.EndDate).Take(150))
                {
                    var stock = aggregateHistory.Stock;
                    var now = DateTime.Now;
                    var result = LoadStackPrices(stock, aggregateHistory.EndDate, now);
                    foreach (var stockPrice in result)
                    {
                        stockPriceRepository.Create(stock, stockPrice.Date, stockPrice.Dealings, stockPrice.ClosingPrice);
                    }
                    aggregateHistory.EndDate = now;
                    aggregateHistoryRepository.Update(aggregateHistory);
                }
            }
        }

        private IEnumerable<StockPrice> LoadStackPrices(Stock stock,DateTime startDate,DateTime endDate)
        {
            var stockService = DependencyContainer.Instance.Resolve<IStockService>();
            return stockService.Load(stock, startDate, endDate);
        }
    }
}
