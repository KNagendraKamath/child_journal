using GraphEngine.Graph;
using static GraphEngine.Graph.AxisFactory;

namespace GraphMediator.GraphEngineMediator
{
    internal class WeightRuleSet:RuleSet
    {
        public AxisFactory Factory(int recordCount) => new AxisFactory([new WeightRule()]);
    }
    internal class WeightRule : Rule
    {
        public void Check(double min, double max)
        {
        }
    }
}
