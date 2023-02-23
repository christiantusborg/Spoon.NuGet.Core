namespace Spoon.NuGet.Core
{
    /// <summary>
    /// Class MockbleDateTimeDefault.
    /// Implements the <see cref="IMockbleDateTime" />
    /// </summary>
    /// <seealso cref="IMockbleDateTime" />
    public class MockbleDateTimeDefault : IMockbleDateTime
    {
        /// <summary>
        /// Gets the UTC now.
        /// </summary>
        /// <value>The UTC now.</value>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}