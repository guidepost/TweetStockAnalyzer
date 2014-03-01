using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
{
    public interface ISearchResultRepository : IRepository<SearchResult>
    {
        SearchResult Create(SearchWord searchWord, Product product, long tweetCount, long lastTweetId, DateTime date);
    }
    public class SearchResultRepository : RepositoryBase<SearchResult> , ISearchResultRepository
    {
        protected override DbSet<SearchResult> DbSet
        {
            get { return Entities.SearchResult; }
        }

        public override void Update(SearchResult value)
        {
            var result = Read(value.SearchResultId);
            result.TweetCount = value.TweetCount;
            result.LastTweetId = value.LastTweetId;
            result.SearchDate = value.SearchDate;
            Entities.SaveChanges();
        }

        public SearchResult Create(SearchWord searchWord, Product product, long tweetCount, long lastTweetId, DateTime date)
        {
            var entity = new SearchResult();
            searchWord.SearchResult.Add(entity);
            product.SearchResult.Add(entity);
            entity.TweetCount = tweetCount;
            entity.LastTweetId = lastTweetId;
            entity.SearchDate = date;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
    }
}
