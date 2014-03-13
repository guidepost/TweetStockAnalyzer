﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzerWeb.Models
{
    public class CompanyWithScore
    {
        public TweetStockAnalyzer.Model.Company Company { get; set; }

        public CompanyScore[] CompanyScores { get; set; }
    }
}