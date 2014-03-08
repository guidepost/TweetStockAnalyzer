using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzerWeb.ViewModel.Company;

namespace TweetStockAnalyzerWeb.WorkerService
{
    public interface ICompanyWorkerService
    {
        CompanyIndexViewModel GetIndexViewModel();

        CompanyDetailViewModel GetDetailViewModel();
    }

    [AutoRegist(typeof(ICompanyWorkerService))]
    public class CompanyWorkerService : ICompanyWorkerService
    {
        private IUnityContainer _container = DependencyContainer.Instance;

        public CompanyIndexViewModel GetIndexViewModel()
        {
            using (var repository = _container.Resolve<ICompanyRepository>())
            {
                //return new CompanyIndexViewModel
                //{
                //    Companies = repository.ReadAll()
                //};

                return new CompanyIndexViewModel
                {
                    Companies = new Company[] {
                        new Company {
                            CompanyId = 1,
                            CompanyName = "companyName",
                            RegisterDate = DateTime.Now,
                            UpdateDate = DateTime.Now},
                        new Company {
                            CompanyId = 2,
                            CompanyName = "companyName",
                            RegisterDate = DateTime.Now,
                            UpdateDate = DateTime.Now},
                        new Company {
                            CompanyId = 3,
                            CompanyName = "companyName",
                            RegisterDate = DateTime.Now,
                            UpdateDate = DateTime.Now}}
                };
            }
        }

        public CompanyDetailViewModel GetDetailViewModel()
        {
            var viewModel = new CompanyDetailViewModel();

            using (var repository = _container.Resolve<ICompanyRepository>())
            {
                viewModel.Company = new Company
                    {
                        CompanyId = 1,
                        CompanyName = "companyName",
                        RegisterDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    };
            }

            return viewModel;
        }
    }
}