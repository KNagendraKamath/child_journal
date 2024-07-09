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
        internal AxisDto axisDto;
        public GraphDataDump(Axis axis)
        {
           axis.Accept(this);
        }

        public void Visit(Axis axis, double min, double max, double step, string label)
        {
            axisDto = new AxisDto(min, max, step, label);
        }
        internal record AxisDto(double Min, double Max, double Step, string Label);
    }
}

