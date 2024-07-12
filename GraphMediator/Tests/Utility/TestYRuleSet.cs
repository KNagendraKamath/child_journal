using GraphEngine.Graph;

namespace GraphMediator.Tests.Utility
{
    public class TestYRuleSet : RuleSet
    {
        public AxisFactory Factory(int recordCount) =>
            new(new List<Rule>() { new SingleRule("Weight") });

    }
}
