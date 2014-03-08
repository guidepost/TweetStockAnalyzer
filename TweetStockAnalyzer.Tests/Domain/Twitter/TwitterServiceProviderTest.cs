using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetSharp;
using TweetStockAnalyzer.Domain.Twitter;

namespace TweetStockAnalyzer.Tests.Domain.Twitter
{
    [TestClass]
    public class TwitterServiceProviderTest
    {
        [TestMethod]
        public void GetService()
        {
            var provider = new TwitterServiceProvider();
            var service = provider.GetService();

            var result = service.SearchAsync(new SearchOptions()
            {
                Q = "test"
            });

            result.Result.Statuses.Any().IsTrue();
        }

    }
}
