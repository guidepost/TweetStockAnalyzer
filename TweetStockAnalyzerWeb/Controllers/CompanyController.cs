﻿using Microsoft.Practices.Unity;
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
            var model = _workerService.GetDetailViewModel();
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

        public ActionResult Update()
        {
            return View();
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
                var result = repository.Delete(companyId);
                if (result != null)
                {
                    return RedirectToIndex("The company is deleted!");
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