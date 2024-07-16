using GraphEngine.Graph;
using static GraphEngine.Graph.AxisFactory;

namespace GraphMediator.GraphEngineMediator
{
    internal class AgeRuleSet : RuleSet
    {
        private static readonly (IEnumerable<int> Range, AxisFactory factory) Default = (
            [],
            new AxisFactory([
                new MaxAgeRule(2/12.0), 
                new MaxAgeRule(1), 
                new MaxAgeRule(2), 
                new MaxAgeRule(3), 
                new MaxAgeRule(5), 
                new MaxAgeRule(10), 
                new MaxAgeRule(18)]));

        private readonly List<(IEnumerable<int> Range, AxisFactory factory)> _rules =
        [
            (Enumerable.Range(1, 1), 
            new AxisFactory([
                new MaxAgeRule(2/12.0), 
                new MaxAgeRule(1),
                new MaxAgeRule(2), 
                new MaxAgeRule(3), 
                new MaxAgeRule(5)])),
        ];
        public AxisFactory Factory(int recordCount) =>
         _rules.FirstOrDefault(r => r.Range.Contains(recordCount), defaultValue: Default).factory;
            
    }

    internal class MaxAgeRule(double range) : Rule
    {
        public void Check(double min, double max)
        {
            if (max - min > range) return;
        }
    }
}