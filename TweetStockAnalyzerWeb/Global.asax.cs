﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TweetStockAnalyzer.Infrastructure.Dependency;
using TweetStockAnalyzerWeb.WorkerService;

namespace TweetStockAnalyzerWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = DependencyContainer.Instance;

            container.AddExtension(new AutoRegisterExtension(typeof(AutoRegistAttribute).Assembly));
            container.AddExtension(new AutoRegisterExtension(Assembly.GetExecutingAssembly()));

            IDependencyResolver resolver = new UnityDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);

            
        }
    }
}
