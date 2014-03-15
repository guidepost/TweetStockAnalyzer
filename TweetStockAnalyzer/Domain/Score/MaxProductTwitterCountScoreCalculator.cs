using System.Linq;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.Domain.Score
{
    [AutoRegist(typeof(IScoreCalculator))]
    public class MaxProductTwitterCountScoreCalculator : IScoreCalculator
    {
        public int GetScore(Company company)
        {
            var productScores = company.Products.Select(p => GetProductSocre(p));
            return productScores.Max();
        }

        private int GetProductSocre(Product product)
        {
            return product.SearchWords.Max(
                searchWord => GetLastSearchResult(searchWord));
        }

        private int GetLastSearchResult(SearchWord searchWord)
        {
            // Tweet数をそのまま使うと数字が大きくなりそうなので、桁を減らす
            return (int)searchWord.SearchResults
                .Where(p => p.UpdateDate.Date == searchWord.UpdateDate.Date)
                .Sum(p => p.TweetCount / 100); 
        }

    }
}
