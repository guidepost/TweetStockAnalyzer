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
using ITwitterService = TweetStockAnalyzer.Domain.Twitter.ITwitterService;

namespace TweetStockAnalyzeSchedule.Test
{
    [TestClass]
    public class StockCrawlerTest
    {
        private SearchWord _searchWord;
        private Mock<ISearchWordRepository> _mockRepository;
        private Mock<ISearchResultRepository> _mockResultRepository;
        private Mock<ITwitterService> _mockTwitter;
        [TestInitialize]
        public void Initialize()
        {
            _mockTwitter = new Mock<ITwitterService>();
            _mockTwitter.Setup(p => p.SearchAsync(It.IsAny<SearchOptions>())).Returns(
                Task.Factory.StartNew(()=>
                {
                    var result = new TwitterSearchResult();
                    result.Statuses = new List<TwitterStatus>{new TwitterStatus() };
                    return result;
                }));
            _mockRepository = new Mock<ISearchWordRepository>();
            _searchWord = new SearchWord { LastTweetId = 100, Word = Guid.NewGuid().ToString()};
            _searchWord.Product = new Product();
            _mockRepository.Setup(p => p.ReadAll()).Returns(() =>
            {
                SearchWord[] products = {_searchWord};
                return products;
            } );
            _mockResultRepository = new Mock<ISearchResultRepository>();
            DependencyContainer.Instance.RegisterInstance(_mockRepository.Object);
            DependencyContainer.Instance.RegisterInstance(_mockResultRepository.Object);
            DependencyContainer.Instance.RegisterInstance(_mockTwitter.Object);
        }

        [TestMethod]
        public void Start()
        {
            var crawler = new TweetCrawler();
            crawler.Start();

            _mockTwitter.Verify(
                p => p.SearchAsync(
                    It.Is<SearchOptions>(it=> it.Q == _searchWord.Word && it.SinceId == _searchWord.LastTweetId)));
            _mockResultRepository.Verify(p => p.Create(It.IsAny<SearchWord>(), It.IsAny<Product>(), 1, It.IsAny<DateTime>()));
            _mockRepository.Verify(p => p.Update(It.IsAny<SearchWord>()));
        }
    }
}
