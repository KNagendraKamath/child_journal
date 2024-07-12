using Engine.ResultRecords;
using GraphEngine.Graph;
using System;

namespace GraphEngine.Graph
{
    public class DataSet
    {
        private readonly RuleSet _ruleSet;
        private readonly Axis _defaultXAxis;
        private readonly BasicDataSet _dataSet;

        public DataSet(BasicDataSet dataSet,RuleSet ruleSet, Axis defaultXAxis)
        {
            _dataSet = dataSet;
            _ruleSet = ruleSet;
            _defaultXAxis = defaultXAxis;
        }

        public Axis XAxis()
        {
            if (_dataSet.Count == 0) return _defaultXAxis;
            return _ruleSet.Factory(_dataSet.Count).Axis(_dataSet.Min(), _dataSet.Max());
        }

    }
}
