using Engine.ResultRecords;
using GraphEngine.Graph;
using System;

namespace GraphEngine.Graph
{
    public class XAxisBuilder
    {
        private readonly RuleSet _ruleSet;
        private readonly Axis _defaultXAxis;
        private readonly DataSet _dataSet;

        public XAxisBuilder(DataSet dataSet,RuleSet ruleSet, Axis defaultXAxis)
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
