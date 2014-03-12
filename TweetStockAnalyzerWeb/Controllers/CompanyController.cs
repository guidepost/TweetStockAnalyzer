using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzerWeb.Models.InputModel;
using TweetStockAnalyzerWeb.WorkerService;

namespace TweetStockAnalyzerWeb.Controllers
{
    [AutoRegist]
    public class CompanyController : Controller
    {
        private IUnityContainer _container = DependencyContainer.Instance;

        private ICompanyWorkerService _workerService;

        public CompanyController(ICompanyWorkerService workerService)
        {
            _workerService = workerService;
        }

        public ActionResult Index(string successMessage)
        {
            var model = _workerService.GetIndexViewModel();

            model.SuccessMessage = successMessage;

            return View(model);
        }

        public ActionResult Detail(int companyId)
        {
            var model = _workerService.GetDetailViewModel(companyId);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CompanyInputModel companyInputModel)
        {
            _workerService.CreateCompany(companyInputModel);

            return RedirectToIndex(string.Format("{0} is created!", companyInputModel.CompanyName));
        }

        public ActionResult Update(int companyId)
        {
            using (var repository = _container.Resolve<ICompanyRepository>())
            {
                var company = repository.Read(companyId);

                var model = new CompanyInputModel();

                model.CompanyId = companyId;
                model.ParentCompanyId = company.ParentCompanyId;
                model.CompanyName = company.CompanyName;
                if (company.Stock != null && !company.Stock.IsDeleted)
                {
                    model.StockCode = company.Stock.StockCode;

                    model.BussinessCategoryId = company.Stock.BussinessCategoryId;
                }

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Update(CompanyInputModel companyInputModel)
        {
            _workerService.UpdateCompany(companyInputModel);

            return RedirectToIndex(string.Format("{0} is updated!", companyInputModel.CompanyName));
        }

        public ActionResult Delete(int companyId)
        {
            var result = _workerService.DeleteCompany(companyId);
            if (result != null)
            {
                return RedirectToIndex("The company is deleted!");
            }

            return RedirectToIndex();
        }

        private ActionResult RedirectToIndex(string successMessage = null)
        {
            return RedirectToAction("Index", new { successMessage = successMessage });
        }
    }
}