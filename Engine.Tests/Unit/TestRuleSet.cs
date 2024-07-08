using System;
using GraphEngine.Graph;
using System.Collections.Generic;
using System.Linq;
using static GraphEngine.Graph.AxisFactory;

namespace GraphEngine.Tests.Unit;

internal class TestRuleSet:RuleSet
{
    private static readonly (IEnumerable<int> Range, AxisFactory factory) Default =([], 
        new AxisFactory ([new RangeTestRule(5), new RangeTestRule(10), new RangeTestRule(15)]));
    private readonly List<(IEnumerable<int> Range,AxisFactory factory)> _rules= 
    [
        (Enumerable.Range(0, 1), new AxisFactory( [new SingleTestRule()])),
        (Enumerable.Range(1, 1), new AxisFactory([new RangeTestRule(5),new RangeTestRule(10)]))
    ];

    public AxisFactory Factory(int recordCount) =>
        _rules.FirstOrDefault(r => r.Range.Contains(recordCount), defaultValue: Default).factory;

    internal class SingleTestRule : Rule
    {
        public void Check(double min, double max)
        {
            throw new XAxisException(new XAxis(min, max,0.1,"month"));
        }
    }

    internal class RangeTestRule(double range) : Rule
    {
        public void Check(double min, double max)
        {
            if (max - min < range) return;
            throw new XAxisException(new XAxis(min, max, 0.1, "month"));
        }
    }
}
