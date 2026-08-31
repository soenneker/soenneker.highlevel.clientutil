using Soenneker.HighLevel.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.HighLevel.ClientUtil.Abstract;

/// <summary>
/// Provides cached HighLevel generated clients for one or more API keys.
/// </summary>
public interface IHighLevelClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the client configured with <c>HighLevel:ApiKey</c>.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the configured client.</returns>
    ValueTask<HighLevelOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a client authenticated with the supplied API key.
    /// </summary>
    /// <param name="apiKey">The HighLevel API key.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the configured client.</returns>
    ValueTask<HighLevelOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);
}
