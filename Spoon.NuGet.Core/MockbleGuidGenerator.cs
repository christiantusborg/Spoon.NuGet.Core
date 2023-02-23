namespace Spoon.NuGet.Core
{
    /// <summary>
    /// Class MockbleGuidGenerator.
    /// Implements the <see cref="IMockbleGuidGenerator" />
    /// </summary>
    /// <seealso cref="IMockbleGuidGenerator" />
    public class MockbleGuidGenerator : IMockbleGuidGenerator
    {
        /// <summary>
        /// Creates new guid.
        /// </summary>
        /// <returns>Guid.</returns>
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}