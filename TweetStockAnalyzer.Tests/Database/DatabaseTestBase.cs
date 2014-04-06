using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.Model;

namespace TweetStockAnalyzer.Tests.Database
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
