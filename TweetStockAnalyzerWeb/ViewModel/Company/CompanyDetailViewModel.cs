using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzerWeb.ViewModel.Company
{
    public class CompanyDetailViewModel
    {
        public TweetStockAnalyzer.Model.Company Company { get; set; }

        public string ParentCompanyName { get; set; }

        public CompanyScore[] CompanyScores { get; set; }
    }
}