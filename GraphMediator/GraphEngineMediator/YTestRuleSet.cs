using GraphEngine.Graph;
using static GraphEngine.Graph.AxisFactory;

namespace GraphMediator.GraphEngineMediator
{
    internal class YRuleSet(string label) : RuleSet
    {
      
        public AxisFactory Factory(int recordCount)
        {
            return new AxisFactory([new SingleTestRule(label)]);
        }
    }
    internal class SingleTestRule(string label) : Rule
    {
        public void Check(double min, double max)
        {
            throw new AxisException(new Axis(min, max, 1, label));
        }
    }
}