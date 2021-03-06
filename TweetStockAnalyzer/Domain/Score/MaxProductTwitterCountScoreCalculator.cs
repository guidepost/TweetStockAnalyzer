﻿using System.Linq;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.Domain.Score
{
    // プロダクトのうち最もツイート数が多いものの1/100をスコアとする
    public class MaxProductTwitterCountScoreCalculator : IScoreCalculator
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

            // Tweet数をそのまま使うと数字が大きくなりそうなので、桁を減らす
            return product.SearchWords.Max(
                searchWord => GetLastSearchResult(searchWord) / 100);
        }

        private int GetLastSearchResult(SearchWord searchWord)
        {
            return (int)searchWord.SearchResults
                .Where(p => p.UpdateDate.Date == searchWord.UpdateDate.Date)
                .Sum(p => p.TweetCount); 
        }

    }
}
