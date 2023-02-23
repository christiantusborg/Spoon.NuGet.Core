namespace Spoon.NuGet.Core
{
    /// <summary>
    /// Interface IMockbleDateTime
    /// </summary>
    public interface IMockbleDateTime
    {
        /// <summary>
        /// Gets the UTC now.
        /// </summary>
        /// <value>The UTC now.</value>
        DateTime UtcNow { get; }
    }
}