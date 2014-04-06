using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;

namespace TweetStockAnalyzer.Tests.Database
{
    [TestClass]
    public class SearchWordRepositoryTest : DatabaseTestBase
    {
        [TestMethod]
        public void Create()
        {
            var productRepository = new ProductRepository();
            var productResult = productRepository.Create("test", new DateTime(2000, 7, 7));
            var wordRepository = new SearchWordRepository();
            var word = wordRepository.Create(productResult, "word");
            wordRepository = new SearchWordRepository();
            var wordResult = wordRepository.Read(word.SearchWordId);
            wordResult.IsNotNull();
            wordResult.Product.ProductId.Is(productResult.ProductId);
            wordResult.Word.Is("word");
            productRepository = new ProductRepository();
            productResult = productRepository.Read(productResult.ProductId);
            productResult.SearchWords.Any(p => p.SearchWordId == wordResult.SearchWordId).IsTrue();
        }
    }
}
