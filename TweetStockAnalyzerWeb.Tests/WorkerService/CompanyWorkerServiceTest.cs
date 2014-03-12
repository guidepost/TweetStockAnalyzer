using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;
using TweetStockAnalyzerWeb.Models.InputModel;
using TweetStockAnalyzerWeb.ViewModel.Company;
using TweetStockAnalyzerWeb.WorkerService;

namespace TweetStockAnalyzerWeb.Tests.WorkerService
{
    [TestClass]
    public class CompanyWorkerServiceTest
    {
        private CompanyWorkerService _workerService = new CompanyWorkerService();

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            using (var categoryRepository = new BussinessCategoryRepository())
            {
                foreach (var category in categoryRepository.ReadAll().ToArray())
                {
                    categoryRepository.Delete(category.BussinessCategoryId);
                }

                categoryRepository.Create("テストカテゴリー１", "1");
                categoryRepository.Create("テストカテゴリー２", "2");
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            CleanupCompany();
        }

        [TestCleanup]
        public void Cleanup()
        {
            CleanupCompany();
        }

        private void CleanupCompany()
        {
            using (var repository = new CompanyRepository())
            {
                foreach (var company in repository.ReadAll().ToList())
                {
                    repository.Delete(company.CompanyId);
                }
            }
        }

        [TestMethod]
        public void GetIndexViewModel()
        {
            using (var companyRepository = new CompanyRepository())
            {
                companyRepository.Create("TestCompany1", null);
                var parentCompany = companyRepository.ReadAll().First();
                companyRepository.Create("TestCompany2", parentCompany);

                var viewModel = _workerService.GetIndexViewModel();

                var company1 = viewModel.Companies.ElementAt(0);
                var company2 = viewModel.Companies.ElementAt(1);

                company1.CompanyName.Is("TestCompany1");
                company1.ParentCompanyId.IsNull();

                company2.CompanyName.Is("TestCompany2");
                company2.ParentCompanyId.Is(parentCompany.CompanyId);
            }
        }

        [TestMethod]
        public void GetDetailViewModel()
        {
            using (var companyRepository = new CompanyRepository())
            using (var scoreRepository = new CompanyScoreRepository())
            {
                var company1 = companyRepository.Create("TestCompany1", null);
                var company2 = companyRepository.Create("TestCompany2", company1);

                scoreRepository.Create(company2, 100);
                scoreRepository.Create(company2, 200);

                var viewModel1 = _workerService.GetDetailViewModel(company1.CompanyId);
                var viewModel2 = _workerService.GetDetailViewModel(company2.CompanyId);

                viewModel1.Company.CompanyName.Is("TestCompany1");

                viewModel2.Company.CompanyName.Is("TestCompany2");
                viewModel2.Company.ParentCompanyId.Is(company1.CompanyId);

                viewModel1.Company.CompanyScores.Count().Is(0);
                viewModel2.Company.CompanyScores.First().Score.Is(100);
                viewModel2.Company.CompanyScores.ToArray()[1].Score.Is(200);
            }
        }

        [TestMethod]
        public void CreateCompany()
        {
            using (var companyRepository = new CompanyRepository())
            using (var categoryRepository = new BussinessCategoryRepository())
            {
                var companyInputModel1 = new CompanyInputModel();

                companyInputModel1.CompanyName = "TestCompany1";

                _workerService.CreateCompany(companyInputModel1);

                var company1 = companyRepository.ReadAll().First();
                company1.CompanyName.Is("TestCompany1");

                var companyInputModel2 = new CompanyInputModel();

                companyInputModel2.CompanyName = "TestCompany2";
                companyInputModel2.ParentCompanyId = company1.CompanyId;
                companyInputModel2.StockCode = "100";
                var bussinessCategory = categoryRepository.ReadAll().First();
                companyInputModel2.BussinessCategoryId = bussinessCategory.BussinessCategoryId;

                _workerService.CreateCompany(companyInputModel2);

                var company2 = companyRepository.ReadAll().ToArray()[1];
                company2.CompanyName.Is("TestCompany2");
                company2.ParentCompanyId.Is(company1.CompanyId);
                company2.Stock.StockCode.Is("100");
                company2.Stock.BussinessCategory.BussinessCategoryCode.Is("1");
                company2.Stock.BussinessCategory.BussinessCategoryName.Is("テストカテゴリー１");

                var companyInputModel3 = new CompanyInputModel();
                companyInputModel3.CompanyName = "TestCompany3";
                companyInputModel3.BussinessCategoryId = bussinessCategory.BussinessCategoryId;

                _workerService.CreateCompany(companyInputModel3);

                var company3 = companyRepository.ReadAll().ToArray()[2];
                company3.CompanyName.Is("TestCompany3");
                company3.Stock.IsNull();
            }
        }

