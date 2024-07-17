using GraphEngine.Quantities;
using GraphEngine.Quantities.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GraphEngine.Graph
{
    public class Dimension
    {
        private readonly string _label;
        private readonly Unit _unit;
        private readonly List<RatioQuantity> _increments;
        private readonly RatioQuantity _zeroRecordMin;
        private readonly RatioQuantity _zeroRecordMax;

        public Dimension(string label, Unit unit, List<RatioQuantity> increments, RatioQuantity zeroRecordMin, RatioQuantity zerRecordMax)
        {
            _label = label;
            _unit = unit;
            _increments = increments;
            _zeroRecordMin = zeroRecordMin;
            _zeroRecordMax = zerRecordMax;
        }

        public Axis Axis(RatioQuantity min, RatioQuantity max, int maxStepCount)
        {
            var diff = max - min;
            var step = _increments.First(i => diff/i <= maxStepCount);
            return new Axis(_label, min.RoundDown(step), max.RoundUp(step), step);
        }
        public Axis Axis(RatioQuantity singleRecordQuantity, int maxStepCount)
        {
            var diff = _zeroRecordMax - _zeroRecordMin;
            var min = singleRecordQuantity - diff.ScaleBy(0.5);
            if(min <= Quantity(0)) min = Quantity(0);
            var max = min + diff;
            return Axis(min, max, maxStepCount);
        }

        public RatioQuantity Quantity(double amount) => new RatioQuantity(amount, _unit);

        internal Axis DefaultAxis(int maxStepCount) => Axis(_zeroRecordMin, _zeroRecordMax, maxStepCount);
    }

    public class Axis
    {
       internal readonly string _label;
       internal readonly RatioQuantity _min;
       internal readonly RatioQuantity _max;
       internal readonly RatioQuantity _step;

        public Axis(string Label, RatioQuantity Min, RatioQuantity Max, RatioQuantity Step)
        {
            _label = Label;
            _min = Min;
            _max = Max;
            _step = Step;
        }
        public override bool Equals(object obj) => this == obj || obj is Axis other && this.Equals(other);
        private bool Equals(Axis other) => _label.Equals(other._label) && _min.Equals(other._min) && _max.Equals(other._max) && _step.Equals(other._step) ;
        public bool Contains(RatioQuantity quantity) => quantity >= _min && quantity <= _max;

        public List<(double, string)> Labels(RatioQuantity.FriendlyFormatter formatter)
        {
            var quantities = new List<RatioQuantity>();
            for (var i = _min; i <= _max; i += _step) quantities.Add(i);
            return quantities.Format(formatter);
        }
    }
}
