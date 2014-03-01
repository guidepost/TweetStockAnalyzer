using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.DataBase;

namespace TweetStockAnalyzer.Tests.Database
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            var repository = new ProductRepository();
            foreach (var product in repository.ReadAll().ToList())
            {
                repository.Delete(product.ProductId);
            }
        }

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
    }
}