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
            var viewModel = _workerService.GetIndexViewModel();

            viewModel.SuccessMessage = successMessage;

            return View(viewModel);
        }

        public ActionResult Detail(int companyId)
        {
            var viewModel = _workerService.GetDetailViewModel(companyId);
            return View(viewModel);
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
                if (company.Stock != null)
                {
                    model.StockCode = company.Stock.StockCode;

                    model.BussinessCategoryId = company.Stock.BussinessCategoryId.ToString();
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
            using (var repository = _container.Resolve<ICompanyRepository>())
            {
                var company = repository.Delete(companyId);
                if (company != null)
                {
                    return RedirectToIndex(string.Format("{0} is deleted!", company.CompanyName));
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