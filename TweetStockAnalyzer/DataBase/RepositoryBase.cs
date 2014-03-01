﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, new()
    {
        protected TweetStockAnalyzerEntities Entities { get; private set; }

        protected RepositoryBase()
        {
            Entities = new TweetStockAnalyzerEntities();
        }

        protected abstract DbSet<T> DbSet { get; }
        public abstract void Update(T value);

        public abstract T Create();
        
        public virtual T Read(int id)
        {
            return DbSet.Find(id);
        }
        public virtual IEnumerable<T> ReadAll()
        {
            return DbSet;
        }

        public virtual void Delte(int id)
        {
            var entity = Read(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                Entities.SaveChanges();
            }
        }
    }
}
