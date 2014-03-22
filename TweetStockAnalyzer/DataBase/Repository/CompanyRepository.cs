using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase.Repository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Company Create(string name,  Company parentCompany);
    }
    [AutoRegist(typeof(ICompanyRepository))]
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        protected override DbSet<Company> DbSet
        {
            get { return Entities.Company; }
        }

        public override void Update(Company value)
        {
            var entity = Read(value.CompanyId);
            entity.CompanyName = value.CompanyName;
            entity.ParentCompanyId = value.ParentCompanyId;
            entity.IsDeleted = value.IsDeleted;
            base.Update(entity);
        }

        public override Company Read(Expression<Func<Company, object>> include, params object[] id)
        {
            return ReadAll().Include(include).FirstOrDefault(p => p.CompanyId == (int) id[0]);
        }

        public Company Create(string name,  Company parentCompany)
        {
            var entity = new Company();
            entity.CompanyName = name;
            if(parentCompany != null)
            {
                entity.ParentCompanyId = parentCompany.CompanyId;
            }
            return base.Create(entity);
        }
    }
}

