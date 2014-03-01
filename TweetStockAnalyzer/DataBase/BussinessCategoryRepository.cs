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
            Entities.SaveChanges();
        }

        public BussinessCategory Create(string name, string code)
        {
            var entity = new BussinessCategory();
            entity.BussinessCategoryName = name;
            entity.BussinessCategoryCode = code;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }
    }
}
