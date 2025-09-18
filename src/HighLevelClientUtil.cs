using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.HighLevel.Client.Abstract;
using Soenneker.HighLevel.ClientUtil.Abstract;
using Soenneker.HighLevel.OpenApiClient;
using Soenneker.Kiota.BearerAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.HighLevel.ClientUtil;

///<inheritdoc cref="IHighLevelClientUtil"/>
public sealed class HighLevelClientUtil : IHighLevelClientUtil
{
    private readonly AsyncSingleton<HighLevelOpenApiClient> _client;

    public HighLevelClientUtil(IHighLevelHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<HighLevelOpenApiClient>(async (token, _) =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("HighLevel:ApiKey");

            var requestAdapter = new HttpClientRequestAdapter(new BearerAuthenticationProvider(apiKey), httpClient: httpClient);

            return new HighLevelOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<HighLevelOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
