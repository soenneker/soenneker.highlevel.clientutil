using Soenneker.HighLevel.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.HighLevel.ClientUtil.Abstract;

/// <summary>
/// A .NET thread-safe singleton HttpClient for 
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
    /// Gets the value.
    /// </summary>
    /// <param name="apiKey">The API key.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<HighLevelOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);
}
