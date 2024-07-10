﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphEngine.Tests.Unit.ChildJournalColumns;
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
                TestSources.ReferenceData,
                new XTestRuleSet(),
                new YTestRuleSet(),
                new Axis(0, 1, 0.1, "Age"),
                new Axis(0, 1, 0.1, "Weight"),
                null);
            var graphData = factory.GraphData(TestSources.ExaminationData);
            Assert.True(true);
        }

    }
}
