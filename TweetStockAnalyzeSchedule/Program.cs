using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Infrastructure.Dependency;

namespace TweetStockAnalyzeSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyContainer.Instance;
            container.AddExtension(new AutoRegisterExtension(typeof(AutoRegistAttribute).Assembly));
            if (args == null)
            {
                var crawler = new TweetCrawler();
                crawler.Start();
            }
            if(args[0] == "stock")
            {
                var crowler = new StockCrawler();
                crowler.Start();
            }
        }
    }
}
