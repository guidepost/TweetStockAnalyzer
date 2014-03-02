using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace TweetStockAnalyzer.Infrastructure.Dependency
{
    public static class DependencyContainer
    {
        private static IUnityContainer _container = new UnityContainer();
        public static IUnityContainer Instance
        {
            get { return _container;}
            set { _container = value; }
        }
    }
}
