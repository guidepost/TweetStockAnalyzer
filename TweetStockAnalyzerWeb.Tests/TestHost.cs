using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzerWeb.WorkerService;

namespace TweetStockAnalyzerWeb.Tests
{
    [TestClass]
    public class TestHost
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            var container = DependencyContainer.Instance;
            container.AddExtension(new AutoRegisterExtension(typeof(AutoRegistAttribute).Assembly));
            container.AddExtension(new AutoRegisterExtension(typeof(CompanyWorkerService).Assembly));
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
        }
    }
}
