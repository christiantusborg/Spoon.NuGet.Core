namespace Spoon.NuGet.Core
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Class ServiceCollectionExtensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds IMockbleDateTime => MockbleDateTimeDefault.
        ///     Adds IMockbleGuidGenerator => MockbleGuidGenerator.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IMockbleDateTime, MockbleDateTimeDefault>();
            services.AddTransient<IMockbleGuidGenerator, MockbleGuidGenerator>();
            return services;
        }
    }
}