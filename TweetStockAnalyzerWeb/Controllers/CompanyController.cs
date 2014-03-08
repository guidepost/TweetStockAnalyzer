using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetStockAnalyzer.DataBase;
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

        public ActionResult Detail(int? companyId)//TODO:intにする
        {
            var model = _workerService.GetDetailViewModel();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string companyName, int? parentComapnyId, string stockCode, int? bussinessCategoryCode)
        {
            var container = DependencyContainer.Instance;
            using (var companyRepository = container.Resolve<ICompanyRepository>())
            using (var stockRepository = container.Resolve<IStockRepository>())
            using (var bussinessCategoryRepository = container.Resolve<IBussinessCategoryRepository>())
            {
                var parentCompany = companyRepository.Read(parentComapnyId);
                var insertedCompany = companyRepository.Create(companyName, parentCompany);

                if (string.IsNullOrEmpty(stockCode) && bussinessCategoryCode.HasValue)
                {
                    var bussinessCategory = bussinessCategoryRepository.Read(bussinessCategoryCode);
                    stockRepository.Create(insertedCompany, bussinessCategory, stockCode);
                }
            }

            return RedirectToAction("Index");
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