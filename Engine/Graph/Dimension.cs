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

}
