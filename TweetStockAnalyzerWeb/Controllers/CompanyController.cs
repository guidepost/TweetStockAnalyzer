using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TweetStockAnalyzerWeb.Controllers
{
    public class CompanyController : Controller
    {
        public ActionResult Index()
        {
            var worker = new WorkerService.CompanyWorkerService();
            var model = worker.GetIndexViewModel();
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