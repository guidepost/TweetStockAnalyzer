using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetStockAnalyzer.Model
{
    public partial class Stock
    {
        [ForeignKey("AggregateHistories")]
        public AggregateHistory AggregateHistory { get { return AggregateHistories.FirstOrDefault(); } }
    }
}
