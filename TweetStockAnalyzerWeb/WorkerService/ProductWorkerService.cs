using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            return viewModel;
        }

        public void CreateProduct()
        {
        }

        public void UpdateProduct(Product product)
        {
        }

        public void DeleteProduct()
        {
        }
    }
}