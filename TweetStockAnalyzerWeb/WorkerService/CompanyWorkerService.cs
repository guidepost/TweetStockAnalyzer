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
            //using (var repository = new CompanyRepository())
            //{
            //    return new CompanyIndexViewModel
            //    {
            //        Companies = repository.ReadAll()
            //    };
            //}
            return null;
        }
    }
}