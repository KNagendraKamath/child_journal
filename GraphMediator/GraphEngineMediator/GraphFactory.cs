using Engine.ResultRecords;
using GraphEngine.Graph;
using GraphEngine.Graph.Extensions;
using GraphEngine.Quantities;
using GraphMediator.GraphEngineMediator.Data;
using static GraphMediator.GraphEngineMediator.Data.WhoReference;
using static GraphEngine.Graph.DataSet;
using Xunit;

namespace GraphMediator.GraphEngineMediator {
    internal class GraphFactory {
        internal const int IncrementLimit = 12;
        private readonly Column _xColumn;
        private readonly Column _yColumn;
        private readonly List<WhoReference.ReferenceRecord> _referenceRecords;
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
            DateTime birthdate,
            int maxStepCount)
        {
            _spec = spec;
            _completeList = completeList;
            _xColumn = xColumn;
            _yColumn = yColumn;
            _referenceRecords = referenceRecords;
            _birthdate = birthdate;
            _maxStepCount = maxStepCount;
        }

        internal GraphData GraphData() {
            var examinationDataSet = ExaminationDataSet();
            var xAxis = examinationDataSet.Axis(_maxStepCount);
            var referenceDataSets = ReferenceDataSets(xAxis);
            referenceDataSets.Add(examinationDataSet);
            var yAxis = referenceDataSets.YAxis(_maxStepCount);
            return new GraphData(xAxis, yAxis, referenceDataSets);
        }

        // This doesn't seem to be working (360 datasets are created)
        private List<DataSet> ReferenceDataSets(Axis xAxis) {
            List<List<DataSetRecord>> results = [[], [], [], [], []];
            _referenceRecords
                .Where(r => xAxis.Contains(r.XValue))
                .ToList()
                .ForEach(r => {
                        AddRecord(results[0], r.XValue, r.Mean);
                        AddRecord(results[1], r.XValue, r.Negative1);
                        AddRecord(results[2], r.XValue, r.Negative2);
                        AddRecord(results[3], r.XValue, r.Positive1);
                        AddRecord(results[4], r.XValue, r.Positive2);
                    }
                );
            return results.Select(records => new DataSet(records, _spec)).ToList();
        }

        private void AddRecord(List<DataSetRecord> records, RatioQuantity xValue, RatioQuantity yValue) =>
            records.Add(new(_spec.XDimension, xValue, _spec.YDimension, yValue));

        private DataSet ExaminationDataSet() {
            var records = new RecordExtraction(_completeList, _xColumn, _yColumn, _spec).Results();
            return new DataSet(records, _spec);
        }

        private class RecordExtraction : ListVisitor {
            private readonly Column _xColumn;
            private readonly Column _yColumn;
            private readonly GraphSpec _spec;
            private List<DataSetRecord> _records = new();

            internal RecordExtraction(CompleteList completeList, Column xColumn, Column yColumn, GraphSpec spec) {
                _xColumn = xColumn;
                _yColumn = yColumn;
                _spec = spec;
                completeList.Accept(this);
            }

            public void Visit(ResultRecord record, IReadOnlyDictionary<string, object> fieldValues) =>
                _records.Add(new DataSetRecord(
                    _spec.XDimension,
                    _spec.XDimension.Quantity(XValue(fieldValues)),
                    _spec.YDimension,
                    _spec.YDimension.Quantity(YValue(fieldValues))
                ));

            private double YValue(IReadOnlyDictionary<string, object> fieldValues) =>
                fieldValues.TryGetValue(_yColumn.ToString(), out object value)
                    ? (double)value
                    : BMI((double)fieldValues["Weight"], (double)fieldValues["Height"]);

            private double BMI(double weight, double height) => Math.Round(weight / (height / 100 * (height / 100)), 1);

            private double XValue(IReadOnlyDictionary<string, object> fieldValues) =>
                fieldValues.TryGetValue(_xColumn.ToString(), out object value)
                    ? (double)value
                    : CalculateAge(fieldValues["ExaminationDate"].ToString());

            private double CalculateAge(string date) {
                DateTime examinationDate =
                    DateTime.ParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None);
                DateTime birthDate = DateTime.ParseExact("22-04-2005",
                    "dd-MM-yyyy",
                    null,
                    System.Globalization.DateTimeStyles.None);
                return (examinationDate - birthDate).TotalDays / 365.25;
            }

            internal List<DataSet.DataSetRecord> Results() {
                _records.Sort((left, right) => left.XValue.CompareTo(right.XValue));
                return _records;
            }
        }
    }
}