using GraphEngine.Quantities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEngine.Graph;

public class Dimension
{
    private readonly string _label;
    private readonly Unit _unit;
    private readonly List<RatioQuantity> _increments;
    private readonly RatioQuantity _zeroRecordMin;
    private readonly RatioQuantity _zerRecordMax;

    public Dimension(string label, Unit unit, List<RatioQuantity> increments, RatioQuantity zeroRecordMin, RatioQuantity zerRecordMax)
    {
        _label = label;
        _unit = unit;
        _increments = increments;
        _zeroRecordMin = zeroRecordMin;
        _zerRecordMax = zerRecordMax;
    }

    public Scale Axis(RatioQuantity min, RatioQuantity max, int maxStepCount)
    {
        var diff = max - min;
        var step = _increments.First(i => diff/i <= maxStepCount);
        return new Scale(_label, min.RoundDown(step), max.RoundUp(step), step);
    }

    public RatioQuantity Quantity(double amount) => new(amount, _unit);

    internal Scale DefaultAxis(int maxStepCount) => Axis(_zeroRecordMin, _zerRecordMax, maxStepCount);
}

public record Scale(string Label, RatioQuantity Min, RatioQuantity Max, RatioQuantity Step)
{
    public bool Contains(RatioQuantity quantity) => quantity >= Min && quantity <= Max;
}
