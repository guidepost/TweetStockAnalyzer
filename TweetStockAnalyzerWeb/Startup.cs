using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TweetStockAnalyzerWeb.Startup))]
namespace TweetStockAnalyzerWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
