using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzerWeb.Tests
{
    public abstract class DatabaseTestBase
    {
        [TestInitialize]
        public virtual void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<TweetStockAnalyzerEntities>());
            var context = new TweetStockAnalyzerEntities();
            context.Database.Initialize(true);
        }
    }
}
