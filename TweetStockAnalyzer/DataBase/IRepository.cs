using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TweetStockAnalyzer.DataBase
{
    public interface IRepository<T> : IDisposable
    {
        T Read(params object[] id);
        T Read(Expression<Func<T, object>> include, params object[] id);
        IQueryable<T> ReadAll();

        void Update(T value);
        T Delete(params object[] id);
    }
}
