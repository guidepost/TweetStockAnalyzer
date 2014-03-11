using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TweetStockAnalyzerWeb.ViewModel.Product
{
    public class ProductIndexViewModel
    {
        public TweetStockAnalyzer.Model.Product[] Products { get; set; }

        public string SuccessMessage { get; set; }
    }
}