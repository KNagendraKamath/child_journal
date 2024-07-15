using Engine.ResultRecords;
using GraphEngine.Graph;
using GraphEngine.Graph.Extensions;
using static GraphMediator.GraphEngineMediator.WhoReference;
using static GraphEngine.Graph.DataSet;
using Xunit;

namespace GraphMediator.GraphEngineMediator
{
    internal class GraphFactory
    {
        internal const int IncrementLimit = 12;
        private readonly Column _xColumn;
        private readonly Column _yColumn;
        private readonly List<WhoReference.ReferenceRecord> _referenceRecords;
        private readonly RuleSet _xRuleSet;
        private readonly RuleSet _yRuleSet;
        private readonly Axis _defaultXAxis;
        private readonly Axis _defaultYAxis;
        private readonly DateTime _birthdate;
        private readonly int _maxStepCount;
        private readonly GraphSpec _spec;
        private readonly CompleteList _completeList;

        public GraphFactory(
            GraphSpec spec,
            CompleteList completeList,
            Column xColumn,
            Column yColumn,
            List<ReferenceRecord> referenceRecords,
            RuleSet xRuleSet,
            RuleSet yRuleSet,
            Axis defaultXAxis,
            Axis defaultYAxis,
            DateTime birthdate,
            int maxStepCount)
        {
            _spec = spec;
            _completeList = completeList;
            _xColumn = xColumn;
            _yColumn = yColumn;
            _referenceRecords = referenceRecords;
            _xRuleSet = xRuleSet;
            _yRuleSet = yRuleSet;
            _defaultXAxis = defaultXAxis;
            _defaultYAxis = defaultYAxis;
            _birthdate = birthdate;
            _maxStepCount = maxStepCount;
        }   

        internal GraphData GraphData(CompleteList list)
        {
            var examinationDataSet = ExaminationDataSet();
            var xAxis = examinationDataSet.Axis(_maxStepCount);
            var referenceDataSets = ReferenceDataSets(xAxis);
            referenceDataSets.Add(examinationDataSet);
            var yAxis = referenceDataSets.YAxis(_yRuleSet, _defaultYAxis);
            return new GraphData(xAxis, yAxis, referenceDataSets);
        }

        private List<DataSet> ReferenceDataSets(Scale xAxis)
        {
            var dataSetRecords =  _referenceRecords
                .Where(r=>xAxis.Contains(r.xValue))
                .Select(r=>new List<List<DataSetRecord>>
                {
                    new List<DataSetRecord>(){new DataSetRecord(_spec.XDimension, r.xValue, _spec.YDimension, r.mean)},
                    new List<DataSetRecord>(){new DataSetRecord(_spec.XDimension, r.xValue, _spec.YDimension, r.negative1)},
                    new List<DataSetRecord>(){new DataSetRecord(_spec.XDimension, r.xValue, _spec.YDimension, r.negative2)},
                    new List<DataSetRecord>(){new DataSetRecord(_spec.XDimension, r.xValue, _spec.YDimension, r.positive1)},
                    new List<DataSetRecord>(){new DataSetRecord(_spec.XDimension, r.xValue, _spec.YDimension, r.positive2)}
                })
                .ToList();

            return dataSetRecords.SelectMany(x =>
                x).Select(y=>new DataSet(y, _spec)).ToList();
        }

        private DataSet ExaminationDataSet()
        {
            var records = new RecordExtraction(_completeList, _xColumn, _yColumn, _spec).Results();
            return new DataSet(records, _spec);
        }

        internal class RecordExtraction : ListVisitor
        {
            private readonly Column _xColumn;
            private readonly Column _yColumn;
            private readonly GraphSpec _spec;
            private List<DataSetRecord> _records = new();

            internal RecordExtraction(CompleteList completeList, Column xColumn, Column yColumn, GraphSpec spec)
            {
                _xColumn = xColumn;
                _yColumn = yColumn;
                _spec = spec;
                completeList.Accept(this);
            }
            public void Visit(ResultRecord record, IReadOnlyDictionary<string, object> fieldValues) =>
                _records.Add(new DataSetRecord(_spec.XDimension, XValue(fieldValues), _spec.YDimension, YValue(fieldValues)));


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

            internal List<DataSet.DataSetRecord> Results()
            {
                _records.Sort((left, right) => left.XValue.CompareTo(right.XValue));
                return _records;
            }
        }

    }
}