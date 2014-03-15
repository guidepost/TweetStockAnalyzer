using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetStockAnalyzer.DataBase;
using TweetStockAnalyzer.DataBase.Repository;
using TweetStockAnalyzer.Infrastructure.Dependency;
using Microsoft.Practices.Unity;

namespace TweetStockAnalyzerWeb.Models
{
    public static class DropDownListItems
    {
        private static IUnityContainer _container = DependencyContainer.Instance;

        static DropDownListItems()
        {
            using (var repository = _container.Resolve<IBussinessCategoryRepository>())
            {
                var categories = repository.ReadAll().ToArray();
                BussinessCategories = categories.Select(c => new SelectListItem
                {
                    Value = c.BussinessCategoryId.ToString(),
                    Text = string.Format("{0}:{1}", c.BussinessCategoryCode, c.BussinessCategoryName)
                })
                    .ToArray();
            }
        }

        public static SelectListItem[] BussinessCategories { get; set; }

        public static SelectListItem[] Companies
        {
            get
            {
                using (var repository = _container.Resolve<ICompanyRepository>())
                {
                    var companies = repository.ReadAll().ToArray();

                    var selectItems = companies.Select(c => new SelectListItem
                                                       {
                                                           Value = c.CompanyId.ToString(),
                                                           Text = c.CompanyName
                                                       })
                                                       .ToList();
                    selectItems.Insert(0, new SelectListItem { Value = "", Text = "" });

                    return selectItems.ToArray();
                }
            }
        }

        public static SelectListItem[] Products
        {
            get
            {
                using (var repository = _container.Resolve<IProductRepository>())
                {
                    var products = repository.ReadAll().ToArray();

                    var selectItems = products.Select(p => new SelectListItem
                                                      {
                                                          Value = p.ProductId.ToString(),
                                                          Text = p.ProductName
                                                      })
                                                      .ToList();

                    return selectItems.ToArray();
                }
            }
        }
    }
}