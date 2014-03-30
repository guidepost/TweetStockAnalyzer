using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.Domain.Score
{
    // 先週の平均と比べてのツイート数の変化率をスコアとする
    [AutoRegist(typeof(IScoreCalculator))]
    public class IncreaseTweetRateScoreCalculator: IScoreCalculator
    {
        public int GetScore(Company company)
        {
            if (company.Products.Any() == false)
            {
                return 0;
            }
            var productScores = company.Products.Select(p => GetProductSocre(p));
            return productScores.Max();
        }

        private int GetProductSocre(Product product)
        {
            if(product.SearchWords.Any() == false)
            {
                return 0;
            }

            return product.SearchWords.Max(
                searchWord => GetRate(searchWord));
        }

        private int GetRate(SearchWord searchWord)
        {
            var today = GetLastSearchResult(searchWord);
            var lastAverage = GetAverageTweetCount(searchWord);
            return (int)(today / lastAverage * 100);
        }

        private double GetLastSearchResult(SearchWord searchWord)
        {
            return searchWord.SearchResults
                .Where(p => p.RegisterDate.Date == searchWord.UpdateDate.Date)
                .Sum(p => p.TweetCount); 
        }

        private double GetAverageTweetCount(SearchWord searchWord)
        {
            var lastSunday = GetLastSunday(searchWord.UpdateDate);
            return searchWord.SearchResults.Where(p => p.RegisterDate >= lastSunday.AddDays(-7) && p.RegisterDate < lastSunday)
                .GroupBy(p => p.RegisterDate.Date).Select(p => GetSumOrDefault(p)).Average();
        }
        private double GetSumOrDefault(IEnumerable<SearchResult> results)
        {
            if(results.Any() == false)
            {
                return 0;
            }
            return results.Sum(p => p.TweetCount);
        }

        private DateTime GetLastSunday(DateTime dateTime)
        {
            return dateTime.AddDays(DayOfWeek.Sunday - dateTime.DayOfWeek);
        }

    }
}
