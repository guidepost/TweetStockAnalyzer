using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TweetStockAnalyzer.Twitter
{
    public class TwitterServiceProvider
    {
        public TwitterService GetAuthenticatedService()
        {
            string comsumerKey = ConfigurationManager.AppSettings["TwitterComsumerKey"];
            string comsumerSecret = ConfigurationManager.AppSettings["TwitterComsumerSecret"];
            string accessToken = ConfigurationManager.AppSettings["TwitterAccessToken"];
            string accessTokenSecret = ConfigurationManager.AppSettings["TwitterAccessTokenSecret"];

            var service = new TwitterService(comsumerKey, comsumerSecret);
            service.AuthenticateWith(accessToken, accessTokenSecret);

            return service;
        }
    }
}
