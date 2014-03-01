using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected TweetStockAnalyzerEntities Entities { get; private set; }

        protected RepositoryBase()
        {
            Entities = new TweetStockAnalyzerEntities();
        }

        public abstract T Create();
        public abstract T Read(int id);
        public abstract void Update(T valud);
        public abstract void Delte(int id);
    }
}
