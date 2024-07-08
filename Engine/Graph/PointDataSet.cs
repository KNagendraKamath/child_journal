using Engine.ResultRecords;
using GraphEngine.Graph;

namespace GraphEngine.Graph;

public class PointDataSet
{
    private readonly RuleSet _ruleSet;
    private readonly CompleteList _completeList;
    private readonly Column _dateOfExamination;
    private readonly Column _weight;

    public PointDataSet(RuleSet ruleSet, CompleteList completeList, Column dateOfExamination, Column weight)
    {
        _ruleSet = ruleSet;
        _completeList = completeList;
        _dateOfExamination = dateOfExamination;
        _weight = weight;
    }

    public XAxis XAxis(double min, double max) => _ruleSet.Factory(_completeList.Count).XAxis(min, max);
}