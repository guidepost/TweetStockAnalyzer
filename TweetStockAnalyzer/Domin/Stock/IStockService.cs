using System;
using System.Collections.Generic;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.Domin.Stock
{
    public interface IStockService
    {
        IEnumerable<StockPrice> Load(Model.Stock stock, DateTime startDate, DateTime endDate);
    }
}
