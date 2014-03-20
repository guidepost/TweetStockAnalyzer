using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase.Repository
{
    public interface ICompanyProductRelationRepository : IRepository<CompanyProductRelation>
    {
        CompanyProductRelation Create(Company company, Product product);
    }
    [AutoRegist(typeof(ICompanyProductRelationRepository))]
    public class CompanyProductRelationRepository : RepositoryBase<CompanyProductRelation>, ICompanyProductRelationRepository
    {
        protected override DbSet<CompanyProductRelation> DbSet
        {
            get { return Entities.CompanyProductRelation; }
        }

        public CompanyProductRelation Create(Company company, Product product)
        {
            var entity = new CompanyProductRelation();
            entity.CompanyId = company.CompanyId;
            entity.ProductId = product.ProductId;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
    }
}
