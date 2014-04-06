using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.DataBase.Repository;

namespace TweetStockAnalyzer.Tests.Database
{
    [TestClass]
    public class CompanyProductRelationRepositoryTest : DatabaseTestBase
    {
        [TestMethod]
        public void Create()
        {
            var companyRepository = new CompanyRepository();
            var company = companyRepository.Create("name",null);
            var productRepository = new ProductRepository();
            var product = productRepository.Create("test", DateTime.Now);
            var repository = new CompanyProductRelationRepository();
            repository.Create(company, product);


            companyRepository = new CompanyRepository();
            var result = companyRepository.Read(company.CompanyId);
            result.Products.First().ProductId.Is(product.ProductId);

            repository = new CompanyProductRelationRepository();
            var relation = repository.Read(company.CompanyId, product.ProductId);
            relation.IsNotNull();
        }
    }
}
