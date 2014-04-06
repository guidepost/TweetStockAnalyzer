using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;

namespace TweetStockAnalyzer.Tests.Database
{
    [TestClass]
    public class ProductRepositoryTest : DatabaseTestBase
    {
        [TestMethod]
        public void Create()
        {
            var repository = new ProductRepository();
            var result = repository.Create("test", new DateTime(2000, 7, 7));
            result.IsNotNull();
            result = repository.Read(result.ProductId);
            result.ProductName.Is("test");
            result.ServiceStartDate.Is(new DateTime(2000, 7, 7));
        }

        [TestMethod]
        public void Update()
        {
            var repository = new ProductRepository();
            var result = repository.Create("test", new DateTime(2000, 7, 7));
            result.IsNotNull();
            repository = new ProductRepository();
            result = repository.Read(result.ProductId);
            result.ProductName = Guid.NewGuid().ToString();
            repository.Update(result);

            repository = new ProductRepository();
            var result2 = repository.Read(result.ProductId);
            result2.ProductName.Is(result.ProductName);
        }

    }
}