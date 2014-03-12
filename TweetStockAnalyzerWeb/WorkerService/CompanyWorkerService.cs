using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzerWeb.Models;
using TweetStockAnalyzerWeb.Models.InputModel;
using TweetStockAnalyzerWeb.ViewModel.Company;

namespace TweetStockAnalyzerWeb.WorkerService
{
    public interface ICompanyWorkerService
    {
        CompanyIndexViewModel GetIndexViewModel();

        CompanyDetailViewModel GetDetailViewModel(int companyId);

        void CreateCompany(CompanyInputModel companyInputModel);

        void UpdateCompany(CompanyInputModel companyInputModel);

        Company DeleteCompany(int companyId);
    }

    [AutoRegist(typeof(ICompanyWorkerService))]
    public class CompanyWorkerService : ICompanyWorkerService
    {
        private IUnityContainer _container = DependencyContainer.Instance;

        public CompanyIndexViewModel GetIndexViewModel()
        {
            var viewModel = new CompanyIndexViewModel();

            using (var repository = _container.Resolve<ICompanyRepository>())
            {
                viewModel.Companies = repository.ReadAll().Include(c => c.Stock)
                                                          .Include(c => c.CompanyScore)
                                                          .ToArray();
            }

            return viewModel;
        }

        public CompanyDetailViewModel GetDetailViewModel(int companyId)
        {
            var viewModel = new CompanyDetailViewModel();

            using (var repository = _container.Resolve<ICompanyRepository>())
            {
                var company = repository.ReadAll()
                                        .Include(c => c.CompanyScore)
                                        .FirstOrDefault(c => c.CompanyId == companyId);

                viewModel.Company = company;
            }

            return viewModel;
        }

        public void CreateCompany(CompanyInputModel companyInputModel)
        {
            using (var companyRepository = _container.Resolve<ICompanyRepository>())
            using (var stockRepository = _container.Resolve<IStockRepository>())
            using (var bussinessCategoryRepository = _container.Resolve<IBussinessCategoryRepository>())
            {
                var parentCompany = companyRepository.Read(companyInputModel.ParentCompanyId);
                var insertedCompany = companyRepository.Create(companyInputModel.CompanyName, parentCompany);

                if (!string.IsNullOrEmpty(companyInputModel.StockCode))
                {
                    var bussinessCategory = bussinessCategoryRepository.Read(companyInputModel.BussinessCategoryId);
                    stockRepository.Create(insertedCompany, bussinessCategory, companyInputModel.StockCode);
                }
            }
        }

        public void UpdateCompany(CompanyInputModel companyInputModel)
        {
            using (var companyRepository = _container.Resolve<ICompanyRepository>())
            using (var stockRepository = _container.Resolve<IStockRepository>())
            using (var bussinessCategoryRepository = _container.Resolve<IBussinessCategoryRepository>())
            {
                var targetCompany = companyRepository.ReadAll()
                                                     .Include(c => c.Stock)
                                                     .FirstOrDefault(c => c.CompanyId == companyInputModel.CompanyId);

                targetCompany.CompanyName = companyInputModel.CompanyName;
                targetCompany.ParentCompanyId = companyInputModel.ParentCompanyId;

                if (targetCompany.Stock == null || targetCompany.Stock.IsDeleted)
                {
                    if (!string.IsNullOrEmpty(companyInputModel.StockCode))
                    {
                        var bussinessCategory = bussinessCategoryRepository.Read(companyInputModel.BussinessCategoryId);
                        stockRepository.Create(targetCompany, bussinessCategory, companyInputModel.StockCode);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(companyInputModel.StockCode))
                    {
                        targetCompany.Stock.IsDeleted = true;
                    }
                    else
                    {
                        var stock = targetCompany.Stock;
                        stock.StockCode = companyInputModel.StockCode;
                        stock.BussinessCategoryId = companyInputModel.BussinessCategoryId;
                        stockRepository.Update(stock);
                    }
                }

                companyRepository.Update(targetCompany);
            }
        }

        public Company DeleteCompany(int companyId)
        {
            using (var repository = _container.Resolve<ICompanyRepository>())
            {
                return repository.Delete(companyId);
            }
        }
    }
}