        [TestMethod]
        public void UpdateCompany()
        {
            CreateCompany();

            using (var companyRepository = new CompanyRepository())
            using (var categoryRepository = new BussinessCategoryRepository())
            using (var nextCompanyRepository = new CompanyRepository())
            using (var lastCompanyRepository = new CompanyRepository())
            {
                var bussinessCategories = categoryRepository.ReadAll().ToArray();

                var beforeCompanies = companyRepository.ReadAll().ToArray();

                //①：親なし、株なし
                //②：親あり、株あり
                //③：親なし、株なし
                var beforeCompany1 = beforeCompanies[0];
                var beforeCompany2 = beforeCompanies[1];
                var beforeCompany3 = beforeCompanies[2];

                var beforeCompanyInputModel1 = ConvertCompanyToInputModel(beforeCompany1);
                var beforeCompanyInputModel2 = ConvertCompanyToInputModel(beforeCompany2);
                var beforeCompanyInputModel3 = ConvertCompanyToInputModel(beforeCompany3);

                beforeCompanyInputModel1.CompanyName = "AfterCompany1";
                beforeCompanyInputModel1.ParentCompanyId = beforeCompany3.CompanyId;

                beforeCompanyInputModel2.CompanyName = "AfterCompany2";
                beforeCompanyInputModel2.ParentCompanyId = beforeCompany3.CompanyId;
                beforeCompanyInputModel2.StockCode = "200";
                beforeCompanyInputModel2.BussinessCategoryId = bussinessCategories[1].BussinessCategoryId;

                beforeCompanyInputModel3.CompanyName = "AfterCompany3";
                beforeCompanyInputModel3.StockCode = string.Empty;
                beforeCompanyInputModel3.BussinessCategoryId = bussinessCategories[1].BussinessCategoryId;

                _workerService.UpdateCompany(beforeCompanyInputModel1);
                _workerService.UpdateCompany(beforeCompanyInputModel2);
                _workerService.UpdateCompany(beforeCompanyInputModel3);

                var afterCompanies = nextCompanyRepository.ReadAll().ToArray();

                var afterCompany1 = afterCompanies[0];
                var afterCompany2 = afterCompanies[1];
                var afterCompany3 = afterCompanies[2];

                afterCompany1.CompanyName.Is("AfterCompany1");
                afterCompany1.ParentCompanyId.Is(beforeCompany3.CompanyId);

                afterCompany2.CompanyName.Is("AfterCompany2");
                afterCompany2.ParentCompanyId.Is(beforeCompany3.CompanyId);
                afterCompany2.Stock.StockCode.Is("200");
                afterCompany2.Stock.BussinessCategory.BussinessCategoryCode.Is("2");
                afterCompany2.Stock.BussinessCategory.BussinessCategoryName.Is("テストカテゴリー２");

                afterCompany3.CompanyName.Is("AfterCompany3");
                afterCompany3.Stock.IsNull();

                beforeCompanyInputModel2.ParentCompanyId = null;
                beforeCompanyInputModel2.StockCode = string.Empty;

                _workerService.UpdateCompany(beforeCompanyInputModel2);

                var lastCompany2 = lastCompanyRepository.Read(beforeCompany2.CompanyId);
                lastCompany2.ParentCompanyId.IsNull();
                lastCompany2.Stock.IsDeleted.IsTrue();
            }
        }

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

        [TestMethod]
        public void DeleteCompany()
        {
            CreateCompany();

            using (var companyRepository = new CompanyRepository())
            {
                var companies = companyRepository.ReadAll().ToArray();

                _workerService.DeleteCompany(companies[0].CompanyId);
                _workerService.DeleteCompany(companies[1].CompanyId);
                _workerService.DeleteCompany(companies[2].CompanyId);

                companyRepository.ReadAll().Count().Is(0);
            }
        }
    }
}
