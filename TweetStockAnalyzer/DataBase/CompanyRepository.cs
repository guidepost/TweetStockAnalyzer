using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
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
            Entities.SaveChanges();
        }

        public Company Create(string name,  Company parentCompany)
        {
            var entity = new Company();
            entity.CompanyName = name;
            if(parentCompany != null)
            {
                entity.ParentCompanyId = parentCompany.CompanyId;
            }
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
    }
}

