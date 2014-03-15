using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TweetStockAnalyzer.Domain.Score;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.DataBase.Repository;

namespace TweetStockAnalyzeSchedule
{
    public class CompanyScoreUpdater
    {
        public void Update()
        {
            using (var companyRepository = DependencyContainer.Instance.Resolve<ICompanyRepository>())
            using (var companyScoreRepository = DependencyContainer.Instance.Resolve<ICompanyScoreRepository>())
            {
                foreach (var company in companyRepository.ReadAll())
                {
                    var calculator = DependencyContainer.Instance.Resolve<IScoreCalculator>();
                    var score = calculator.GetScore(company);
                    companyScoreRepository.Create(company, score);
                }
            }
        }
    }
}
