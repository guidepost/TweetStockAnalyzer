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
        [TestMethod]
        public void Create()
        {
            var repository = new ProductRepository();
            var result = repository.Create();
            result.IsNotNull();
            result = repository.Read(result.ProductId);
            result.IsNotNull();
        }
    }
}
