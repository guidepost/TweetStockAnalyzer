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
        private readonly int _requestCount;

        public StockCrawler() : this(100)
        {
        }

        public StockCrawler(int requestCount)
        {
            _requestCount = requestCount;
        }

        public void Start()
        {
            using (var searchResultRepository = DependencyContainer.Instance.Resolve<ISearchResultRepository>())
            using (var searchWordRepository = DependencyContainer.Instance.Resolve<ISearchWordRepository>())
            {
                foreach (var searchWord in searchWordRepository.ReadAll().OrderByDescending(p => p.UpdateDate).Take(_requestCount))
                {
                    var taskCount = GetTweetCountAsync(searchWord);
                    searchResultRepository.Create(searchWord, searchWord.Product, taskCount.Result, DateTime.Now);
                    searchWordRepository.Update(searchWord);
                }
            }
        }
        private Task<long> GetTweetCountAsync(SearchWord searchWord)
        {
            var service = new TwitterServiceProvider().GetAuthenticatedService();
            var option = new SearchOptions()
            {
                Q = searchWord.Word,
                SinceId = searchWord.LastTweetId
            };
            return service.SearchAsync(option).ContinueWith(p=>(long)p.Result.Statuses.Count());
        }
    }
}
