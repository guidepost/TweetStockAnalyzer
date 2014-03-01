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
    public class StockRepositoryTest
    {
        [TestMethod]
        public void Get()
        {
            var notFoundKey = -100;
            var repository = new StockRepository();
            var result = repository.Read(notFoundKey);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create()
        {
            var repository = new StockRepository();
            var result = repository.Create();
            Assert.AreNotEqual(0, result.StockId);
        }
    }
}
