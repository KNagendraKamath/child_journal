using GraphEngine.Quantities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace GraphEngine.Graph
{
    // Understands information for a graph
    public class DataSet {
        private readonly List<DataSetRecord> _records;
        private readonly GraphSpec _spec;

        public int Count => _records.Count;

        public DataSet(List<DataSetRecord> records, GraphSpec spec) {
            _records = records;
            _spec = spec;
        }

        internal RatioQuantity Max() => _records.Last()._xValue;

        internal RatioQuantity Min() => _records.First()._xValue;

        internal static Axis YAxis(List<DataSet> dataSets, int maxStepCount)
        {
            var recordCount = dataSets.Sum(d => d._records.Count());
            if (recordCount == 0) return dataSets.First()._spec._yDimension.DefaultAxis(maxStepCount);
            if (recordCount == 1) return SingleValueAxis(dataSets, maxStepCount);
            var min = dataSets.Min(d => d._records.Min(r => r._yValue));
            var max = dataSets.Max(d => d._records.Max(r => r._yValue));
            return dataSets.First()._spec._yDimension.Axis(min, max, maxStepCount);
        }

        private static Axis SingleValueAxis(List<DataSet> dataSets,int maxStepCount) 
        {
            var dataSet = dataSets.First(d => d._records.Count == 1);
            return dataSet._spec._yDimension.Axis(dataSet._records.First()._yValue, maxStepCount);
        }
        

        public Axis Axis(int maxStepCount) 
        {
            switch(_records.Count())
            {
                case 0:
                    return _spec._xDimension.DefaultAxis(maxStepCount);

                case 1:
                    return _spec._xDimension.Axis(_records.First()._xValue, maxStepCount);

                default:
                    return _spec._xDimension.Axis(Min(), Max(), maxStepCount);
            }
        }

        public void Accept(GraphDataVisitor visitor) {
            visitor.PreVisit(this, _spec, Axis(12));
            foreach (DataSetRecord dataSetRecord in _records) dataSetRecord.Accept(visitor);
            visitor.PostVisit(this, _spec);
        }

        public class DataSetRecord {
            public readonly Dimension _xDim;
            public readonly RatioQuantity _xValue;
            public readonly Dimension _yDim;
            public readonly RatioQuantity _yValue;

            public DataSetRecord(Dimension XDim, RatioQuantity XValue, Dimension YDim, RatioQuantity YValue)
            {
                _xDim = XDim;
                _xValue = XValue;
                _yDim = YDim;
                _yValue = YValue;
            }
            public override bool Equals(object obj) => this == obj || obj is DataSetRecord other && this.Equals(other);
            private bool Equals(DataSetRecord other) => _xDim.Equals(other._xDim) && _xValue.Equals(other._xValue) && _yDim.Equals(other._yDim) && _yValue.Equals(other._yValue);
            internal void Accept(GraphDataVisitor visitor) {
                visitor.Visit(this);
            }
        }
    }
}

namespace GraphEngine.Graph.Extensions
{
    public static class DataSetExtensions {
        public static Axis YAxis(this List<DataSet> dataSets, int maxStepCount) =>
            DataSet.YAxis(dataSets, maxStepCount);
    }
}