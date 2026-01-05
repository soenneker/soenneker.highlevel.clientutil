using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.HighLevel.Client.Abstract;
using Soenneker.HighLevel.ClientUtil.Abstract;
using Soenneker.HighLevel.OpenApiClient;
using Soenneker.Kiota.BearerAuthenticationProvider;
using Soenneker.Utils.SingletonDictionary;

namespace Soenneker.HighLevel.ClientUtil;

///<inheritdoc cref="IHighLevelClientUtil"/>
public sealed class HighLevelClientUtil : IHighLevelClientUtil
{
    /// <summary>
    /// Cache of Kiota clients keyed by API key (each with unique bearer but shared HttpClient)
    /// </summary>
    private readonly SingletonDictionary<HighLevelOpenApiClient> _clients;
    private readonly IHighLevelHttpClient _httpClientUtil;

    public HighLevelClientUtil(IHighLevelHttpClient httpClientUtil)
    {
        _httpClientUtil = httpClientUtil;
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

    public ValueTask<HighLevelOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default)
    {
        return _clients.Get(apiKey, cancellationToken);
    }

    public void Dispose()
    {
        _clients.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _clients.DisposeAsync();
    }
}