using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase.Repository
{
    public interface IBussinessCategoryRepository : IRepository<BussinessCategory>
    {
        BussinessCategory Create(string name, string code);
    }
    [AutoRegist(typeof(IBussinessCategoryRepository))]
    public class BussinessCategoryRepository : RepositoryBase<BussinessCategory>, IBussinessCategoryRepository
    {
        protected override DbSet<BussinessCategory> DbSet
        {
            get { return Entities.BussinessCategory; }
        }

        public override void Update(BussinessCategory value)
        {
            var entity = Read(value.BussinessCategoryId);
            entity.BussinessCategoryCode = value.BussinessCategoryCode;
            entity.BussinessCategoryName = value.BussinessCategoryName;
            entity.IsDeleted = value.IsDeleted;
            base.Update(entity);
        }

        public override BussinessCategory Read(Expression<Func<BussinessCategory, object>> include, params object[] id)
        {
            return ReadAll().Include(include).FirstOrDefault(p => p.BussinessCategoryId == (int) id[0]);
        }

        public BussinessCategory Create(string name, string code)
        {
            var entity = new BussinessCategory();
            entity.BussinessCategoryName = name;
            entity.BussinessCategoryCode = code;
            return base.Create(entity);
        }
    }
}
