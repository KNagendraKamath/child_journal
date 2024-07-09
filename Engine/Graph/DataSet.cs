using Engine.ResultRecords;
using GraphEngine.Graph;

namespace GraphEngine.Graph;

public class DataSet
{
    private readonly RuleSet _ruleSet;
    private readonly CompleteList _completeList;
    private readonly Column _xColumn;
    private readonly Column _yColumn;
    private readonly object _memento;
    private readonly XAxis _defaultXAxis;
    private List<DataSetRecord>? _records = null;

    private List<DataSetRecord> Records
    {
        get
        {
            if (_records == null) _records = new RecordExtraction(_completeList, _xColumn, _yColumn).Results();
            return _records;
        }
    }


    public DataSet(CompleteList completeList, Column xColumn, Column yColumn, RuleSet ruleSet, XAxis defaultXAxis, object memento)
    {
        _ruleSet = ruleSet;
        _completeList = completeList;
        _xColumn = xColumn;
        _yColumn = yColumn;
        _memento = memento;
        _defaultXAxis = defaultXAxis;
    }

    public XAxis XAxis()
    {
        if (_completeList.Count == 0) return _defaultXAxis;
        return _ruleSet.Factory(_completeList.Count).XAxis(_records.First().xValue,_records.Last().xValue);
    }
    internal class RecordExtraction:ListVisitor
    {
        private readonly Column _xColumn;
        private readonly Column _yColumn;
        private List<DataSetRecord> _records = new List<DataSetRecord>();
        internal RecordExtraction(CompleteList completeList, Column xColumn, Column yColumn)
        {
            _xColumn = xColumn;
            _yColumn = yColumn;
            completeList.Accept(this);
        }
       public void Visit(ResultRecord record, IReadOnlyDictionary<string, object> fieldValues)
        {
            _records.Add(new DataSetRecord(_xColumn,(double)fieldValues[_xColumn.ToString()], _yColumn, (double)fieldValues[_yColumn.ToString()]));
        }
        internal List<DataSetRecord> Results()
        {
            _records.Sort((left, right) => left.xValue.CompareTo(right.xValue));
            return _records;    
        }
    }
    internal record DataSetRecord(Column xColumn, double xValue, Column yColumn, double yValue);
}

