using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.DataBase;

namespace TweetStockAnalyzer.Tests.Database
{
    [TestClass]
    public class StockRepositoryTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            var repository = new CompanyRepository();
            foreach (var product in repository.ReadAll().ToList())
            {
                repository.Delete(product.CompanyId);
            }
        }
        [TestMethod]
        public void Get()
        {
            var notFoundKey = -100;
            var repository = new StockRepository();
            var result = repository.Read(notFoundKey);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create()
        {
            var companyRepository = new CompanyRepository();
            var company = companyRepository.Create("test", null);
            company = new CompanyRepository().Read(company.CompanyId);
            var bussinessRepository = new BussinessCategoryRepository();
            var category = bussinessRepository.Create("aaa", "bbb");
            var stockRepository = new StockRepository();
            var stock = stockRepository.Create(company, category, "123");

            var stock2 = new StockRepository();
            var result = stock2.Read(stock.StockId);
            result.Company.CompanyId.Is( company.CompanyId);
            result.BussinessCategory.BussinessCategoryId.Is(category.BussinessCategoryId);
            result.StockCode.Is("123");

            company = new CompanyRepository().Read(company.CompanyId);
            company.Stock.IsNotNull();
        }

        [TestMethod]
        public void ReadWithInclude()
        {
            Create();
            var repository = new CompanyRepository();
            var company  = repository.ReadAll().Include(p=>p.Stock).FirstOrDefault();
            repository.Dispose();
            company.Stock.IsNotNull();
        }
    }
}
