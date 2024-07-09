
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

    public Axis XAxis(double min,double max)
    {
        try
        {
            foreach (var rule in _rules) rule.Check(min, max);
            throw new InvalidProgramException("No Rule Matched");
        }
        catch (XAxisException e)
        {
            return e.XAxis;
        }
    }
    public class XAxisException : Exception
    {
        internal readonly Axis XAxis;
        public XAxisException(Axis xAxis)
        {
            XAxis = xAxis;
        }
    }
}
