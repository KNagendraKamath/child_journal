using ExtensionMethods.Probability.Quantities;
using GraphEngine.Graph;
using GraphEngine.Quantities;
using Xunit;
using static GraphEngine.Graph.DataSet;
using static GraphEngine.Tests.Unit.TestDimensions;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace GraphEngine.Tests.Unit;

public class BasicDataSetTest {
    [Fact]
    public void EmptyDataSetTest() {
        var dataSet = new DataSet([], AgeWeight);
        Assert.Equal(new BasicDataSetVisitor.RecordDto(0.Months(), 5.Months()), new BasicDataSetVisitor(dataSet).record);
    }

    [Fact]
    public void NonEmptyDataSet() {
        var dataSet = new DataSet([
                new DataSetRecord(Age, 0.2.Years(), Weight, 2.5.kg()),
                new DataSetRecord(Age, 1.1.Years(), Weight, 8.43.kg()),
                new DataSetRecord(Age, 1.2.Years(), Weight, 3.67.kg()),
                new DataSetRecord(Age, 1.3.Years(), Weight, 6.32.kg()),
                new DataSetRecord(Age, 1.4.Years(), Weight, 5.3.kg())
            ],
            AgeWeight);
        Assert.Equal(new BasicDataSetVisitor.RecordDto(0.Years(), 1.5.Years()), new BasicDataSetVisitor(dataSet).record);
    }
}

internal class BasicDataSetVisitor : GraphDataVisitor {
    internal RecordDto record;

    internal BasicDataSetVisitor(DataSet dataSet) {
        dataSet.Accept(this);
    }

    public void PreVisit(DataSet dataSet, GraphSpec spec, Scale xAxis) => 
        record = new RecordDto(xAxis.Min, xAxis.Max);

    internal record RecordDto(RatioQuantity Min, RatioQuantity Max);
}