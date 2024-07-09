using Engine.ResultRecords;
using GraphEngine.Graph;

namespace GraphEngine.Graph
{

    public class DataSet
    {
        private readonly RuleSet _ruleSet;
        private readonly CompleteList _completeList;
        private readonly Column _xColumn;
        private readonly Column _yColumn;
        private readonly object _memento;
        private readonly Axis _defaultXAxis;
        private List<DataSetRecord>? _records = null;

        private List<DataSetRecord> Records
        {
            get
            {
                if (_records == null) _records = new RecordExtraction(_completeList, _xColumn, _yColumn).Results();
                return _records;
            }
        }


        public DataSet(CompleteList completeList, Column xColumn, Column yColumn, RuleSet ruleSet, Axis defaultXAxis, object memento)
        {
            _ruleSet = ruleSet;
            _completeList = completeList;
            _xColumn = xColumn;
            _yColumn = yColumn;
            _memento = memento;
            _defaultXAxis = defaultXAxis;
        }

        public Axis XAxis()
        {
            if (_completeList.Count == 0) return _defaultXAxis;
            return _ruleSet.Factory(_completeList.Count).Axis(Min(), Max());
        }

        private double Max() => Records.Last().xValue;

        private double Min() => Records.First().xValue;

        internal class RecordExtraction : ListVisitor
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
                _records.Add(new DataSetRecord(_xColumn, (double)fieldValues[_xColumn.ToString()], _yColumn, (double)fieldValues[_yColumn.ToString()]));
            }
            internal List<DataSetRecord> Results()
            {
                _records.Sort((left, right) => left.xValue.CompareTo(right.xValue));
                return _records;
            }
        }
        public record DataSetRecord(Column xColumn, double xValue, Column yColumn, double yValue)
        {
            internal void Accept(GraphDataVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        internal static Axis YAxis(List<DataSet> dataSets, RuleSet ruleSet, Axis defaultYAxis)
        {
            var recordCount = dataSets.Sum(d => d.Records.Count());
            if (recordCount == 0) return defaultYAxis;
            var min = dataSets.Min(d => d.Records.Min(r => r.yValue));
            var max = dataSets.Max(d => d.Records.Max(r => r.yValue));
            return ruleSet.Factory(recordCount).Axis(min, max);
        }

        internal void Accept(GraphDataVisitor visitor)
        {
            visitor.PreVisit(this, _xColumn,_yColumn,_memento);
            _defaultXAxis.Accept(visitor);
            foreach(DataSetRecord dataSetRecord in Records) dataSetRecord.Accept(visitor);
            visitor.PostVisit(this, _xColumn,_yColumn,_memento);
        }
    }
}
namespace GraphEngine.Graph.Extensions
{
    public static class DataSetExtensions
    {
        public static Axis YAxis(this List<DataSet> dataSets, RuleSet ruleSet, Axis defaultYAxis) => 
            DataSet.YAxis(dataSets, ruleSet, defaultYAxis);

    }
}

