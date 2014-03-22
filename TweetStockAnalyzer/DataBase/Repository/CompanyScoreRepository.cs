using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase.Repository
{
    public interface ICompanyScoreRepository : IRepository<CompanyScore>
    {
        CompanyScore Create(Company company, int score);
    }
    [AutoRegist(typeof(ICompanyScoreRepository))]
    public class CompanyScoreRepository : RepositoryBase<CompanyScore>, ICompanyScoreRepository
    {
        protected override DbSet<CompanyScore> DbSet
        {
            get { return Entities.CompanyScore; }
        }

        public override void Update(CompanyScore value)
        {
            var entity = Read(value.CompanyScoreId);
            entity.Score = value.Score;
            entity.IsDeleted = value.IsDeleted;
            base.Update(entity);
        }

        public override CompanyScore Read(Expression<Func<CompanyScore, object>> include, params object[] id)
        {
            return ReadAll().Include(include).FirstOrDefault(p=>p.CompanyScoreId == (int)id[0]);
        }

        public CompanyScore Create(Company company, int score)
        {
            var entity = new CompanyScore();
            entity.Score = score;
            entity.CompanyId = company.CompanyId;
            return base.Create(entity);
        }

    }
}
