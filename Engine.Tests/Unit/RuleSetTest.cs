using Xunit;

namespace GraphEngine.Tests.Unit;

public class RuleSetTest
{
    [Fact]
    public void NoRecordRule()
    {
        Assert.Equal(1, new TestRuleSet().Rules(0).Count);
    }

}

