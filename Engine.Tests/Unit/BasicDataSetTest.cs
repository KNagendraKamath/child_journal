using Engine.ResultRecords;
using GraphEngine.Graph;
using Xunit;
using static GraphEngine.Graph.DataSet;
using static GraphEngine.Tests.Unit.TestDimensions;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace GraphEngine.Tests.Unit;

public class BasicDataSetTest {
    [Fact]
    public void EmptyDataSetTest() {
        var dataSet = new DataSet([], Age, Weight, new object());
        Assert.Equal(new BasicDataSetVisitor.RecordDto(0, 0), new BasicDataSetVisitor(dataSet).record);
    }

    [Fact]
    public void NonEmptyDataSet() {
        var dataSet = new DataSet([
                new DataSetRecord(Age, 0.2, Weight, 2.5),
                new DataSetRecord(Age, 1.1, Weight, 8.43),
                new DataSetRecord(Age, 1.2, Weight, 3.67),
                new DataSetRecord(Age, 1.3, Weight, 6.32),
                new DataSetRecord(Age, 1.4, Weight, 5.3)
            ],
            Age,
            Weight,
            new object());
        Assert.Equal(new BasicDataSetVisitor.RecordDto(0.2, 1.4), new BasicDataSetVisitor(dataSet).record);
    }
}

internal class BasicDataSetVisitor : GraphDataVisitor {
    internal RecordDto record;

    internal BasicDataSetVisitor(DataSet dataSet) {
        dataSet.Accept(this);
    }

    public void PreVisit(DataSet dataSet, Dimension xDim, Dimension yDim, object memento, double min, double max) => 
        record = new RecordDto(min, max);

    internal record RecordDto(double Min, double Max);
}