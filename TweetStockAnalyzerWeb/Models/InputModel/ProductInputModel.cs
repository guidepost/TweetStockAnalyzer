using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TweetStockAnalyzerWeb.Models.InputModel
{
    public class ProductInputModel
    {
        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public DateTime ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public string[] SearchWords { get; set; }
    }
}