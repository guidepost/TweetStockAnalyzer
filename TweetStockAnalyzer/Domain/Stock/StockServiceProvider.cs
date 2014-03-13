using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Infrastructure.Dependency;
using Microsoft.Practices.Unity;

namespace TweetStockAnalyzer.Domain.Stock
{
    public class StockServiceProvider
    {
        public IStockService GetService()
        {
            return DependencyContainer.Instance.Resolve<IStockService>();
        }
    }
}
