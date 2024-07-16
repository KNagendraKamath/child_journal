
namespace GraphEngine.Graph;

public interface RuleSet
{
    AxisFactory Factory(int recordCount);
}

public interface Rule
{
    void Check(double min, double max);
}

public class AxisFactory
{
    private readonly List<Rule> _rules;

    public AxisFactory(List<Rule> rules)
    {
        _rules = rules;
    }
    public int Count => _rules.Count;

}
