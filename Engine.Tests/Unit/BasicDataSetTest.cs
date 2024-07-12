using System.Collections.Generic;
using Engine.ResultRecords;
using GraphEngine.Graph;
using Xunit;
using static GraphEngine.Tests.Unit.TestColumns;

namespace GraphEngine.Tests.Unit
{
    public class BasicDataSetTest
    {
        [Fact]
        public void EmptyDataSetTest()
        {
            var dataSet = new BasicDataSet([],Age,Weight,new object());
     
            Assert.Equal(new BasicDataSetVistor.RecordDto(0,0),new BasicDataSetVistor(dataSet).record);
        }

        [Fact]
        public void NonEmptyDataSet()
        {
            var dataSet = new BasicDataSet([
                new BasicDataSet.DataSetRecord(Age, 0.2, Weight, 2.5),
                new BasicDataSet.DataSetRecord(Age, 1.1, Weight, 8.43),
                new BasicDataSet.DataSetRecord(Age, 1.2, Weight, 3.67),
                new BasicDataSet.DataSetRecord(Age, 1.3, Weight, 6.32),
                new BasicDataSet.DataSetRecord(Age, 1.4, Weight, 5.3)
            ], Age, Weight, new object());
            Assert.Equal(new BasicDataSetVistor.RecordDto(0.2, 1.4), new BasicDataSetVistor(dataSet).record);
        }
    }


    internal class BasicDataSetVistor : GraphDataVisitor
    {
        public RecordDto record;
        public BasicDataSetVistor(BasicDataSet dataSet)
        {
            dataSet.Accept(this);
        }

        public void Visit(double min, double max)
        {
            record = new RecordDto(min, max);
        }
        public record RecordDto( double Min, double Max);

    }
}


