using GraphEngine.Graph;
using static GraphEngine.Graph.AxisFactory;

namespace GraphMediator.GraphEngineMediator
{
    internal class YTestRuleSet : RuleSet
    {
        public AxisFactory Factory(int recordCount)
        {
            new AxisFactory(RuleSet)
        }
    }
    internal class SingleTestRule : Rule
    {
        public void Check(double min, double max)
        {
            throw new AxisException(new Axis(min, max, 0.1, "month"));
        }
    }
}