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
    public class StockCrawler : ISchedule
    {
        private int _requestCount;

        public StockCrawler() : this(150)
        {
        }

        public StockCrawler(int requestCount)
        {
            _requestCount = requestCount;
        }

        public void Start()
        {
            using (var stockPriceRepository = DependencyContainer.Instance.Resolve<IStockPriceRepository>())
            using (var aggregateHistoryRepository = DependencyContainer.Instance.Resolve<IAggregateHistoryRepository>())
            {
                foreach (var aggregateHistory in aggregateHistoryRepository.ReadAll().OrderBy(p=>p.EndDate).Take(_requestCount).ToList())
                {
                    var stock = aggregateHistory.Stock;
                    var now = DateTime.Now;
                    var result = LoadStackPrices(stock, aggregateHistory.EndDate.AddDays(1), now);
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
