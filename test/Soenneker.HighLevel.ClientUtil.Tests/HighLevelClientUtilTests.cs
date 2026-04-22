using Soenneker.HighLevel.ClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.HighLevel.ClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class HighLevelClientUtilTests : HostedUnitTest
{
    private readonly IHighLevelClientUtil _openapiclientutil;

    public HighLevelClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IHighLevelClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
