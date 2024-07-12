using Engine.ResultRecords;
using static GraphEngine.Graph.DataSet;

namespace GraphEngine.Graph {

// Understands information for a graph
    public class BasicDataSet {
        private readonly List<DataSetRecord> _records;
        private readonly Column _xColumn;
        private readonly Column _yColumn;
        private readonly object _memento;

        public int Count => _records.Count;

        public BasicDataSet(List<DataSetRecord> records, Column xColumn, Column yColumn, object memento) {
            _records = records;
            _xColumn = xColumn;
            _yColumn = yColumn;
            _memento = memento;
        }

        internal double Max() => _records.LastOrDefault(defaultValue:new DataSetRecord(_xColumn,0,_yColumn,0)).xValue;

        internal double Min() => _records.FirstOrDefault(defaultValue: new DataSetRecord(_xColumn, 0, _yColumn, 0)).xValue;

        internal static Axis YAxis(List<BasicDataSet> dataSets, RuleSet ruleSet, Axis defaultYAxis) {
            var recordCount = dataSets.Sum(d => d._records.Count());
            if (recordCount == 0) return defaultYAxis;
            var min = dataSets.Min(d => d._records.Min(r => r.yValue));
            var max = dataSets.Max(d => d._records.Max(r => r.yValue));
            return ruleSet.Factory(recordCount).Axis(min, max);
        }

        public void Accept(GraphDataVisitor visitor)
        {
            visitor.PreVisit(this, _xColumn,_yColumn,_memento, Min(), Max());
            foreach(DataSetRecord dataSetRecord in _records) dataSetRecord.Accept(visitor);
            visitor.Visit(Min(),Max());
            visitor.PostVisit(this, _xColumn,_yColumn,_memento);
        }

        public record DataSetRecord(Column xColumn, double xValue, Column yColumn, double yValue)
        {
            internal void Accept(GraphDataVisitor visitor)
            {
                visitor.Visit(this);
            }
        }
    }

    
}

namespace GraphEngine.Graph.Extensions
{
    public static class DataSetExtensions
    {
        public static Axis YAxis(this List<BasicDataSet> dataSets, RuleSet ruleSet, Axis defaultYAxis) => 
            BasicDataSet.YAxis(dataSets, ruleSet, defaultYAxis);

    }
}
