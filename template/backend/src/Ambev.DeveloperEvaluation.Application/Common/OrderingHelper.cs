using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    public static class OrderingHelper
    {
        public static IQueryable<T> ApplyOrdering<T>(IQueryable<T> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                orderBy = "Id asc";

            var orderByParams = orderBy.Split(',')
                                       .Select(param => param.Trim())
                                       .ToArray();

            IOrderedQueryable<T>? orderedQuery = null;

            foreach (var orderByPart in orderByParams)
            {
                var parts = orderByPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var propertyName = parts[0];
                var descending = parts.Length > 1 && parts[1].ToLower() == "desc"; 

                var parameter = Expression.Parameter(typeof(T), "p");
                var propertyAccess = Expression.Property(parameter, propertyName);
                var lambda = Expression.Lambda(propertyAccess, parameter);

                if (orderedQuery == null)
                {
                    var methodName = descending ? "OrderByDescending" : "OrderBy";
                    orderedQuery = ApplyOrderingMethod(query, methodName, lambda);
                }
                else
                {
                    var methodName = descending ? "ThenByDescending" : "ThenBy";
                    orderedQuery = ApplyOrderingMethod(orderedQuery, methodName, lambda);
                }
            }

            return orderedQuery ?? query;
        }

        private static IOrderedQueryable<T> ApplyOrderingMethod<T>(IQueryable<T> source, string methodName, LambdaExpression keySelector)
        {
            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), keySelector.ReturnType);

            return (IOrderedQueryable<T>)method.Invoke(null, new object[] { source, keySelector })!;
        }
    }
}
