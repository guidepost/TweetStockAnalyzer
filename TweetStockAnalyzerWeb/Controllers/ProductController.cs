using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzerWeb.Models.InputModel;
using TweetStockAnalyzerWeb.WorkerService;

namespace TweetStockAnalyzerWeb.Controllers
{
    public class ProductController : Controller
    {
        private IUnityContainer _container = DependencyContainer.Instance;

        private ProductWorkerService _workerService = new ProductWorkerService();

        public ActionResult Index(string successMessage)
        {
            var viewModel = _workerService.GetIndexViewModel();

            viewModel.SuccessMessage = successMessage;

            return View(viewModel);
        }

        public ActionResult Detail(int productId)
        {
            var viewModel = _workerService.GetDetailViewModel(productId);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductInputModel productInputModel)
        {
            _workerService.CreateProduct(productInputModel);

            return RedirectToIndex(string.Format("{0} is created!", productInputModel.ProductName));
        }

        public ActionResult Update(int productId)
        {
            using (var productRepository = new ProductRepository())
            {
                var product = productRepository.ReadAll()
                                               .Include(p => p.SearchWord)
                                               .FirstOrDefault(p => p.ProductId == productId);

                var model = new ProductInputModel();
                model.ProductId = product.ProductId;
                model.ProductName = product.ProductName;
                model.ServiceStartDate = product.ServiceStartDate;
                model.ServiceEndDate = product.ServiceEndDate;
                model.SearchWords = product.SearchWord.Select(s => s.Word).ToArray();

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Update(ProductInputModel productInputModel)
        {
            _workerService.UpdateProduct(productInputModel);

            return RedirectToIndex(string.Format("{0} is updated!", productInputModel.ProductName));
        }

        public ActionResult Delete(int productId)
        {
            using (var repository = _container.Resolve<IProductRepository>())
            {
                var product = repository.Delete(productId);
                if (product != null)
                {
                    return RedirectToIndex(string.Format("{0} is deleted!", product.ProductName));
                }
            }

            return RedirectToIndex();
        }

        private ActionResult RedirectToIndex(string successMessage = null)
        {
            return RedirectToAction("Index", new { successMessage = successMessage });
        }
    }
}