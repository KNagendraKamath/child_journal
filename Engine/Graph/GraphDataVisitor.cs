﻿using Engine.ResultRecords;
using static GraphEngine.Graph.BasicDataSet;
namespace GraphEngine.Graph
{
    public interface GraphDataVisitor
    {
        void PreVisit(GraphData graphData) { }
        void Visit(Axis axis, double min, double max,double step,string label) { }
        void PreVisit(List<BasicDataSet> dataSets) { }
        void PreVisit(BasicDataSet graphData, Column xColumn, Column yColumn, object memento, double min, double max) { }
        void Visit(DataSetRecord record) { }
        void Visit(double min, double max) { }
        void PostVisit(BasicDataSet dataSet, Column xColumn, Column yColumn, object memento) { }
        void PostVisit(List<BasicDataSet> dataSets) { }
        void PostVisit(GraphData graphData) { }
    }
}
