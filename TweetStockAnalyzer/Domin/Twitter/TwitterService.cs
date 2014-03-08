using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.Infrastructure.Dependency;

namespace TweetStockAnalyzer.Domain.Twitter
{
    [AutoRegist(typeof(ITwitterService))]
    public class TwitterService : ITwitterService
    {
        private TweetSharp.TwitterService _service;
                 
        public TwitterService()
        {
            string comsumerKey = ConfigurationManager.AppSettings["TwitterComsumerKey"];
            string comsumerSecret = ConfigurationManager.AppSettings["TwitterComsumerSecret"];
            string accessToken = ConfigurationManager.AppSettings["TwitterAccessToken"];
            string accessTokenSecret = ConfigurationManager.AppSettings["TwitterAccessTokenSecret"];

            _service = new TweetSharp.TwitterService(comsumerKey, comsumerSecret);
            _service.AuthenticateWith(accessToken, accessTokenSecret);
        }


        public Task<TwitterSearchResult> SearchAsync(SearchOptions option)
        {
            var taskSource = new TaskCompletionSource<TwitterSearchResult>();
         
            _service.Search(option, new Action<TwitterSearchResult, TwitterResponse>((result, reponse) =>
            {
                taskSource.SetResult(result);
            }));
            return taskSource.Task;
        }
    }
}
