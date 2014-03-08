using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.Infrastructure.Dependency;

namespace TweetStockAnalyzer.Tests
{
    [TestClass]
    public class TestHost
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            var container = DependencyContainer.Instance;
            container.AddExtension(new AutoRegisterExtension(typeof(AutoRegistAttribute).Assembly));
        }
        [AssemblyCleanup]
        public static void Cleanup()
        {
        }
    }
}
