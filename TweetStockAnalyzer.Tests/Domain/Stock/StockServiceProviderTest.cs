using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetSharp;
using TweetStockAnalyzer.Domain.Twitter;
using TweetStockAnalyzer.Domin.Stock;

namespace TweetStockAnalyzer.Tests.Domain.Stock
{
    [TestClass]
    public class StockServiceProviderTest
    {
        [TestMethod]
        public void GetService()
        {
            var provider = new StockServiceProvider();
            var service = provider.GetService();

            var yahooCode = new Model.Stock {StockCode = "4689"};
            var result = service.Load(yahooCode, new DateTime(2011,1,1), new DateTime(2012,2,1));
            result.Any().IsTrue();
        }
    }
}
