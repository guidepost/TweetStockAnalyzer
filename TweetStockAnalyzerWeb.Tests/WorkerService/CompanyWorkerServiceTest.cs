using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzerWeb.Models.InputModel;
using TweetStockAnalyzerWeb.ViewModel.Company;
using TweetStockAnalyzerWeb.WorkerService;

namespace TweetStockAnalyzerWeb.Tests.WorkerService
{
    [TestClass]
    public class CompanyWorkerServiceTest
    {
        #region Fields

        private static CompanyWorkerService _workerService = new CompanyWorkerService();

        private static CompanyRepository _companyRepository = new CompanyRepository();
        private static ProductRepository _productRepository = new ProductRepository();
        private static StockRepository _stockRepository = new StockRepository();
        private static StockPriceRepository _stockPriceRepository = new StockPriceRepository();
        private static BussinessCategoryRepository _bussinessCategoryRepository = new BussinessCategoryRepository();
        private static AggregateHistoryRepository _aggregateHistoryRepository = new AggregateHistoryRepository();
        private static CompanyProductRelationRepository _companyProductRelationRepository = new CompanyProductRelationRepository();
        private static CompanyScoreRepository _companyScoreRepository = new CompanyScoreRepository();

        private static BussinessCategory _testBussinessCategory;
        private static Product _testProduct1;
        private static Product _testProduct2;

        #endregion

