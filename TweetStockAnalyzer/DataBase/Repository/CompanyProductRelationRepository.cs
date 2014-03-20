using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
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

        public override CompanyProductRelation Read(Expression<Func<CompanyProductRelation, object>> include,params object[] id)
        {
            return ReadAll().Include(include).FirstOrDefault(p => p.CompanyId == (int)id[0] && p.ProductId == (int)id[1]);
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
