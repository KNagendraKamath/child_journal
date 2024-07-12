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

    public Dimension(string label,Unit unit,List<RatioQuantity> increments)
    {
        _label = label;
        _unit = unit;
        _increments = increments;
    }

    public Scale Axis(RatioQuantity min, RatioQuantity max, int maxStepCount)
    {
        var diff = max - min;
        var step = _increments.First(i => diff/i <= maxStepCount);
        return new Scale(_label, min.RoundDown(step), max.RoundUp(step), step);
    }
}
public record Scale(string label, RatioQuantity min, RatioQuantity max, RatioQuantity step);
