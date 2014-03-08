using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetStockAnalyzer.DataBase
{
    public interface IRepository<T> : IDisposable
    {
        T Read(object id);
        IQueryable<T> ReadAll();
        void Update(T value);
        T Delete(int id);
    }
}
