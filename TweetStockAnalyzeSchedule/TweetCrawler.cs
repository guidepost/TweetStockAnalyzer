using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;
using TweetStockAnalyzer.Infrastructure.Dependency;
using Microsoft.Practices.Unity;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzer.Domain.Twitter;

namespace TweetStockAnalyzeSchedule
{
    public class TweetCrawler : IClawler
    {
        private readonly int _requestCount;

        public TweetCrawler() : this(100)
        {
        }

        public TweetCrawler(int requestCount)
        {
            _requestCount = requestCount;
        }

        public void Start()
        {
            using (var searchResultRepository = DependencyContainer.Instance.Resolve<ISearchResultRepository>())
            using (var searchWordRepository = DependencyContainer.Instance.Resolve<ISearchWordRepository>())
            {
                foreach (var searchWord in searchWordRepository.ReadAll().OrderBy(p => p.UpdateDate).Take(_requestCount))
                {
                    var tweets = GetTweetCountAsync(searchWord);
                    if (tweets.Result.Statuses.Any())
                    {
                        searchResultRepository.Create(searchWord, searchWord.Product, tweets.Result.Statuses.Count(), DateTime.Now);
                        searchWord.LastTweetId = tweets.Result.Statuses.Last().Id;
                        searchWordRepository.Update(searchWord);
                    }
                }
            }
        }
        private Task<TwitterSearchResult> GetTweetCountAsync(SearchWord searchWord)
        {
            var service = new TwitterServiceProvider().GetService();
            var option = new SearchOptions()
            {
                Q = searchWord.Word,
                SinceId = searchWord.LastTweetId
            };
            return service.SearchAsync(option);
        }
    }
}
