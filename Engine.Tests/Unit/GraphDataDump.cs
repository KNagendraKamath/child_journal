using GraphEngine.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEngine.Tests.Unit
{
    internal class GraphDataDump : GraphDataVisitor
    {
        internal XAxisDto xAxisDto;
        public GraphDataDump(Axis xAxis)
        {
           xAxis.Accept(this);
        }

        public void Visit(Axis xAxis, double min, double max, double step, string label)
        {
            xAxisDto = new XAxisDto(min, max, step, label);
        }
        internal record XAxisDto(double Min, double Max, double Step, string Label);
    }
}

