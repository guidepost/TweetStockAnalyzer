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

        public SearchWord Create(Product product, string word)
        {
            var entity = new SearchWord();
            entity.Word = word;
            entity.ProductId = product.ProductId;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
    }
}
