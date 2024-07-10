using Engine.ResultRecords;
using GraphEngine.Graph;
using GraphEngine.Graph.Extensions;
using GraphMediator.Tests.Utility;

namespace GraphMediator.GraphEngineMediator
{
    internal class GraphFactory
    {
        private readonly Column _xColumn;
        private readonly Column _yColumn;
        private readonly Dictionary<Column, List<ReferenceRecord>> _referenceSources;
        private readonly RuleSet _xRuleSet;
        private readonly RuleSet _yRuleSet;
        private readonly Axis _defaultXAxis;
        private readonly Axis _defaultYAxis;
        private readonly object _memento;

        public GraphFactory(Column xColumn, Column yColumn, Dictionary<Column, List<ReferenceRecord>> referenceSources, RuleSet xRuleSet, RuleSet yRuleSet, Axis defaultXAxis, 
            Axis defaultYAxis, object memento)
        {
            _xColumn = xColumn;
            _yColumn = yColumn;
            _referenceSources = referenceSources;
            _xRuleSet = xRuleSet;
            _yRuleSet = yRuleSet;
            _defaultXAxis = defaultXAxis;
            _defaultYAxis = defaultYAxis;
            _memento = memento;
        }   

        internal GraphData GraphData(CompleteList list)
        {
            var examinationDataSet = ExaminationDataSet();
            var xAxis = examinationDataSet.XAxis();
            var referenceDataSets = ReferenceDataSets();
            referenceDataSets.Add(examinationDataSet);
            var yAxis = referenceDataSets.YAxis(_yRuleSet, _defaultYAxis);
            return new GraphData(xAxis, yAxis, referenceDataSets);
        }

        private List<DataSet> ReferenceDataSets()
        {
            throw new NotImplementedException();
        }

        private DataSet ExaminationDataSet()
        {
            throw new NotImplementedException();
        }
    }
}