using Soenneker.HighLevel.ClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.HighLevel.ClientUtil.Tests;

[Collection("Collection")]
public sealed class HighLevelClientUtilTests : FixturedUnitTest
{
    private readonly IHighLevelClientUtil _openapiclientutil;

    public HighLevelClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IHighLevelClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
