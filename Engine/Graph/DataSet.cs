using Engine.ResultRecords;
using GraphEngine.Graph;
using System;

namespace GraphEngine.Graph
{
    public class DataSet
    {
        private readonly RuleSet _ruleSet;
        private readonly CompleteList _completeList;
        private readonly Column _xColumn;
        private readonly Column _yColumn;
        private readonly object _memento;
        private readonly Axis _defaultXAxis;
        private List<DataSetRecord>? _records = null;

        private List<DataSetRecord> Records
        {
            get
            {
                if (_records == null) _records = new RecordExtraction(_completeList, _xColumn, _yColumn).Results();
                return _records;
            }
        }

        public DataSet(CompleteList completeList, Column xColumn, Column yColumn, RuleSet ruleSet, Axis defaultXAxis, object memento)
        {
            _ruleSet = ruleSet;
            _completeList = completeList;
            _xColumn = xColumn;
            _yColumn = yColumn;
            _memento = memento;
            _defaultXAxis = defaultXAxis;
        }

        public Axis XAxis()
        {
            if (_completeList.Count == 0) return _defaultXAxis;
            return _ruleSet.Factory(_completeList.Count).Axis(Min(), Max());
        }

        private double Max() => Records.Last().xValue;

        private double Min() => Records.First().xValue;

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
            public void Visit(ResultRecord record, IReadOnlyDictionary<string, object> fieldValues) {
                if (_records != null) // TODO: why is this check necessary?
                    _records.Add(new DataSetRecord(_xColumn, XValue(fieldValues), _yColumn, YValue(fieldValues)));
            }

            private double YValue(IReadOnlyDictionary<string, object> fieldValues) =>
                fieldValues.TryGetValue(_yColumn.ToString(), out object value) ? (double)value : 
                    BMI((double)fieldValues["Weight"], (double)fieldValues["Height"]);

            private double BMI(double weight, double height) => Math.Round(weight / (height / 100 * (height / 100)),1);

            private double XValue(IReadOnlyDictionary<string, object> fieldValues) =>
                fieldValues.TryGetValue(_xColumn.ToString(), out object value)? (double)value : 
                    CalculateAge(fieldValues["ExaminationDate"].ToString());

            private double CalculateAge(string date)
            {
                DateTime examinationDate = DateTime.ParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None);
                DateTime birthDate = DateTime.ParseExact("22-04-2005","dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None);
                return (examinationDate - birthDate).TotalDays/365.25;
            }

            internal List<DataSetRecord> Results()
            {
                _records.Sort((left, right) => left.xValue.CompareTo(right.xValue));
                return _records;
            }
        }
        public record DataSetRecord(Column xColumn, double xValue, Column yColumn, double yValue)
        {
            internal void Accept(GraphDataVisitor visitor)
            {
                visitor.Visit(this);
            }
        }
    }
}
