using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
{
    public class SearchResultRepository : RepositoryBase<SearchResult>
    {
        protected override DbSet<SearchResult> DbSet
        {
            get { return Entities.SearchResult; }
        }

        public override void Update(SearchResult value)
        {
            throw new NotImplementedException();
        }

        public override SearchResult Create()
        {
            throw new NotImplementedException();
        }
    }
}
