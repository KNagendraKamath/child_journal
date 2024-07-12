using Engine.ResultRecords;
using GraphEngine.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GraphMediator.Tests.Unit
{
    internal class TestGraphDataVisitor: GraphDataVisitor
    {
        private BasicDataSet dataSet;
        private Column xColumn;
        private Column yColumn;
        private object memento;

        private List<BasicDataSet> dataSets;
        
        public List<AxisDto> axisDtos = new();

        private BasicDataSet.DataSetRecord record;

        internal TestGraphDataVisitor(GraphData data) {
            data.Accept(this);
        }

        public void PostVisit(BasicDataSet dataSet, Column xColumn, Column yColumn, object memento)
        {
            this.dataSet = dataSet;
            this.xColumn = xColumn;
            this.yColumn = yColumn;
            this.memento = memento;
        }

        public void PostVisit(List<BasicDataSet> dataSets)
        {
           this.dataSets = dataSets;
        }

        public void PreVisit(List<BasicDataSet> dataSets)
        {
            this.dataSets = dataSets;
        }

        public void PreVisit(BasicDataSet dataSet, Column xColumn, Column yColumn, object memento)
        {
            this.dataSet = dataSet;
            this.xColumn = xColumn;
            this.yColumn = yColumn;
            this.memento = memento;
        }

        public void Visit(Axis axis, double min, double max, double step, string label)
        {
            this.axisDtos.Add(new AxisDto(min,max,step,label));
        }

        public void Visit(BasicDataSet.DataSetRecord record)
        {
           this.record = record;
        }

        public void Visit(double min, double max)
        {
           
        }

        public record AxisDto(double min, double max, double step, string label);
    }
}
