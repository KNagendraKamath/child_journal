using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphEngine.Graph;

namespace GraphMediator.Tests.Utility
{
    public class TestXRuleSet:RuleSet
    {
        public AxisFactory Factory(int recordCount)=>
            new (new List<Rule>(){new SingleRule("Age")});
        
    }

    public class SingleRule(string label) : Rule
    {
        public void Check(double min, double max)
        {
            throw new AxisFactory.AxisException(new Axis(min, max, 1, label));
        }
    }
}
