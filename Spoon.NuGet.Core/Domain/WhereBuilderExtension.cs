namespace Spoon.NuGet.Core.Domain;

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

/// <summary>
/// Where Builder.
/// </summary>
public static class WhereBuilderExtension
{
    /// <summary>
    /// The contains method.
    /// </summary>
    private static readonly MethodInfo? ContainsMethod = typeof(string).GetMethod("Contains");

    /// <summary>
    /// The starts with method.
    /// </summary>
    private static readonly MethodInfo? StartsWithMethod =
        typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

    /// <summary>
    /// The ends with method.
    /// </summary>
    private static readonly MethodInfo? EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

    /// <summary>
    /// Something.
    /// </summary>
    /// <typeparam name="T">T Something.</typeparam>
    /// <param name="filters">filters.</param>
    /// <param name="expression">expression.</param>
    /// <returns>Expression.</returns>
    public static Expression<Func<T, bool>>? GetExpression<T>(IList<Filter> filters, Expression? expression = null)
    {
        if (filters.Count == 0 && expression == null) return null;

        var parameter = Expression.Parameter(typeof(T), "t");

        if (filters.Count == 0 && expression != null) return Expression.Lambda<Func<T, bool>>(expression, parameter);

        if (filters.Count == 1 && expression == null)
            expression = GetExpression<T>(parameter, filters[0]);
        else if (filters.Count == 2 && expression == null)
            expression = GetExpression<T>(parameter, filters[0], filters[1]);

        if (filters.Count == 1 && expression != null)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            expression = Expression.AndAlso(expression, GetExpression<T>(parameter, filters[0]));
#pragma warning restore CS8604 // Possible null reference argument.
        }
        else if (filters.Count == 2 && expression != null)
        {
            expression = Expression.AndAlso(expression, GetExpression<T>(parameter, filters[0], filters[1]));
        }
        else
        {
            while (filters.Count > 0)
            {
                var f1 = filters[0];
                var f2 = filters[1];

                if (expression == null)
                    expression = GetExpression<T>(parameter, filters[0], filters[1]);
                else
                    expression = Expression.AndAlso(expression, GetExpression<T>(parameter, filters[0], filters[1]));

                filters.Remove(f1);
                filters.Remove(f2);

                if (filters.Count == 1)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    expression = Expression.AndAlso(expression, GetExpression<T>(parameter, filters[0]));
#pragma warning restore CS8604 // Possible null reference argument.
                    filters.RemoveAt(0);
                }
            }
        }

#pragma warning disable CS8604 // Possible null reference argument.
        return Expression.Lambda<Func<T, bool>>(expression, parameter);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Gets the expression.
    /// </summary>
    /// <typeparam name="T">T is result.</typeparam>
    /// <param name="param">The parameter.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>System.Nullable&lt;Expression&gt;.</returns>
    private static Expression? GetExpression<T>(ParameterExpression param, Filter filter)
    {
        var member = Expression.Property(param, filter.PropertyName);
        var constant = Expression.Constant(filter.Value);

        switch (filter.Operation)
        {
            case Operation.Equals:
                return Expression.Equal(member, constant);
            case Operation.GreaterThan:
                return Expression.GreaterThan(member, constant);
            case Operation.GreaterThanOrEqual:
                return Expression.GreaterThanOrEqual(member, constant);
            case Operation.LessThan:
                return Expression.LessThan(member, constant);
            case Operation.LessThanOrEqual:
                return Expression.LessThanOrEqual(member, constant);
            case Operation.Contains:
#pragma warning disable CS8604 // Possible null reference argument.
                return Expression.Call(member, ContainsMethod, constant);
#pragma warning restore CS8604 // Possible null reference argument.
            case Operation.StartsWith:
#pragma warning disable CS8604 // Possible null reference argument.
                return Expression.Call(member, StartsWithMethod, constant);
#pragma warning restore CS8604 // Possible null reference argument.
            case Operation.EndsWith:
#pragma warning disable CS8604 // Possible null reference argument.
                return Expression.Call(member, EndsWithMethod, constant);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        return null;
    }

    /// <summary>
    /// The get expression.
    /// </summary>
    /// <typeparam name="T">Expression.</typeparam>
    /// <param name="param">The param.</param>
    /// <param name="filter1">The filter 1.</param>
    /// <param name="filter2">The filter 2.</param>
    /// <returns>The <see cref="BinaryExpression" />.</returns>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    private static BinaryExpression GetExpression<T>(ParameterExpression param, Filter filter1, Filter filter2)
    {
        var bin1 = GetExpression<T>(param, filter1);
        var bin2 = GetExpression<T>(param, filter2);

#pragma warning disable CS8604 // Possible null reference argument.
        return Expression.AndAlso(bin1, bin2);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Ands the also.
    /// </summary>
    /// <typeparam name="T">T is result.</typeparam>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>Expression&lt;Func&lt;T, System.Boolean&gt;&gt;.</returns>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    private static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var body = Expression.AndAlso(Expression.Invoke(left, param), Expression.Invoke(right, param));
        var lambda = Expression.Lambda<Func<T, bool>>(body, param);

        return lambda;
    }
}