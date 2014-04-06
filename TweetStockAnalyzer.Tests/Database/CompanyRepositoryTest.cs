using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;

namespace TweetStockAnalyzer.Tests.Database
{
    [TestClass]
    public class CompanyRepositoryTest : DatabaseTestBase
    {
        [TestMethod]
        public void Update()
        {
            var companyRepository = new CompanyRepository();
            var company = companyRepository.Create("TestCompany1", null);

            var company1 = companyRepository.ReadAll().First();
            company1.CompanyName.Is("TestCompany1");
            company1.CompanyName = "HogeCompany";
            companyRepository.Update(company1);

            var company2 = companyRepository.ReadAll().First();
            company2.CompanyName.Is("HogeCompany");

        }
    }
}
