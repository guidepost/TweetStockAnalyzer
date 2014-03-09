﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzerWeb.Models.InputModel;
using TweetStockAnalyzerWeb.ViewModel.Company;

namespace TweetStockAnalyzerWeb.WorkerService
{
    public interface ICompanyWorkerService
    {
        CompanyIndexViewModel GetIndexViewModel();

        CompanyDetailViewModel GetDetailViewModel();

        void CreateCompany(CompanyInputModel companyInputModel);

        void UpdateCompany(CompanyInputModel companyInputModel);
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
                viewModel.Companies = repository.ReadAll().Include(c=>c.Stock).ToArray();
            }

            return viewModel;
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
                    var bussinessCategory = bussinessCategoryRepository.ReadAll()
                                                                       .FirstOrDefault(c => c.BussinessCategoryId.ToString() == companyInputModel.BussinessCategoryId);
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
                var targetCompany = companyRepository.Read(companyInputModel.CompanyId);
                targetCompany.CompanyName = companyInputModel.CompanyName;
                targetCompany.ParentCompanyId = companyInputModel.ParentCompanyId;

                if (targetCompany.Stock == null || targetCompany.Stock.IsDeleted)
                {
                    if (!string.IsNullOrEmpty(companyInputModel.StockCode))
                    {
                        var bussinessCategory = bussinessCategoryRepository.ReadAll()
                                                                           .FirstOrDefault(c => c.BussinessCategoryId.ToString() == companyInputModel.BussinessCategoryId);
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
                        stockRepository.Update(stock);
                    }
                }

                companyRepository.Update(targetCompany);
            }
        }
    }
}