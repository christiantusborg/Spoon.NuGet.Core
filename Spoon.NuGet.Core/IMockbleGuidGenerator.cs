namespace Spoon.NuGet.Core;

/// <summary>
/// Interface IMockbleGuidGenerator
/// </summary>
public interface IMockbleGuidGenerator
{
    /// <summary>
    /// Creates new guid.
    /// </summary>
    /// <returns>Guid.</returns>
    Guid NewGuid();
}