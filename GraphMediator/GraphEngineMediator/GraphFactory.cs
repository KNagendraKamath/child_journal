﻿using Engine.ResultRecords;
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
        private readonly List<ReferenceRecord> _referenceRecords;
        private readonly RuleSet _xRuleSet;
        private readonly RuleSet _yRuleSet;
        private readonly Axis _defaultXAxis;
        private readonly Axis _defaultYAxis;
        private readonly DateTime _birthdate;
        private readonly object _memento;

        public GraphFactory(
            Column xColumn, 
            Column yColumn, 
            List<ReferenceRecord> referenceRecords, 
            RuleSet xRuleSet,
            RuleSet yRuleSet, 
            Axis defaultXAxis, 
            Axis defaultYAxis, 
            DateTime birthdate, 
            object memento)
        {
            _xColumn = xColumn;
            _yColumn = yColumn;
            _referenceRecords = referenceRecords;
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
            // var xAxis = examinationDataSet.XAxis();
            var xAxis = new Axis(0, 1, 0.1, "place holder");
            var referenceDataSets = ReferenceDataSets(xAxis);
            referenceDataSets.Add(examinationDataSet);
            var yAxis = referenceDataSets.YAxis(_yRuleSet, _defaultYAxis);
            return new GraphData(xAxis, yAxis, referenceDataSets);
        }

        private List<BasicDataSet> ReferenceDataSets(Axis xAxis)
        {
            var dataSetRecords =  _referenceRecords
                .Where(r=>xAxis.Contains(r.age))
                .Select(r=>new DataSet.DataSetRecord(_xColumn, r.age, _yColumn, r.mean))
                .ToList();
            return [new BasicDataSet(dataSetRecords, _xColumn, _yColumn, _memento)];
        }
        private BasicDataSet ExaminationDataSet()
        {
            return new BasicDataSet(new(), Age, Weight, _memento);
            // return new DataSet(TestCompleteList.completeList, Age, Weight, _xRuleSet, new Axis(0, 0.5, 1, "Alder"), _memento);
        }
        
    }
}