using GraphEngine.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphEngine.Graph.AxisFactory;

namespace GraphMediator.GraphEngineMediator
{
    internal class HeightRuleSet:RuleSet
    {
        public AxisFactory Factory(int recordCount) => 
            recordCount == 1 ? 
            new AxisFactory([new HeightRule(1)]) : 
            new AxisFactory([new HeightRule(5)]);
    }
    internal class HeightRule(int step) : Rule
    {
        public void Check(double min, double max)
        {
            throw new AxisException(new Axis(min, max, step, "Height"));
        }
    }
}
