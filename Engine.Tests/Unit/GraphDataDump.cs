using GraphEngine.Graph;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace GraphEngine.Tests.Unit
{
    internal class GraphDataDump : GraphDataVisitor {
        internal AxisDto axisDto => XAxisDto;
        private Axis? _xAxis;
        private Axis? _yAxix;
        internal AxisDto XAxisDto;
        internal AxisDto YAxixDto;

        internal GraphDataDump(GraphData data) {
            data.Accept(this);
        }
        
        internal GraphDataDump(Axis axis)
        {
           axis.Accept(this);
        }

        public void PreVisit(GraphData graphData, Axis xAxis, Axis yAxis) {
            _xAxis = xAxis;
            _yAxix = yAxis;
        }

        public void Visit(Axis axis, double min, double max, double step, string label)
        {
            var dto = new AxisDto(min, max, step, label);
            if (_xAxis == null || axis == _xAxis) XAxisDto = dto;
            else YAxixDto = dto;
        }
        internal record AxisDto(double Min, double Max, double Step, string Label);
    }
}

