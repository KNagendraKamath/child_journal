using Engine.ResultRecords;

namespace GraphEngine.Graph {

// Understands information for a graph
    public class DataSet {
        private readonly List<DataSetRecord> _records;
        private readonly GraphSpec _spec;

        public int Count => _records.Count;

        public DataSet(List<DataSetRecord> records, GraphSpec spec) {
            _records = records;
            _spec = spec;
        }

        internal double Max() => _records.LastOrDefault(defaultValue:new DataSetRecord(_spec.XDimension, 0,_spec.YDimension,0)).XValue;

        internal double Min() => _records.FirstOrDefault(defaultValue: new DataSetRecord(_spec.XDimension, 0, _spec.YDimension, 0)).XValue;

        internal static Axis YAxis(List<DataSet> dataSets, RuleSet ruleSet, Axis defaultYAxis) {
            var recordCount = dataSets.Sum(d => d._records.Count());
            if (recordCount == 0) return defaultYAxis;
            var min = dataSets.Min(d => d._records.Min(r => r.YValue));
            var max = dataSets.Max(d => d._records.Max(r => r.YValue));
            return ruleSet.Factory(recordCount).Axis(min, max);
        }

        public void Accept(GraphDataVisitor visitor)
        {
            visitor.PreVisit(this, _spec, Min(), Max());
            foreach(DataSetRecord dataSetRecord in _records) dataSetRecord.Accept(visitor);
            //visitor.Visit(Min(),Max());  // TODO: This is a bit of a hack. If interesting, it should be on PreVisit
            visitor.PostVisit(this, _spec);
        }

        public record DataSetRecord(Dimension XDim, double XValue, Dimension YDim, double YValue)
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
        public static Axis YAxis(this List<DataSet> dataSets, RuleSet ruleSet, Axis defaultYAxis) => 
            DataSet.YAxis(dataSets, ruleSet, defaultYAxis);

    }
}
