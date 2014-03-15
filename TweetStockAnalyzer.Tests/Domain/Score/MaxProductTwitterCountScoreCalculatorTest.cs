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
    public class MaxProductTwitterCountScoreCalculatorTest
    {
        [TestMethod]
        public void GetScore()
        {
            var searchWord = new SearchWord();
            searchWord.UpdateDate = new DateTime(2000, 1, 1);
            var result = new SearchResult();
            result.UpdateDate = new DateTime(2000, 1, 1);
            result.TweetCount = 100;
            searchWord.SearchResults.Add(result);

            var result2 = new SearchResult();
            result2.UpdateDate = new DateTime(2000, 1, 1);
            result2.TweetCount = 200;
            searchWord.SearchResults.Add(result2);

            var resultIgnored = new SearchResult();
            resultIgnored.UpdateDate = new DateTime(1500, 1, 1);
            resultIgnored.TweetCount = 10000;
            searchWord.SearchResults.Add(resultIgnored);
            var product = new Product();
            product.SearchWords.Add(searchWord);
            var company = new Mock<Company>();
            company.Setup(p=>p.Products).Returns(new List<Product>{product});

            var calculator = new MaxProductTwitterCountScoreCalculator();
            var score = calculator.GetScore(company.Object);
            score.Is(3);
        }
    }
}
