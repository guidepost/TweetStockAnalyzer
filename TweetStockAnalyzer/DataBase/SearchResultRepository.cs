using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
{
    public interface ISearchResultRepository : IRepository<SearchResult>
    {
        SearchResult Create(SearchWord searchWord, Product product, long tweetCount, long lastTweetId, DateTime date);
    }
    [AutoRegist(typeof(ISearchResultRepository))]
    public class SearchResultRepository : RepositoryBase<SearchResult> , ISearchResultRepository
    {
        protected override DbSet<SearchResult> DbSet
        {
            get { return Entities.SearchResult; }
        }

        public override void Update(SearchResult value)
        {
            var entity = Read(value.SearchResultId);
            entity.TweetCount = value.TweetCount;
            entity.SearchDate = value.SearchDate;
            entity.IsDeleted = value.IsDeleted;
            Entities.SaveChanges();
        }

        public SearchResult Create(SearchWord searchWord, Product product, long tweetCount, long lastTweetId, DateTime date)
        {
            var entity = new SearchResult();
            entity.SearchWord = searchWord;
            entity.Product = product;
            entity.TweetCount = tweetCount;
            entity.SearchDate = date;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
    }
}
