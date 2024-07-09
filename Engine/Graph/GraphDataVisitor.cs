using Engine.ResultRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphEngine.Graph.DataSet;

namespace GraphEngine.Graph
{
    public interface GraphDataVisitor
    {
        void PreVisit(GraphData graphData) { }
        void Visit(Axis axis, double min, double max,double step,string label) { }
        void PreVisit(List<DataSet> dataSets) { }
        void PreVisit(DataSet dataSet, Column xColumn, Column yColumn, object memento) { }
        void Visit(DataSetRecord record) { }
        void PostVisit(DataSet dataSet, Column xColumn, Column yColumn, object memento) { }
        void PostVisit(List<DataSet> dataSets) { }
        void PostVisit(GraphData graphData) { }
    }
}
