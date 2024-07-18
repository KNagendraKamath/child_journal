using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEngine.Graph
{
    public class GraphData
    {
        private readonly Axis _xAxis;
        private readonly Axis _yAxis;
        private readonly List<DataSet> _dataSets;
        private readonly string _label;

        public GraphData(Axis xAxis, Axis yAxis, List<DataSet> dataSets, string label) 
        {
            _xAxis = xAxis;
            _yAxis = yAxis;
            _dataSets = dataSets;
            _label = label;
        }
        public void Accept(GraphDataVisitor visitor)
        {
            visitor.PreVisit(this, _xAxis, _yAxis, _dataSets);
            foreach (var dataSet in _dataSets) dataSet.Accept(visitor);
            visitor.PostVisit(this);
        }
    }
}
