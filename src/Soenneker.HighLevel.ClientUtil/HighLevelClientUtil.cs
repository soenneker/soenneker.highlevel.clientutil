using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Dictionaries.Singletons;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.HighLevel.Client.Abstract;
using Soenneker.HighLevel.ClientUtil.Abstract;
using Soenneker.HighLevel.OpenApiClient;

namespace Soenneker.HighLevel.ClientUtil;

public sealed class HighLevelClientUtil : IHighLevelClientUtil
{
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
        HttpClient httpClient = await _httpClientUtil.Get(apiKey, token)
                                                     .NoSync();

        var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

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

    public void Dispose()
    {
        _clients.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _clients.DisposeAsync();
    }
}
