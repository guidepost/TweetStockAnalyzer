using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TweetStockAnalyzer.Twitter
{
    public class TwitterAuthenticator
    {
        public bool Authenticate()
        {
            string comsumerKey = ConfigurationManager.AppSettings["TwitterComsumerKey"];
            string comsumerSecret = ConfigurationManager.AppSettings["TwitterComsumerSecret"];
            string userName = ConfigurationManager.AppSettings["TwitterUserName"];
            string password = ConfigurationManager.AppSettings["TwitterPassword"];

            TwitterService service = new TwitterService(comsumerKey, comsumerSecret);
            OAuthAccessToken access = service.GetAccessTokenWithXAuth(userName, password);

            service.AuthenticateWith(access.Token, access.TokenSecret);
            TwitterUser user = service.VerifyCredentials(null);
            return string.IsNullOrEmpty(user.Name);
        }
    }
}
