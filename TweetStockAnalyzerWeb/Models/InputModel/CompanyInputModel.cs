using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzerWeb.Models.InputModel
{
    public class CompanyInputModel
    {
        public int? CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }
        public int? ParentCompanyId { get; set; }
        public string StockCode { get; set; }
        public int BussinessCategoryId { get; set; }

        public Product[] Products { get; set; }
    }
}