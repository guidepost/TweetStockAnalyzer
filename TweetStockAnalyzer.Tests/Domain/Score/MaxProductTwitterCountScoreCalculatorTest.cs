using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var result3 = new SearchResult();
            result3.UpdateDate = new DateTime(1500, 1, 1);
            result3.TweetCount = 10000;
            searchWord.SearchResults.Add(result3);
            var product = new Product();
            product.SearchWords.Add(searchWord);
            var company = new Company();
            company.CompanyProductRelations.Add(new CompanyProductRelation {Product = product});

            var calculator = new MaxProductTwitterCountScoreCalculator();
            var score = calculator.GetScore(company);
            score.Is(3);
        }
    }
}
