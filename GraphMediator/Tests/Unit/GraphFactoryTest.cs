using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphMediator.GraphEngineMediator.ChildJournalColumns;
using Xunit;
using GraphMediator.Tests.Utility;
using GraphMediator.GraphEngineMediator;
using GraphEngine.Graph;

namespace GraphMediator.Tests.Unit
{
    public class GraphFactoryTest
    {
        [Fact]
        public void CreateFactory()
        {
            var factory = new GraphFactory(
                Age,
                Weight,
                WhoReference.Data["Age/Weight/Girls"],
                new AgeRuleSet(),
                new YTestRuleSet(),
                new Axis(0, 1, 0.1, "Age"),
                new Axis(0, 1, 0.1, "Weight"),
                new DateTime(2005, 04, 22),
                null);
            var graphData = factory.GraphData(TestSources.ExaminationData);
            Assert.True(true);
        }

    }
}