        #region Initialize and Cleanup

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _testBussinessCategory = _bussinessCategoryRepository.Create("TestCategory", "1");
            _testProduct1 = _productRepository.Create("TestProduct1", new DateTime(2000, 1, 1));
            _testProduct2 = _productRepository.Create("TestProduct2", new DateTime(2000, 2, 2));
        }

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var company in _companyRepository.ReadAll().ToArray())
            {
                _companyRepository.Delete(company.CompanyId);
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            DeleteEntities();

            _companyRepository.Dispose();
            _productRepository.Dispose();
            _stockRepository.Dispose();
            _stockPriceRepository.Dispose();
            _bussinessCategoryRepository.Dispose();
            _aggregateHistoryRepository.Dispose();
            _companyProductRelationRepository.Dispose();
        }

        private static void DeleteEntities()
        {
            foreach (var category in _bussinessCategoryRepository.ReadAll().ToArray())
            {
                _bussinessCategoryRepository.Delete(category.BussinessCategoryId);
            }

            foreach (var product in _productRepository.ReadAll().ToArray())
            {
                _productRepository.Delete(product.ProductId);
            }

            foreach (var score in _companyScoreRepository.ReadAll().ToArray())
            {
                _companyScoreRepository.Delete(score.CompanyScoreId);
            }

            foreach (var companyProductRelation in _companyProductRelationRepository.ReadAll().ToArray())
            {
                _companyProductRelationRepository.Delete(companyProductRelation.CompanyId);
            }
        }

        #endregion

        [TestMethod]
        public void GetIndexViewModel()
        {
            var parentCompany = _companyRepository.Create("ParentCompany", null);
            var childCompany = _companyRepository.Create("ChildCompany", parentCompany);
            _stockRepository.Create(parentCompany, _testBussinessCategory, "1234");
            _stockRepository.Create(childCompany, _testBussinessCategory, "1235");
            var resultViewModel = _workerService.GetIndexViewModel();

            var resultCompany = resultViewModel.Companies.ElementAt(1);

            resultCompany.CompanyName.Is("ChildCompany");
            resultCompany.ParentCompanyId.Is(parentCompany.CompanyId);
        }

        [TestMethod]
        public void GetDetailViewModel()
        {
            var parentCompany = _companyRepository.Create("TestCompany1", null);
            var childCompany = _companyRepository.Create("TestCompany2", parentCompany);

            _companyScoreRepository.Create(childCompany, 100);
            _companyScoreRepository.Create(childCompany, 200);

            var resultViewModel = _workerService.GetDetailViewModel(childCompany.CompanyId);

            resultViewModel.Company.CompanyName.Is("TestCompany2");
            resultViewModel.Company.ParentCompanyId.Is(parentCompany.CompanyId);

            resultViewModel.Company.CompanyScores.ToArray()[0].Score.Is(200);
            resultViewModel.Company.CompanyScores.ToArray()[1].Score.Is(100);
        }

        [TestMethod]
        public void GetDetailViewModelMoreThan7Score()
        {
            var company = _companyRepository.Create("TestCompany1", null);

            for (int i = 0; i < 10; i++)
            {
                _companyScoreRepository.Create(company, 100);
            }

            var resultViewModel = _workerService.GetDetailViewModel(company.CompanyId);
            resultViewModel.Company.CompanyScores.ToArray().Count().Is(7);
        }


        [TestMethod]
        public void CreateCompany()
        {
            var testCompanies = PrepareCompanies();

            testCompanies[0].CompanyName.Is("TestCompany1");
            testCompanies[0].Products.First().ProductName.Is("TestProduct1");
            testCompanies[0].Products.First().ServiceStartDate.Is(new DateTime(2000, 1, 1));

            testCompanies[1].CompanyName.Is("TestCompany2");
            testCompanies[1].ParentCompanyId.Is(testCompanies[0].CompanyId);
            testCompanies[1].Stock.StockCode.Is("100");
            testCompanies[1].Stock.BussinessCategory.BussinessCategoryCode.Is("1");
            testCompanies[1].Stock.BussinessCategory.BussinessCategoryName.Is("TestCategory");

            testCompanies[2].CompanyName.Is("TestCompany3");
            testCompanies[2].Stock.IsNull();
        }

        //[TestMethod]
        //public void UpdateCompany()
        //{
        //    var companies = PrepareCompanies();

        //    var beforeCompanies = _companyRepository.ReadAll().ToArray();

        //    var beforeCompanyInputModels = PrepareInputModel(beforeCompanies);

        //    _workerService.UpdateCompany(beforeCompanyInputModels[0]);
        //    _workerService.UpdateCompany(beforeCompanyInputModels[1]);
        //    _workerService.UpdateCompany(beforeCompanyInputModels[2]);

        //    ResetCompanyRepository();

        //    var afterCompanies = _companyRepository.ReadAll().ToArray();

        //    afterCompanies[0].CompanyName.Is("AfterCompany1");
        //    afterCompanies[0].ParentCompanyId.Is(beforeCompanies[2].CompanyId);
        //    afterCompanies[0].Stock.StockCode.Is("100");
        //    afterCompanies[0].Stock.BussinessCategory.BussinessCategoryCode.Is("1");
        //    afterCompanies[0].Stock.BussinessCategory.BussinessCategoryName.Is("TestCategory");
        //    (afterCompanies[0].Products == null || afterCompanies[0].Products.Count() == 0).IsTrue();

        //    afterCompanies[1].CompanyName.Is("AfterCompany2");
        //    afterCompanies[1].Stock.IsNull();
        //    afterCompanies[1].Products.First().ProductId.Is(_testProduct2.ProductId);

        //    beforeCompanyInputModels[2].ParentCompanyId = null;
        //    beforeCompanyInputModels[2].StockCode = string.Empty;

        //    _workerService.UpdateCompany(beforeCompanyInputModels[2]);

        //    ResetCompanyRepository();

        //    afterCompanies = _companyRepository.ReadAll().ToArray();

        //    afterCompanies[2].ParentCompanyId.IsNull();
        //    afterCompanies[2].Stock.IsNull();
        //    afterCompanies[2].Products.First().ProductId.Is(_testProduct1.ProductId);
        //}

        [TestMethod]
        public void DeleteCompany()
        {
            var company = _companyRepository.Create("TestCompany", null);

            _workerService.DeleteCompany(company.CompanyId);

            _companyRepository.ReadAll().Count().Is(0);
        }

        #region Private Methods

        private CompanyInputModel ConvertCompanyToInputModel(TweetStockAnalyzer.Model.Company company)
        {
            var companyInputModel = new CompanyInputModel();

            companyInputModel.CompanyId = company.CompanyId;
            companyInputModel.CompanyName = company.CompanyName;
            companyInputModel.ParentCompanyId = company.ParentCompanyId;
            if (company.Stock != null)
            {
                companyInputModel.StockCode = company.Stock.StockCode;
                companyInputModel.BussinessCategoryId = company.Stock.BussinessCategoryId;
            }

            return companyInputModel;
        }

        private static void ResetCompanyRepository()
        {
            _companyRepository.Dispose();
            _companyRepository = new CompanyRepository();
        }

        private Company[] PrepareCompanies()
        {
            var companyInputModel1 = new CompanyInputModel
            {
                CompanyName = "TestCompany1",
                ProductIds = new[] { _testProduct1.ProductId }
            };

            _workerService.CreateCompany(companyInputModel1);
            var company1 = _companyRepository.ReadAll().First();

            var companyInputModel2 = new CompanyInputModel
            {
                CompanyName = "TestCompany2",
                ParentCompanyId = company1.CompanyId,
                StockCode = "100",
                BussinessCategoryId = _testBussinessCategory.BussinessCategoryId,
                ProductIds = new[] { _testProduct1.ProductId }
            };

            _workerService.CreateCompany(companyInputModel2);
            var company2 = _companyRepository.ReadAll().ToArray()[1];

            var companyInputModel3 = new CompanyInputModel
            {
                CompanyName = "TestCompany3",
                BussinessCategoryId = _testBussinessCategory.BussinessCategoryId
            };

            _workerService.CreateCompany(companyInputModel3);
            var company3 = _companyRepository.ReadAll().ToArray()[2];

            return _companyRepository.ReadAll()
                                     .Include(c => c.Products)
                                     .ToArray();
        }

        private CompanyInputModel[] PrepareInputModel(Company[] beforeCompanies)
        {
            var beforeCompanyInputModel1 = ConvertCompanyToInputModel(beforeCompanies[0]);
            var beforeCompanyInputModel2 = ConvertCompanyToInputModel(beforeCompanies[1]);
            var beforeCompanyInputModel3 = ConvertCompanyToInputModel(beforeCompanies[2]);

            beforeCompanyInputModel1.CompanyName = "AfterCompany1";
            beforeCompanyInputModel1.ParentCompanyId = beforeCompanies[2].CompanyId;
            beforeCompanyInputModel1.StockCode = "100";
            beforeCompanyInputModel1.BussinessCategoryId = _testBussinessCategory.BussinessCategoryId;
            beforeCompanyInputModel1.ProductIds = new int[] { };

            beforeCompanyInputModel2.CompanyName = "AfterCompany2";
            beforeCompanyInputModel2.StockCode = string.Empty;
            beforeCompanyInputModel2.BussinessCategoryId = _testBussinessCategory.BussinessCategoryId;
            beforeCompanyInputModel2.ProductIds = new[] { _testProduct2.ProductId };

            beforeCompanyInputModel3.CompanyName = "AfterCompany3";
            beforeCompanyInputModel3.ParentCompanyId = beforeCompanies[2].CompanyId;
            beforeCompanyInputModel3.StockCode = "200";
            beforeCompanyInputModel3.BussinessCategoryId = _testBussinessCategory.BussinessCategoryId;
            beforeCompanyInputModel3.ProductIds = new[] { _testProduct1.ProductId };

            return new CompanyInputModel[] { beforeCompanyInputModel1, beforeCompanyInputModel2, beforeCompanyInputModel3 };
        }

        #endregion

    }
}
