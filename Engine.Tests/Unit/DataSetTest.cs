using Engine.ResultRecords;
using GraphEngine.Graph;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static GraphEngine.Tests.Unit.GraphDataDump;
using static GraphEngine.Graph.Extensions.DataSetExtensions;
using static GraphEngine.Tests.Unit.TestColumns;
using DataSet = GraphEngine.Graph.DataSet;

namespace GraphEngine.Tests.Unit
{
    public class DataSetTest
    {
        [Fact]
        public void EmptyDataSet()
        {
            var dataset = DataSet([]);
            AssertXAxis(dataset, 0, 0.5);
        }
        [Fact]
        public void NonEmptyDataSet()
        {
            var dataset = DataSet([
                    Record((Age, 0.1), (Weight, 1.0),(Height,100)),
                    Record((Age, 0.2), (Weight, 2.0),(Height,200)),
                    Record((Age, 0.3), (Weight, 3.0),(Height,300)),
                    ]);
            AssertXAxis(dataset, 0.1, 0.3);

        }
        [Fact]
        public void EmptyDataSets()
        {
            List<DataSet> datasets = [DataSet([]), DataSet([]), DataSet([])];
            var dump = new GraphDataDump(datasets.YAxis(new TestRuleSet(), new Axis(0,1,0.1,"SD")));
            Assert.Equal(new AxisDto(0, 1, 0.1, "SD"), dump.axisDto);
        }
        [Fact]
        public void NonEmptyDataSets()
        {
            List<DataSet> datasets = [
                DataSet([
                    Record((Age, 0.1), (Weight, 3.0),(Height,100)),
                    Record((Age, 0.2), (Weight, 2.0),(Height,200))]), 
                DataSet([
                    Record((Age, 0.1), (Weight, 1.0),(Height,100)),
                    Record((Age, 0.2), (Weight, 2.5),(Height,200))]), 
                DataSet([
                    Record((Age, 0.1), (Weight, 1.0),(Height,100)),
                    Record((Age, 0.2), (Weight, 0.5),(Height,200))])];
            var dump = new GraphDataDump(datasets.YAxis(new TestRuleSet(), new Axis(0, 1, 0.1, "SD")));
            Assert.Equal(new AxisDto(0.5, 3, 0.1, "month"), dump.axisDto);
        }
        private ResultRecord Record(params(Column column, object value)[] fieldValues)
        {
            return new ResultRecord(fieldValues.ToDictionary(x=>x.column.ToString(),x=>x.value));
        }

        private void AssertXAxis(DataSet dataset, double min, double max)
        {
            var dump = new GraphDataDump(dataset.XAxis());
            Assert.Equal(new AxisDto(min, max, 0.1, "month"), dump.axisDto);
        }

        private DataSet DataSet(List<ResultRecord> resultRecords) => new DataSet(
                new CompleteList(resultRecords),
                Age,
                Weight,
                new TestRuleSet(),
                new Axis(0, 0.5, 0.1, "month"),
                new object()
                );
    }
}
