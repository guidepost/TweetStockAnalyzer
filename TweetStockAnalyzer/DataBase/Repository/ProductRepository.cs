using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Create(string name, DateTime serviceStartDate);
    }
    [AutoRegist(typeof(IProductRepository))]
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        protected override DbSet<Product> DbSet
        {
            get { return Entities.Product; }
        }

        public override void Update(Product value)
        {
            var entity = Read(value.ProductId);
            entity.ProductName = value.ProductName;
            entity.ServiceStartDate = value.ServiceStartDate;
            entity.IsDeleted = value.IsDeleted;
            base.Update(entity);
        }

        public override Product Read(Expression<Func<Product, object>> include, params object[] id)
        {
            return ReadAll().Include(include).FirstOrDefault(p => p.ProductId == (int) id[0]);
        }

        public Product Create(string name, DateTime serviceStartDate)
        {
            var entity = new Product();
            entity.ProductName = name;
            entity.ServiceStartDate = serviceStartDate;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
    }
}
