using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzerWeb.ViewModel.Company
{
    public class CompanyIndexViewModel
    {
        public IEnumerable<TweetStockAnalyzer.Model.Company> Companies { get; set; }
    }
}