using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEngine.Graph;

public class GraphData
{
    private readonly Axis _xAxis;
    private readonly Axis _yAxis;
    private readonly List<DataSet> _dataSets;

    public GraphData(Axis xAxis, Axis yAxis, List<DataSet> dataSets) 
    {
        _xAxis = xAxis;
        _yAxis = yAxis;
        _dataSets = dataSets;
    }
    public void Accept(GraphDataVisitor visitor)
    {
        visitor.PreVisit(this, _xAxis, _yAxis, _dataSets);
        foreach (var dataSet in _dataSets) dataSet.Accept(visitor);
        visitor.PostVisit(this);
    }
}
