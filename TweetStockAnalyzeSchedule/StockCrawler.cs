using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using Microsoft.Practices.Unity;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzer.Twitter;

namespace TweetStockAnalyzeSchedule
{
    public class StockCrawler
    {
        public void Start()
        {
            using (var productRepository = DependencyContainer.Instance.Resolve<IProductRepository>())
            {
                foreach (var product in productRepository.ReadAll())
                {
                    foreach (var searchWord in product.SearchWord)
                    {
                        var count = GetTweetCountAsync(searchWord);
                    }
                }
            }
        }
        private Task<int> GetTweetCountAsync(SearchWord searchWord)
        {
            var service = new TwitterServiceProvider().GetAuthenticatedService();
            var option = new SearchOptions()
            {
                Q = searchWord.Word,
                SinceId = searchWord.LastTweetId
            };
            return service.SearchAsync(option).ContinueWith(p=>p.Result.Statuses.Count());
        }
    }
}
