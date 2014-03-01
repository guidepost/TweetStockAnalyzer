using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace TweetStockAnalyzer.Infrastructure.Dependency
{
    public class AutoRegisterExtension : UnityContainerExtension
    {
        private Assembly[] loadTargets;

        public AutoRegisterExtension(params Assembly[] loadTargets)
        {
            this.loadTargets = loadTargets;
        }

        protected override void Initialize()
        {
            var targetTypes = loadTargets.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttributes(typeof(AutoRegistAttribute), false).Any())
                .Select(type => new
                {
                    Type = type,
                    Attribute = type.GetCustomAttributes(typeof(AutoRegistAttribute), false).OfType<AutoRegistAttribute>().First()
                });

            foreach (var type in targetTypes)
            {
                var registerType = type.Attribute.RegisterType ?? type.Type;
                if (string.IsNullOrEmpty(type.Attribute.Name))
                {
                    this.Container.RegisterType(registerType, type.Type, type.Attribute.InjectionMembers);
                }
                else
                {
                    this.Container.RegisterType(registerType, type.Type, type.Attribute.Name, type.Attribute.InjectionMembers);
                }
            }
        }
    }
}
