using System;
using GraphEngine.Graph;
using System.Collections.Generic;
using System.Linq;

namespace GraphEngine.Tests.Unit;

internal class TestRuleSet:RuleSet
{
    private readonly List<(IEnumerable<int> Range,List<Rule> Rules)> _rules= 
    [
        (Enumerable.Range(0, 1), [new TestRule()]),
        (Enumerable.Range(1, 1), [new TestRule(),new TestRule()])
    ];

    public List<Rule> Rules(int recordCount)
    {
        return _rules.FirstOrDefault(r => r.Range.Contains(recordCount),defaultValue: ([],[new TestRule(),
            new TestRule(),
            new TestRule()])).Rules;
        
    }

    internal class TestRule : Rule
    {
    }
}
