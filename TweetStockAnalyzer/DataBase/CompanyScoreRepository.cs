﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.DataBase
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
            Entities.SaveChanges();
        }

        public CompanyScore Create(Company company, int score)
        {
            var entity = new CompanyScore();
            entity.Score = score;
            entity.Company = company;
            DbSet.Add(entity);
            Entities.SaveChanges();
            return entity;
        }

    }
}