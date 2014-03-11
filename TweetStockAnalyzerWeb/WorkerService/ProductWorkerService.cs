using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;
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
                viewModel.Products = repository.ReadAll().ToArray();
            }

            return viewModel;
        }

        public ProductDetailViewModel GetDetailViewModel(int productId)
        {
            var viewModel = new ProductDetailViewModel();

            using (var repository = _container.Resolve<IProductRepository>())
            {
                var product = repository.Read(productId);
                viewModel.Product = product;
            }

            return viewModel;
        }

        public void CreateProduct(string productName, DateTime serviceStartDate)
        {
            using (var repository = _container.Resolve<IProductRepository>())
            {
                repository.Create(productName, serviceStartDate);
            }
        }

        public void UpdateProduct(Product product)
        {
        }

        public void DeleteProduct()
        {
        }
    }
}