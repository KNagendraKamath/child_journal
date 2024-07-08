using System;
using GraphEngine.Graph;
using System.Collections.Generic;
using System.Linq;
using static GraphEngine.Graph.AxisFactory;

namespace GraphEngine.Tests.Unit;

internal class TestRuleSet:RuleSet
{
    private static readonly (IEnumerable<int> Range, AxisFactory factory) Default =([], 
        new AxisFactory ([new TestRule(),
        new TestRule(),
        new TestRule()]));
    private readonly List<(IEnumerable<int> Range,AxisFactory factory)> _rules= 
    [
        (Enumerable.Range(0, 1), new AxisFactory( [new TestRule()])),
        (Enumerable.Range(1, 1), new AxisFactory([new TestRule(),new TestRule()]))
    ];

    public AxisFactory Factory(int recordCount) =>
        _rules.FirstOrDefault(r => r.Range.Contains(recordCount), defaultValue: Default).factory;

    internal class TestRule : Rule
    {
        public void Check(double min, double max)
        {
            throw new XAxisException(new XAxis(min, max,0.1,"month"));
        }
    }
}
