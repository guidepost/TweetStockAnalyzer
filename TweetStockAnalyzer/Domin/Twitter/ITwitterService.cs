using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TweetStockAnalyzer.Domain.Twitter
{
    public interface ITwitterService
    {
        Task<TwitterSearchResult> SearchAsync(SearchOptions option);
    }
}
