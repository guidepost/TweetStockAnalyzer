using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TweetStockAnalyzer.Model
{
    public partial class Company
    {
        [ForeignKey("Stocks")]
        public Stock Stock { get { return Stocks.FirstOrDefault(); }  }

        [ForeignKey("CompanyProductRelations.Product")]
        public IEnumerable<Product> Products { get { return CompanyProductRelations.Select(p => p.Product); } }
    }
}
