using System;
using Xunit;
using static GraphEngine.Tests.Unit.GraphDataDump;
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
        AssertXAxis(0, 0, 0.5);
        AssertXAxis(1, 6, 9);
        AssertXAxis(1, 3, 9);
        AssertXAxis(3, 3, 9);
        AssertXAxis(3, 5, 18);
        AssertXAxisThrows(1, 5, 18);
    }
    private void AssertXAxis(int recordCount, double min, double max)
    {
        var dump = new GraphDataDump(new TestRuleSet().Factory(recordCount).XAxis(min, max));
        Assert.Equal(new XAxisDto(min, max, 0.1, "month"), dump.xAxisDto);
    }
    private void AssertXAxisThrows(int recordCount, double min, double max)
    {
        Assert.Throws<InvalidProgramException>(()=> new TestRuleSet().Factory(recordCount).XAxis(min, max));
    }

}

