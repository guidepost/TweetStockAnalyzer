using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzerWeb.ViewModel;

namespace TweetStockAnalyzerWeb.WorkerService
{
    public interface ICompanyWorkerService:IDisposable
    {
        CompanyIndexViewModel GetIndexViewModel();
    }

    [AutoRegist(typeof(ICompanyWorkerService))]
    public class CompanyWorkerService : ICompanyWorkerService
    {
        public CompanyIndexViewModel GetIndexViewModel()
        {
            var container = DependencyContainer.Instance;
            using (var repository = container.Resolve<ICompanyRepository>())
            {
                //return new CompanyIndexViewModel
                //{
                //    Companies = repository.ReadAll()
                //};

                return new CompanyIndexViewModel
                {
                    Companies = new Company[] {
                        new Company {
                            CompanyId = 1,
                            CompanyName = "companyName",
                            RegisterDate = DateTime.Now,
                            UpdateDate = DateTime.Now} }
                };
            }
        }

        public void Dispose()
        {
         
        }
    }
}