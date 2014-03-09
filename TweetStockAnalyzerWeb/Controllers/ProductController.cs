using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzerWeb.WorkerService;

namespace TweetStockAnalyzerWeb.Controllers
{
    public class ProductController : Controller
    {
        private IUnityContainer _container = DependencyContainer.Instance;

        private ProductWorkerService _workerService = new ProductWorkerService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(int productId)
        {
            return View();
        }

        public ActionResult Delete()
        {
            return RedirectToIndex();
        }

        private ActionResult RedirectToIndex(string successMessage = null)
        {
            return RedirectToAction("Index", new { successMessage = successMessage });
        }
	}
}