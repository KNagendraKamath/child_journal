using Engine.ResultRecords;
using GraphEngine.Graph;
using GraphEngine.Graph.Extensions;
using static GraphMediator.GraphEngineMediator.WhoReference;
using static GraphMediator.GraphEngineMediator.ChildJournalColumns;
using static GraphEngine.Graph.BasicDataSet;
using Xunit;

namespace GraphMediator.GraphEngineMediator
{
    internal class GraphFactory
    {
        private readonly Column _xColumn;
        private readonly Column _yColumn;
        private readonly List<WhoReference.ReferenceRecord> _referenceRecords;
        private readonly RuleSet _xRuleSet;
        private readonly RuleSet _yRuleSet;
        private readonly Axis _defaultXAxis;
        private readonly Axis _defaultYAxis;
        private readonly DateTime _birthdate;
        private readonly object _memento;
        private readonly CompleteList _completeList;

        public GraphFactory(
            CompleteList completeList,
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
            _completeList = completeList;
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
            var xAxis = new DataSet(examinationDataSet, _xRuleSet, _defaultXAxis).XAxis();
            var referenceDataSets = ReferenceDataSets(xAxis);
            referenceDataSets.Add(examinationDataSet);
            var yAxis = referenceDataSets.YAxis(_yRuleSet, _defaultYAxis);
            return new GraphData(xAxis, yAxis, referenceDataSets);
        }

        private List<BasicDataSet> ReferenceDataSets(Axis xAxis)
        {
            var dataSetRecords =  _referenceRecords
                .Where(r=>xAxis.Contains(r.age))
                .Select(r=>new DataSetRecord(_xColumn, r.age, _yColumn, r.mean))
                .ToList();
            return [new BasicDataSet(dataSetRecords, _xColumn, _yColumn, _memento)];
        }

        private BasicDataSet ExaminationDataSet()
        {
            var records = new RecordExtraction(_completeList, _xColumn, _yColumn).Results();
            return new BasicDataSet(records, Age, Weight, _memento);
        }

        internal class RecordExtraction : ListVisitor
        {
            private readonly Column _xColumn;
            private readonly Column _yColumn;
            private List<DataSetRecord> _records = new();

            internal RecordExtraction(CompleteList completeList, Column xColumn, Column yColumn)
            {
                _xColumn = xColumn;
                _yColumn = yColumn;
                completeList.Accept(this);
            }
            public void Visit(ResultRecord record, IReadOnlyDictionary<string, object> fieldValues) =>
                _records.Add(new DataSetRecord(_xColumn, XValue(fieldValues), _yColumn, YValue(fieldValues)));


            private double YValue(IReadOnlyDictionary<string, object> fieldValues) =>
                fieldValues.TryGetValue(_yColumn.ToString(), out object value) ? (double)value :
                    BMI((double)fieldValues["Weight"], (double)fieldValues["Height"]);

            private double BMI(double weight, double height) => Math.Round(weight / (height / 100 * (height / 100)), 1);

            private double XValue(IReadOnlyDictionary<string, object> fieldValues) =>
                fieldValues.TryGetValue(_xColumn.ToString(), out object value) ? (double)value :
                    CalculateAge(fieldValues["ExaminationDate"].ToString());

            private double CalculateAge(string date)
            {
                DateTime examinationDate = DateTime.ParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None);
                DateTime birthDate = DateTime.ParseExact("22-04-2005", "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None);
                return (examinationDate - birthDate).TotalDays / 365.25;
            }

            internal List<BasicDataSet.DataSetRecord> Results()
            {
                _records.Sort((left, right) => left.xValue.CompareTo(right.xValue));
                return _records;
            }
        }

    }
}