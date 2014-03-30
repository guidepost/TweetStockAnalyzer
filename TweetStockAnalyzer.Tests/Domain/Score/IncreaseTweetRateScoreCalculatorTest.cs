using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TweetStockAnalyzer.Domain.Score;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.Tests.Domain.Score
{
    [TestClass]
    public class IncreaseTweetRateScoreCalculatorTest
    {
        [TestMethod]
        public void GetScore()
        {
            var searchWord = new SearchWord();
            searchWord.UpdateDate = new DateTime(2000, 1, 10);

            searchWord.SearchResults.Add(CreateResult(new DateTime(2000, 1, 3, 1, 0, 0), 30));
            searchWord.SearchResults.Add(CreateResult(new DateTime(2000, 1, 3, 2, 0, 0), 50));
            searchWord.SearchResults.Add(CreateResult(new DateTime(2000, 1, 4, 1, 0, 0), 20));
            searchWord.SearchResults.Add(CreateResult(new DateTime(2000, 1, 10, 1, 0, 0), 100));
            searchWord.SearchResults.Add(CreateResult(new DateTime(2000, 1, 10, 2, 0, 0), 100));

            var product = new Product();
            product.SearchWords.Add(searchWord);
            var company = new Mock<Company>();
            company.Setup(p => p.Products).Returns(new List<Product> { product });

            var calculator = new IncreaseTweetRateScoreCalculator();
            var score = calculator.GetScore(company.Object);
            score.Is(400);
        }

        private SearchResult CreateResult(DateTime date, int score)
        {
            var result = new SearchResult();
            result.RegisterDate = date;
            result.TweetCount = score;
            return result;
        }
    }
}
