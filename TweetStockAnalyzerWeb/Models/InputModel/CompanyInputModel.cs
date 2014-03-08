using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TweetStockAnalyzerWeb.Models.InputModel
{
    public class CompanyInputModel
    {
        [Required]
        public string CompanyName { get; set; }
        public string ParentCompanyCode { get; set; }
    }
}