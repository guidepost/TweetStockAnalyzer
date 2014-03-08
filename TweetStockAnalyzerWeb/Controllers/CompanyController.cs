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
    [AutoRegist]
    public class CompanyController : Controller
    {
        private ICompanyWorkerService _workerService;

        public CompanyController(ICompanyWorkerService workerService)
        {
            _workerService = workerService;
        }

        public ActionResult Index()
        {
            var model = _workerService.GetIndexViewModel();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
	}
}