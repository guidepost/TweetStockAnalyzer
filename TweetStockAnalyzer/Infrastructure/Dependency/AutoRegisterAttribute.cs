using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace TweetStockAnalyzer.Infrastructure.Dependency
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AutoRegistAttribute : Attribute
    {
        private static readonly InjectionMember[] InjectionMembersInitialValue = new InjectionMember[0];

        public AutoRegistAttribute()
        {
            this.InjectionMembers = InjectionMembersInitialValue;
        }
        public AutoRegistAttribute(Type type) : this()
        {
            RegisterType = type;
        }
        public string Name { get; set; }
        public Type RegisterType { get; set; }
        public InjectionMember[] InjectionMembers { get; set; }
    }
}
