using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzerWeb.ViewModel
{
    public class CompanyIndexViewModel
    {
        public IEnumerable<Company> Companies { get; set; }
    }
}