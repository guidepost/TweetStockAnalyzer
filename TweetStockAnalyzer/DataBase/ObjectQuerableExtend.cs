using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TweetStockAnalyzer.DataBase
{
    public static class ObjectQueryExtension
    {
        public static ObjectQuery<T> Include<T>(this ObjectQuery<T> query, Expression<Func<T, object>> includes)
      where T : class
        {
            return query.Include(includes.Body.GetExpressionName());
        }

        public static DbQuery<T> Include<T>(this DbQuery<T> query, Expression<Func<T, object>> includes)
       where T : class
        {
            var expression = includes.Body.GetExpressionName();
            return query.Include(expression);
        }

        private static string GetActualName(MemberInfo memberInfo)
        {
            var attribute = memberInfo.GetCustomAttribute<ForeignKeyAttribute>();
            if(attribute != null)
            {
                return attribute.Name;
            }
            return memberInfo.Name;
        }
        private static string GetExpressionName(this Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                return null;
            }
            if (memberExpression.Expression.NodeType == ExpressionType.Parameter)
            {
                return GetActualName(memberExpression.Member);
            }
            if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
            {
                var childName = memberExpression.Expression.GetExpressionName();
                if (string.IsNullOrEmpty(childName))
                {
                    return GetActualName(memberExpression.Member);
                }
                return string.Format("{0}.{1}", childName, GetActualName(memberExpression.Member));
            }
            if (memberExpression.Expression.NodeType == ExpressionType.Constant)
            {
                return null;
            }
            MethodCallExpression methodCallExpression = (MethodCallExpression)memberExpression.Expression;
            if (methodCallExpression.Arguments.Count != 1)
            {
                throw new Exception("invalid method call in Include expression");
            }
            return string.Format("{0}.{1}", methodCallExpression.Arguments[0].GetExpressionName(), GetActualName(memberExpression.Member));
        }

    }


}
