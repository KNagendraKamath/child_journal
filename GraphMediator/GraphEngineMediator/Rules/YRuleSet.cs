using GraphEngine.Graph;
using static GraphEngine.Graph.AxisFactory;

namespace GraphMediator.GraphEngineMediator;

internal class YRuleSet(string label) : RuleSet {
    public AxisFactory Factory(int recordCount) => new([new SingleRule(label)]);
}

internal class SingleRule(string label) : Rule {
    public void Check(double min, double max) => throw new AxisException(new Axis(min, max, 1, label));
}