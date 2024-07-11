using Engine.ResultRecords;
using GraphEngine.Graph;
using GraphEngine.Graph.Extensions;
using GraphMediator.Tests.Utility;
using static GraphMediator.GraphEngineMediator.ChildJournalColumns;

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
        private readonly DateTime _birthdate;
        private readonly object _memento;

        public GraphFactory(Column xColumn, Column yColumn, Dictionary<Column, List<ReferenceRecord>> referenceSources, RuleSet xRuleSet, RuleSet yRuleSet, Axis defaultXAxis, 
            Axis defaultYAxis, DateTime birthdate, object memento)
        {
            _xColumn = xColumn;
            _yColumn = yColumn;
            _referenceSources = referenceSources;
            _xRuleSet = xRuleSet;
            _yRuleSet = yRuleSet;
            _defaultXAxis = defaultXAxis;
            _defaultYAxis = defaultYAxis;
            _birthdate = birthdate;
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
            return new DataSet(TestCompleteList.completeList, Age, Weight, , new Axis(0,0.5,1,"Alder"),null);
        }
        
    }
}