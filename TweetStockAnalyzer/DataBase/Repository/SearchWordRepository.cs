using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase.Repository
{

    public interface ISearchWordRepository : IRepository<SearchWord>
    {
        SearchWord Create(Product product, string word);
    }
    [AutoRegist(typeof(ISearchWordRepository))]
    public class SearchWordRepository : RepositoryBase<SearchWord>, ISearchWordRepository
    {
        protected override DbSet<SearchWord> DbSet
        {
            get { return Entities.SearchWord; }
        }

        public override void Update(SearchWord value)
        {
            var entity = Read(value.SearchWordId);
            entity.Word = value.Word;
            entity.IsDeleted = value.IsDeleted;
            entity.LastTweetId = value.LastTweetId;
            base.Update(entity);
        }

        public override SearchWord Read(Expression<Func<SearchWord, object>> include, params object[] id)
        {
            return ReadAll().Include(include).FirstOrDefault(p=>p.SearchWordId == (int)id[0]);
        }

        public SearchWord Create(Product product, string word)
        {
            var entity = new SearchWord();
            entity.Word = word;
            entity.ProductId = product.ProductId;
            return base.Create(entity);
        }
    }
}
