using Engine.ResultRecords;
using GraphEngine.Quantities;
using static GraphEngine.Graph.DataSet;

namespace GraphEngine.Graph;

public interface GraphDataVisitor {
    void PreVisit(GraphData graphData, Scale xAxis, Scale yAxis, List<DataSet> dataSets) { }
    void PreVisit(List<DataSet> dataSets) { }
    void PreVisit(DataSet dataSet, GraphSpec spec, Scale xAxis) { }
    void Visit(DataSetRecord record) { }
    void PostVisit(DataSet dataSet, GraphSpec spec) { }
    void PostVisit(List<DataSet> dataSets) { }
    void PostVisit(GraphData graphData) { }
}

public interface QuantityVisitor {
    void Visit(RatioQuantity quantity, double amount, Unit unit) { }
    void Visit(IntervalQuantity quantity, double amount, Unit unit) { }
    void Visit(Unit unit, Unit baseUnit, double baseUnitRatio, double offset, string label) { }
}