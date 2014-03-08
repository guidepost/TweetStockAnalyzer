using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzerWeb.Models
{
    public class AnalyzedCompany
    {
        public Company Company { get; set; }
        public Dictionary<DateTime,double> Scores { get; set; }
    }
}