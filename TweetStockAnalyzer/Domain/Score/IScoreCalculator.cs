using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.Domain.Score
{
    public interface IScoreCalculator
    {
        int GetScore(Company company);
    }
}
