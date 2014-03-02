using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetStockAnalyzer.Infrastructure.Dependency;

namespace TweetStockAnalyzer.Tests.Infrastructure.Dependency
{
    [TestClass]
    public class AutoRegisterExtensionTest
    {
        [TestMethod]
        public void Resolve()
        {
            var container = new UnityContainer();
            container.AddExtension(new AutoRegisterExtension(typeof(Sample).Assembly));
            var sample = container.Resolve<ISample>();
            sample.Get().Is("test");
            container = new UnityContainer();
            try
            {
                container.Resolve<ISample>();
                Assert.Fail();
            }
            catch
            {
            }
        }
    }
    public interface ISample
    {
        string Get();
    }
    [AutoRegist(typeof(ISample))]
    public class Sample : ISample
    {
        public Sample()
        {
            
        }
        public string Get()
        {
            return "test";
        }
    }
}
