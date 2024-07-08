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
    public class PointDataSetTest
    {
        [Fact]
        public void EmptyDataSet()
        {
            var dataset = new PointDataSet(
                new TestRuleSet(),
                new CompleteList([]),
                DateOfExamination,
                Weight
                );
            AssertXAxis(dataset, 0, 0.5);
        }

        private void AssertXAxis(PointDataSet dataset, double min, double max)
        {

            var dump = new GraphDataDump(dataset.XAxis(min,max));
            Assert.Equal(new XAxisDto(min, max, 0.1, "month"), dump.xAxisDto);
        }
    }
}
