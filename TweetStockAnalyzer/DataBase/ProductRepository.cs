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
            Entities.SaveChanges();
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
