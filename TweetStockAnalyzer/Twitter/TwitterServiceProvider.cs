using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TweetStockAnalyzer.Infrastructure.Dependency;
using Microsoft.Practices.Unity;

namespace TweetStockAnalyzer.Twitter
{
    public class TwitterServiceProvider
    {
        public ITwitterService GetAuthenticatedService()
        {
            return DependencyContainer.Instance.Resolve<ITwitterService>();
        }
    }
}
