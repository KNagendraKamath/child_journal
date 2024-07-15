using Engine.ResultRecords;
using GraphEngine.Quantities;

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

        internal RatioQuantity Max() => _records.Last().XValue;

        internal RatioQuantity Min() => _records.First().XValue;

        internal static Scale YAxis(List<DataSet> dataSets, int maxStepCount) {
            var recordCount = dataSets.Sum(d => d._records.Count());
            if (recordCount == 0) return dataSets.First()._spec.YDimension.DefaultAxis(maxStepCount);
            var min = dataSets.Min(d => d._records.Min(r => r.YValue));
            var max = dataSets.Max(d => d._records.Max(r => r.YValue));
            return dataSets.First()._spec.YDimension.Axis(min, max, maxStepCount);
        }
        public Scale Axis(int maxStepCount) => 
            _records.Count() == 0 
            ? _spec.XDimension.DefaultAxis(maxStepCount) 
            : _spec.XDimension.Axis(Min(), Max(), maxStepCount);
        public void Accept(GraphDataVisitor visitor)
        {
            visitor.PreVisit(this, _spec, TODO, TODO);
            foreach(DataSetRecord dataSetRecord in _records) dataSetRecord.Accept(visitor);
            //visitor.Visit(Min(),Max());  // TODO: This is a bit of a hack. If interesting, it should be on PreVisit
            visitor.PostVisit(this, _spec);
        }

        public record DataSetRecord(Dimension XDim, RatioQuantity XValue, Dimension YDim, RatioQuantity YValue)
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
        public static Scale YAxis(this List<DataSet> dataSets, RuleSet ruleSet, Axis defaultYAxis) => 
            DataSet.YAxis(dataSets, ruleSet, defaultYAxis);

    }
}
