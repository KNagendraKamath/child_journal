using GraphEngine.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphEngine.Graph.AxisFactory;

namespace GraphMediator.GraphEngineMediator
{
    internal class HeadCircumferenceRuleSet:RuleSet
    {
        public AxisFactory Factory(int recordCount) => new AxisFactory([new MinHeadCircumferenceRule(31.9)]);
    }
    internal class MinHeadCircumferenceRule(double range) : Rule
    {
        public void Check(double min, double max)
        {
            if (max - min > range) return;
        }
    }
}
