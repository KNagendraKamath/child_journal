using Engine.ResultRecords;
using GraphEngine.Graph;

namespace GraphMediator.Tests.Unit
{
    internal class TestGraphDataVisitor: GraphDataVisitor
    {
        private DataSet dataSet;
        private Column xColumn;
        private Column yColumn;
        private object memento;

        private List<DataSet> dataSets;
        
        public List<AxisDto> axisDtos = new();

        private DataSet.DataSetRecord record;

        internal TestGraphDataVisitor(GraphData data) {
            data.Accept(this);
        }

        public void PostVisit(DataSet dataSet, Column xColumn, Column yColumn, object memento)
        {
            this.dataSet = dataSet;
            this.xColumn = xColumn;
            this.yColumn = yColumn;
            this.memento = memento;
        }

        public void PostVisit(List<DataSet> dataSets)
        {
           this.dataSets = dataSets;
        }

        public void PreVisit(List<DataSet> dataSets)
        {
            this.dataSets = dataSets;
        }

        public void PreVisit(DataSet dataSet, Column xColumn, Column yColumn, object memento)
        {
            this.dataSet = dataSet;
            this.xColumn = xColumn;
            this.yColumn = yColumn;
            this.memento = memento;
        }

        public void Visit(Axis axis, string label, double min, double max, double step)
        {
            this.axisDtos.Add(new AxisDto(min,max,step,label));
        }

        public void Visit(DataSet.DataSetRecord record)
        {
           this.record = record;
        }

        public void Visit(double min, double max)
        {
           
        }

        public record AxisDto(double min, double max, double step, string label);
    }
}
