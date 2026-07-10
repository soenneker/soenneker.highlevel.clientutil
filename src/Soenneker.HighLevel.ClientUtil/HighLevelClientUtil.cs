using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Dictionaries.Singletons;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.HighLevel.Client.Abstract;
using Soenneker.HighLevel.ClientUtil.Abstract;
using Soenneker.HighLevel.OpenApiClient;
using Soenneker.Kiota.BearerAuthenticationProvider;

namespace Soenneker.HighLevel.ClientUtil;

///<inheritdoc cref="IHighLevelClientUtil"/>
public sealed class HighLevelClientUtil : IHighLevelClientUtil
{
    /// <summary>
    /// Cache of Kiota clients keyed by API key (each with unique bearer but shared HttpClient)
    /// </summary>
    private readonly SingletonDictionary<HighLevelOpenApiClient> _clients;
    private readonly IHighLevelHttpClient _httpClientUtil;
    private readonly IConfiguration _configuration;

    public HighLevelClientUtil(IHighLevelHttpClient httpClientUtil, IConfiguration configuration)
    {
        _httpClientUtil = httpClientUtil;
        _configuration = configuration;
        _clients = new SingletonDictionary<HighLevelOpenApiClient>(CreateClient);
    }

    private async ValueTask<HighLevelOpenApiClient> CreateClient(string apiKey, CancellationToken token)
    {
        HttpClient httpClient = await _httpClientUtil.Get(token)
                                                     .NoSync();

        // Each adapter has its own fixed bearer provider for the given token
        var authProvider = new BearerAuthenticationProvider(apiKey);

        var adapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);

        return new HighLevelOpenApiClient(adapter);
    }

    public ValueTask<HighLevelOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        var apiKey = _configuration.GetValueStrict<string>("HighLevel:ApiKey");

        return Get(apiKey, cancellationToken);
    }

    public ValueTask<HighLevelOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default)
    {
        System.ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        return _clients.Get(apiKey, cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _clients.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _clients.DisposeAsync();
    }
}
