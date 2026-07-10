[![](https://img.shields.io/nuget/v/soenneker.highlevel.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.highlevel.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.highlevel.clientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.highlevel.clientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.highlevel.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.highlevel.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.highlevel.clientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.highlevel.clientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.HighLevel.ClientUtil
### An async thread-safe singleton for HighLevel OpenApiClient.

## Installation

```
dotnet add package Soenneker.HighLevel.ClientUtil
```

The parameterless `Get()` uses `HighLevel:ApiKey`. To work with multiple HighLevel accounts or tenants, pass each API key explicitly:

```csharp
HighLevelOpenApiClient tenantClient = await highLevelClientUtil.Get(tenantApiKey);
```
