namespace Spoon.NuGet.Core.Domain;

/// <summary>
/// Class Entity.
/// </summary>
public abstract class Entity : IEquatable<Entity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity" /> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    protected Entity(Guid id) => this.Id = id;

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity" /> class.
    /// </summary>
    protected Entity()
    {
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public Guid Id { get; private init; }

    /// <summary>
    /// Implements the == operator.
    /// </summary>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator ==(Entity? first, Entity? second) =>
        first is not null && second is not null && first.Equals(second);

    /// <summary>
    /// Implements the != operator.
    /// </summary>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns>The result of the operator.</returns>
    public static bool operator !=(Entity? first, Entity? second) =>
        !(first == second);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != this.GetType())
        {
            return false;
        }

        return other.Id == this.Id;
    }

    /// <summary>
    /// Determines whether the specified <see cref="object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == this.Id;
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public override int GetHashCode() => this.Id.GetHashCode() * 41;
}