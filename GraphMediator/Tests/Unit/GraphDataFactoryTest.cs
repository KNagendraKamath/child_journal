﻿using GraphEngine.Graph;
using GraphMediator.GraphEngineMediator;
using GraphMediator.Tests.Utility;
using Xunit;
using static GraphMediator.GraphEngineMediator.Data.ChildJournalGraphSpec;

namespace GraphMediator.Tests.Unit
{
    public class GraphDataFactoryTest 
    {
        [Fact]
        public void ReferenceDataTest()
        {
           var graphData= new GraphFactory(TestCompleteList.completeList,
                ChildJournalColumns.Age,
                ChildJournalColumns.Weight,
                WhoReference.Data[(AgeWeight, "female")],
                new TestXRuleSet(), new TestYRuleSet(),
                new Axis(0, 0.5, 1, "Age"),
                new Axis(0, 50, 10, "Weight"),
                new DateTime(2005, 05, 01),
                new object()).GraphData(TestCompleteList.completeList);

            var graphVisitor = new TestGraphDataVisitor(graphData);

            graphData.Accept(graphVisitor);  // TODO: Pass graphData into visitor as a parameter, and have it automatically do this in the constructor

            Assert.True(graphVisitor.axisDtos.Contains(new TestGraphDataVisitor.AxisDto(0, 4.999315537303217, 1, "Age")));
        }

        
    }
}