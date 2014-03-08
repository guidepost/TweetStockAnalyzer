using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.Domin.Stock
{
    [AutoRegist(typeof(IStockService))]
    public class YahooFinanceStockService : IStockService
    {
        public IEnumerable<StockPrice> Load(Model.Stock stock)
        {
            throw new NotImplementedException();
        }



        public static IEnumerable<StockPrice> ExtractStockPriceFromHtml(Model.Stock stock, HtmlNode tableBodyNode)
        {
            return tableBodyNode.ChildNodes
                                .Skip(1)
                                .Where(p => p.ChildNodes.Count >= 7)
                                .Select(tr => new StockPrice
                                {
                                    StockId = stock.StockId,
                                    Date = DateTime.Parse(tr.ChildNodes[0].InnerText),
                                    Dealings = ConvertDealingStr(tr.ChildNodes[5].InnerText.Replace(",", "")),
                                    ClosingPrice = ConvertPriceStr(tr.ChildNodes[6].InnerText)
                                });
        }

        public static HtmlNode GetTargetTableNode(string stockCode, DateTime startDate, DateTime endDate, int page)
        {
            string url = GetRequestUrl(stockCode, startDate, endDate, page);
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string html = wc.DownloadString(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var tableBodyNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id=\"main\"]/div[5]/table");
            if (tableBodyNode == null || tableBodyNode.ChildNodes.Count < 2)
            {
                return null;
            }

            return tableBodyNode;
        }

        private static double ConvertPriceStr(string priceStr)
        {
            return double.Parse(priceStr.Replace(",", ""));
        }

        private static int ConvertDealingStr(string dealingStr)
        {
            return int.Parse(dealingStr.Replace(",", ""));
        }

        private static readonly string BaseUrl = "http://info.finance.yahoo.co.jp/history/?code={0}&sy={1}&sm={2}&sd={3}&ey={4}&em={5}&ed={6}&tm=d&p={7}";
        private static string GetRequestUrl(string stockCode, DateTime startDate, DateTime endDate, int page)
        {
            return string.Format(BaseUrl, stockCode, startDate.Year, startDate.Month, startDate.Day, endDate.Year, endDate.Month, endDate.Day, page);
        }
    }
}
