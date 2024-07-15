using Engine.ResultRecords;
using GraphEngine.Quantities;
using static GraphEngine.Graph.BasicDataSet;

namespace GraphEngine.Graph;

public interface GraphDataVisitor {
    void PreVisit(GraphData graphData, Axis xAxis, Axis yAxis) { }
    void Visit(Axis axis, string label, double min, double max, double step) { }
    void PreVisit(List<BasicDataSet> dataSets) { }
    void PreVisit(BasicDataSet dataSet, Column xColumn, Column yColumn, object memento, double min, double max) { }
    void Visit(DataSetRecord record) { }
    void PostVisit(BasicDataSet dataSet, Column xColumn, Column yColumn, object memento) { }
    void PostVisit(List<BasicDataSet> dataSets) { }
    void PostVisit(GraphData graphData) { }
}

public interface QuantityVisitor {
    void Visit(RatioQuantity quantity, double amount, Unit unit) { }
    void Visit(IntervalQuantity quantity, double amount, Unit unit) { }
    void Visit(Unit unit, Unit baseUnit, double baseUnitRatio, double offset, string label) { }
}