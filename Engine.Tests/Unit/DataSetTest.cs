using Engine.ResultRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphEngine.Tests.Unit.TestColumns;
using static GraphEngine.Tests.Unit.RuleSetTest; 
using Xunit;
using GraphEngine.Graph;
using static GraphEngine.Tests.Unit.GraphDataDump;

namespace GraphEngine.Tests.Unit
{
    public class DataSetTest
    {
        [Fact]
        public void EmptyDataSet()
        {
            var dataset = new DataSet(
                new CompleteList([]),
                Age,
                Weight,
                new TestRuleSet(),
                new Axis(0,0.5, 0.1, "month"), 
                new object()
                );
            AssertXAxis(dataset, 0, 0.5);
        }
        [Fact]
        public void NonEmptyDataSet()
        {
            var dataset = new DataSet(
                new CompleteList([
                    Record((Age, 0.1), (Weight, 1.0),(Height,100)),
                    Record((Age, 0.2), (Weight, 2.0),(Height,200)),
                    Record((Age, 0.3), (Weight, 3.0),(Height,300)),
                    ]),
                Age,
                Weight,
                new TestRuleSet(),
                new Axis(0, 0.5, 0.1, "month"),
                new object()
                );
            AssertXAxis(dataset, 0.1, 0.3);

        }
        private ResultRecord Record(params(Column column, object value)[] fieldValues)
        {
            return new ResultRecord(fieldValues.ToDictionary(x=>x.column.ToString(),x=>x.value));
        }

        private void AssertXAxis(DataSet dataset, double min, double max)
        {
            var dump = new GraphDataDump(dataset.XAxis());
            Assert.Equal(new XAxisDto(min, max, 0.1, "month"), dump.xAxisDto);
        }

    }
}
