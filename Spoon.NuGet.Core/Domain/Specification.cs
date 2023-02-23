﻿namespace Spoon.NuGet.Core.Domain;

using System.Linq.Expressions;

/// <summary>
/// Class Specification.
/// </summary>
/// <typeparam name="TEntity">The type of the t entity.</typeparam>
public abstract class Specification<TEntity>
    where TEntity : Entity
{

    /// <summary>
    /// Gets or sets a value indicating whether this instance is split query.
    /// </summary>
    /// <value><c>true</c> if this instance is split query; otherwise, <c>false</c>.</value>
    public bool IsSplitQuery { get; protected set; }

    /// <summary>
    /// Gets the filters.
    /// </summary>
    /// <value>The filters.</value>
    public List<Filter>? Filters { get; private set; }

    /// <summary>
    /// Gets the include expressions.
    /// </summary>
    /// <value>The include expressions.</value>
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new ();

    /// <summary>
    /// Gets the order by expression.
    /// </summary>
    /// <value>The order by expression.</value>
    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

    /// <summary>
    /// Gets the order by descending expression.
    /// </summary>
    /// <value>The order by descending expression.</value>
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    /// <summary>
    /// Gets the skip.
    /// </summary>
    /// <value>The skip.</value>
    public int Skip { get; private set; }

    /// <summary>
    /// Gets the take.
    /// </summary>
    /// <value>The take.</value>
    public int Take { get; private set; }

    /// <summary>
    /// Adds the include.
    /// </summary>
    /// <param name="includeExpression">The include expression.</param>
    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
        this.IncludeExpressions.Add(includeExpression);

    /// <summary>
    /// Adds the order by.
    /// </summary>
    /// <param name="orderByExpression">The order by expression.</param>
    protected void AddOrderBy(
        Expression<Func<TEntity, object>> orderByExpression) =>
        this.OrderByExpression = orderByExpression;

    /// <summary>
    /// Adds the order by descending.
    /// </summary>
    /// <param name="orderByDescendingExpression">The order by descending expression.</param>
    protected void AddOrderByDescending(
        Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        this.OrderByDescendingExpression = orderByDescendingExpression;

    /// <summary>
    /// Adds the filters.
    /// </summary>
    /// <param name="filters">The filters.</param>
    protected void AddFilters(
        List<Filter> filters) =>
        this.Filters = filters;

    /// <summary>
    /// Adds the skip.
    /// </summary>
    /// <param name="skip">The skip.</param>
    protected void AddSkip(int skip) =>
        this.Skip = skip;

    /// <summary>
    /// Adds the take.
    /// </summary>
    /// <param name="take">The take.</param>
    protected void AddTake(int take) =>
        this.Take = take;
}