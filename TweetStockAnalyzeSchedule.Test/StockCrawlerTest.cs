using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TweetSharp;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using Microsoft.Practices.Unity;
using TweetStockAnalyzer.Model;
using ITwitterService = TweetStockAnalyzer.Twitter.ITwitterService;

namespace TweetStockAnalyzeSchedule.Test
{
    [TestClass]
    public class StockCrawlerTest
    {
        private SearchWord _searchWord;
        private Mock<IProductRepository> _mockRepository;
        private Mock<ITwitterService> _mockTwitter;
        [TestInitialize]
        public void Initialize()
        {
            _mockTwitter = new Mock<ITwitterService>();
            _mockTwitter.Setup(p => p.SearchAsync(It.IsAny<SearchOptions>())).Returns(
                Task.Factory.StartNew(()=>
                {
                    var result = new TwitterSearchResult();
                    result.Statuses = new List<TwitterStatus>();
                    return result;
                }));
            _mockRepository = new Mock<IProductRepository>();
            _searchWord = new SearchWord { LastTweetId = 100, Word = Guid.NewGuid().ToString()};
            _mockRepository.Setup(p => p.ReadAll()).Returns(() =>
            {
                Product[] products = {new Product {SearchWord = {_searchWord}}};
                return products;
            } );
            DependencyContainer.Instance.RegisterInstance(_mockRepository.Object);
            DependencyContainer.Instance.RegisterInstance(_mockTwitter.Object);
        }

        [TestMethod]
        public void Start()
        {
            var crawler = new StockCrawler();
            crawler.Start();

            _mockTwitter.Verify(
                p => p.SearchAsync(
                    It.Is<SearchOptions>(it=> it.Q == _searchWord.Word && it.SinceId == _searchWord.LastTweetId)));
        }
    }
}
