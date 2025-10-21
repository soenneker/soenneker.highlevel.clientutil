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
    ValueTask<HighLevelOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);
}