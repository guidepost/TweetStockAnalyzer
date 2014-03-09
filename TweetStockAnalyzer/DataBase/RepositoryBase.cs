using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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
        
        public virtual T Read(object id)
        {
            return DbSet.Find(id);
        }
        public virtual IQueryable<T> ReadAll()
        {
            return DbSet;
        }



        public virtual T Delete(int id)
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
