using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzerWeb.ViewModel;

namespace TweetStockAnalyzerWeb.WorkerService
{
    public class CompanyWorkerService
    {
        public CompanyIndexViewModel GetIndexViewModel()
        {
            var container = new UnityContainer();
            using (var repository = container.Resolve<ICompanyRepository>())
            {
                return new CompanyIndexViewModel
                {
                    Companies = repository.ReadAll()
                };
            }
        }
    }
}