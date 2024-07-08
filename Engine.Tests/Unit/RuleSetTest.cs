using Xunit;

namespace GraphEngine.Tests.Unit;

public class RuleSetTest
{
    [Fact]
    public void RuleSetCheck()
    {
        Assert.Equal(1, new TestRuleSet().Factory(0).Count);
        Assert.Equal(2, new TestRuleSet().Factory(1).Count);
        Assert.Equal(3, new TestRuleSet().Factory(6).Count);
    }
    [Fact]
    public void XaxisTest()
    {
    }
}

