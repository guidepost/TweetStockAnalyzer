using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TweetStockAnalyzer.DataBase.Repository;
using TweetStockAnalyzer.Domain.Score;
using TweetStockAnalyzer.Domain.Stock;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzeSchedule.Test
{
    [TestClass]
    public class ScoreUpdatorTest
    {
        private int _score = 100;
        private Mock<ICompanyRepository> _mockRepository;
        private Mock<IScoreCalculator> _mockCalculator;
        private Mock<ICompanyScoreRepository> _mockScoreRepository;
        [TestInitialize]
        public void Initialize()
        {
            _mockScoreRepository = new Mock<ICompanyScoreRepository>();
            _mockRepository = new Mock<ICompanyRepository>();
            _mockCalculator = new Mock<IScoreCalculator>();
            _mockCalculator.Setup(p => p.GetScore(It.IsAny<Company>())).Returns(_score);
            var company = new Company();
            _mockRepository.Setup(p => p.ReadAll()).Returns(() =>
            {
                Company[] companies = { company };
                return companies.AsQueryable();
            });
            DependencyContainer.Instance.RegisterInstance(_mockRepository.Object);
            DependencyContainer.Instance.RegisterInstance(_mockScoreRepository.Object);
            DependencyContainer.Instance.RegisterInstance(_mockCalculator.Object);
        }
        [TestMethod]
        public void Start()
        {
            var updator = new ScoreUpdator();
            updator.Start();
            _mockCalculator.Verify(p => p.GetScore(It.IsAny<Company>()));
            _mockScoreRepository.Verify(p => p.Create(It.IsAny<Company>(), _score));
        }
    }
}
