using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class,IEntity, new()
    {
        protected TweetStockAnalyzerEntities Entities { get; private set; }

        protected RepositoryBase()
        {
            Entities = new TweetStockAnalyzerEntities();
        }

        protected abstract DbSet<T> DbSet { get; }

        public virtual void Update(T value)
        {
            value.UpdateDate = DateTime.Now;
            Entities.SaveChanges();
        }
        public virtual T Create(T entity)
        {
            entity.UpdateDate = DateTime.Now;
            entity.RegisterDate = DateTime.Now;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
        public virtual T Read(params object[] id)
        {
            return DbSet.Find(id);
        }

        public abstract T Read(Expression<Func<T, object>> include, params object[] id);

        public virtual IQueryable<T> ReadAll()
        {
            return DbSet;
        }



        public virtual T Delete(params object[] id)
        {
            var entity = Read(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                Entities.SaveChanges();
            }
            return entity;
        }

        public void Dispose()
        {
            Entities.Dispose();
        }
    }
}
