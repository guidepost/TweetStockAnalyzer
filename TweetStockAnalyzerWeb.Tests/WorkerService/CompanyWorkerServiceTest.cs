using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzerWeb.Models.InputModel;
using TweetStockAnalyzerWeb.ViewModel.Company;
using TweetStockAnalyzerWeb.WorkerService;

namespace TweetStockAnalyzerWeb.Tests.WorkerService
{
    [TestClass]
    public class CompanyWorkerServiceTest
    {
        private CompanyRepository _companyRepository = new CompanyRepository();
        private CompanyWorkerService _workerService = new CompanyWorkerService();

        [TestInitialize]

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var company in _companyRepository.ReadAll().ToList())
            {
                _companyRepository.Delete(company.CompanyId);
            }
        }

        [TestMethod]
        public void GetIndexViewModel()
        {
            _companyRepository.Create("TestCompany1", null);
            var parentCompany = _companyRepository.ReadAll().First();
            _companyRepository.Create("TestCompany2", parentCompany);

            var viewModel = _workerService.GetIndexViewModel();

            var company1 = viewModel.Companies.ElementAt(0);
            var company2 = viewModel.Companies.ElementAt(1);

            company1.CompanyName.Is("TestCompany1");
            company1.ParentCompanyId.IsNull();

            company2.CompanyName.Is("TestCompany2");
            company2.ParentCompanyId.Is(parentCompany.CompanyId);
        }

        [TestMethod]
        public void GetDetailViewModel()
        {

        }

        [TestMethod]
        public void CreateCompany(CompanyInputModel companyInputModel)
        {

        }

        [TestMethod]
        public void UpdateCompany(CompanyInputModel companyInputModel)
        {

        }
    }
}
