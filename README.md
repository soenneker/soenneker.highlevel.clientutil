[![](https://img.shields.io/nuget/v/soenneker.highlevel.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.highlevel.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.highlevel.clientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.highlevel.clientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.highlevel.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.highlevel.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.highlevel.clientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.highlevel.clientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.HighLevel.ClientUtil

Create and reuse authenticated HighLevel generated clients for one or more accounts.

## Install

```bash
dotnet add package Soenneker.HighLevel.ClientUtil
```

## Configuration

The parameterless `Get()` reads the default account key from configuration:

```json
{
  "HighLevel": {
    "ApiKey": "<API key>",
    "Version": "2021-07-28"
  }
}
```

`Version` is optional and defaults to `2021-07-28`.

## Register

```csharp
using Soenneker.HighLevel.ClientUtil.Registrars;

services.AddHighLevelClientUtilAsScoped();
```

The scoped utility deliberately uses a singleton `IHighLevelHttpClient`. Disposing a scope releases that utility's generated-client cache while keeping the underlying HTTP clients available to later scopes. Use `AddHighLevelClientUtilAsSingleton()` when the generated-client cache should also live for the application lifetime.

## Usage

```csharp
using Soenneker.HighLevel.ClientUtil.Abstract;
using Soenneker.HighLevel.OpenApiClient;
using Soenneker.HighLevel.OpenApiClient.Models;

public sealed class ContactService(IHighLevelClientUtil clientUtil)
{
    public async Task<ContactsByIdSuccessfulResponseDto?> Get(
        string contactId,
        CancellationToken cancellationToken)
    {
        HighLevelOpenApiClient client = await clientUtil.Get(cancellationToken);

        return await client.Contacts[contactId]
            .GetAsync(cancellationToken: cancellationToken);
    }
}
```

For multiple HighLevel accounts, supply the key for each call:

```csharp
HighLevelOpenApiClient tenantClient = await clientUtil.Get(
    tenantApiKey,
    cancellationToken);
```

Calls using the same key on the same utility instance reuse the generated client. Different keys receive separate generated clients and separate authenticated HTTP clients.

Authentication is applied by the underlying HTTP provider; the Kiota adapter does not add a second bearer header. Let the service container dispose the utility and provider rather than disposing cached clients directly.
