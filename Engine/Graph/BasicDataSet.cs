using Engine.ResultRecords;
using static GraphEngine.Graph.DataSet;
namespace GraphEngine.Graph;

// Understands information for a graph
public class BasicDataSet {
    private readonly List<DataSetRecord> _records;
    private readonly Column _xColumn;
    private readonly Column _yColumn;
    private readonly object _memento;
    
    public int Count => _records.Count;
    
    public BasicDataSet(List<DataSetRecord> records, Column xColumn, Column yColumn, object memento) {
        _records = records;
        _xColumn = xColumn;
        _yColumn = yColumn;
        _memento = memento;
    }
    
    private double Max() => _records.Last().xValue;

    private double Min() => _records.First().xValue;
}
