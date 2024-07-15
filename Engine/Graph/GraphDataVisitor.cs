using Engine.ResultRecords;
using static GraphEngine.Graph.BasicDataSet;

namespace GraphEngine.Graph {
    public interface GraphDataVisitor {
        void PreVisit(GraphData graphData, Axis xAxis, Axis yAxis) { }
        void Visit(Axis axis, double min, double max, double step, string label) { }
        void PreVisit(List<BasicDataSet> dataSets) { }
        void PreVisit(BasicDataSet dataSet, Column xColumn, Column yColumn, object memento, double min, double max) { }
        void Visit(DataSetRecord record) { }
        void PostVisit(BasicDataSet dataSet, Column xColumn, Column yColumn, object memento) { }
        void PostVisit(List<BasicDataSet> dataSets) { }
        void PostVisit(GraphData graphData) { }
    }
}