using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;
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
    public class CompanyWorkerService : ICompanyWorkerService, IDisposable
    {
        #region Fields

        private IUnityContainer _container;
        private ICompanyRepository _companyRepository;
        private IProductRepository _productRepository;
        private IStockRepository _stockRepository;
        private IStockPriceRepository _stockPriceRepository;
        private IBussinessCategoryRepository _bussinessCategoryRepository;
        private IAggregateHistoryRepository _aggregateHistoryRepository;
        private ICompanyProductRelationRepository _companyProductRelationRepository;

        #endregion

        #region Constructor

        public CompanyWorkerService()
        {
            _container = DependencyContainer.Instance;
            _companyRepository = _container.Resolve<ICompanyRepository>();
            _productRepository = _container.Resolve<IProductRepository>();
            _stockRepository = _container.Resolve<IStockRepository>();
            _stockPriceRepository = _container.Resolve<IStockPriceRepository>();
            _bussinessCategoryRepository = _container.Resolve<IBussinessCategoryRepository>();
            _aggregateHistoryRepository = _container.Resolve<IAggregateHistoryRepository>();
            _companyProductRelationRepository = _container.Resolve<ICompanyProductRelationRepository>();
        }

        #endregion

        public CompanyIndexViewModel GetIndexViewModel()
        {
            return new CompanyIndexViewModel()
            {
                Companies = _companyRepository.ReadAll()
                                             .Include(c => c.Stock)
                                             .Include(c => c.CompanyScores)
                                             .ToArray()
            };
        }

        public CompanyDetailViewModel GetDetailViewModel(int companyId)
        {
            return new CompanyDetailViewModel()
            {
                Company = _companyRepository.ReadAll()
                                            .Include(c => c.CompanyScores)
                                            .FirstOrDefault(c => c.CompanyId == companyId)
            };
        }

        public void CreateCompany(CompanyInputModel companyInputModel)
        {
            var createdCompany = InsertCompany(companyInputModel.CompanyName, companyInputModel.ParentCompanyId);

            InsertStock(createdCompany, companyInputModel.StockCode, companyInputModel.BussinessCategoryId);

            InsertCompanyProductRelation(companyInputModel.Products, companyInputModel.ProductIds, createdCompany);
        }

        public void UpdateCompany(CompanyInputModel companyInputModel)
        {
            var targetCompany = _companyRepository.ReadAll()
                                                  .Include(c => c.Stock)
                                                  .SingleOrDefault(c => c.CompanyId == companyInputModel.CompanyId);

            targetCompany.CompanyName = companyInputModel.CompanyName;
            targetCompany.ParentCompanyId = companyInputModel.ParentCompanyId;

            _companyRepository.Update(targetCompany);

            ManipulateStock(targetCompany, companyInputModel.StockCode, companyInputModel.BussinessCategoryId);

            ManipulateCompanyProductRelation(targetCompany, companyInputModel.Products, companyInputModel.ProductIds);

        }
        
        public Company DeleteCompany(int companyId)
        {
            return _companyRepository.Delete(companyId);
        }

        #region Private Methods

        private Company InsertCompany(string companyName, int? parentCompanyId)
        {
            var parentCompany = _companyRepository.Read(parentCompanyId);
            return _companyRepository.Create(companyName, parentCompany);
        }

        private void InsertStock(Company createdCompany, string stockCode, int bussinessCategoryId)
        {
            if (!string.IsNullOrEmpty(stockCode))
            {
                var bussinessCategory = _bussinessCategoryRepository.Read(bussinessCategoryId);
                var stock = _stockRepository.Create(createdCompany, bussinessCategory, stockCode);

                _aggregateHistoryRepository.Create(stock, DateTime.Now, DateTime.Now);
            }
        }

        private void InsertCompanyProductRelation(Product[] products, int[] productIds, Company insertedCompany)
        {
            if (products != null && products.Count() > 0)
            {
                foreach (var productId in productIds)
                {
                    var product = _productRepository.Read(productId);
                    if (product != null)
                    {
                        _companyProductRelationRepository.Create(insertedCompany, product);
                    }
                }
            }
        }

        private void ManipulateStock(Company targetCompany, string stockCode, int bussinessCategoryId)
        {
            if (targetCompany.Stock == null || targetCompany.Stock.IsDeleted)
            {
                InsertStock(targetCompany, stockCode, bussinessCategoryId);
            }
            else
            {
                if (string.IsNullOrEmpty(stockCode))
                {
                    DeleteStock(targetCompany);
                }
                else
                {
                    UpdateStock(targetCompany.Stock, stockCode, bussinessCategoryId);
                }
            }
        }

        private void ManipulateCompanyProductRelation(Company targetCompany, Product[] products, int[] productIds)
        {
            if (targetCompany.Products == null || targetCompany.Products.Count() == 0)
            {
                InsertCompanyProductRelation(products, productIds, targetCompany);
            }
            else
            {
                UpdateCompanyProductRelation(targetCompany, productIds);
            }
        }

        private void UpdateCompanyProductRelation(Company targetCompany, int[] productIds)
        {
            foreach (var product in targetCompany.Products)
            {
                if (!productIds.Contains(product.ProductId))
                {
                    _companyProductRelationRepository.Delete(product.ProductId);
                }
            }

            foreach (var productId in productIds)
            {
                if (!targetCompany.Products.ToList().Exists(p => p.ProductId == productId))
                {
                    var product = _productRepository.Read(productId);
                    _companyProductRelationRepository.Create(targetCompany, product);
                }
            }
        }

        private void UpdateStock(Stock stock, string stockCode, int bussinessCategoryId)
        {
            stock.StockCode = stockCode;
            stock.BussinessCategoryId = bussinessCategoryId;
            _stockRepository.Update(stock);
        }

        private void DeleteStock(Company targetCompany)
        {
            _aggregateHistoryRepository.Delete(targetCompany.Stock.AggregateHistory.AggregateHistoryId);

            _stockRepository.Delete(targetCompany.Stock.StockId);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _companyRepository.Dispose();
            _productRepository.Dispose();
            _stockRepository.Dispose();
            _stockPriceRepository.Dispose();
            _bussinessCategoryRepository.Dispose();
            _aggregateHistoryRepository.Dispose();
        }

        #endregion
    }
}