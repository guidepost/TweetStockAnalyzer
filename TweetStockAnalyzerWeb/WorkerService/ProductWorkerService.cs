using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzerWeb.Models.InputModel;
using TweetStockAnalyzerWeb.ViewModel.Product;

namespace TweetStockAnalyzerWeb.WorkerService
{
    public class ProductWorkerService
    {
        private IUnityContainer _container = DependencyContainer.Instance;

        public ProductIndexViewModel GetIndexViewModel()
        {
            var viewModel = new ProductIndexViewModel();

            using (var repository = _container.Resolve<IProductRepository>())
            {
                viewModel.Products = repository.ReadAll()
                                               .Include(p => p.SearchResult.First().SearchWord)
                                               .ToArray();
            }

            return viewModel;
        }

        public ProductDetailViewModel GetDetailViewModel(int productId)
        {
            var viewModel = new ProductDetailViewModel();

            using (var repository = _container.Resolve<IProductRepository>())
            {
                var product = repository.ReadAll()
                                        .Include(p => p.SearchResult)
                                        .FirstOrDefault(p=>p.ProductId == productId);
                viewModel.Product = product;
            }

            return viewModel;
        }

        public void CreateProduct(ProductInputModel productInputModel)
        {
            using (var productRepository = _container.Resolve<IProductRepository>())
            using (var searchWordRepository = _container.Resolve<ISearchWordRepository>())
            {
                var product = productRepository.Create(productInputModel.ProductName, productInputModel.ServiceStartDate);
                if (productInputModel.SearchWords != null && productInputModel.SearchWords.Count() > 0)
                {
                    foreach (var searchWord in productInputModel.SearchWords)
                    {
                        searchWordRepository.Create(product, searchWord);
                    }
                }
            }
        }

        public void UpdateProduct(ProductInputModel productInputModel)
        {
            using (var productRepository = _container.Resolve<IProductRepository>())
            using (var searchWordRepository = _container.Resolve<ISearchWordRepository>())
            {
                var product = productRepository.Read(productInputModel.ProductId);

                product.ProductName = productInputModel.ProductName;
                product.ServiceStartDate = productInputModel.ServiceStartDate;
                product.ServiceEndDate = productInputModel.ServiceEndDate;

                productRepository.Update(product);

                if (product.SearchWord == null || product.SearchWord.Count() == 0)
                {
                    foreach (var searchWord in productInputModel.SearchWords)
                    {
                        searchWordRepository.Create(product, searchWord);
                    }
                }
                else if (productInputModel.SearchWords == null || productInputModel.SearchWords.Count() == 0)
                {
                    foreach (var searchWord in product.SearchWord)
                    {
                        searchWordRepository.Delete(searchWord.SearchWordId);
                    }
                }
                else
                {
                    foreach (var existingSearchWord in product.SearchWord)
                    {
                        if (!productInputModel.SearchWords.Any(s => s == existingSearchWord.Word))
                        {
                            searchWordRepository.Delete(existingSearchWord.SearchWordId);
                        }
                    }
                    foreach (var inputSearchWord in productInputModel.SearchWords)
                    {
                        if (!product.SearchWord.Any(s => s.Word == inputSearchWord))
                        {
                            searchWordRepository.Create(product, inputSearchWord);
                        }
                    }
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (var productRepository = _container.Resolve<ProductRepository>())
            {
                productRepository.Delete(productId);
            }
        }
    }
}