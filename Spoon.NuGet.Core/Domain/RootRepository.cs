namespace Spoon.NuGet.Core.Domain;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Class RootRepository.
/// Implements the <see cref="IRootRepository{TEntity}" />.
/// </summary>
/// <typeparam name="TEntity">The type of the t entity.</typeparam>
/// <seealso cref="IRootRepository{TEntity}" />
public class RootRepository<TEntity> : IRootRepository<TEntity>
    where TEntity : Entity
{
    /// <summary>
    /// The database context.
    /// </summary>
    private readonly DbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RootRepository{TEntity}" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public RootRepository(DbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    /// <summary>
    /// Gets the by identifier.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;System.Nullable&lt;TEntity&gt;&gt;.</returns>
    public async Task<TEntity?> Get(Specification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        
        return await this.ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Searches the specified specification.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;List&lt;TEntity&gt;&gt;.</returns>
    public Task<List<TEntity>> Search(Specification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return this.ApplySpecification(specification).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Adds the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Add(TEntity entity)
    {
        this._dbContext.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// Removes the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public void Remove(TEntity entity)
    {
        this._dbContext.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// Applies the specification.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <returns>IQueryable&lt;TEntity&gt;.</returns>
    protected IQueryable<TEntity> ApplySpecification(
        Specification<TEntity> specification)
    {
        return SpecificationEvaluator.GetQuery(
            this._dbContext.Set<TEntity>(),
            specification);
    }
}
