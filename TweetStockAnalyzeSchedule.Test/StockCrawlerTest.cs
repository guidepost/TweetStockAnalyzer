using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TweetSharp;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Domain.Stock;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzeSchedule.Test
{
    [TestClass]
    public class StockCrawlerTest
    {
        private DateTime _endDate;
        private AggregateHistory _aggregateHistory;
        private Mock<IAggregateHistoryRepository> _mockRepository;
        private Mock<IStockPriceRepository> _mockPriceRepository;
        private Mock<IStockService> _mockStockService;
        [TestInitialize]
        public void Initialize()
        {
            _mockStockService = new Mock<IStockService>();
            _mockStockService.Setup(p => p.Load(It.IsAny<Stock>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).
                Returns(() =>
                {
                    return new StockPrice[] { new StockPrice(), new StockPrice() };
                });
            _mockRepository = new Mock<IAggregateHistoryRepository>();
            _aggregateHistory = new AggregateHistory();
            _endDate  = new DateTime(2000, 7, 7);
            _aggregateHistory.EndDate = _endDate;
            _aggregateHistory.Stock = new Stock();
            _mockRepository.Setup(p => p.ReadAll()).Returns(() =>
            {
                AggregateHistory[] histories = { _aggregateHistory };
                return histories.AsQueryable();
            });
            _mockPriceRepository = new Mock<IStockPriceRepository>();
            DependencyContainer.Instance.RegisterInstance(_mockRepository.Object);
            DependencyContainer.Instance.RegisterInstance(_mockPriceRepository.Object);
            DependencyContainer.Instance.RegisterInstance(_mockStockService.Object);
        }

        [TestMethod]
        public void Start()
        {
             var crawler = new StockCrawler();
            crawler.Start();
            _mockStockService.Verify(p => p.Load(It.IsAny<Stock>(), _endDate, It.IsAny<DateTime>()));
            _mockPriceRepository.Verify(p=>p.Create(It.IsAny<Stock>(),It.IsAny<DateTime>(),0,0), Times.Exactly(2));
            _mockRepository.Verify(p => p.Update(_aggregateHistory));
        }

    }
}
