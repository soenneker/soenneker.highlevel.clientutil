using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.HighLevel.Client.Registrars;
using Soenneker.HighLevel.ClientUtil.Abstract;

namespace Soenneker.HighLevel.ClientUtil.Registrars;

/// <summary>
/// Registers the cached HighLevel generated-client provider.
/// </summary>
public static class HighLevelClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="HighLevelClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddHighLevelClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddHighLevelHttpClientAsSingleton()
            .TryAddSingleton<IHighLevelClientUtil, HighLevelClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="HighLevelClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddHighLevelClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddHighLevelHttpClientAsSingleton()
            .TryAddScoped<IHighLevelClientUtil, HighLevelClientUtil>();

        return services;
    }
}
