using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetStockAnalyzer.Model
{
    public partial class Company
    {
        public Stock Stock { get { return Stocks.FirstOrDefault(); }  }
    }
}
