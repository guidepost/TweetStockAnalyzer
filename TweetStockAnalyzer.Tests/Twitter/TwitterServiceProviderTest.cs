using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.Twitter;
using TweetSharp;

namespace TweetStockAnalyzer.Tests.Twitter
{
    [TestClass]
    public class TwitterServiceProviderTest
    {
        [TestMethod]
        public void GetAuthenticatedService()
        {
            var provider = new TwitterServiceProvider();
            var service = provider.GetAuthenticatedService();

            var result = service.Search(new SearchOptions()
            {
                Q = "ハピプリ"
            });

            (result.Statuses.Count() > 0).IsTrue();
        }
    }
}
