using Engine.ResultRecords;
using static GraphEngine.Graph.DataSet;
namespace GraphEngine.Graph;

// Understands information for a graph
public class BasicDataSet {
    private readonly List<DataSetRecord> _records;
    private readonly Column _xColumn;
    private readonly Column _yColumn;
    private readonly object _memento;
    private List<DataSetRecord> Records => _records;
    public BasicDataSet(List<DataSetRecord> records,Column xColumn, Column yColumn, object memento) {
        _records = records;
        _xColumn = xColumn;
        _yColumn = yColumn;
        _memento = memento;
    }
    
    private double Max() => Records.Last().xValue;

    private double Min() => Records.First().xValue;
}
