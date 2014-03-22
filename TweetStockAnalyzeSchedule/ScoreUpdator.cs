using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TweetStockAnalyzer.DataBase.Repository;
using TweetStockAnalyzer.Domain.Score;
using TweetStockAnalyzer.Infrastructure.Dependency;

namespace TweetStockAnalyzeSchedule
{
    public class ScoreUpdator : ISchedule
    {
        public void Start()
        {
            var scoreCalculator = DependencyContainer.Instance.Resolve<IScoreCalculator>();
            using (var companyScoreRepository = DependencyContainer.Instance.Resolve<ICompanyScoreRepository>())
            using (var companyRepository = DependencyContainer.Instance.Resolve<ICompanyRepository>())
            {
                foreach (var company in companyRepository.ReadAll().ToList())
                {
                    var score = scoreCalculator.GetScore(company);
                    companyScoreRepository.Create(company, score);
                }
            }
        }
    }
}
