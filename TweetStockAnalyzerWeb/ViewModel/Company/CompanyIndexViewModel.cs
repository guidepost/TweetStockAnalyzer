using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.Model;
using TweetStockAnalyzerWeb.Models;

namespace TweetStockAnalyzerWeb.ViewModel.Company
{
    public class CompanyIndexViewModel
    {
        public CompanyWithScore[] CompanyWithScores { get; set; }

        public string SuccessMessage { get; set; }
    }
}