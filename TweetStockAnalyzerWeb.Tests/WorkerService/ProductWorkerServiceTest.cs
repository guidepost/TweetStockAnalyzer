using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzerWeb.Models.InputModel;
using TweetStockAnalyzerWeb.WorkerService;

namespace TweetStockAnalyzerWeb.Tests.WorkerService
{
    [TestClass]
    public class ProductWorkerServiceTest : DatabaseTestBase
    {
        private ProductWorkerService _workerService = new ProductWorkerService();

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            using (var productRepository = new ProductRepository())
            using (var searchResultRepository = new SearchResultRepository())
            using (var searchWordRepository = new SearchWordRepository())
            {
                var product1 = productRepository.Create("TestProduct1", new DateTime(2001, 1, 1));
                var product2 = productRepository.Create("TestProduct2", new DateTime(2002, 2, 2));


                var searchWord2 = searchWordRepository.Create(product2, "SearchWord1");
                searchResultRepository.Create(searchWord2, product2, 100, new DateTime(2010, 1, 1));
                searchResultRepository.Create(searchWord2, product2, 200, new DateTime(2010, 1, 2));

                var product3 = productRepository.Create("TestProduct3", new DateTime(2003, 3, 3));
            }
        }

        [TestMethod]
        public void GetIndexViewModel()
        {
            var viewModel = _workerService.GetIndexViewModel();

            viewModel.Products[0].ProductName.Is("TestProduct1");
            viewModel.Products[1].ProductName.Is("TestProduct2");

            var searchResults = viewModel.Products[1].SearchResults.ToArray();

            searchResults[0].SearchWord.Word.Is("SearchWord1");
            searchResults[0].TweetCount.Is(100);
            searchResults[0].SearchDate.Is(new DateTime(2010, 1, 1));

            searchResults[1].SearchWord.Word.Is("SearchWord1");
            searchResults[1].TweetCount.Is(200);
            searchResults[1].SearchDate.Is(new DateTime(2010, 1, 2));

            viewModel.Products[2].ProductName.Is("TestProduct3");
        }

        [TestMethod]
        public void GetDetailViewModel()
        {
            using (var productRepository = new ProductRepository())
            {
                var products = productRepository.ReadAll().ToArray();

                var viewModel1 = _workerService.GetDetailViewModel(products[0].ProductId);
                var viewModel2 = _workerService.GetDetailViewModel(products[1].ProductId);

                viewModel1.Product.ProductName.Is("TestProduct1");
                viewModel2.Product.ProductName.Is("TestProduct2");
                viewModel2.Product.SearchResults.ToArray()[0].TweetCount.Is(100);
                viewModel2.Product.SearchResults.ToArray()[1].TweetCount.Is(200);
            }
        }

        [TestMethod]
        public void CreateProduct()
        {
            using (var productRepository4 = new ProductRepository())
            using (var productRepository5 = new ProductRepository())
            {
                var productInputModel4 = new ProductInputModel();

                productInputModel4.ProductName = "TestProduct4";
                productInputModel4.ServiceStartDate = new DateTime(2004, 4, 4);
                productInputModel4.SearchWords = new string[] { "SearchWord4_1", "SearchWord4_2" };

                _workerService.CreateProduct(productInputModel4);

                var product4 = productRepository4.ReadAll().ToArray()[3];

                product4.ProductName.Is("TestProduct4");
                product4.ServiceStartDate.Is(new DateTime(2004, 4, 4));
                product4.SearchWords.ElementAt(0).Word.Is("SearchWord4_1");
                product4.SearchWords.ElementAt(1).Word.Is("SearchWord4_2");


                var productInputModel5 = new ProductInputModel();

                productInputModel5.ProductName = "TestProduct5";
                productInputModel5.ServiceStartDate = new DateTime(2005, 5, 5);

                _workerService.CreateProduct(productInputModel5);

                var product5 = productRepository5.ReadAll().ToArray()[4];

                product5.ProductName.Is("TestProduct5");
                product5.ServiceStartDate.Is(new DateTime(2005, 5, 5));
                product5.SearchWords.Count().Is(0);
            }
        }

        [TestMethod]
        public void UpdateProduct()
        {
        }

        [TestMethod]
        public void DeleteProduct()
        {
        }
    }
}
